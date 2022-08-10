using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{

    [SerializeField] Animator anim;
    [Header("Detection")]
    [SerializeField] float radius;
    [SerializeField,Range(0,360)] float angle;
    [SerializeField, Range(0, 20)] float range = 15f;
    Vector3 acceptablePoint;

    public LayerMask mask;

    public Transform player;
    public bool targetIsInSight { get; private set; }

    NavMeshAgent agent;
    //Rigidbody rb;
    Ray ray;

    Vector3 lastSeenPos;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        StartCoroutine(Roam());
    }

    void Update()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);

    }

    //methods

    void CheckForTarget()
    {
        float view = Vector3.Angle(transform.forward, Vector3.Normalize(player.position - transform.position));

        if (view < angle / 2)
        {
            if (Physics.Raycast(transform.position, player.position - transform.position, out RaycastHit hit, mask))
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

    IEnumerator Roam()
    {
        while (true)
        {
            Vector3 randomPoint = Random.insideUnitSphere * range;
            if (randomPoint.magnitude > range-2)
            {
                acceptablePoint = randomPoint + transform.position;
                if (NavMesh.SamplePosition(acceptablePoint, out NavMeshHit navMeshHit, range, NavMesh.AllAreas))
                {
                    agent.SetDestination(navMeshHit.position);
                    Debug.DrawLine(transform.position, navMeshHit.position, Color.blue, 1f);
                }
                yield return new WaitForSeconds(5f);
            }
            else
            {
                yield return null;
            }
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

    private void OnDrawGizmos()
    {

        //Gizmos.DrawWireSphere(transform.position, radius);

        //Gizmos.DrawRay(transform.position, transform.forward * radius);

        //Gizmos.DrawRay(transform.position, DirFromAngle(angle / 2, false) * radius);
        //Gizmos.DrawRay(transform.position, DirFromAngle(-angle / 2, false) * radius);

        //Gizmos.color = targetIsInSight ? Color.red : Color.white;
        //Gizmos.DrawWireSphere(lastSeenPos, .5f);
        //Gizmos.DrawRay(transform.position, _target.position - transform.position);
    }

}
