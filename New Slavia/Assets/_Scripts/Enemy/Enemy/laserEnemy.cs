using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserEnemy : MonoBehaviour
{
    public gunLaser gunLas;
    public float heal;
    private bool hasReal;
    public float speedRight = 0;
    public float speedUpDown = 0;

    public float movingDelay = 0f;
    float movingTimer = 0f;

    public float shootDelay = 0f;
    float shootTimer = 0f;

    bool isMoving = true;
    bool isSlide = false;

    private int i;
    public Transform[] points;
    Vector2[] upDown = new Vector2[2];

    float speed;


    void Start()
    {
        speed = speedUpDown;
        hasReal = true;
    }


    void FixedUpdate()
    {
        if(movingDelay <= movingTimer && isMoving)
        {
            speedRight = 0;
            upDown[0] = points[0].position;
            upDown[1] = points[1].position;
            movingTimer = 0;
            isMoving = false;
            isSlide = true;           
        }
        else if(movingDelay > movingTimer && isMoving)
        {

            this.gameObject.transform.position += new Vector3(-speedRight * Time.deltaTime, 0, 0);
            movingTimer += Time.deltaTime;
        }


        if (shootDelay <= shootTimer && !isMoving && isSlide)
        {
            gunLas.autoShoot = true;
            StartCoroutine(speedStop());
            shootTimer = 0;
            
        }
        else if (shootDelay > shootTimer && !isMoving && isSlide)
        {

            if (Vector2.Distance(transform.position, upDown[i]) < 0.02f)
            {
                i++;
                if (i == points.Length)
                {
                    i = 0;
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, upDown[i], speedUpDown * Time.deltaTime);

            shootTimer += Time.deltaTime;
        }
    }

    public void HealCheck()
    {
        heal = heal - GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().finalDamage;
        if (heal <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Bullet":
                HealCheck();
                Destroy(collision.gameObject);
                break;
            case "Enemy":
                break;
            case "Player":
                GameObject.Find("Player").GetComponent<Player>().currentHealth--;
                Destroy(gameObject);
                break;
        }
    }

    IEnumerator speedStop()
    {
        speedUpDown = 0;

        yield return new WaitForSeconds(5f);

        speedUpDown = speed;
    }
    private void OnBecameInvisible()
    {       
        hasReal = false;
    }
    private void OnBecameVisible()
    {       
        hasReal = true;
    }
}
