using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Heatmap : MonoBehaviour {

    private Material visHeatMat;
    private Material[] drawMat;
    private DrawSplat[] drawObjects;
    private HeatInitData _h;

    public bool saveOnQuit = false;
    public bool saveNow = false;
    public bool visualizeCurrentNow = false;
    public bool visualizePreviousOnOpen = false;
    public bool resetAllHeatMaps = false;
    public bool revertAllMaterials = false;

    public float scale = .01f;


    private void Awake()
    {
        //_h = new HeatInitData();
        //_h.Save();
        _h = getInitData();
        HeatmapInitalize(_h);
        visHeatMat = Resources.Load<Material>("Shaders/VisHeatMat") as Material;
        drawObjects = FindObjectsOfType<DrawSplat>();
        storeMats();
    }


    // Use this for initialization
    void Start () {
        ScaleAll();

        if (visualizePreviousOnOpen)
        {
            VisualizeAll();
        }
        if (saveOnQuit)
        {
            SaveAll();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (saveOnQuit)
        {
            SaveAll();
        }
        if (resetAllHeatMaps)
        {
            resetAllHeatMaps = false;
            ResetAll();
        }

        if (visualizeCurrentNow)
        {
            visualizeCurrentNow = false;
            VisualizeAllRealtime();
        }

        if (saveNow)
        {
            saveNow = false;
            SaveAll();
        }

        if (revertAllMaterials)
        {
            revertAllMaterials = false;
            RevertAll();

        }
	}

    void OnDisable()
    {
        
    }
    /* TODO:
     * also need things that take "screenshots"
     * 
     * 
     */

    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }

    void storeMats()
    {
        drawMat = new Material[drawObjects.Length];
        for (int i = 0; i < drawObjects.Length; i++)
        {
            if (drawObjects[i].gameObject.GetComponent<SkinnedMeshRenderer>() != null) {
                drawMat[i] = drawObjects[i].gameObject.GetComponent<SkinnedMeshRenderer>().material;
            } else
            {
                drawMat[i] = drawObjects[i].gameObject.GetComponent<MeshRenderer>().material;

            }
        }

    }
    void ScaleAll()
    {
        foreach (DrawSplat d in drawObjects)
        {
            d.setScale(scale);
        }
    }

    void VisualizeAll()
    {
        //AssetDatabase.Refresh();
        drawMat = new Material[drawObjects.Length];
        for (int i = 0; i < drawObjects.Length; i++)
        {
            drawObjects[i].genMap = false;
            VisualizeOne(drawObjects[i]);
            if (drawObjects[i].gameObject.GetComponent<SkinnedMeshRenderer>() != null)
            {
                InitOne(drawObjects[i].gameObject.GetComponent<SkinnedMeshRenderer>().material, drawObjects[i].gameObject.name);

            }
            else
            {
                InitOne(drawObjects[i].gameObject.GetComponent<MeshRenderer>().material, drawObjects[i].gameObject.name);

            }
            drawObjects[i].enabled = false;
        }

    }
    void VisualizeOne(DrawSplat d)
    {
        if (d.gameObject.GetComponent<SkinnedMeshRenderer>() != null)
        {
            d.gameObject.GetComponent<SkinnedMeshRenderer>().material = visHeatMat;
        }
        else
        {
            d.gameObject.GetComponent<MeshRenderer>().material = visHeatMat;

        }
        
    }

    void VisualizeAllRealtime()
    {
        for (int i = 0; i < drawObjects.Length; i++)
        {
            VisualizeRealtime(drawObjects[i]);
        }
    }

    void VisualizeRealtime(DrawSplat d)
    {
        VisualizeOne(d);
        d.InitRealtime();
    }


    void InitOne(Material mat, string uniqueName)
    {
        /*
        Texture tex;
        tex = Resources.Load<Texture>("StreamingAssets/" + uniqueName) as Texture;
        Debug.Log(Path.Combine(Application.streamingAssetsPath, uniqueName));
        //tex = Resources.Load<Texture>(Path.Combine(Application.streamingAssetsPath, uniqueName)) as Texture;
        mat.SetTexture("_Splat", tex); */
        string finalPath;
        WWW localFile;

        finalPath = "file://" + Path.Combine(Application.streamingAssetsPath, uniqueName + ".png");
        Debug.Log(finalPath);
        localFile = new WWW(finalPath);
        
        mat.SetTexture("_Splat", localFile.texture);

        
    }

    void SaveAll()
    {
        foreach (DrawSplat d in drawObjects)
        {
            d.Save(d.gameObject.name);
        }
    }

    void RevertAll()
    {
        for (int i = 0; i < drawObjects.Length; i++)
        {
            RevertOne(drawObjects[i], drawMat[i]);
        }
    }

    void RevertOne(DrawSplat d, Material mat)
    {
        d.gameObject.GetComponent<MeshRenderer>().material = mat;
    }

    void ResetAll()
    {
        foreach (DrawSplat d in drawObjects)
        {
            ResetOne(d);
        }
    }

    void ResetOne(DrawSplat d)
    {
        d.InitSplat();
    }

    HeatInitData getInitData()
    {
        string finalPath;
        WWW localFile;

        finalPath = "file://" + Path.Combine(Application.streamingAssetsPath, "heat_ini.txt");
        Debug.Log(finalPath);
        localFile = new WWW(finalPath);
        Debug.Log(localFile.text);
        return createData(localFile.text);
    }

    void HeatmapInitalize(HeatInitData data)
    {
        saveOnQuit = data.saveOnQuit;
        //saveNow = data.saveNow;
        visualizeCurrentNow = data.visualizeCurrentNow;
        visualizePreviousOnOpen = data.visualizePreviousOnOpen;
        //resetAllHeatMaps = data.resetAllHeatMaps;
        //revertAllMaterials = data.revertAllMaterials;

    }

    public HeatInitData createData(string json)
        {
            return JsonUtility.FromJson<HeatInitData>(json);

        }
}
[System.Serializable]
public class HeatInitData
{
    public bool saveOnQuit = false;
    public bool saveNow = false;
    public bool visualizeCurrentNow = false;
    public bool visualizePreviousOnOpen = false;
    public bool resetAllHeatMaps = false;
    public bool revertAllMaterials = false;

    public float scale = 0.01f;

    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }

    public void Save()
    {
        string fileName = System.IO.Path.Combine(Application.streamingAssetsPath, "heat_ini.txt");
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(fileName, true);
        writer.WriteLine(SaveToString());
        writer.Close();
    }

}