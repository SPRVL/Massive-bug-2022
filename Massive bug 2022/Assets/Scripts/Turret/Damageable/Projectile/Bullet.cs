using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    //how many hit bullet will be destroyed after first hit, if negative => never destroy      
    [SerializeField] protected float lifeTime;    

    protected Vector2 moveDirection;
    protected Rigidbody2D rb2D;

    protected void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();

        Destroy(gameObject, lifeTime);

        moveDirection = Caculator.NormalizedDirection2D(transform.rotation.eulerAngles.z);
    }
    protected void FixedUpdate()
    {
        MoveProjectile();
    }

    protected void MoveProjectile()
    {
        rb2D.velocity = moveDirection * speed;
    }

    protected override Vector2 KnockbackDirection()
    {
        return moveDirection;
    }
}
