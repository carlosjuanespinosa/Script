using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDisparo : MonoBehaviour
{
    [SerializeField] private ContadoresPlayer contadoresplayer;
    [SerializeField] private GameObject Tomate;
    [SerializeField] private GameObject Maiz;
    [SerializeField] private GameObject Pimiento;
    [SerializeField] private Animator animator;
    
    private Ataque coger;
    

    private void Start()
    {
        
      

    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Disparo();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            CuerpoaCuerpo();
           
        }
    }
    private void Disparo ()
    {
        if(contadoresplayer.Cultivos > 0)
        {
            if (contadoresplayer.Pimientos > 0)
            {
                contadoresplayer.Disparar();
                GameObject objectInstance = Instantiate(Pimiento, transform.position, transform.rotation);
                Destroy(objectInstance, 2f);
                contadoresplayer.SumarCultivos();
                
            }
            else if (contadoresplayer.Maiz > 0)
            {
                contadoresplayer.Disparar();
                GameObject objectInstance = Instantiate(Maiz, transform.position, transform.rotation);
                Destroy(objectInstance, 2f);
                contadoresplayer.SumarCultivos();
              
            }
            else if (contadoresplayer.Tomates > 0)
            {
                contadoresplayer.Disparar();
                GameObject objectInstance = Instantiate(Tomate, transform.position, transform.rotation);
                Destroy(objectInstance, 2f);
                contadoresplayer.SumarCultivos();
               
            }
        }
             
    }
    private void CuerpoaCuerpo()
    {
        animator.SetTrigger("Tongue");
       
    }
  

}
