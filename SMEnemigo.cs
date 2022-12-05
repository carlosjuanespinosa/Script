using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SMEnemigo : MonoBehaviour
{
    [SerializeField] private float patrolMaxRange =15;
    [SerializeField] private float patrolMinRange = 0;
    [SerializeField] private float detectionRange = 15;
    [SerializeField] private float atackRange = 1.8F;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject Hocico;
    
    [SerializeField] private Transform prepoint;
    [SerializeField] private Transform point;
    [SerializeField] private Transform visorPoint;

    private enum State
    {
        Patrol,
        chaseTarget,
        AtackTarget,
        ToInitialPosition,
        Paralizado
    }

    private State state;

    private GameObject target;
    private NavMeshAgent agent;
    private Vector3 initialPosition;
    private Vector3 patrolPosition;
    
    
    private float nextTimeToAtack = 5;
    private float atackRate = 2;
   
    private float timeParaliz = 10;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
       // line = GetComponentInChildren<LineRenderer>();
    }
    // Start is called before the first frame update

    private void Start()
    {
        state = State.Patrol;
        target = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position;
        patrolPosition = GetPatrolPosition();
    }
    private Vector3 GetPatrolPosition()
    {
        Vector3 pointToPatrol = initialPosition;
        float randomRange = Random.Range(patrolMinRange, patrolMaxRange);
        Vector3 proposedPoint = initialPosition + Random.insideUnitSphere * randomRange;

        prepoint.position = proposedPoint;

        if (NavMesh.SamplePosition(proposedPoint, out NavMeshHit navMeshHit, 5f, NavMesh.AllAreas))
        {
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(navMeshHit.position, path);
            bool canReachPoint = path.status == NavMeshPathStatus.PathComplete;

            if (canReachPoint)
                pointToPatrol = navMeshHit.position;
        }
        point.position = pointToPatrol;
        return pointToPatrol;
    }
    // Update is called once per frame
    void Update()
    { //Completar Switch ******
        switch (state)
        {
            default:
            case State.Patrol:
                /*Detection(false);*/
                agent.SetDestination(patrolPosition);
                if (agent.remainingDistance < 1f)
                    patrolPosition = GetPatrolPosition();

                FindTarget();
                break;

            case State.chaseTarget:
               // Detection(true);
                agent.SetDestination(target.transform.position);
                if (Vector3.Distance(transform.position, target.transform.position)<= atackRange)
                {
                    state = State.AtackTarget;
                }

                if (Vector3.Distance(transform.position, target.transform.position) > detectionRange)
                {
                    state = State.ToInitialPosition;
                }

                break;

            case State.AtackTarget:
                agent.isStopped = true;
               // Detection(true);
                LookTarget();
                if(Vector3.Distance(transform.position, target.transform.position) <= atackRange)
                {
                    AtackTimer();
                }
                 if(Vector3.Distance(transform.position, target.transform.position) > atackRange)
                {
                    agent.isStopped = false;
                    state = State.chaseTarget;
                }
                break;

            case State.ToInitialPosition:
              //  Detection(false);
                agent.SetDestination(initialPosition);
                if (agent.remainingDistance < 1f)
                    state = State.Patrol;
                break;

            case State.Paralizado:
                agent.isStopped = true;
                if(Time.time > timeParaliz)
                {
                    agent.isStopped = false;
                    state = State.Patrol;
                }
                break;
        }
    }
    private void FindTarget()
    {
        
        if (target == null)
           
            return;
        if (Vector3.Distance(transform.position, target.transform.position) <= detectionRange)
        {
            
            state = State.chaseTarget;
        }
            
    }

 

    private void LookTarget()
    {
        Vector3 lookDirection = target.transform.position - transform.position;
        lookDirection.y = 0;

        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 90 * Time.deltaTime);
    }


    private void AtackTimer()
    {
        if (Time.time > nextTimeToAtack)
        {
            PerformAtack();
            nextTimeToAtack = Time.time + 5 / atackRate;
        }
    }
   public void Paraliz()
    {
        timeParaliz = Time.time + 10;
        state = State.Paralizado;
        Debug.Log("Puto paralizado");
            }
    private void PerformAtack()
    {
        GameObject mordisco = Instantiate(Hocico, visorPoint.position, visorPoint.rotation);
        Destroy(mordisco, 1f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.DrawWireSphere(transform.position, atackRange);
    }
}
