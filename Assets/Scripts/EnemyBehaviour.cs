using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public enum EnemyState
    {
        Idle,
        Patrol,
        Chase,
        Attack
    }
    public EnemyState currentState;
    public float distanceToPlayer;
    public bool canSeePlayer = false;
    public Transform raycastOrigin;

    void Start()
    {
        currentState = EnemyState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
        switch (currentState)
        {
            case EnemyState.Idle:
                // Start patrol
                currentState = EnemyState.Patrol;
                break;
            case EnemyState.Patrol:
                // Move along a predefined path
                Patrol();
                break;
            case EnemyState.Chase:
                // Move towards the player
                ChasePlayer();
                break;
            case EnemyState.Attack:
                // Attack the player
                AttackPlayer();
                break;
        }
    }

    public void CanSeePlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, 10f))
        {
            if (hit.collider.CompareTag("Player"))
            {
                canSeePlayer = true;
                distanceToPlayer = Vector3.Distance(transform.position, hit.collider.transform.position);
            }
        }
        else
        {
            canSeePlayer = false;
            distanceToPlayer = Mathf.Infinity;
        }

    }

    public void CheckState()
    {
        CanSeePlayer();
        if (canSeePlayer && distanceToPlayer > 5f)
        {
            currentState = EnemyState.Patrol;
        }
    }

    public void Patrol()
    {
        Debug.Log("Patrolling the area.");

        if (distanceToPlayer < 5f)
        {
            currentState = EnemyState.Chase;
        }
    }

    public void ChasePlayer()
    {
        Debug.Log("Chasing the player!");

        if (distanceToPlayer < 2f)
        {
            currentState = EnemyState.Attack;
        }
        else if (!canSeePlayer)
        {
            currentState = EnemyState.Idle;
        }
    }

    public void AttackPlayer()
    {
        Debug.Log("PIUM PIUM!");
        if (distanceToPlayer > 2f)
        {
            currentState = EnemyState.Chase;
        }
        else if (!canSeePlayer)
        {
            currentState = EnemyState.Idle;
        }
    }
}
