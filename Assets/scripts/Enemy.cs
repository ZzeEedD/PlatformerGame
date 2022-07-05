using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;
    public List<Transform> points;
    private int currentIndex;
    public int maxHealth = 100;
    public int enemyDamage = 1;
    public float speed = 1f;
    public bool faceRight = true;
    public bool walking;
    public Vector2 moveVector;
    int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        moveVector = points[0].position;
        walking = true;
        
    }
    public void Update()
    {
        Walk();
    }
    private void OnTriggerEnter2D(Collider2D enemyhit)
    {
        Player player = enemyhit.GetComponent<Player>();
        if (player != null)
        {
            anim.SetTrigger("Attack");
            player.Hurt(enemyDamage);
        }
    }

    void Walk()
    {
        anim.SetBool("Walk", walking);

        if (walking)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, moveVector, step);

            if (Vector3.Distance(transform.position, moveVector) < 0.3f)
            {
                StartCoroutine(Idle());
            }
        }
    }

    private IEnumerator Idle()
    {
        walking = false;
        anim.SetTrigger("Idle");
        ChooseNextPoint();

        yield return new WaitForSeconds(1);

        walking = true;

    }

    private void ChooseNextPoint()
    {
        currentIndex = ++currentIndex < points.Count ? currentIndex : 0;
        moveVector = points[currentIndex].position;

        ChooseDirection();
    }
    private void ChooseDirection()
    {
        GetComponent<SpriteRenderer>().flipX = moveVector.x < transform.position.x;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Enemy died");
        anim.SetBool("isDead", true);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        GetComponent<CapsuleCollider2D>().enabled = false;
        this.enabled = false;
    }
}
