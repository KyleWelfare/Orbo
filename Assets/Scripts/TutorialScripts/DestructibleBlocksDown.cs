using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBlocksDown : MonoBehaviour, IDamageable
{
    OrbShoot orbShoot;

    public void Damage(int damage)
    {
        orbShoot = FindObjectOfType<OrbShoot>();
        if (orbShoot.transform.position.y < gameObject.transform.position.y)
        {
            this.gameObject.SetActive(false);
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
