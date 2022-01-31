using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbCheckpointController : MonoBehaviour
{
    public static OrbCheckpointController instance;

    private OrbCheckpoint[] orbCheckpoints;

    public Vector3 orbSpawnPoint;

    private void Awake()
    {
        instance = this;
        orbCheckpoints = FindObjectsOfType<OrbCheckpoint>();
    }

    void Start()
    {   
        orbSpawnPoint = CheckpointController.instance.spawnPoint;
    }

    void Update()
    {

    }

    public void DeactivateOrbCheckpoints()
    {
        for (int i = 0; i < orbCheckpoints.Length; i++)
        {
            orbCheckpoints[i].ResetOrbCheckpoint();
        }
    }

    public void SetOrbSpawnPoint(Vector3 newOrbSpawnPoint)
    {
        orbSpawnPoint = newOrbSpawnPoint;
    }
}
