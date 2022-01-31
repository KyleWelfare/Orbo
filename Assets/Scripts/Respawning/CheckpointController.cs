using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public static CheckpointController instance;

    private Checkpoint[] checkpoints;

    public Vector3 spawnPoint;

    private Player player;

    private void Awake()
    {
        instance = this;
        player = FindObjectOfType<Player>();
        checkpoints = FindObjectsOfType<Checkpoint>();
    }

    void Start()
    {
        spawnPoint = player.gameObject.transform.position;
    }


    void Update()
    {

    }

    public void DeactivateCheckpoints()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i].ResetCheckpoint();
        }
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }
}
