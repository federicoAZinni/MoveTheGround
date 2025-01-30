using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.2f;
    private float gravityValue = -9.8f;
    bool on2DMode = true;

    private void OnEnable()
    {
        GameManager.OnChangeSceneState += Repos2DSceneState;
    }
    private void OnDisable()
    {
        GameManager.OnChangeSceneState -= Repos2DSceneState;
    }


    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        groundedPlayer = controller.isGrounded;

        Move();

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }


        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        
    }


    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (on2DMode) y = 0;
        
        Vector3 move = new Vector3(x, 0, y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
            gameObject.transform.forward = move;
    }

    private void Repos2DSceneState(SceneState state)
    {
        if (state == SceneState.scene2D)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);//Lo arregle yendo a physics en projectsetting y activando el auto sync transf
            on2DMode = true;
        }
        else on2DMode = false;

    }


}
