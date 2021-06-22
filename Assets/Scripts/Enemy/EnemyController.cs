using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private float lookRadius = 10f;
    [SerializeField] private float attackRadius = 2.5f;
    private float _attackCooldown = 0f;
    [SerializeField] private float attackSpeed = 1f;

    public Animator animator;

    [SerializeField] private GameObject swordSlash;

    private Transform _target;
    [SerializeField] private PlayerSwitcher playerSwitcher;
    [SerializeField] private NavMeshAgent agent;

    private bool _canWalk = true;

    public bool CanWalk
    {
        get => _canWalk;
        set => _canWalk = value;
    }


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        swordSlash.SetActive(false);
        //find the player
        if (playerSwitcher == null) playerSwitcher = FindObjectOfType<PlayerSwitcher>();
    }

    private void FixCharacterFollow()
    {
        if (playerSwitcher == null) return;
        _target = playerSwitcher.GetCurrentPlayer().transform;
    }

    void Update()
    {
        FixCharacterFollow();
        
        float distance = Vector3.Distance(_target.position, transform.position);
        _attackCooldown -= Time.deltaTime;

        Look(distance);
        Attack(distance);
    }

    private void Look(float distance)
    {
        if (!_canWalk)
        {
            //we cant walk
            agent.SetDestination(this.transform.position);
            return;
        }
        if(distance <= lookRadius)
        {
            agent.SetDestination(_target.position);
            animator.SetBool("IsWalking", true);
            
            if(distance <= agent.stoppingDistance)
            {
                FaceTarget();
                animator.SetBool("IsWalking", false);
            }
        }
    }

    private void Attack(float distance)
    {
        if (distance <= attackRadius)
        {
            if (_attackCooldown <= 0)
            {
                animator.SetBool("IsAttacking", true);
                _attackCooldown = 1f / attackSpeed;
                Damage(25);
            }
            else
            {
                animator.SetBool("IsAttacking", false);
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
    private void Damage(int damage)
    {
        GameObject player = playerSwitcher.GetCurrentPlayer();
        Health playerHealth = player.GetComponent<Health>();
        playerHealth.RemoveHealth(damage);
    }
}
