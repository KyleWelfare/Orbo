using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCorpse : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DeathOrb")
        {
            RespawnManager.instance.RespawnAtCorpse();
        }
    }
}
