using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{

    public GameObject dude,fire,pouches,options;
    private float tempTime;

    public void Start()
    {
        tempTime = 0f;
        options.SetActive(false);
    }

    public void FixedUpdate()
    {
        tempTime += Time.fixedDeltaTime;
    }
    public void PlayGame()
    {
        if (tempTime < 1) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuiteGame()
    {
        if (tempTime < 2) return;
        Debug.Log("Quite");
    }

    public void redPouch()
    {
        if (tempTime < 2) return;
        tempTime = 0;
        pouches.GetComponent<SpriteRenderer>().sortingOrder = 1;
        dude.GetComponent<Animator>().SetTrigger("throwDust");
        Invoke("createRedFire", 1.66f);
    }

    public void bluePouch()
    {
        if (tempTime < 2) return;
        tempTime = 0;
        pouches.GetComponent<SpriteRenderer>().sortingOrder = 1;
        dude.GetComponent<Animator>().SetTrigger("throwDust");
        Invoke("createBlueFire", 1.66f);
    }

    public void greenPouch()
    {
        if (tempTime < 2) return;
        tempTime = 0;
        pouches.GetComponent<SpriteRenderer>().sortingOrder = 1;
        dude.GetComponent<Animator>().SetTrigger("throwDust");
        Invoke("createGreenFire", 1.66f);
    }

    public void MainButtonClick()
    {
        if (tempTime < 2) return;
        tempTime = 0;
        switch (pouches.GetComponent<Animator>().GetInteger("bottleId"))
        {
            case 1:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case 2:
                options.SetActive(true);
                gameObject.SetActive(false);
                break;
            case 3:
                print("Выход");
                Application.Quit();
                break;
            default:
                break;
        }
    }

    public void createBlueFire()
    {
        pouches.GetComponent<SpriteRenderer>().sortingOrder = 3;
        pouches.GetComponent<Animator>().SetInteger("bottleId", 3);
        fire.GetComponent<Animator>().SetInteger("fireColor",2);
    }

    public void createRedFire()
    {
        pouches.GetComponent<SpriteRenderer>().sortingOrder = 3;
        pouches.GetComponent<Animator>().SetInteger("bottleId", 1);
        fire.GetComponent<Animator>().SetInteger("fireColor", 1);
    }

    public void createGreenFire()
    {
        pouches.GetComponent<SpriteRenderer>().sortingOrder = 3;
        pouches.GetComponent<Animator>().SetInteger("bottleId", 2);
        fire.GetComponent<Animator>().SetInteger("fireColor", 3);
    }
}
