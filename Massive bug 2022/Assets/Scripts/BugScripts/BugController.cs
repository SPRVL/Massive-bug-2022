using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugController : MonoBehaviour
{
    [SerializeField] GameObject explosionEffect;
    [SerializeField] int maxHP;
    [SerializeField] float movementSpeed;

    private bool isBeingKnockback;

    private int currentHP;
    private Transform destination;
    private Rigidbody2D rb2D;

    private void Awake()
    {
        BugSpawner.instance.AddBugTrans(transform);
    }
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        //destination = IngameManager.instance.PlayerTrans;

        currentHP = maxHP;

        isBeingKnockback = false;
    }

    private void FixedUpdate()
    {
        if(!isBeingKnockback)
        {
            if (destination != null)
            {
                LookAtPlayer();
                MoveForward();
            }
        }
    }
    private void OnDestroy()
    {
        BugSpawner.instance.RemoveBugTrans(transform);
    }
    private void LookAtPlayer()
    {
        transform.rotation = Quaternion.AngleAxis(Caculator.Vector2ToAngleInDegree(destination.position - transform.position), Vector3.forward);
    }
    private void MoveForward()
    {
        rb2D.velocity = (destination.position - transform.position).normalized * movementSpeed;
    }
    public void GetHit(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            //IngameManager.instance.OnEnemyKilled();
            Explode();

        }
    }
    public void GetHit(int damage, float knockbackTime, Vector2 knockbackForce)
    {
        GetHit(damage);
        StartCoroutine(InvokeKnockback(knockbackTime,knockbackForce));
    }

    private IEnumerator InvokeKnockback(float knockbackTime, Vector2 force)
    {
        rb2D.velocity = force;
        isBeingKnockback = true;
        yield return new WaitForSeconds(knockbackTime);
        isBeingKnockback = false;
        rb2D.velocity = Vector2.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //collision.GetComponent<PlayerController>().GetHit(currentHP);
            Explode();
        }
    }
    private void Explode()
    {
        if(explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
   
}
