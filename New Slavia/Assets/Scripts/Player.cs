using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float padding = 0.5f;

    public bool isDead = false;

    public float tempACD, AttackCD, FinalDamage, FinalShootSpeed, FinalRange;
    private float MoveSpeed, Damage, DamageMul, FloatDamage, AttackSpeed, AttackSpeedMul, FloatAttackSpeed, ShootSpeed, ShootSpeedMul, Range, RangeMul;
    public float health;
    public float maxHealth = 6;
    public enum ControlType { PC, Android }
    public ControlType controlType;
    //public Joystick joystick;
    public float speed;
    public GameObject Bullet, Bullet1, Bullet2, Bullet3;
    public Transform shotPoint;
    public Quaternion rotation;
    private Vector2 MoveInput;
    private Vector2 MoveVelocity;
    private Rigidbody2D rb;

    public float xMinborder; //
    public float yMinborder;
    public float xMaxborder;
    public float yMaxborder;

    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private UI_Stats uiStats;
    private Inventory inventory;

    void Start() //Исходные статы
    {
        health = maxHealth;

        tempACD = 0;
        MoveSpeed = 5;
        rotation = Quaternion.Euler(0, 0, 0); //Направление пули
        rb = GetComponent<Rigidbody2D>();
        Damage = 1; //Урон
        DamageMul = 1;  //Множитель урона
        FloatDamage = 0; //Плоский урон
        AttackSpeed = 1; //Скорость атаки
        AttackSpeedMul = 1; //Множитель скорости атаки
        FloatAttackSpeed = 0; //Плоская скорость атаки
        ShootSpeed = 10; //Скорость полета пули
        ShootSpeedMul = 1; //Множитель скорости полета пули
        Range = 10; //Дальность атаки
        RangeMul = 1; //Множитель дальности атаки
        switch (PlayerPrefs.GetInt("GunType"))
        {
            case (1): //Обычный выстрел
                ShootSpeedMul = 1;
                DamageMul = 1;
                AttackSpeedMul = 1f;
                break;
            case (2): //Мощный выстрел
                ShootSpeedMul = 2;
                DamageMul = 2;
                AttackSpeedMul = 0.5f;
                RangeMul = 2;
                Bullet = Bullet2;
                break;
            case (3): //Короткий выстрел
                ShootSpeedMul = 1;
                DamageMul = 0.5f;
                AttackSpeedMul = 2;
                RangeMul = 0.5f;
                Bullet = Bullet3;
                break;
            default: //Дефолт, обычный выстрел
                ShootSpeedMul = 1f;
                DamageMul = 1;
                AttackSpeedMul = 1;
                break;
        }

        Bullet.transform.localScale = new Vector3(6, 6, 1);

        inventory = new Inventory();
        uiInventory.SetInventory(inventory);

    }

    void Update()
    {
        float BonusMoveSpeed = 0;
        float BonusDamage = 0;
        float BonusAttackSpeed = 0;

        // Может сменить название на BulletSpeed?
        float BonusShootSpeed = 0;
        float BonusRange = 0;
        float BonusMaxHealth = 0;
        float BonusAttackSize = 0;

        foreach (Item item in inventory.GetItemList()) {
            switch (item.itemType)
            {
                default:
                case Item.ItemType.Apple: BonusDamage = Damage * DamageMul / 10f * item.amount; break;
                case Item.ItemType.BullHeart: BonusMaxHealth = maxHealth / 5f * item.amount; break;
                case Item.ItemType.ShadowInABottle: BonusAttackSize = 6f / 10f * item.amount; break;
                case Item.ItemType.BirchLeaves: BonusShootSpeed = ShootSpeed * ShootSpeedMul / 2f * item.amount; break; //10
            }
        }

        Bullet.transform.localScale = new Vector2(6 + BonusAttackSize, 6 + BonusAttackSize);
        maxHealth = 6 + BonusMaxHealth;

        if (controlType == ControlType.PC) MoveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //Передвижение ПК
        //else if (controlType == ControlType.Android) MoveInput = new Vector2(joystick.Horizontal, joystick.Vertical); //Передвижение Андроид
        MoveVelocity = MoveInput.normalized * MoveSpeed;
        AttackCD = 1 / (AttackSpeed * AttackSpeedMul + FloatAttackSpeed); //Конечная скорость атаки
        FinalDamage = Damage * DamageMul + FloatDamage + BonusDamage; //Конечный урон
        FinalShootSpeed = ShootSpeed * ShootSpeedMul + BonusShootSpeed; //Конечная скорость полета пули
        FinalRange = Range * RangeMul; //Конечная дальность атаки

        uiStats.SetStats(MoveSpeed, FinalDamage, AttackCD, FinalShootSpeed, FinalRange, Bullet.transform.localScale.x, maxHealth);

        if (tempACD <= 0)
        {
            Shoot();
            tempACD = AttackCD;
        }
        else tempACD -= Time.deltaTime;
    }

    //private void Move()

    //{

    //    var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
    
    //    var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
    //    var newPosY = Mathf.Clamp(transform.position.y + deltaY, yMinborder, yMaxborder);

    //    transform.position = new Vector2(newPosX, newPosY);

    //}
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + MoveVelocity * Time.fixedDeltaTime);
        //moveBrorders();
    }

    void Shoot()
    {
        Instantiate(Bullet, shotPoint.position, rotation);
    }
    
    private void moveBrorders()
    {
        Camera gameCamera= Camera.main;
        xMinborder = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMaxborder = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMinborder = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMaxborder = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null) {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }
        
}
