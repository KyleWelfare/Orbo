using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    
    
   
    void Start()
    {
     
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "orbProjectile")
        {
            RespawnManager.instance.RespawnTarget(this);
        }
    }


}
