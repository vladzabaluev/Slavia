/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBullet : MonoBehaviour
{
    private float Damage, Speed, Range;
    private Vector3 startPos;
    void Start()
    {
        Damage = GameObject.Find("Player").GetComponent<AutoShootNMove>().FinalDamage; //Получение урона
        Speed = GameObject.Find("Player").GetComponent<AutoShootNMove>().FinalShootSpeed; //Получение скорости полета
        Range = GameObject.Find("Player").GetComponent<AutoShootNMove>().FinalRange; //Получения дальности атаки
        startPos = transform.position;
    }
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, 0.7f);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(Damage);
                Damage = 0;
                Destroy(gameObject);
            }
            if (hitInfo.collider.CompareTag("Solid")) DestroyBullet();
        }
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
        if (Mathf.Abs(Vector3.Distance(startPos, transform.position)) >= Range) DestroyBullet();
    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
*/