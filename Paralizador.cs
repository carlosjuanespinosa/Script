using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralizador : MonoBehaviour
{
    SMEnemigo enemigo;
   Collider col;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Tongue")|| other.CompareTag("Disparo"))
        {
            gameObject.GetComponent<SMEnemigo>().Paraliz();
        }
    }

}
