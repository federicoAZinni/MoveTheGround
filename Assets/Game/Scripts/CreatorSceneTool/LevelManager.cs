using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<Box> allBoxes;
    [Space(5)]
    [Header("Lvl Dependencies")]
    [SerializeField] PlayerAnimationController playerAnimatorController;
    [SerializeField] Transform playerT;
    [SerializeField] Transform startPos;
    [SerializeField] float timeToFinishLevel;
    [SerializeField] Gallinero gallinero;

    

    private void OnEnable()
    {
        GameManager.OnChangeSceneState += SetBoxesAccordingSceneState;
    }
    private void OnDisable()
    {
        GameManager.OnChangeSceneState -= SetBoxesAccordingSceneState;
    }

    public void InitToCreatorTool()
    {
        allBoxes = new List<Box>();
    }

    public void StartLevel()
    {
        gameObject.SetActive(true);
        //foreach (Box box in allBoxes) {box.gameObject.SetActive(true);}
        playerAnimatorController.ToggleMeshPlayer(false);
        playerT.position = startPos.position;
        gallinero.StartAnim(this,playerAnimatorController,startPos);
        FadeInOut.instance.FadeInOutAnim(0);
    }
    public void EndLevel()
    {
        foreach (Box box in allBoxes) { box.gameObject.SetActive(false); }
    }
    public void ResetLevel()
    {
        RePosPlayerToStartPosition();
    }
    public void RePosPlayerToStartPosition()
    {
        playerT.position = startPos.position;
    }


    public void AddNewBoxInLayer(int layerindex,GameObject box)
    {
        if (box.TryGetComponent<Box>(out Box currentBox))
        {
            currentBox.transform.SetParent(transform);
            currentBox.SetLayer(layerindex);
            allBoxes.Add(currentBox);
        }
    }

    public void RemoveBox(Box box)
    {
        allBoxes.Remove(box);
    }

    public void ClearAllBoxInLayer()
    {
        allBoxes.Clear();
    }

    private void SetBoxesAccordingSceneState(SceneState _sceneState)
    {
        switch (_sceneState)
        {
            case SceneState.scene2D:
                foreach (var box in allBoxes)
                    box.SetPos2D();
                break;
            case SceneState.scene3D:
                foreach (var box in allBoxes)
                    box.SetPos3D();
                break;
            default:
                break;
        }
    }
}


