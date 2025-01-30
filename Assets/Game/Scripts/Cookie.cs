using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookie : MonoBehaviour
{
    public static System.Action OnGetCookie;

    void Start()
    {
        AnimationCookie();
    }

    private void AnimationCookie()
    {
        LeanTween.moveLocalY(gameObject,transform.localPosition.y +0.2f,1).setLoopPingPong(-1);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnGetCookie?.Invoke();
        LeanTween.cancel(gameObject);
    }

}
