using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultivos : MonoBehaviour
{
    [SerializeField] ContadoresPlayer contadoresPlayer;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tongue"))
        {
            if(CompareTag ("Tomates"))
            {
                contadoresPlayer.RCultivo(ContadoresPlayer.TipoCultivo.Tomate);
                Debug.Log("Tomate Detectado");
            }
            if (CompareTag ("Maiz"))
            {
                contadoresPlayer.RCultivo(ContadoresPlayer.TipoCultivo.Maiz);
               Debug.Log("Maiz Detectado");
            }
            if (CompareTag("Pimientos")) 
            {
                Debug.Log("Pimientos Detectados");
                contadoresPlayer.RCultivo(ContadoresPlayer.TipoCultivo.Pimiento);
            }
            

        }

    }
   
}
