using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneController : MonoBehaviour
{
    [SerializeField] Transform[] layersScene;

    [SerializeField] Animator camAnim;
    [SerializeField] PlayerController player;
    public static bool switchScene = true;

    public Transform startPosPlayerRef; 

    bool delaySwitch = true;

    bool tutorialFlag = true;

    public void StartLevel()
    {
        camAnim.Play("cam1");
        switchScene = true;

        for (int i = 0; i < layersScene.Length; i++)
        {
            layersScene[i].position = Vector3.zero;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)&& delaySwitch)
        {
            if (tutorialFlag) { UIManager.INS.AnimTutorialTextOut(); tutorialFlag = false; }

            delaySwitch = false;
            LeanTween.delayedCall(0.5f, () => { delaySwitch = true; });

            if (switchScene)
                SceneOn3D();
            else
               SceneOff3D();

            switchScene = !switchScene;
            
        }
    }

    public void SceneOn3D()
    {
        for (int i = 0; i < layersScene.Length; i++)
        {
            layersScene[i].position = new Vector3(layersScene[i].position.x, layersScene[i].position.y, -i);
        }

        camAnim.Play("cam2");
    }
    public void SceneOff3D()
    {
        camAnim.Play("cam1");
        LeanTween.delayedCall(0.5f, () =>
         {
             for (int i = 0; i < layersScene.Length; i++)
             {
                 layersScene[i].position = Vector3.zero;
             }
             player.ReposPlayer2D();
         });
    }

    public void WinLevelCam()
    {
        camAnim.Play("cam3");
    }

}
