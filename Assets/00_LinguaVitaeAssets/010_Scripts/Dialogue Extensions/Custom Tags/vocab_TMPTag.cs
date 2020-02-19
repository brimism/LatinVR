using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VocabTag", menuName = "CustomTags/VocabTag")]
public class vocab_TMPTag : CustomTMPTag
{
    BoxCollider boxCollider;
    public override string tag_name
    {
        get
        {
            return "vocab";
        }
    }

    public override bool needs_closing_tag
    {
        get
        {
            return true;
        }
    }

    public override IEnumerator applyToText(TMPro.TextMeshPro text, int startIndex, int length, string param)
    {
        // Set up
        var textInfo = text.textInfo;

        // Cache the vertex data of the text object as the Jitter FX is applied to the original position of the characters.
        TMPro.TMP_MeshInfo[] cachedMeshInfo = textInfo.CopyMeshInfoVertexData();
        
            //Debug.Log("shaking");
            // Get new copy of vertex data if the text has changed.
            if (hasTextChanged)
            {
                // Update the copy of the vertex data for the text object.
                cachedMeshInfo = textInfo.CopyMeshInfoVertexData();

                hasTextChanged = false;
            }
            
            Vector3 topLeft, bottomLeft, topRight, bottomRight;
            
            int matIndexFirst = textInfo.characterInfo[startIndex].materialReferenceIndex;
            int matIndexLast = textInfo.characterInfo[startIndex + length - 1].materialReferenceIndex;
            int vertIndexFirst = textInfo.characterInfo[startIndex].vertexIndex;
            int vertIndexLast = textInfo.characterInfo[startIndex + length - 1].vertexIndex;
            Vector3[] sourceVerticesFirst = cachedMeshInfo[matIndexFirst].vertices;
            Vector3[] sourceVerticesLast = cachedMeshInfo[matIndexLast].vertices;

            topLeft = sourceVerticesFirst[vertIndexFirst];
            bottomLeft = sourceVerticesFirst[vertIndexFirst + 2];
            topRight = sourceVerticesLast[vertIndexFirst];
            bottomRight = sourceVerticesLast[vertIndexLast + 2];

            Vector3 center = (topLeft + bottomRight)/2;
            float width = Vector3.Distance(topLeft, topRight);
            float height = Vector3.Distance(topLeft, bottomLeft);
            float depth = 0.5f;

            boxCollider = text.gameObject.AddComponent<BoxCollider>();
            boxCollider.tag = "vocab";
            boxCollider.size = new Vector3(width, height, depth);
            boxCollider.center = center;

            yield return new WaitForSeconds(0.05f);
    }

    public void OnDestroy() {
        Destroy(boxCollider);
    }
}
