using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrb : MonoBehaviour, IPooledObject
{
    private Rigidbody2D RB;

    //orb stats
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDelay;
    //[SerializeField] private int projectileDamage;
    private Enemy enemy;
    private float delayCounter;

    //target player
    private Transform player;
    [SerializeField] GameObject orbSpawner;
    private Vector3 aim;


    public void OnObjectSpawn()
    {
        enemy = FindObjectOfType<Enemy>();
        RB = GetComponent<Rigidbody2D>();
        delayCounter = projectileDelay;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        aim = (player.transform.position - transform.position).normalized;
        RB.velocity = Vector2.zero;
    }

    private void Update()
    {
        if (delayCounter <= 0)
        {
            RB.velocity = new Vector2(aim.x * projectileSpeed, aim.y * projectileSpeed + 0.2f);
         
            //destroy if reaches target
            //if(transform.position.x == target.x && transform.position.y == target.y)
            //{
            //    DestroyProjectile();
            //}
        }
        else
        {
            delayCounter -= Time.deltaTime;
        }

        //if (gameObject.activeSelf == false)
        //{
        //    transform.parent = ObjectPooler.instance.pools[0].spawner.transform;
        //}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.instance.DealDamageToPlayer(enemy.projectileDamage);
        }
    }
}
