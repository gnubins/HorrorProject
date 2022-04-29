    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{

    [SerializeField] List<GameObject> wayPoints;

    int currPointIndex;

    #region
    [Header("Detection")]
    [SerializeField] float radius;
    [SerializeField,Range(0,360)] float angle;

    public LayerMask mask;

    public Transform _target;
    public bool targetIsInSight { get; private set; }
    
    bool playerWasInSight;

    NavMeshAgent agent;
    //Rigidbody rb;
    Ray ray;

    Vector3 lastSeenPos;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        currPointIndex = 0;

        agent.SetDestination(wayPoints[currPointIndex].transform.position);
        playerWasInSight = false;
        //agent.SetDestination(Target.position);
    }

    // Update is called once per frame
    void Update()
    {
        CheckForTarget();

        if (targetIsInSight)
        {
            agent.SetDestination(_target.position);
            lastSeenPos = agent.destination;
            playerWasInSight = true;
        }
        else
        {
            if (playerWasInSight)
            {
                agent.SetDestination(lastSeenPos);
                //search method
                Invoke("forget", 5f);
            }
            else if (Vector3.Distance(transform.position, wayPoints[currPointIndex].transform.position) >= 2f)
            {
                agent.SetDestination(wayPoints[currPointIndex].transform.position);
            }

            if (Vector3.Distance(transform.position, wayPoints[currPointIndex].transform.position) <= 2f)
            {
                Iterate();
            }
        }
    }

    //methods
    #region

    void CheckForTarget()
    {
        float view = Vector3.Angle(transform.forward, Vector3.Normalize(_target.position - transform.position));

        if (view < angle / 2)
        {
            if (Physics.Raycast(transform.position, _target.position - transform.position, out RaycastHit hit, mask))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    targetIsInSight = true;
                }
                else targetIsInSight = false;
            }
        }
        else targetIsInSight = false;

    }

    void Iterate()
    {
        if (currPointIndex < wayPoints.Count-1)
        {
            currPointIndex++;
        }
        else
        {
            currPointIndex = 0;
        }

        agent.SetDestination(wayPoints[currPointIndex].transform.position);
    }


    void RunAwayFrom(Vector3 position)
    {

    }

    void forget()
    {
        playerWasInSight = false;
    }

    Vector3 DirFromAngle(float Angle,bool isGlobal)
    {
        if (!isGlobal)
        {
            Angle += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(Angle * Mathf.Deg2Rad), 0, Mathf.Cos(Angle * Mathf.Deg2Rad));
    }
    #endregion

    private void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.DrawRay(transform.position, transform.forward * radius);

        Gizmos.DrawRay(transform.position, DirFromAngle(angle / 2, false) * radius);
        Gizmos.DrawRay(transform.position, DirFromAngle(-angle / 2, false) * radius);

        Gizmos.color = targetIsInSight ? Color.red : Color.white;
        Gizmos.DrawWireSphere(lastSeenPos, .5f);
        Gizmos.DrawRay(transform.position, _target.position - transform.position);
    }

}