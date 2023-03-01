using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{
    public float speed;
    private Transform transf;
    private float backSize;
    private float backPos;
    void Start()
    {
        transf = gameObject.GetComponent<Transform>();
        backSize = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    public void move()
    {
        backPos += speed * Time.deltaTime;
        backPos = Mathf.Repeat(backPos, backSize);
        transf.position = new Vector3(backPos, 0, 0);
    }
}
