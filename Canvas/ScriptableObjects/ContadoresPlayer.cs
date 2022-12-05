
using System;
using UnityEngine;

[CreateAssetMenu(fileName ="ContadoresPlayer", menuName ="ScriptableObjects/ContadoresPlayer")]

public class ContadoresPlayer : ScriptableObject
{
    public float maxHealth = 20;
    
    public float health { get; private set; }
    public float Tomates { get; private set; }
    public float Maiz { get; private set; }
    public float Pimientos { get; private set; }

    public float Cultivos=0;
    public event Action Changed;


    public enum TipoCultivo
    {
        Tomate,
        Maiz,
        Pimiento
    }

    private void OnEnable()
    {
        health = maxHealth;
        Tomates = 0;
        Maiz = 0;
        Pimientos = 0;
        Cultivos = 0;
       

        
    }
    public void DamagePlayer(float amount)
    {
        health -= amount;
        Changed?.Invoke();
    }

    public void SumarCultivos()
    {
        Cultivos = (Tomates + Maiz + Pimientos); 
        Changed?.Invoke();
       
       
        
    }

    public void RCultivo(TipoCultivo tipoCultivo)
    {
        if (Cultivos >= 5)
            return;

        switch (tipoCultivo)
        {
            case TipoCultivo.Tomate:
                Tomates ++;
                
                break;
            case TipoCultivo.Maiz:
                Maiz++;
                
                break;
            case TipoCultivo.Pimiento:
                Pimientos++;
                
                break;
        }

        SumarCultivos();
       
    }
    
    public void Disparar()
    {
        if (Pimientos > 0)
        {
            Pimientos--;
        }
        else
        {
            if(Maiz > 0)
            {
                Maiz--;
            }
            else
            {
                Tomates--;
            }
        }
       
        Changed?.Invoke();
    }
    public void Reset()
    {
        health = maxHealth;
        
        Tomates = 0;
        Maiz = 0;
        Pimientos = 0;
        SumarCultivos();
        
    }
    public void DTomates(float amount)
    {
        Tomates -= amount;
        SumarCultivos();
        
        
    }
    public void DMaiz(float amount)
    {
        Maiz -= amount;
        
        SumarCultivos();
    }
    public void DPimientos(float amount)
    {
        Pimientos -= amount;
       
        SumarCultivos();
    }
    
}