using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneController : MonoBehaviour
{
    [SerializeField] Transform[] layersScene;

    [SerializeField] Animator camAnim;
    [SerializeField] PlayerController player;
    public static bool switchScene = true;
    public bool invert = false;

    public Transform startPosPlayerRef; 

    bool delaySwitch = true;


    private void OnEnable()
    {
        UIManager.OnChangePerspective += Change3d2d;
    }
    private void OnDisable()
    {
        UIManager.OnChangePerspective -= Change3d2d;
    }

    public void StartLevel()
    {
        camAnim.Play("cam1");
        switchScene = true;

        for (int i = 0; i < layersScene.Length; i++)
        {
            layersScene[i].position = Vector3.zero;
        }
    }

    public void Change3d2d()
    {
        if (delaySwitch)
        {

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
        if (invert) 
        for (int i = 0; i < layersScene.Length; i++)
        {
            layersScene[i].position = new Vector3(layersScene[i].position.x, layersScene[i].position.y, i);
        }
        else
        for (int i = 0; i < layersScene.Length; i++)
        {
            layersScene[i].position = new Vector3(layersScene[i].position.x, layersScene[i].position.y, -i);
        }
        camAnim.Play("cam2");
    }
    public void SceneOff3D()
    {
        camAnim.Play("cam1");
        player.ReposPlayer2D();
        LeanTween.delayedCall(0.5f, () =>
         {
             for (int i = 0; i < layersScene.Length; i++)
             {
                 layersScene[i].position = Vector3.zero;
             }
             
         });
    }

    public void WinLevelCam()
    {
        camAnim.Play("cam3");
    }

}
