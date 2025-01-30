using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Animator animator_cineMachine;
    private void OnEnable() { GameManager.OnChangeSceneState += ChangeCamera; }

    private void ChangeCamera(SceneState sceneState)
    {
        switch (sceneState)
        {
            case SceneState.scene2D:
                animator_cineMachine.Play("2DCamera");
                break;
            case SceneState.scene3D:
                animator_cineMachine.Play("3DCamera");
                break;
            default:
                break;
        }
    }

}
