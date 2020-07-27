using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource stepsSound;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        jumpSound = GetComponents<AudioSource>()[0];
        stepsSound = GetComponents<AudioSource>()[1];
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        playerRigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * walkSpeed, playerRigidBody.velocity.y);

        if (Input.GetAxis("Horizontal") == 0)
        {
            playerAnimator.SetBool("isWalking", false);
            stepsSound.Pause();
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            playerAnimator.SetBool("isWalking", true);
            GetComponent<SpriteRenderer>().flipX = true;
            if (!stepsSound.isPlaying && isGrounded)
            {
                stepsSound.Play();
            }
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            playerAnimator.SetBool("isWalking", true);
            GetComponent<SpriteRenderer>().flipX = false;
            if (!stepsSound.isPlaying && isGrounded)
            {
                stepsSound.Play();
            }
        }
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                stepsSound.Pause();
                playerRigidBody.AddForce(Vector2.up * jumpSpeed);
                isGrounded = false;
                playerAnimator.SetTrigger("Jump");
                jumpSound.Play();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
