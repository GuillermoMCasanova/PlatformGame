using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Rigidbody2D enemyRigidBody;
    [SerializeField] private float timeBeforeChange;
    [SerializeField] private float delay;
    [SerializeField] private float wallSpeed;
    [SerializeField] private ParticleSystem enemyParticle;
    [SerializeField] private AudioSource hurtSound;
    private SpriteRenderer enemySpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
        hurtSound = GetComponentInParent<AudioSource>();
        enemyParticle = GameObject.Find("EnemyParticle").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyRigidBody.velocity = Vector2.right * wallSpeed;
        enemySpriteRenderer.flipX = (wallSpeed<0);

        if (timeBeforeChange < Time.time)
        {
            wallSpeed *= -1;
            timeBeforeChange = Time.time + delay;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (transform.position.y + 0.3f < collision.transform.position.y)
            {
                GetComponent<Animator>().SetTrigger("Hurt");
                enemyParticle.transform.position = transform.position;
                enemyParticle.Play();
                hurtSound.Play();
            }
        }
    }

    public void DisableEnemy()
    {
        gameObject.SetActive(false);
    }
}
