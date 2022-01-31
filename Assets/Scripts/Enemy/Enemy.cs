using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject deathEffect;
    [SerializeField] protected int maxHealth;
    protected int currentHealth;
    [SerializeField] protected int damageToPlayerOnContact = 1;
    public int projectileDamage;
    
    protected Rigidbody2D enemyRB;
    protected SpriteRenderer enemySR;
    protected Animator anim;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        enemyRB = GetComponent<Rigidbody2D>();
        enemySR = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;
    }
    public virtual void Damage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(EnemyDamageAnimation());

        if (currentHealth <= 0)
        {
            Instantiate(deathEffect, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.instance.DealDamageToPlayer(damageToPlayerOnContact);
        }
    }

    protected virtual IEnumerator EnemyDamageAnimation()
    {
        enemySR.color = new Color(enemySR.color.r, enemySR.color.g, enemySR.color.b, 0.5f);
        yield return new WaitForSeconds(0.3f);
        enemySR.color = new Color(enemySR.color.r, enemySR.color.g, enemySR.color.b, 1.0f);
    }
}
