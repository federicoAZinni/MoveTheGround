using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] GameObject meshPlayer;

    public void ToggleMeshPlayer(bool turnOnOff) { meshPlayer.SetActive(turnOnOff); }
}
