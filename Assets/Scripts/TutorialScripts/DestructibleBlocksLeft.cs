using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBlocksLeft : MonoBehaviour, IDamageable
{
    OrbShoot orbShoot;

    public void Damage(int damage)
    {
        orbShoot = FindObjectOfType<OrbShoot>();
        if (orbShoot.transform.position.x < gameObject.transform.position.x) {
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
