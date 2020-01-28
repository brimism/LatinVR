using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class lw_DrawSplat : MonoBehaviour {
    public Camera cam;
    public Shader drawShader;
    public bool genMap = true;
    public bool initialized = false;
    public Transform pointer;

    public bool saveOnQuit = false;


    public RenderTexture _splatmap;
    Material _mat, _drawMat;
    RaycastHit _hit;

    
	void Awake () {
        _drawMat = new Material(drawShader);
        _drawMat.SetVector("_Color", Color.red);

        _mat = GetComponent<MeshRenderer>().material;
        InitSplat();
        _mat.SetTexture("_Splat", _splatmap);


	}
	
	// Update is called once per frame
	void Update () {

        if (genMap) //trigger to draw goes here
        {
            if(Physics.Raycast(pointer.transform.position, pointer.transform.forward, out _hit) && _hit.collider.gameObject == this.gameObject)
            {
                _drawMat.SetVector("_Coordinate", new Vector4(_hit.textureCoord.x, _hit.textureCoord.y));
                
                RenderTexture temp = RenderTexture.GetTemporary(_splatmap.width, _splatmap.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(_splatmap, temp);
                Graphics.Blit(temp, _splatmap, _drawMat);
                RenderTexture.ReleaseTemporary(temp);
            }
        }
	}

    private void OnDisable()
    {
        if (saveOnQuit)
            Save(this.gameObject.name);
    }

    public void setScale(float f)
    {
        _drawMat.SetFloat("_Scale", f);
    }

    public void Save(string uniqueName)
    {
        //string fileName = "Assets/Resources/heatdata/" + uniqueName + ".png";
        string fileName = System.IO.Path.Combine(Application.streamingAssetsPath, uniqueName+".png");
        byte[] bytes = toTexture2D(_splatmap).EncodeToPNG();
        System.IO.File.WriteAllBytes(fileName, bytes);
    }

    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGB24, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }

    public void InitRealtime()
    {
        _mat = GetComponent<MeshRenderer>().material;
        _mat.SetTexture("_Splat", _splatmap);
    }
    public void InitSplat()
    {
        _splatmap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
    }

}
