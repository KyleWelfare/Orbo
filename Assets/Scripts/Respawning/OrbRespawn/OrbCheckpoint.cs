using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbCheckpoint : MonoBehaviour
{
    private SpriteRenderer SR;

    [SerializeField] private Sprite orbCpOn, orbCpOff;

    private void Awake()
    {
        SR = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OrbCheckpointController.instance.DeactivateOrbCheckpoints();
            SR.sprite = orbCpOn;
            OrbCheckpointController.instance.SetOrbSpawnPoint(transform.position);
        }
    }

    public void ResetOrbCheckpoint()
    {
        SR.sprite = orbCpOff;
    }
}
