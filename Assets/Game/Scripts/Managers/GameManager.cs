using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController playerController;
    public PlayerAnimationController playerAnimatorController;

    [Space(5)]
    [Header("Scene Referens")]
    [SerializeField] SceneState currentSceneState;
    public static Action<SceneState> OnChangeSceneState;

    [Space(5)]
    [Header("Level Referens")]
    [SerializeField] Transform levelsTranform;
    [SerializeField] LevelManager[] levels;
    int currentLvl = 0;


    private void OnEnable()
    {
        Cookie.OnGetCookie += NextLvl;
    }
    private void OnDisable()
    {
        Cookie.OnGetCookie -= NextLvl;
    }


    private void Start()
    {
        levels[currentLvl].StartLevel();
    }

    public void ChangeSceneState()
    {
        if (SceneState.scene3D == currentSceneState) currentSceneState = SceneState.scene2D;
        else currentSceneState = SceneState.scene3D;
        OnChangeSceneState?.Invoke(currentSceneState);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) ChangeSceneState();
    }

    public void NextLvl()
    {
        if(currentLvl>= levels.Length) currentLvl = 0;

        if (currentLvl != 0)
        {
            levels[currentLvl].StartLevel();
            levels[currentLvl-1].EndLevel();
        }
        else 
            levels[currentLvl].StartLevel();

        currentLvl++;
    }
}


public enum SceneState { scene2D, scene3D }