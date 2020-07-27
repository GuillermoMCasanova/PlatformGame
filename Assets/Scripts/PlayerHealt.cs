using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealt : MonoBehaviour
{
    // Start is called before the first frame update
    int health = 3;
    public Image[] hearts;
    public bool hasCoolDown=false;
    public SceneChanger sceneChanger;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && transform.position.y-0.2 <= collision.transform.position.y)
        {
            SubstractHealt();
        }
    }
    void SubstractHealt()
    {
        if (!hasCoolDown)
        {
            if (health > 0)
            {
                health--;
                hasCoolDown = true;
                StartCoroutine(Cooldown());
            }
            if (health <= 0)
            {
                sceneChanger.ChangeSceneTo("FirstLevel");
            }
            EmptyHearts();
        }
    }

    void EmptyHearts()
    {
        for(int i=0; i<hearts.Length; i++)
        {
            if (health - 1 < i)
            {
                hearts[i].gameObject.SetActive(false);
            }
        }
    }
    
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.5f);
        hasCoolDown = false;
        StopCoroutine(Cooldown());
    }
}
