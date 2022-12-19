using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SMEnemigo : MonoBehaviour
{
    [SerializeField] private float patrolMaxRange =15;
    [SerializeField] private float patrolMinRange = 0;
    [SerializeField] private float detectionRange = 15;
    [SerializeField] private float atackRange = 2F;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Animator animator;
    
    
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
    private GameObject save;
    
    
    private float nextTimeToAtack = 0;
    private float attackCooldown = 2;
   
    private float timeParaliz = 5;

    private bool isParalizado = false;

    private float timesound;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
       // line = GetComponentInChildren<LineRenderer>();
    }
    // Start is called before the first frame update

    private void Start()
    {
     
            timesound = Time.time + Random.Range(0f, 30f);
        
        state = State.Patrol;
        target = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position;
        patrolPosition = GetPatrolPosition();
        save = GameObject.FindGameObjectWithTag("Casa");
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
    {
      
        animator.SetFloat("Velocidad", agent.velocity.magnitude);
        animator.SetBool("IsParalizado", isParalizado = true);
        switch (state)
        {
            default:
            case State.Patrol:

                if (Time.time >= timesound)
                {
                    AudioManager.i.PlaySound(SoundName.LadridoPerro, gameObject.transform.position);
                    timesound = Time.time + Random.Range(0f, 30f);
                }

                agent.isStopped = false;
                agent.SetDestination(patrolPosition);
                if (agent.remainingDistance < 1f)
                    patrolPosition = GetPatrolPosition();
                
                FindTarget();
                break;

            case State.chaseTarget:
               // Detection(true);
                agent.SetDestination(target.transform.position);
                FindSave();
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
                    
                    AttackTimer();
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
                isParalizado = true;
                if (Time.time > timeParaliz)
                {
                   
                    state = State.Patrol;
                    isParalizado = false;
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

    private void FindSave()
    {
        if (save == null)
            return;
        if (Vector3.Distance(transform.position, save.transform.position) <= detectionRange)
        {

            state = State.ToInitialPosition;
        }
    }
 

    private void LookTarget()
    {
        Vector3 lookDirection = target.transform.position - transform.position;
        lookDirection.y = 0;

        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 90 * Time.deltaTime);
    }


    private void AttackTimer()
    {
        if (Time.time > nextTimeToAtack)
        {
            Debug.Log("mando Ataque");
            PerformAtack();
            nextTimeToAtack = Time.time + attackCooldown;
        }
    }
   public void Paraliz()
    {
        AudioManager.i.PlaySound(SoundName.GemidoPerro, gameObject.transform.position);

        animator.SetTrigger("Paralizado");
        timeParaliz = Time.time + 10;
        state = State.Paralizado;
       
            }
    private void PerformAtack()
    {
        animator.SetTrigger("Ataque");
        AudioManager.i.PlaySound(SoundName.AtaquePerro, gameObject.transform.position);
        Debug.Log("Triguer Ataque");
       

        

    }

    private void HerirJugador()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > atackRange)
            return;

        if (target.TryGetComponent(out ContadorPlayer player))
        {
            player.DamagePlayer(5);
        }
        
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.DrawWireSphere(transform.position, atackRange);
    }
}
