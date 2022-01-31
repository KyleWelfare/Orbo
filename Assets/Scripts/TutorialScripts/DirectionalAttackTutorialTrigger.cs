using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalAttackTutorialTrigger : MonoBehaviour
{
    [SerializeField] private GameObject destructibleBlock;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            UIController.instance.EnableDirectionalAttackTutorial();
        }
    }

    private void Update()
    {
        if (destructibleBlock.activeInHierarchy == false)
        {
            UIController.instance.DisableDirectionalAttackTutorial();
        }
    }
}
