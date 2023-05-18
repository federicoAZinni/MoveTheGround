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
       
        

        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);

       
       if(hitObjects.Length>0)
        {
            materials.Clear();

            for (int i = 0; i < hitObjects.Length; ++i)
            {
                materials.Add(hitObjects[i].transform.GetChild(0).GetComponent<Renderer>().material);

                for (int m = 0; m < materials.Count; ++m)
                {
                    materials[m].SetVector("_CutoutPos", new Vector2(0.5f,0.51f));
                    materials[m].SetFloat("_CutoutSize", 0.08f);
                    materials[m].SetFloat("_FalloffSize", 0.05f);
                }
            }
        }
        else
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
}
