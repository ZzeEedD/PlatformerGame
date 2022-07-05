using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{

    public Rigidbody2D rb;
    public Animator anim;
    public Transform attackPoint;
    public HealthBar healthBar;
    public Player player;
    [SerializeField]private GameController gameController;
    public int playerMaxHealth = 10;
    public float attackRange = 0.5f;
    public int attackDamage = 100;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    private int depthDamage = 10;
    public static int playerCurrentHealth;
    public LayerMask enemyLayers;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GroundCheckRadius = GroundCheck.GetComponent<CircleCollider2D>().radius;
        playerCurrentHealth = playerMaxHealth;
        healthBar.SetMaxHealth(playerMaxHealth);
        Score.Coin = 0;

    }

    void Update()
    {
        Walk();
        Reflect();
        Jump();
        CheckingGround();
        Attack();
        Depth();

    }

    public Vector2 moveVector;
    public int speed = 3;
    void Walk()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
        anim.SetFloat("moveX", Mathf.Abs(moveVector.x));
    }

    public bool faceRight = true;
    void Reflect()
    {
        if (moveVector.x > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        if (moveVector.x < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    public int jumpForce = 10;
    void Jump()
    {
        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }


    public bool onGround;
    public LayerMask Ground;
    public Transform GroundCheck;
    private float GroundCheckRadius;
    void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, Ground);
        anim.SetBool("onGround", onGround);
    }


    void Attack()
    {
        if (Time.time >= nextAttackTime)
            if (Input.GetKeyDown(KeyCode.Mouse0) && (Mathf.Abs(moveVector.x) <= 0.00005f) && (Mathf.Abs(rb.velocity.y) <= 0.00005f))
            {
                anim.SetTrigger("attackTrigger");
                nextAttackTime = Time.time + 1f / attackRate;

                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
                }
            }
    }

    public void Depth()
    {
        if (transform.position.y < -5)
        {
            DepthDamage();
        }
    }
    public void DepthDamage()
    {
        playerCurrentHealth -= depthDamage;
        anim.SetTrigger("hurt");
        healthBar.SetHealth(playerCurrentHealth);
        Debug.Log("Player hited" + playerCurrentHealth);

        if (playerCurrentHealth <= 0)
        {
            Die();
        }
    }


    public void Hurt(int enemyDamage)
    {
        playerCurrentHealth -= enemyDamage;
        anim.SetTrigger("hurt");
        healthBar.SetHealth(playerCurrentHealth);
        Debug.Log("Player hited" + playerCurrentHealth);
        this.player.GetComponent<Rigidbody2D>().AddForce(transform.up * 10f, ForceMode2D.Impulse);

        if (playerCurrentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        anim.SetBool("Dead", true);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        GetComponent<CapsuleCollider2D>().enabled = false;
        gameController.LoseGame();
        this.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}