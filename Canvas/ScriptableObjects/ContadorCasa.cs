using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName ="ContadorCasa", menuName ="ScriptableObjects/ContadorCasa")]


public class ContadorCasa : ScriptableObject
{
    public float almacen = 0;
    public float Tomates { get; private set; }
    public float Maiz { get; private set; }
    public float Pimientos { get; private set; }
    

    public event Action Changed;

    private void OnEnable()
    {
        Tomates = 0;
        Maiz = 0;
        Pimientos = 0;
        almacen = 0;

    }
        public void ITomates(float amount)
    {
        Tomates += amount;

        SAlmacen();
    }
    public void IMaiz(float amount)
    {
        Maiz += amount;

        SAlmacen();
    }
    public void IPimientos(float amount)
    {
        Pimientos += amount;

        SAlmacen();
    }

    public void SAlmacen()
    {
        almacen = Tomates + Maiz + Pimientos;
        
        Changed?.Invoke();
        Ganar();
    }

    public void Ganar()
    {
        if(Tomates>=20 && Maiz >= 10 && Pimientos >= 5)
        {
            SceneManager.LoadScene("Win", LoadSceneMode.Single);
        }
    }
}
