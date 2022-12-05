using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque : MonoBehaviour
{
    
  
   public void Lengua()
    {
        GameObject tongue = GameObject.FindGameObjectWithTag("Tongue");

        if (tongue is not null)
        {

            if (tongue.TryGetComponent(out Collider tongueCollider))
            {
                if (tongueCollider.enabled)
                {
                    tongueCollider.enabled = false;
                    
                }
                else
                {
                    tongueCollider.enabled = true;
                    
                }
            }
        }

    }



}
