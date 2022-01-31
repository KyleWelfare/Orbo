using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOrbController : MonoBehaviour
{
    private Rigidbody2D RB;
    private DeathOrbInputHandler inputHandler;
    private SpriteRenderer SR;

    private bool dashInput;
    private Vector2 movementInput;

    [SerializeField] private float deathOrbVelocity;
    [SerializeField] private float deathOrbDashVelocity;

    private float dashCounter;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;

    void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        inputHandler = GetComponent<DeathOrbInputHandler>();
        SR = GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        inputHandler.enabled = true;
        inputHandler.UseDashInput();
        dashCounter = dashDuration;
    }
    void OnDisable()
    {
        inputHandler.enabled = false;
    }
    private void Start()
    {
        inputHandler.enabled = true;
    }
    void Update()
    {
        dashInput = inputHandler.DashInput;
        if (dashInput == false)
        {
            movementInput = inputHandler.movementInput;
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (dashInput && dashCounter <= 0)
        {
            StartCoroutine(DashCo());
        }

        if (dashInput == false)
        {
            RB.velocity = new Vector2(movementInput.x * deathOrbVelocity, movementInput.y * deathOrbVelocity);
        }
    }


    private IEnumerator DashCo()
    {
        RB.velocity = new Vector2(movementInput.x * deathOrbDashVelocity, movementInput.y * deathOrbDashVelocity);
        SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, 0.5f);
        yield return new WaitForSeconds(dashDuration);
        SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, 1f);
        inputHandler.UseDashInput();
        dashCounter = dashCooldown;
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && dashInput == false)
        {
            gameObject.SetActive(false);
            RespawnManager.instance.RespawnPlayer();
        }
        else if (collision.tag == "Enemy" && dashInput == true)
        {
            if (RespawnManager.instance.deathOrbDashHP < PlayerHealthController.instance.maxHealth - 1)
            {
                RespawnManager.instance.deathOrbDashHP++;
            }
        }

        if (collision.tag == "PlayerCorpse")
        {
            RespawnManager.instance.RespawnAtCorpse();
        }
    }
}
