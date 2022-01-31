using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    //singleton
    public static PlayerHealthController instance;

    //components
    private SpriteRenderer playerSR;
    private Player player;

    //player health
    public int maxHealth, currentHealth;
    public bool tookDamage;

    //invincibility
    private float invincibleCounter = 0;
    [SerializeField] private float invincibleDuration;

    private void Awake()
    {
        instance = this;
        playerSR = GetComponent<SpriteRenderer>();
        player = GetComponent<Player>();
    }

    void Start()
    {    
        currentHealth = maxHealth;    
    }

    void Update()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;
            if (invincibleCounter <= 0)
            {
                playerSR.color = new Color(playerSR.color.r, playerSR.color.g, playerSR.color.b, 1);
            }
        }
    }

    public void DealDamageToPlayer(int damage)
    {
        if (invincibleCounter <= 0 && player.StateMachine.CurrentState != player.DashState) 
        {   
            //AudioManager.instance.playSFX(0);
            currentHealth -= damage;

            if (currentHealth <= 0) //death
            {
                gameObject.SetActive(false); 
                currentHealth = 0;
                //RespawnManager.instance.DeathOrbSpawn();
                TutorialRespawnManager.instance.TutorialRespawn();
    
            }
            else
            {
                invincibleCounter = invincibleDuration; //when damage is taken, begin invincibility
                playerSR.color = new Color(playerSR.color.r, playerSR.color.g, playerSR.color.b, 0.5f);
                tookDamage = true;
            }
            UIController.instance.UpdateHealthDisplay();
        }
    }
}
