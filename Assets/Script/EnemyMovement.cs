using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float speed;
    private Rigidbody enemyRb;
    public GameObject player;
    public float radius;
    [Range(0, 360)]
    public float angle;
    

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;


    private void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(FOVRoutine());
    }

    private void FixedUpdate()
    {
        if(player != null)
        {
            if(canSeePlayer == true)
            {
                enemyRb.AddForce((player.transform.position - transform.position).normalized * speed);
            }
        }
    }

    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, radius, targetMask);

        if(rangeCheck.Length != 0 )
        {
            Transform target = rangeCheck[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if(Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)) 
                    canSeePlayer = true;
                else 
                    canSeePlayer = false;
            }
            else 
                canSeePlayer = false;
        }
        else if(canSeePlayer)
        {
            canSeePlayer = false;
        }
    }
}
