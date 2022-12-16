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
                
            }
            if (CompareTag ("Maiz"))
            {
                contadoresPlayer.RCultivo(ContadoresPlayer.TipoCultivo.Maiz);
              
            }
            if (CompareTag("Pimientos")) 
            {
                
                contadoresPlayer.RCultivo(ContadoresPlayer.TipoCultivo.Pimiento);
            }
            

        }

    }
   
}
