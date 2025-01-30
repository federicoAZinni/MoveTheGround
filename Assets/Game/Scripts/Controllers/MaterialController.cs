using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    [SerializeField] Material[] materials2DMode;
    [SerializeField] Material[] materials3DMode;

    [SerializeField] MeshRenderer m_Renderer;

    [SerializeField] CinemachineBlenderSettings cinemachineBlenderSettings;

    private void Awake()
    {
       if(m_Renderer==null) m_Renderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable() { GameManager.OnChangeSceneState += ChangeMaterialsAccordingSceneMode; }
    private void OnDisable() { GameManager.OnChangeSceneState -= ChangeMaterialsAccordingSceneMode; }

    void ChangeMaterialsAccordingSceneMode(SceneState state)
    {
        float blendTime2D = cinemachineBlenderSettings.GetBlendForVirtualCameras("2DCamera", "3DCamera", new CinemachineBlendDefinition()).BlendTime;
        
        switch (state)
        {
            case SceneState.scene2D:
                LeanTween.delayedCall(blendTime2D, () =>
                {
                    m_Renderer.materials = materials2DMode;
                });
                break;
            case SceneState.scene3D:
                    m_Renderer.materials = materials3DMode;
                break;
            default:
                break;
        }
        
    }
}
