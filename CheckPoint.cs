using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private ContadoresPlayer contadoresPlayer;
    Vector3 checkpoint;
    Vector3 inicio;
    // Start is called before the first frame update
    void Start()
    {
        inicio = transform.position;
        checkpoint = transform.position;
        Debug.Log(transform.position);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (contadoresPlayer.health <= 0)
        {
            Debug.Log("contador 0");
            transform.position = checkpoint;
            contadoresPlayer.Reset();
        }
    }
}
