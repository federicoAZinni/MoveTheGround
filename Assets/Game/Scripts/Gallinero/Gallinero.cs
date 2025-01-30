using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gallinero : MonoBehaviour
{
    [SerializeField] GameObject fakePlayer;
    Vector3 startPorFakePlayerRef;

    private void Awake()
    {
        startPorFakePlayerRef = fakePlayer.transform.position;
    }

    public void StartAnim(LevelManager levelManager, PlayerAnimationController playerAnimatorController, Transform startReflevel)
    {
        LeanTween.moveLocalZ(fakePlayer, 0.2f, 1f).setEaseInBack().setOnComplete(() => {
            LeanTween.delayedCall(0.5f, () => { 
                LeanTween.move(fakePlayer, startReflevel.position, 1).setEaseInBack().setOnComplete(() => {
                    LeanTween.rotateY(fakePlayer, 180, 1f).setEaseInOutBack().setOnComplete(() => {
                        LeanTween.delayedCall(0.5f, () =>
                        {
                            levelManager.RePosPlayerToStartPosition();
                            fakePlayer.transform.position = startPorFakePlayerRef;
                            fakePlayer.transform.localEulerAngles =new Vector3(0,90,0);
                            playerAnimatorController.ToggleMeshPlayer(true);
                        });
                    });
                });
            });
        });
    }
}
