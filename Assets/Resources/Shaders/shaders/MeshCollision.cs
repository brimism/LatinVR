using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCollision : MonoBehaviour
{
    SkinnedMeshRenderer meshRenderer;
    MeshCollider mcollider;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        mcollider = GetComponent<MeshCollider>();
        StartCoroutine("UpdateMeshCollider");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator UpdateMeshCollider()
    {
        while(true) {
            
            Mesh colliderMesh = new Mesh();
            meshRenderer.BakeMesh(colliderMesh);
            mcollider.sharedMesh = null;
            mcollider.sharedMesh = colliderMesh;
            yield return new WaitForSeconds(.50F);
        }
    }

}
