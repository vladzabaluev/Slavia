using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class babaYaga : MonoBehaviour
{
    public float heal;

    public float speed;
    public float stoppingDistance;
    public float retreateDistance;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;     
    }

   
    void FixedUpdate()
    {
        if(Vector2.Distance(transform.position, player.position)>stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if(Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position,player.position) >retreateDistance)
        {
            transform.position = this.transform.position;
        }
        else if(Vector2.Distance(transform.position, player.position)<retreateDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }
    }

    public void HealCheck()
    {
        heal = heal - GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().FinalDamage;
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
                GameObject.Find("Player").GetComponent<Player>().health--;
                Destroy(gameObject);
                break;
        }
    }
}
