﻿using System.Collections.Generic;
using TweenMachine;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class WindSlash : AttackBase
{
    [SerializeField] private GameObject objectToShoot;
    [SerializeField] private float speed;
    [SerializeField] private List<Transform> shootLocations;
    [SerializeField] private float maxDistance;

    private void Start()
    {
        if (objectToShoot == null)
        {
            Debug.LogError(this.gameObject + "No object to shoot");
        }
        if (shootLocations.Count <= 0)
        {
            Debug.LogWarning(this.gameObject + "No placed to shoot from");
        }
    }

    public override void Use()
    {
        if (objectToShoot == null) return;
        if (shootLocations.Count <= 0) return;
        
        //logic
        for (int i = 0; i < shootLocations.Count; i++)
        {
         //for all the locations we want to instantiate a slash. 
         GameObject obj = Instantiate(objectToShoot);
         Transform trans = shootLocations[i].transform;
         obj.transform.position = trans.position;
         
         ThrowObject throwObjectComponent = obj.AddComponent<ThrowObject>();
         throwObjectComponent.Instantiate(Random.Range(minDamage, maxDamage), true, null);

         TweenBuild tweenBuild = new TweenBuild(obj);
         Tween tweenPosition = tweenBuild.SetTweenPosition(shootLocations[i].position + (shootLocations[i].forward * maxDistance), .4f, EasingType.Linear);
        
         tweenBuild.DestroyOnFinish = true;
         tweenBuild.StartTween();
        }
    }

}
