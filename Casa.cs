using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Casa : MonoBehaviour
{
    [SerializeField] private ContadorCasa contadorCasa;
    [SerializeField] private ContadoresPlayer contadoresPlayer;
    // Start is called before the first frame update
   public void TTomates(float amount)
    {
        contadorCasa.ITomates(amount);
    }
    public void TMaiz(float amount)
    {
        contadorCasa.IMaiz(amount);
    }
    public void TPimientos(float amount)
    {
        contadorCasa.IPimientos(amount);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out ContadorPlayer player))
        {

            if (contadoresPlayer.Cultivos >= 0)
            {
                if (contadoresPlayer.Tomates > 0)
                {                
                    player.DTomates(1);
                    TTomates(1);
                    
                }
                if(contadoresPlayer.Maiz > 0)
                {                
                    player.DMaiz(1);
                    TMaiz(1);
                    
                }
                if (contadoresPlayer.Pimientos > 0)
                {               
                    player.DPimientos(1);
                    TPimientos(1);
                    
                }                         
            }else
            {
                return;
            }
        }
    }
}
