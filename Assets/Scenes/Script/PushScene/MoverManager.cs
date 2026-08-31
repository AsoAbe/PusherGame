using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverManager : MonoBehaviour 
{
    void OnTriggerEnter(Collider collider)
    {
       if(collider.gameObject.tag=="Coin")
        {
            collider.gameObject.transform.parent = this.transform;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Coin")
        {
            collider.gameObject.transform.parent = null;
        }
    }
}
