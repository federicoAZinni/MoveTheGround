using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
     [SerializeField]int layer;

    public void SetLayer(int _layer)
    {
        layer = _layer;
    }

    public void SetPos3D()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, layer);
    }

    public void SetPos2D()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
