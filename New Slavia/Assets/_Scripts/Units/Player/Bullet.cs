using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float Damage;
    [SerializeField] private float Speed;
    [SerializeField] private float Range;

    private Vector3 startPos;

    private void Start()
    {
        Damage = GameObject.Find("Player").GetComponent<Player>().finalDamage; //Получение урона
        Speed = GameObject.Find("Player").GetComponent<Player>().finalShootSpeed; //Получение скорости полета
        Range = GameObject.Find("Player").GetComponent<Player>().finalRange; //Получения дальности атаки
        startPos = transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector2.right * Speed * Time.deltaTime); //Полет пули
        if (Mathf.Abs(Vector3.Distance(startPos, transform.position)) >= Range) DestroyBullet(); //Уничтожение по достижению макс расстояния полета
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}