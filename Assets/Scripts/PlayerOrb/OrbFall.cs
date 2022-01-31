using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbFall : MonoBehaviour
{
    public OrbShoot orbShoot;
    public 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "orbProjectile" && OrbController.instance.shotComplete == true)
        {
            orbShoot.orbRB.velocity = new Vector2(orbShoot.orbRB.velocity.x, -1.0f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "orbProjectile" && OrbController.instance.shotComplete == true)
        {
            orbShoot.orbRB.velocity = Vector2.zero;
        }
    }


    //&& OrbShoot.instance.orbRB.velocity == Vector2.zero
}
