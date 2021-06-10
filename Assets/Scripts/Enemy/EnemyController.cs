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

    [SerializeField] private GameObject swordSlash;

    private Transform _target;
    [SerializeField] private PlayerSwitcher playerSwitcher;
    [SerializeField] private NavMeshAgent agent;


    void Start()
    {
        // target = PlayerManager.Instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        swordSlash.SetActive(false);
        if (playerSwitcher == null) GameObject.FindObjectOfType<PlayerSwitcher>();
        print(playerSwitcher);
    }

    private void FixCharacterFollow()
    {
        if (playerSwitcher == null) return;
       // print("OMFG");
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
        if(distance <= lookRadius)
        {
            agent.SetDestination(_target.position);
            
            if(distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        }
    }

    private void Attack(float distance)
    {
        if (distance <= attackRadius)
        {
            if (_attackCooldown <= 0)
            {
                StartCoroutine(Slash());
                _attackCooldown = 1f / attackSpeed;
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
    
    IEnumerator Slash()
    {
        swordSlash.SetActive(true);
        yield return new WaitForSeconds(0.5f); 
        swordSlash.SetActive(false);
        yield return new WaitForSeconds(0.5f);
    }
}
