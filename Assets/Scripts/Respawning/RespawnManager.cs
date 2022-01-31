using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager instance;

    private Player player;
    private DeathOrbController deathOrb;
    [SerializeField] private GameObject playerCorpse;

    public int deathOrbDashHP;

    [SerializeField] private float waitToRespawn, waitToRespawnSpikeFall, targetRespawnTime;
    //[SerializeField] private TargetController targetCon;

    private void Awake()
    {
        instance = this;
        player = FindObjectOfType<Player>();
        deathOrb = FindObjectOfType<DeathOrbController>();
    }
    private void Start()
    {
        deathOrb.gameObject.SetActive(false);
    }

    public void DeathOrbSpawn()
    {
        StartCoroutine(DeathOrbSpawnCo());
    }
    public void RespawnAtCorpse()
    {
        //player.transform.position = playerCorpse.transform.position;

        deathOrb.gameObject.SetActive(false);
        playerCorpse.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
        
        CameraController.instance.target = player.gameObject.transform;

        PlayerHealthController.instance.currentHealth = 1 + deathOrbDashHP;
        UIController.instance.UpdateHealthDisplay();
        deathOrbDashHP = 0;
    }
     public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }

    public void RespawnSpikeFall()
    {
            StartCoroutine(RespawnCoSpikeFall());
    }

    public void RespawnTarget(TargetController target)
    {
        StartCoroutine(RespawnTargetCo(target));
    }

    private IEnumerator RespawnCo() //respawn upon death
    {
        deathOrb.gameObject.SetActive(false);
        yield return new WaitForSeconds(waitToRespawn - 1f / UIController.instance.fadeSpeed);
        UIController.instance.FadeToBlack();

        playerCorpse.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f / UIController.instance.fadeSpeed + 1f);
        UIController.instance.FadeFromBlack();

        player.gameObject.SetActive(true);
        CameraController.instance.target = player.gameObject.transform;

        player.transform.position = CheckpointController.instance.spawnPoint;
        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
        UIController.instance.UpdateHealthDisplay();
        deathOrbDashHP = 0;
    }

    private IEnumerator RespawnCoSpikeFall() //respawn at last jump point -- needs to be tweaked for fall instead of jump
    {
        player.gameObject.SetActive(false);
        UIController.instance.UpdateHealthDisplay();

        yield return new WaitForSeconds(waitToRespawnSpikeFall - 1f / UIController.instance.fadeSpeed);
        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds(1f / UIController.instance.fadeSpeed + 1f);
        UIController.instance.FadeFromBlack();

        //player.transform.position = player.lastJumpLocation;
        player.gameObject.SetActive(true);
    }

    public IEnumerator RespawnTargetCo(TargetController target)
    {
        target.gameObject.SetActive(false);
        yield return new WaitForSeconds(targetRespawnTime);
        target.gameObject.SetActive(true);
    }

    private IEnumerator DeathOrbSpawnCo()
    {
        //OrbController.instance.transform.position = new Vector2(transform.position.x, 0.6875f);
        player.gameObject.SetActive(false);
        playerCorpse.transform.position = player.transform.position;
        playerCorpse.gameObject.SetActive(true);
        //UIController.instance.UpdateHealthDisplay();

        yield return new WaitForSeconds(waitToRespawn - 1f / UIController.instance.fadeSpeed);
        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds(1f / UIController.instance.fadeSpeed + 1f);
        UIController.instance.FadeFromBlack();

        deathOrb.gameObject.SetActive(true);
        deathOrb.gameObject.transform.position = OrbCheckpointController.instance.orbSpawnPoint;
        CameraController.instance.target = deathOrb.gameObject.transform;       
    }
}
