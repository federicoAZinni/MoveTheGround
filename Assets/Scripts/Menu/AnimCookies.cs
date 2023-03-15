using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCookies : MonoBehaviour
{
    public GameObject[] cookies;
    void Start()
    {

        LeanTween.moveLocalY(cookies[0], transform.localPosition.y + 0.3f, 0.5f).setLoopPingPong();
        LeanTween.delayedCall(0.5f, () => { LeanTween.moveLocalY(cookies[1], transform.localPosition.y + 0.3f, 0.5f).setLoopPingPong(); });
        LeanTween.moveLocalY(cookies[2], transform.localPosition.y + 0.3f, 0.5f).setLoopPingPong();

        for (int i = 0; i < cookies.Length; i++)
        {
            LeanTween.rotateAround(cookies[i], Vector3.forward, -360, 1).setRepeat(-1);
        }
        
    }
}
