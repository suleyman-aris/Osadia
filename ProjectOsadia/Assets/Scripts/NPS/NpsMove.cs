using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpsMove : MonoBehaviour
{
    public Vector3 destinationPoint;
    private bool destinationPointSet;
    public float walkPointRange;

    [SerializeField] Transform _player;
    public LayerMask ground, player;
    public NavMeshAgent _agent;

    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    public GameObject sphere;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private enum NpsState
    {
        Patrol,
        Chase,
        Attack
    }

    private NpsState currentState;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, player);

        switch (currentState)
        {
            case NpsState.Patrol:
                if (!playerInSightRange && !playerInAttackRange)
                {
                    Patroling();
                }
                else if (playerInSightRange && !playerInAttackRange)
                {
                    ChangeState(NpsState.Chase);
                }
                else if (playerInSightRange && playerInAttackRange)
                {
                    ChangeState(NpsState.Attack);
                }
                break;

            case NpsState.Chase:
                if (!playerInSightRange && !playerInAttackRange)
                {
                    ChangeState(NpsState.Patrol);
                }
                else if (playerInSightRange && playerInAttackRange)
                {
                    ChangeState(NpsState.Attack);
                }
                else
                {
                    ChasePlayer();
                }
                break;

            case NpsState.Attack:
                if (!playerInSightRange && !playerInAttackRange)
                {
                    ChangeState(NpsState.Patrol);
                }
                else if (playerInSightRange && !playerInAttackRange)
                {
                    ChangeState(NpsState.Chase);
                }
                else
                {
                    AttackPlayer();
                }
                break;
        }
    }

    private void ChangeState(NpsState newState)
    {
        currentState = newState;
    }

    private void AttackPlayer()
    {
        Vector3 attackPos = new Vector3(transform.position.x - 2f, transform.position.y, transform.position.z - 2f);
        _agent.SetDestination(attackPos);

        transform.LookAt(_player);

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(sphere, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 25f, ForceMode.Impulse);
            rb.AddForce(transform.up * 7f, ForceMode.Impulse);

            alreadyAttacked = true;
            Debug.Log("Saldýrý");
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void ChasePlayer()
    {
        _agent.SetDestination(_player.position);
    }

    private void Patroling()
    {
        if (!destinationPointSet)
        {
            SearchWalkPoint();
        }

        if (destinationPointSet)
        {
            _agent.SetDestination(destinationPoint);
        }

        Vector3 distanceToDestinationPoint = transform.position - destinationPoint;
        if (distanceToDestinationPoint.magnitude < 1.0f)
        {
            destinationPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        destinationPoint = new Vector3(transform.position.x + randomX, transform.position.y,
            transform.position.z + randomZ);
        if (Physics.Raycast(origin: destinationPoint, direction: -transform.up, maxDistance: 2.0f, ground))
        {
            destinationPointSet = true;
        }
    }
}
