using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPatrollingEnemy : Enemy
{
    [SerializeField] private float moveSpeed;

    [SerializeField] private Transform topPoint, botPoint;

    private GameObject player;



    //movement
    private bool movingUp;
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

        topPoint.parent = null;
        botPoint.parent = null;
        movingUp = true;

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

            if (movingUp)
            {
                enemyRB.velocity = new Vector2(enemyRB.velocity.x, moveSpeed);
                //enemySR.transform.localScale = new Vector2(-1, 1);
                if (transform.position.y > topPoint.position.y)
                {
                    movingUp = false;
                }
            }
            else
            {
                enemyRB.velocity = new Vector2(enemyRB.velocity.x, -moveSpeed);
                //enemySR.transform.localScale = new Vector2(1, 1);
                if (transform.position.y < botPoint.position.y)
                {
                    movingUp = true;
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
            anim.SetBool("isWalking", false);
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
