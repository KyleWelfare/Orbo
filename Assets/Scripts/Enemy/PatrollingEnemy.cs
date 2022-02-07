using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : Enemy
{
    [SerializeField] private float moveSpeed;

    [SerializeField] private Transform leftPoint, rightPoint;

    private GameObject player;

    

    //movement
    private bool movingRight;
    [SerializeField] private float moveTime, waitTime;
    private float moveCount, waitCount;

    //attack
    private EnemyOrbSpawner orbSpawner;
    //private bool canShoot;    

    protected override void Start()
    {
        base.Start();

        player = GameObject.FindGameObjectWithTag("Player");
        orbSpawner = GetComponentInChildren<EnemyOrbSpawner>();
        
        leftPoint.parent = null;
        rightPoint.parent = null;
        movingRight = true;

        moveCount = moveTime;
        //canShoot = true;
    }

    protected virtual void FixedUpdate()
    {
        //patrolling movement 

        if (moveCount > 0)
        {
            //anim.SetBool("isWalking", true);
            moveCount -= Time.deltaTime;
            //canShoot = true;

            if (movingRight)
            {
                enemyRB.velocity = new Vector2(moveSpeed, enemyRB.velocity.y);
                enemySR.transform.localScale = new Vector2(-1, 1);
                if (transform.position.x > rightPoint.position.x)
                {
                    movingRight = false;
                }
            }
            else
            {
                enemyRB.velocity = new Vector2(-moveSpeed, enemyRB.velocity.y);
                enemySR.transform.localScale = new Vector2(1, 1);
                if (transform.position.x < leftPoint.position.x)
                {
                    movingRight = true;
                }
            }

            if (moveCount <= 0)
            {
                waitCount = waitTime;
            }
        }

        else if (waitCount > 0)
        {
            orbSpawner.SR.enabled = true;
            // anim.SetBool("isWalking", false);
            waitCount -= Time.deltaTime;
            enemyRB.velocity = new Vector2(0f, enemyRB.velocity.y);
            //if (canShoot == true) { //occurs once per stop
            //    orbSpawner.SpawnNow();
            //    canShoot = false;
            //}

            if (waitCount <= 0)
            {
                moveCount = moveTime;
            }
        }
    }

    /*
    private void SpawnNow() //triggered by animation event
    {
        if (player.activeInHierarchy)
        {
            orbSpawner.SpawnNow();
        }
    } */
}
