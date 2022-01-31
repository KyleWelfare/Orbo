using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRespawnManager : MonoBehaviour
{
    public static TutorialRespawnManager instance;

    private Player player;
    [SerializeField] private float waitToRespawn, waitToRespawnSpikeFell;
    private Vector2 respawnPoint;
    [SerializeField] private GameObject coin;

    private void Awake()
    {
        instance = this;
        player = FindObjectOfType<Player>();
        respawnPoint = player.transform.position;
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TutorialRespawn()
    {
        StartCoroutine(TutorialRespawnCo());
    }

    public void TutorialRespawnSpikeFall()
    {
        StartCoroutine(TutorialRespawnCoSpikeFall());
    }

    private IEnumerator TutorialRespawnCo() //respawn upon death
    {
        if (coin.activeInHierarchy)
        {
            coin.SetActive(false);
        }
        yield return new WaitForSeconds(waitToRespawn - 1f / UIController.instance.fadeSpeed);
        UIController.instance.FadeToBlack();
        player.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f / UIController.instance.fadeSpeed + 1f);
        UIController.instance.FadeFromBlack();

        player.gameObject.SetActive(true);
        CameraController.instance.target = player.gameObject.transform;

        //player.transform.position = CheckpointController.instance.spawnPoint;
        player.transform.position = respawnPoint;
        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
        UIController.instance.UpdateHealthDisplay();
        
    }

    private IEnumerator TutorialRespawnCoSpikeFall() //respawn at last jump point -- needs to be tweaked for fall instead of jump
    {
        player.gameObject.SetActive(false);
        UIController.instance.UpdateHealthDisplay();

        yield return new WaitForSeconds(waitToRespawnSpikeFell - 1f / UIController.instance.fadeSpeed);
        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds(1f / UIController.instance.fadeSpeed + 1f);
        UIController.instance.FadeFromBlack();

        player.transform.position = player.lastJumpLocation;
        player.gameObject.SetActive(true);
        
    }
}
