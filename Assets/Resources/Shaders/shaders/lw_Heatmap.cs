using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class lw_Heatmap : MonoBehaviour {

    private Material visHeatMat;
    private Material[] drawMat;
    private lw_DrawSplat[] drawObjects;
    private HeatInitData _h;

    public bool saveOnQuit = false;
    public bool saveNow = false;
    public bool visualizeCurrentNow = false;
    public bool visualizePreviousOnOpen = true;
    public bool resetAllHeatMaps = false;
    public bool revertAllMaterials = false;

    public bool visualizeMesh = false;

    public float scale = .01f;

    public string sessionID;



    private void Awake()
    {
        //_h = new HeatInitData();
        //_h.Save();
        _h = getInitData();
        HeatmapInitalize(_h);
        sessionID = _h.studentID + "_" + _h.date;
        visHeatMat = Resources.Load<Material>("Shaders/mat_lw_visHeat") as Material;
        drawObjects = FindObjectsOfType<lw_DrawSplat>();
        storeMats();
    }


    // Use this for initialization
    void Start () {
        ScaleAll();

        if(visualizeMesh){
            VisualizeAllMesh();
        }
            //Debug.Log("starting ");
        if (visualizePreviousOnOpen)
        {
            //Debug.Log("starting vis");
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
            drawMat[i] = drawObjects[i].gameObject.GetComponent<MeshRenderer>().material;
        }
    }
    void ScaleAll()
    {
        foreach (lw_DrawSplat d in drawObjects)
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
            //Debug.Log("visualizing "+drawObjects[i].gameObject.name);
            InitOne(drawObjects[i].gameObject.GetComponent<MeshRenderer>().material, drawObjects[i].gameObject.name);
            drawObjects[i].enabled = false;
        }

    }
    void VisualizeOne(lw_DrawSplat d)
    {
        d.gameObject.GetComponent<MeshRenderer>().material = visHeatMat;
    }

    void VisualizeAllMesh()
    {
        for (int i = 0; i < drawObjects.Length; i++)
        {
            VisualizeOneMesh(drawObjects[i]);
        }

    }
    void VisualizeOneMesh(lw_DrawSplat d){
        d.gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    void VisualizeAllRealtime()
    {
        for (int i = 0; i < drawObjects.Length; i++)
        {
            VisualizeRealtime(drawObjects[i]);
        }
    }

    void VisualizeRealtime(lw_DrawSplat d)
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
        Debug.Log("file is "+ sessionID + uniqueName + ".png");
        finalPath = "file://" + Path.Combine(Application.streamingAssetsPath, sessionID + uniqueName + ".png");
        Debug.Log(finalPath);
        localFile = new WWW(finalPath);
        
        mat.SetTexture("_Splat", localFile.texture);

        
    }

    void SaveAll()
    {
        foreach (lw_DrawSplat d in drawObjects)
        {
            d.Save(sessionID + d.gameObject.name);
        }
    }

    void RevertAll()
    {
        for (int i = 0; i < drawObjects.Length; i++)
        {
            RevertOne(drawObjects[i], drawMat[i]);
        }
    }

    void RevertOne(lw_DrawSplat d, Material mat)
    {
        d.gameObject.GetComponent<MeshRenderer>().material = mat;
    }

    void ResetAll()
    {
        foreach (lw_DrawSplat d in drawObjects)
        {
            ResetOne(d);
        }
    }

    void ResetOne(lw_DrawSplat d)
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
        visualizeMesh = data.visualizeMesh;
    }

    public HeatInitData createData(string json)
        {
            return JsonUtility.FromJson<HeatInitData>(json);

        }
}