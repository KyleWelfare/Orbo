using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OrbController : MonoBehaviour
{
    public static OrbController instance;

    public OrbShoot orbShoot;
    //private PlayerInputAction playerInputs;
    private PlayerInputHandler playerInputHandler;
    private SpriteRenderer playerOrbSR;
    private Player player;
    public Vector2 orbDir;

    

    //shooting
    public float shootCounter;
    [SerializeField] private float shootCooldown;
    //private bool canShoot;
    [SerializeField] private float orbVelocityFromPlayerX;
    [SerializeField] private float orbVelocityFromPlayerY;
    [SerializeField] public float orbSpeedX;
    [SerializeField] public float orbSpeedY;
    [SerializeField] private float orbStopTime;
    public bool shotComplete;

    //orb in ground check
    [SerializeField] private bool orbInGround;


    private void Awake()
    {
        instance = this;
        //playerInputs = new PlayerInputAction();
        player = GetComponentInParent<Player>();
        playerOrbSR = GetComponent<SpriteRenderer>();
        playerInputHandler = GetComponentInParent<PlayerInputHandler>();
    }
    void OnEnable()
    {
        //playerInputs.Enable();
        shootCounter = 0.00001f;
    }

    void OnDisable()
    {
        //playerInputs.Disable();
        if (orbShoot.gameObject != null)
        {
            orbShoot.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        shotComplete = true;
        
        //canShoot = true;
    }

    void Update()
    {
        //orbDir = playerInputs.Gameplay.Shoot.ReadValue<Vector2>().normalized;
       
        if (shootCounter > 0)
        {
            shootCounter -= Time.deltaTime;
            if (shootCounter <= 0)
            {
                orbShoot.gameObject.SetActive(false);
                playerOrbSR.enabled = true;
            }
        }

        if (player.StateMachine.CurrentState != player.DashState && shootCounter <= 0)
        {
            playerOrbSR.enabled = true;
        }
        else
        {
            playerOrbSR.enabled = false;
        }

    }

    private void FixedUpdate()
    {
        //if (shootCounter <= 0 && orbDir != new Vector2(0, 0))
        if (playerInputHandler.FireInput && shootCounter <= 0 && player.StateMachine.CurrentState != player.DashState)
        {
            OrbShootAction();
        }

        //if (player.orbInRange && orbRB.velocity == Vector2.zero)
        if (player.orbInRange && shotComplete == true)
        {
            shootCounter = 0.000001f;
            orbShoot.collider.radius = 0.01f;
        }
    }

    public void OrbShootAction()
    {
        shotComplete = false;
        //player can shoot again after cooldown has reached 0
        if (orbInGround) //move orb projectile spawn down to get out of ceiling
        {
            orbShoot.transform.position = new Vector2(transform.position.x, transform.position.y - 0.7f);
        }
        else
        {
            orbShoot.transform.position = new Vector2(transform.position.x, transform.position.y);
        }
        orbShoot.gameObject.SetActive(true);
        playerOrbSR.enabled = false;

        //if (orbDir.y < 0.15f && orbDir.y > -0.15f)
        //{
        //    orbDir.y = 0.0f;
        //}
        //if (orbDir.x < 0.15f && orbDir.x > -0.15f)
        //{
        //    orbDir.x = 0.0f;
        //}

        //lock aim to axis if within 0.15
        if (playerInputHandler.OrbDirection.y < 0.15f && playerInputHandler.OrbDirection.y > -0.15f)
        {
            playerInputHandler.OrbDirection.y = 0.0f;
        }
        if (playerInputHandler.OrbDirection.x < 0.15f && playerInputHandler.OrbDirection.x > -0.15f)
        {
            playerInputHandler.OrbDirection.x = 0.0f;
        }

        if (playerInputHandler.OrbDirection == Vector2.zero && player.FacingDirection == 1)
        {
            playerInputHandler.OrbDirection = new Vector2(1, 0);
        }
        else if (playerInputHandler.OrbDirection == Vector2.zero && player.FacingDirection == -1)
        {
            playerInputHandler.OrbDirection = new Vector2(-1, 0);
        }
        
        //invert shot direction if on wall
        if (player.StateMachine.CurrentState == player.WallSlideState)
        {
            playerInputHandler.OrbDirection = -playerInputHandler.OrbDirection;
        }

        //shooting down while falling will result in greater shot force so character doesn't pass orb
        //if (orbDir.x > -0.8f && orbDir.x < 0.8f  && orbDir.y < 0 && player.CurrentVelocity.y < -1f)
            if (playerInputHandler.OrbDirection.x > -0.8f && playerInputHandler.OrbDirection.x < 0.8f && playerInputHandler.OrbDirection.y < 0 && player.CurrentVelocity.y < -1f)
        {
            //orbShoot.orbRB.velocity = new Vector2(orbDir.x * orbSpeedX, orbDir.y * orbSpeedY + (player.CurrentVelocity.y * orbVelocityFromPlayerY)); //old controls
            //orbShoot.orbRB.velocity = new Vector2(playerInputHandler.OrbDirection.x * orbSpeedX, playerInputHandler.OrbDirection.y * orbSpeedY + (player.CurrentVelocity.y * orbVelocityFromPlayerY)); //adds player velocity
            orbShoot.orbRB.velocity = new Vector2(playerInputHandler.OrbDirection.x * orbSpeedX, playerInputHandler.OrbDirection.y * orbSpeedY * 2.5f);
        }
        //normal shot
        else {
            //orbShoot.orbRB.velocity = new Vector2(orbDir.x * orbSpeedX, orbDir.y * orbSpeedY); //old controls
            orbShoot.orbRB.velocity = new Vector2(playerInputHandler.OrbDirection.x * orbSpeedX, playerInputHandler.OrbDirection.y * orbSpeedY);
        }
        StartCoroutine(StopOrbMovement());
        shootCounter = shootCooldown;
        //orbRB.MovePosition(new Vector2(orbRB.position.x + orbDir.x * Time.deltaTime, orbRB.position.y + orbDir.y * Time.deltaTime));
        //orbRB.AddForce(orbDir * orbSpeed, ForceMode2D.Impulse);

        //canShoot = false;
        //StartCoroutine(OrbShootCooldown());


    }
    private IEnumerator StopOrbMovement()
    {
        orbShoot.collider.radius = 0.01f; //not a good fix. Without this, if orb is shot while facing into wall, it collides immediately and reflects into wall
        yield return new WaitForSeconds(0.07f); 
        orbShoot.collider.radius = 0.3f;

        yield return new WaitForSeconds(orbStopTime);
        orbShoot.orbRB.velocity = Vector2.zero;
        shotComplete = true;     
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            orbInGround = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            orbInGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            orbInGround = false;
        }
    }

    //public IEnumerator OrbShootCooldown() //couritune occurs in a separate timeline to main unity process
    //{
    //    yield return new WaitForSeconds(shootCooldown);
    //    orbShoot.gameObject.SetActive(false);
    //    playerOrbSR.enabled = !playerOrbSR.enabled;
    //    canShoot = true;
    //}

    //private IEnumerator DisableOrbCollider()
    //{
    //    if (orbDir.y < 0 && (orbDir.x > -0.2 || orbDir.x < 0.2))
    //    {
    //        OrbShoot.instance.collider.enabled = false;
    //        yield return new WaitForSeconds(0.1f);
    //        OrbShoot.instance.collider.enabled = true;
    //    }
    //}



    //private void GetOrbDir()
    //{
    //    orbDir = PlayerInputAction.Land.Attack.ReadValue<Vector2>().normalized;
    //}


}
