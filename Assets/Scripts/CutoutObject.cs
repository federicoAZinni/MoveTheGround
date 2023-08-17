using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutoutObject : MonoBehaviour
{
    [SerializeField]
    private Transform targetObject;

    [SerializeField]
    private LayerMask wallMask;

    private Camera mainCamera;

    List<Material> materials;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        materials = new List<Material>();
    }

    private void Update()
    {
        Vector2 cutoutPos = mainCamera.WorldToViewportPoint(targetObject.position);
        cutoutPos.y /= (Screen.width / Screen.height);

        Vector3 offset = targetObject.position - transform.position;
       
        RaycastHit[] hit = Physics.SphereCastAll(transform.position, 0.25f,offset, offset.magnitude, wallMask);

       
       if(hit.Length>0)
        {
            ClearListMaterials();

            materials.Clear();

            for (int i = 0; i < hit.Length; ++i)
            {
                materials.Add(hit[i].transform.GetChild(0).GetComponent<Renderer>().material);

                for (int m = 0; m < materials.Count; ++m)
                {
                    materials[m].SetVector("_CutoutPos", new Vector2(0.5f,0.51f));
                    materials[m].SetFloat("_CutoutSize", 0.1f);
                    materials[m].SetFloat("_FalloffSize", 0.1f);
                }
            }
        }
        else
        {
            ClearListMaterials();
        }

    }

    void ClearListMaterials()
    {
        if (materials.Count <= 0) return;
        foreach (Material item in materials)
        {
            item.SetVector("_CutoutPos", new Vector2(0.5f, 0.5f));
            item.SetFloat("_CutoutSize", 0f);
            item.SetFloat("_FalloffSize", 0f);
        }
    }
}
