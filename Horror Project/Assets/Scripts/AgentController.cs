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

        //agent.SetDestination(wayPoints[currPointIndex].transform.position);
        playerWasInSight = false;

        StartCoroutine(Roam());
    }

    // Update is called once per frame
    void Update()
    {
        //CheckForTarget();

        //if (targetIsInSight)
        //{
        //    agent.SetDestination(_target.position);
        //    lastSeenPos = agent.destination;
        //    playerWasInSight = true;
        //}
        //else
        //{
        //    Invoke("forget", 5f);
        //    if (playerWasInSight)
        //    {
        //        agent.SetDestination(lastSeenPos);
        //    }
           
        //}

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

    void forget()
    {
        playerWasInSight = false;
    }

    Vector3 randomPoint;
    IEnumerator Roam()
    {
        while (true)
        {
            randomPoint = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)) + transform.position;
            agent.SetDestination(randomPoint);
            yield return new WaitForSeconds(3f);
        }
       
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
        Gizmos.DrawLine(transform.position, randomPoint);

        //Gizmos.DrawWireSphere(transform.position, radius);

        //Gizmos.DrawRay(transform.position, transform.forward * radius);

        //Gizmos.DrawRay(transform.position, DirFromAngle(angle / 2, false) * radius);
        //Gizmos.DrawRay(transform.position, DirFromAngle(-angle / 2, false) * radius);

        //Gizmos.color = targetIsInSight ? Color.red : Color.white;
        //Gizmos.DrawWireSphere(lastSeenPos, .5f);
        //Gizmos.DrawRay(transform.position, _target.position - transform.position);
    }

}
