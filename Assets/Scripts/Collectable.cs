using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Text scoreText;
    [SerializeField] private ParticleSystem collectableParticle;
    [SerializeField] private AudioSource coinSound;
    private static int score;
    void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        coinSound = gameObject.GetComponentInParent<AudioSource>();
        collectableParticle = GameObject.Find("CollectableParticle").GetComponent<ParticleSystem>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collectableParticle.transform.position = transform.position;
            collectableParticle.Play();
            gameObject.SetActive(false);
            coinSound.Play();
            score++;
            scoreText.text = "Score: " + score.ToString();
        }    

    }
}
