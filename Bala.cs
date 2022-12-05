using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    
    private Vector3 direccio;
    private SMEnemigo smEnemigo;
    
    

    // Start is called before the first frame update
    void Start()
    {
  
        direccio = transform.forward;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      
        transform.position += direccio * speed * Time.deltaTime;
        transform.Translate(direccio * speed * Time.deltaTime, Space.World);
    }

   /* private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy1"))
        {
            collision.gameObject.GetComponent<SMEnemigo>().Paraliz();
        }
        
        
    }*/

}
