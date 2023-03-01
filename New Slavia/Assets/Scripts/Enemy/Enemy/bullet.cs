using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public Vector2 direction = new Vector2(1, 0);
    public float speed = 2;
    public Vector2 velocity;
    bool hasReal;

    void Start()
    {
        hasReal = true;
    }

    void FixedUpdate()
    {
        velocity = direction * speed;
        Vector2 pos = transform.position;
        pos += velocity * Time.deltaTime;
        transform.position = pos;
    }

    private void OnBecameInvisible()
    {
        hasReal = false;
        Destroy(this.gameObject);
    }
    private void OnBecameVisible()
    {
        hasReal = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<Player>().health--;
            GameObject.Find("Player").GetComponent<Player>().health--;
            Destroy(gameObject);
        }
    }
}
