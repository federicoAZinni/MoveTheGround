using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class FadeInOut : MonoBehaviour
{
    public static FadeInOut instance;
    private void Awake()
    {
        if(instance == null) instance= this;
        else Destroy(gameObject);

        transform.localScale = Vector3.one * 0.01f;
    }
    public void FadeInOutAnim(float timeToWait)
    {
        LeanTween.scale(gameObject, Vector3.one * 0.01f, 0.5f).setOnComplete(()=>
        {
            LeanTween.delayedCall(0.5f + timeToWait, () =>
            {
                LeanTween.scale(gameObject, Vector3.one * 2, 0.5f);
            });
        });
    }
}
