﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    [SerializeField]
    private string projectileType;

    private SpriteRenderer mySpriteRenderer;

    private Monster target;

    private Queue<Monster> monsters = new Queue<Monster>();


    private bool canAttack = true;

    private float attackTimer;

    [SerializeField]
    private float attackCooldown;

    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    private float damage;

    public float ProjectileSpeed
    {
        get
        {
            return projectileSpeed;
        }

    }

    public Monster Target
    {
        get
        {
            return target;
        }


    }

    public float Damage
    {
        get
        {
            return damage;
        }


    }


    // Use this for initialization
    void Start () {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Attack();
	}


    public void Select()
    {
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
    }

    private void Attack()
    {
        if (!canAttack)
        {
            attackTimer += Time.deltaTime;
            if(attackTimer >= attackCooldown)
            {
                canAttack = true;
                attackTimer = 0;
            }
        }


        if(target == null && monsters.Count > 0)
        {
            target = monsters.Dequeue();
        }
        if(target != null && target.IsActive)
        {
            if (canAttack)
            {
                Shoot();
                canAttack = false;

            }
        }
    }

    private void Shoot()
    {
        Projectile projectile = GameManager.Instance.Pool.GetObject(projectileType).GetComponent<Projectile>();

        projectile.transform.position = transform.position;

        projectile.Initialize(this);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Monster")
        {
            monsters.Enqueue(other.GetComponent<Monster>());
            //target = other.GetComponent<Monster>();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Monster")
        {
            target = null;
        }
    }
}
