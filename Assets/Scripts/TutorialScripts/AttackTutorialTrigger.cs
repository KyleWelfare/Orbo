using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTutorialTrigger : MonoBehaviour
{
    [SerializeField] private GameObject destructibleBlock;
    [SerializeField] private Coin coin;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            UIController.instance.EnableAttackTutorial();

            if (coin.isCollected == false)
            {
                coin.gameObject.SetActive(true);
            }

        }
    }

    private void Update()
    {
        if (destructibleBlock.activeInHierarchy == false)
        {
            UIController.instance.DisableAttackTutorial();
        }
    }
}
