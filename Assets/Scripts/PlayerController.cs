using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    [SerializeField] float force;
    [SerializeField] float speed;
    [SerializeField] Transform playerMesh;
    public Transform refAnimWin;
    [SerializeField] Animator anim;
    [SerializeField] GameObject dustParticles;
    [SerializeField] FixedJoystick joystick;

    float moveX;
    float moveZ;

    bool jump;

    bool blockInputs;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        anim.SetFloat("speedY", rb.velocity.y);
        MeshPlayerRot();
    }

    public void ReposPlayerLevelStart()
    {
        transform.position = GameManager.INS. lvlsList[GameManager.INS.levelActual].startPosPlayerRef.position;
        anim.SetBool("Eat", false);
        blockInputs = false;
        jump = false;
    }

    private void Move()
    {
        if (!blockInputs)
        {
            moveX = joystick.Horizontal * speed;
            moveZ = joystick.Vertical * speed;
        }

        if (SceneController.switchScene)
        {
            moveZ = 0;
        }

        rb.velocity = new Vector3(moveX, rb.velocity.y, moveZ);

        if (Input.GetKeyDown(KeyCode.Space) && jump)
        {
            LeanTween.delayedCall(0.2f, () => { rb.AddForce(Vector3.up * force, ForceMode.Impulse); });

            anim.SetBool("isGround", false);
            anim.SetTrigger("Jump");
            jump = false;
        }
    }

    public void ReposPlayer2D()
    {
        Ray rayo = new Ray();

        rayo.origin = transform.position;
        rayo.direction = transform.forward;

        if(!Physics.Raycast(rayo, 10))
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    public void MeshPlayerRot()
    {
        if (moveX!=0 || moveZ != 0) { 

            float angle = Mathf.Atan2(moveX, moveZ);
            angle *= Mathf.Rad2Deg;
            playerMesh.transform.rotation = Quaternion.Euler(0, angle, 0);

            if(jump) anim.SetBool("Walk", true);

        }else
            anim.SetBool("Walk", false);
    }

    public void WinAnim()
    {
        blockInputs = true;
        moveX = 0;
        moveZ = 0;
        LeanTween.delayedCall(0.5f, () => { anim.SetBool("Eat", true); });
    }

    public void DustEffect ()
    {
        dustParticles.SetActive(true);
        LeanTween.delayedCall(0.5f, () =>
        {
            dustParticles.SetActive(false);
        });
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.TryGetComponent<Box>(out Box box))
        //{
        //    if (!jump)
        //    {
        //        DustEffect();
        //    }
        //}
        if (collision.gameObject.CompareTag("Ground"))
        {
            jump = true;
            anim.SetBool("isGround", true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("fall")) GameManager.INS.LevelFailed();
    }
}
