using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class PigBoss : MonoBehaviour
{
    //special move 
    public UnityEvent onSpecialMove = new UnityEvent();
    public UnityEvent onSpecialMoveFinish = new UnityEvent();
    [SerializeField] private List<Transform> specialMoveHitLocations = new List<Transform>();
    [SerializeField] private int fallAmount;
    [SerializeField] private float timeBetweenSpawns = 1;
    [SerializeField] private GameObject fallObject;
    [SerializeField] private PlayerSwitcher playerSwitcher;

    [SerializeField] private int onPlayerLocationSpawnRate = 3;
    [SerializeField] private int onRandomLocationSpawnRate = 7;

    [SerializeField] private Vector3 spawnOffsetFromPlayer = new Vector3(0, 20, 0);
        
    [SerializeField] private Health health;

    private void Start()
    {
        if(health == null) health = GetComponent<Health>();
        health.OnHealthChanged.AddListener(value =>
        {
            if(value <= health.startHealth / 100 * 30) SpecialMove();
        });
        if(playerSwitcher == null) playerSwitcher = GameObject.FindObjectOfType<PlayerSwitcher>();
    }
    

    //trigger special move at 30% of hp
    private void SpecialMove()
    {
     onSpecialMove.Invoke();
     health.canTakeDamage = false;
     //let rock fall out of the sky on the player and random positions on defined locations.
     StartCoroutine(SpawnFalingObjects());
    }

    private void FinishSpecialMove()
    {
        health.canTakeDamage = true;
        onSpecialMoveFinish.Invoke();
    }

    private IEnumerator SpawnFalingObjects()
    {
        for (int i = 0; i < fallAmount; i++)
        {
            //no object to spawn
            if (fallObject == null)
            {
                FinishSpecialMove();
                yield return null;
            }
            //no place to spawn
            if (playerSwitcher == null && specialMoveHitLocations.Count >= 1)
            {
                FinishSpecialMove();
                yield return null;
            }

            GameObject fallingObject = Instantiate(fallObject);
            int random = Random.Range(0, onPlayerLocationSpawnRate + onRandomLocationSpawnRate);
            
            if ((random < onRandomLocationSpawnRate || playerSwitcher == null) && specialMoveHitLocations.Count >= 1)
            {
                fallingObject.transform.position = specialMoveHitLocations[Random.Range(0, specialMoveHitLocations.Count)].position;
            }
            else if (random <= onRandomLocationSpawnRate + onPlayerLocationSpawnRate && playerSwitcher != null)
            {
                fallingObject.transform.position = playerSwitcher.GetCurrentPlayer().transform.position + spawnOffsetFromPlayer;
            }
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
        FinishSpecialMove();
        yield return null;
    }

    public void ActiveObject(GameObject target)
    {
        target.SetActive(true);
    }
    
    //some build in functions
    public void DeactivateObject(GameObject target)
    {
        target.SetActive(false);
    }
}
