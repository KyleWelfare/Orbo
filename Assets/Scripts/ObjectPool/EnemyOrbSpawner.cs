using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrbSpawner : MonoBehaviour
{
    ObjectPooler objectPooler;
    public SpriteRenderer SR;
    //private float spawnCounter;
    //[SerializeField] private float orbSpawnCD;
    

    private void Awake()
    {
        //spawnCounter = orbSpawnCD;
        //GameObject enemyOrb = objectPooler.SpawnFromPool("basicRedOrb", transform.position, Quaternion.identity);
        SR = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        objectPooler = ObjectPooler.instance;
    }
    private void FixedUpdate()
    {
        //objectPooler.SpawnFromPool("basicEnemyOrb", transform.position, Quaternion.identity);
    }

    private void Update()
    {
        //if (spawnCounter <= 0)
        //{
        //    objectPooler.SpawnFromPool("basicRedOrb", transform.position, Quaternion.identity);
        //    spawnCounter = orbSpawnCD;
        //}
        //else
        //{
        //    spawnCounter -= Time.deltaTime;
        //}
    }

    public void SpawnNow()
    {
        GameObject enemyOrb = objectPooler.SpawnFromPool("basicRedOrb", transform.position, Quaternion.identity);
        SR.enabled = !SR.enabled;
        //enemyOrb.transform.parent = transform;
    }
}
