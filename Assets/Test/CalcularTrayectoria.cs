using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CalcularTrayectoria : MonoBehaviour
{

    public float fuerza = 1;
    [Range(0,1f)]
    public float tSeparacion;

    public float angle;
    public Transform[] tr;
    // Update is called once per frame
    void Update()
    {
        //t += Time.deltaTime;
        //if(t>2 )t = 0;
        angle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;



        float t = 0;
        for (int i = 0; i < tr.Length; i++)
        {
            
            float posX = fuerza * Mathf.Cos(angle) * t;
            float posY = fuerza * Mathf.Sin(angle) * t - (0.5f * 9.81f) * (t * t);
            tr[i].position = new Vector3(posX, posY, tr[i].position.z);
            t+= tSeparacion;
        }
       
    }
}
