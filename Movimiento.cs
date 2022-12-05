using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [SerializeField] private CharacterController cc;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float gravityMultiplier = 2f;
    [SerializeField] private int maxJumps = 2;
    

    private Vector3 moveDirection;
    private Vector3 jumpVelocity;
    private float gravityApplied;
    private int remainingJumps;
    // Start is called before the first frame update
    void Start()
    {
        gravityApplied = Physics.gravity.y * gravityMultiplier;
        remainingJumps = maxJumps;
        

    }

    public void SetMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    public void Jump()
    {
        if (cc.isGrounded || (!cc.isGrounded && remainingJumps > 0))
        {
            jumpVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityApplied);
            remainingJumps--;
        }
        
    }
    private void Update()
    {
        Move();
        ApplyGravity();
    }
    private void Move()
    {
        Vector3 movement = transform.forward * moveDirection.z + transform.right * moveDirection.x;
        //ClampMagnitude arregla el problema de velocidad en diagonales.
        movement = Vector3.ClampMagnitude(movement, 1f);

        cc.Move(movement * speed * Time.deltaTime);
       
    }
    private void ApplyGravity()
    {
        cc.Move(jumpVelocity * Time.deltaTime);
        if (cc.isGrounded && jumpVelocity.y < 0f)
        {
            remainingJumps = maxJumps;
            jumpVelocity.y = -2f;
        }
        else
        {
            jumpVelocity.y += gravityApplied * Time.deltaTime;
        }
    }

    
}
