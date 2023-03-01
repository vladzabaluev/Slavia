using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage, Speed, Range;
    private GameObject enemy;
    private Vector3 startPos;
    void Start()
    {
        Damage = GameObject.Find("Player").GetComponent<Player>().FinalDamage; //Получение урона
        Speed = GameObject.Find("Player").GetComponent<Player>().FinalShootSpeed; //Получение скорости полета
        Range = GameObject.Find("Player").GetComponent<Player>().FinalRange; //Получения дальности атаки
        startPos = transform.position;
    }
    void Update()
    {
        transform.Translate(Vector2.right * Speed * Time.deltaTime); //Полет пули
        if (Mathf.Abs(Vector3.Distance(startPos, transform.position)) >= Range) DestroyBullet(); //Уничтожение по достижению макс расстояния полета
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
