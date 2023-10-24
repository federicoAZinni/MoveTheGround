using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{
    
    void Start()
    {
        AnimationRot();
    }

    public void AnimationRot()
    {
        LeanTween.moveY(gameObject, transform.position.y+0.3f, 1).setLoopPingPong();
    }

    public void AnimWin()
    {
        LeanTween.cancel(gameObject);
        LeanTween.rotateX(gameObject, 0, 1);
        PlayerController.player.blockInputs = true;
        PlayerController.player.StopPlayer();
        LeanTween.move(gameObject, GameManager.INS.playerController.refAnimWin.position, 0.5f).setOnComplete(()=> { GameManager.INS.LevelWin(); });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            
            AnimWin();
        }
    }
}
