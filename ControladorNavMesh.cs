
using UnityEngine;
using UnityEngine.AI;

public class ControladorNavMesh : MonoBehaviour
{
    [SerializeField] private string targetName;
    [SerializeField] private LayerMask groundLayer;

    private GameObject target;
    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;

    private bool checkeable = false;


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.Find(targetName);
        rb = GetComponent<Rigidbody>();
    }

    private void Move(Vector3 position)
    {
        navMeshAgent.SetDestination(position);
    }

    // Update is called once per frame
    void Update()
    {
       if (target == null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray,out RaycastHit hit))
                {
                    Move(hit.point);
                }
            }
        }
        else
        {
            if(rb.isKinematic && navMeshAgent.enabled)
               Move(target.transform.position);
           /* if(rb != null && Input.GetKeyDown(KeyCode.Space))
            {
                navMeshAgent.enabled = false;
                rb.isKinematic = false;
               
            }*/
        }
        RBCheck();
    }

    private void SetCheckeable()
    {
        checkeable = true;
    }

    private void RBCheck()
    {
        if (checkeable &&!rb.isKinematic && rb.velocity.magnitude < 0.1 && IsGrounded())
        {

        }
    }

    private bool IsGrounded()
    {
        Debug.DrawRay(transform.position + Vector3.up * 0.2f, Vector3.down * 0.8f, Color.red);

        return Physics.Raycast(transform.position + Vector3.up * 0.2f, Vector3.down * 0.8f, groundLayer);
    }
   
}
