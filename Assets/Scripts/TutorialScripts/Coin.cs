using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public bool isCollected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player" && isCollected == false)
        {
            isCollected = true;
            UIController.instance.UpdateCoinCount();
            gameObject.SetActive(false);
        }        
    }


}
