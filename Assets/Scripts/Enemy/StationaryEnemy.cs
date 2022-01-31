using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryEnemy : Enemy
{

    private GameObject player;


    //attack
    private EnemyOrbSpawner orbSpawner;
    //private bool canShoot;    

    protected override void Start()
    {
        base.Start();

        player = GameObject.FindGameObjectWithTag("Player");
        orbSpawner = GetComponentInChildren<EnemyOrbSpawner>();

    }

}
