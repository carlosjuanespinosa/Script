using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{

    private Movimiento movenementController;
    private CamaraVision lookController;
    [SerializeField] private Animator animator;


    // Start is called before the first frame update
    private void Start()
    {
        AudioManager.i.PlaySound(SoundName.Croar, gameObject.transform.position);
        movenementController = GetComponent<Movimiento>();
        lookController = GetComponentInChildren<CamaraVision>();
      
    }




    // Update is called once per frame

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            movenementController.Jump();
            animator.SetTrigger("Jump");
        }
        
        
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");

          if ((zMovement==0) && (xMovement==0) )
          {
              animator.SetTrigger("Idle");
          }
          else
          {
            animator.SetTrigger("Crawl");
          }
        
        Move(new Vector3(xMovement, 0, zMovement));
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
       
        Look(new Vector2(mouseX, mouseY));

       

    }
    private void Move(Vector3 direction)
    {
        movenementController.SetMoveDirection(direction);
       
        
        
    }
    public void Look(Vector2 mouseVector)
    {

        lookController.SetLookVector(mouseVector);
    }
  
}
