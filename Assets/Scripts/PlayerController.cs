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

   public bool jump;

    bool blockInputs;

    Vector3 lastPos;

    private void OnEnable()
    {
        UIManager.OnJump += Jump;
    }
    private void OnDisable()
    {
        UIManager.OnJump -= Jump;
    }
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
    }

    private void Move()
    {
        if (!blockInputs)
        {
            moveX = joystick.Horizontal * speed * Time.fixedDeltaTime;
            moveZ = joystick.Vertical * speed * Time.fixedDeltaTime;
        }

        if (SceneController.switchScene)
        {
            moveZ = 0;
        }

        rb.velocity = new Vector3(moveX, rb.velocity.y, moveZ);
    }
    private void FixedUpdate()
    {
        IsGrounded();
    }

    void IsGrounded()
    {
        if (transform.position.y == lastPos.y) jump = true;
        else jump = false;
        lastPos = transform.position;
    }

    private void Jump()
    {
        if (jump)
        {
            JumpAnimEvent();
            anim.SetBool("isGround", false);
            anim.SetTrigger("Jump");
            jump = false;
        }
    }
    public void JumpAnimEvent()
    {
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
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
        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("isGround", true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("fall"))GameManager.INS.LevelFailed();

        if (other.CompareTag("DustParticles"))
        {
            if (!jump)
            {
                DustEffect();
            }
        }
    }
}
