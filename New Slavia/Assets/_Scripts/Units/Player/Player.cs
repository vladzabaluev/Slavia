using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameObject Bullet, Bullet1, Bullet2, Bullet3;
    public ControlType controlType;
    public float currentHealth;
    public bool isDead = false;
    public float MaxHealth = 6;

    public Transform shotPoint;

    //public Joystick joystick;
    public float speed;

    public float xMaxborder;
    public float xMinborder;
    public float yMaxborder;

    //
    public float yMinborder;

    private Rigidbody2D _playerRb;
    [SerializeField] private float AttackCD;
    [SerializeField] private float AttackSpeed;
    [SerializeField] private float AttackSpeedMul;
    private float currentAttackCooldown;
    [SerializeField] private float damage;
    [SerializeField] private float damageMultiplier;
    public float finalDamage;
    public float finalRange;
    public float finalShootSpeed;
    [SerializeField] private float FloatAttackSpeed;
    [SerializeField] private float FloatDamage;
    private Inventory inventory;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float padding = 0.5f;
    [SerializeField] private float Range;
    [SerializeField] private float RangeMul;
    [SerializeField] private float ShootSpeed; [SerializeField] private float ShootSpeedMul;
    [SerializeField] private UI_Inventory uiInventory;

    [SerializeField] private UI_Stats uiStats;

    public enum ControlType
    { PC, Android }

    #region КодВлада

    private Vector2 _moveDirection;

    [Header("Передвижение")]
    [SerializeField] private float _moveSpeed;

    [Header("Ввод пользователя")]
    private PlayerInputControlls _playerInputConstrolls;

    private InputAction _playerLook;
    private InputAction _playerMove;

    [Header("Вращение")]
    [SerializeField] private float _rotationSpeed;

    private void Awake()
    {
        _playerInputConstrolls = new PlayerInputControlls();

        _playerMove = _playerInputConstrolls.PlayerGameProcess.Move;
        _playerLook = _playerInputConstrolls.PlayerGameProcess.Look;
    }

    private void OnDisable()
    {
        _playerInputConstrolls.PlayerGameProcess.Disable();
        _playerMove.Disable();
        _playerLook.Disable();
    }

    private void OnEnable()
    {
        _playerInputConstrolls.PlayerGameProcess.Enable();
        _playerMove.Enable();

        _playerLook.Enable();
    }

    #endregion КодВлада

    //}
    //private void FixedUpdate()
    //{
    //    _playerRb.MovePosition(_playerRb.position + MoveVelocity * Time.fixedDeltaTime);
    //    //moveBrorders();
    //}
    private void FixedUpdate()
    {
        _playerRb.velocity = _moveDirection.normalized * _moveSpeed;
    }

    private void moveBrorders()
    {
        Camera gameCamera = Camera.main;
        xMinborder = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMaxborder = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMinborder = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMaxborder = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<ItemWorld>(out ItemWorld itemWorld))
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

    private void Start() //Исходные статы
    {
        currentHealth = MaxHealth;

        currentAttackCooldown = 0;
        moveSpeed = 5;
        //Направление пули
        _playerRb = GetComponent<Rigidbody2D>();
        damage = 1; //Урон
        damageMultiplier = 1;  //Множитель урона
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
                damageMultiplier = 1;
                AttackSpeedMul = 1f;
                break;

            case (2): //Мощный выстрел
                ShootSpeedMul = 2;
                damageMultiplier = 2;
                AttackSpeedMul = 0.5f;
                RangeMul = 2;
                Bullet = Bullet2;
                break;

            case (3): //Короткий выстрел
                ShootSpeedMul = 1;
                damageMultiplier = 0.5f;
                AttackSpeedMul = 2;
                RangeMul = 0.5f;
                Bullet = Bullet3;
                break;

            default: //Дефолт, обычный выстрел
                ShootSpeedMul = 1f;
                damageMultiplier = 1;
                AttackSpeedMul = 1;
                break;
        }

        Bullet.transform.localScale = new Vector3(6, 6, 1);

        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
    }

    private void Update()
    {
        float BonusMoveSpeed = 0;
        float BonusDamage = 0;
        float BonusAttackSpeed = 0;

        // Может сменить название на BulletSpeed?
        float BonusShootSpeed = 0;
        float BonusRange = 0;
        float BonusMaxHealth = 0;
        float BonusAttackSize = 0;

        foreach (Item item in inventory.GetItemList())
        {
            switch (item.itemType)
            {
                default:
                case Item.ItemType.Apple: BonusDamage = damage * damageMultiplier / 10f * item.amount; break;
                case Item.ItemType.BullHeart: BonusMaxHealth = MaxHealth / 5f * item.amount; break;
                case Item.ItemType.ShadowInABottle: BonusAttackSize = 6f / 10f * item.amount; break;
                case Item.ItemType.BirchLeaves: BonusShootSpeed = ShootSpeed * ShootSpeedMul / 2f * item.amount; break; //10
            }
        }

        Bullet.transform.localScale = new Vector2(6 + BonusAttackSize, 6 + BonusAttackSize);
        MaxHealth = 6 + BonusMaxHealth;

        //if (controlType == ControlType.PC) MoveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //Передвижение ПК
        ////else if (controlType == ControlType.Android) MoveInput = new Vector2(joystick.Horizontal, joystick.Vertical); //Передвижение Андроид
        //MoveVelocity = MoveInput.normalized * MoveSpeed;
        AttackCD = 1 / (AttackSpeed * AttackSpeedMul + FloatAttackSpeed); //Конечная скорость атаки
        finalDamage = damage * damageMultiplier + FloatDamage + BonusDamage; //Конечный урон
        finalShootSpeed = ShootSpeed * ShootSpeedMul + BonusShootSpeed; //Конечная скорость полета пули
        finalRange = Range * RangeMul; //Конечная дальность атаки

        uiStats.SetStats(moveSpeed, finalDamage, AttackCD, finalShootSpeed, finalRange, Bullet.transform.localScale.x, MaxHealth);

        //if (tempACD <= 0)
        //{
        //    Shoot();
        //    tempACD = AttackCD;
        //}
        //else tempACD -= Time.deltaTime;

        _moveDirection = _playerMove.ReadValue<Vector2>();
        float lookDirection = (Mathf.Atan2(_playerLook.ReadValue<Vector2>().y, _playerLook.ReadValue<Vector2>().x) * Mathf.Rad2Deg);
        if (currentAttackCooldown <= 0)
        {
            GameObject bullet = Instantiate(Bullet, shotPoint.position, Quaternion.AngleAxis(lookDirection, Vector3.forward));
            Debug.Log(Quaternion.AngleAxis(lookDirection, Vector3.forward).eulerAngles);
            currentAttackCooldown = 1 / AttackCD;
        }
        else
        {
            currentAttackCooldown -= Time.deltaTime;
        }
    }

    //private void Move()

    //{
    //    var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;

    //    var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
    //    var newPosY = Mathf.Clamp(transform.position.y + deltaY, yMinborder, yMaxborder);
}