
using System;
using UnityEngine;

public class ContadorPlayer : MonoBehaviour
{
    
    [SerializeField] private ContadoresPlayer contadoresPlayer;
    [SerializeField] private Casa casa;
public void DamagePlayer(float amount)
    {
        contadoresPlayer.DamagePlayer(amount);
    }

    public void DTomates (float amount)
    {
        contadoresPlayer.DTomates(amount);
    }
    public void DMaiz(float amount)
    {
        contadoresPlayer.DMaiz(amount);
    }
    public void DPimientos(float amount)
    {
        contadoresPlayer.DPimientos(amount);
    }
}
