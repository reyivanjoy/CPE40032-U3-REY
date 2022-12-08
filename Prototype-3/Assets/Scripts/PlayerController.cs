using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnimation;
    private AudioSource playerSound;
    public ParticleSystem collisionExplosion;
    public ParticleSystem dirtParticle;
    public AudioClip crashSound;
    public AudioClip jumpSound;
    public float jumpForce = 10.0f;
    public float gravityModifier;
    public bool isOnGround;
    public bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnimation = GetComponent<Animator>();
        playerSound = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame      
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && gameOver != true)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnimation.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerSound.PlayOneShot(jumpSound, 5.0f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            gameOver = true;
            playerAnimation.SetBool("Death_b", true);
            playerAnimation.SetInteger("DeathType_int", 1);
            collisionExplosion.Play();
            dirtParticle.Stop();
            playerSound.PlayOneShot(crashSound, 5.0f);
        }   
           
    }
}
