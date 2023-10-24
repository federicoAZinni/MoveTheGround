using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager INS;

    public SceneController[] lvlsList;
    public PlayerController playerController;
    [SerializeField] SceneController sceneController;
    [SerializeField] Animator animCam;

    public int levelActual;

    public int countFalls;

    private void Awake()
    {
        INS = this;
    }

    public void LevelFailed()
    {
        UIManager.INS.FadeInOut();
        LeanTween.delayedCall(1, () => {
            playerController.ReposPlayerLevelStart();
            lvlsList[levelActual].StartLevel();
            sceneController.SceneOff3D();
        });

        countFalls++;
        if(countFalls%2==0)
        {
            InterstitialAds.instance.LoadAd();
        }
        if (countFalls % 3 == 0)
        {
            InterstitialAds.instance.ShowAd();
        }
    }
    public void LevelWin()
    {
        playerController.WinAnim();
        sceneController.WinLevelCam();
        LeanTween.delayedCall(1, () => { UIManager.INS.FadeInOut(); });
        LeanTween.delayedCall(2, () => { 
            NextLevel(); 
            playerController.ReposPlayerLevelStart();
            lvlsList[levelActual].StartLevel();
        });
    }
    public void NextLevel()
    {
        lvlsList[levelActual].gameObject.SetActive(false);
        levelActual++;

        if (levelActual >= lvlsList.Length)
        {
            levelActual = 0;
        }

        lvlsList[levelActual].gameObject.SetActive(true);
    }
}
