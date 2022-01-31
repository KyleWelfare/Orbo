using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbShoot : MonoBehaviour
{
    public Rigidbody2D orbRB;

    public new CircleCollider2D collider;

    [SerializeField] private int orbDamage;
   
    private void Awake()
    {
     
    }
    
    void Start()
    {
        orbRB = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();       
    }

    void Update()
    {

    }
   
    void FixedUpdate()
    {
        
    }

    //private void OnCollisionEnter2D(Collision2D other)
    //{
        
    //    if (other.gameObject.tag == "Ground")
    //    {
    //        ContactPoint2D hit = other.GetContact(0);
    //        orbRB.velocity = Vector2.Reflect(orbRB.velocity * 0.5f, hit.normal);
    //    }
    //}

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    //StartCoroutine(OrbBounceWait(collision));
    //    if (collision.gameObject.tag == "Ground")
    //    {
    //        ContactPoint2D hit = collision.GetContact(0);
    //        orbRB.velocity = Vector2.Reflect(orbRB.velocity * 0.5f, hit.normal);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null && orbRB.velocity != Vector2.zero)
        {
            damageable.Damage(orbDamage);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null && orbRB.velocity != Vector2.zero)
        {
            damageable.Damage(orbDamage);
        }
    }

    //private IEnumerator OrbBounceWait(Collision2D collision)
    //{
    //    yield return new WaitForSeconds(0.1f);
      
    //}

}

