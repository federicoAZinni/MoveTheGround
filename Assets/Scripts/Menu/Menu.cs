using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] Animator animCam;

    [SerializeField] Image imgFade;

    [SerializeField] GameObject buttonPlay;

    [SerializeField] GameObject animCookies;

    private void Start()
    {
        imgFade.CrossFadeAlpha(0, 2.0f, false);
    }
    public void StartGame()
    {
        LeanTween.moveLocalY (buttonPlay,1000,1).setEaseInBack();
        LeanTween.delayedCall(0.5f, () => { animCam.Play("cam2"); }); 

        LeanTween.moveX(animCookies, 10, 5);

        LeanTween.delayedCall(4f, () => { imgFade.CrossFadeAlpha(1, 1f, false); });
        LeanTween.delayedCall(4.5f, () => { SceneManager.LoadScene(1); });
    }
}
