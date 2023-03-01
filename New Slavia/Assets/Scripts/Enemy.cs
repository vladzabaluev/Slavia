/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public EnemyType enemyType;
    public float offset;
    private float speed;
    private float timeBtwAttack;
    public float starttimeBtwAttack;
    public float damage;
    private float stopTime;
    public float startStopTime;
    public float normalSpeed;
    private Player player;
    private Animator anim;
    private float rotZ;
    private Vector3 difference;
    public GameObject Bullet;
    public Transform shotPoint;
    private Quaternion rotation;
    public Collider2D attackCollider;
    public enum EnemyType {Melee,Mage};
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (stopTime <= 0) speed = normalSpeed;
        else
        {
            speed = 0;
            stopTime -= Time.deltaTime;
        }
        if (health <= 0)
        {
            player.killCount += 1;
            Destroy(gameObject);
        }
        if (timeBtwAttack > 0) timeBtwAttack -= Time.deltaTime;
        if (!(anim.GetBool("attack"))){
            if (player.transform.position.x > transform.position.x) transform.eulerAngles = new Vector3(0, 0, 0);
            else transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void FixedUpdate()
    {
        difference = player.transform.position - transform.position;
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (enemyType == EnemyType.Melee)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= 7 && Vector3.Distance(player.transform.position, transform.position) >= 1.3 && !(anim.GetBool("attack")))
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                anim.SetBool("IsRunning", true);
            }
            else anim.SetBool("IsRunning", false);
            if (Vector3.Distance(player.transform.position, transform.position) <= 1.3)
            {
                if (timeBtwAttack <= 0)
                {
                    anim.SetTrigger("attack");
                    timeBtwAttack = starttimeBtwAttack;
                }
            }
        }else if (Vector3.Distance(player.transform.position, transform.position) <= 10)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= 8)
            {
                if (timeBtwAttack <= 0)
                {
                    anim.SetTrigger("attack");
                    timeBtwAttack = starttimeBtwAttack;
                }
            }else transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
    public void TakeDamage(float damage)
    {
        stopTime = startStopTime;
        health -= damage;
        spriteRenderer.color =new Color(1,0,0,1);
        Invoke("ChangeColor", 0.1f);
    }

    void ChangeColor()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
    public void onEnemyAttack()
    {
        FindObjectOfType<SoundVolume>().SwordSlash();
        if (attackCollider.IsTouching(player.GetComponent<Collider2D>())) player.health -= damage;
        Invoke("afterAttack", 0.1f);
        timeBtwAttack = starttimeBtwAttack;
    }
    private void afterAttack()
    {
        anim.SetBool("attack", false);
    }
        

    void Shoot()
    {
        anim.SetBool("attack", false);
        FindObjectOfType<SoundVolume>().Fireball();
        Instantiate(Bullet, shotPoint.position, rotation);
    }
}
*/