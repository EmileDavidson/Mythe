using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using TweenMachine;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Debug = UnityEngine.Debug;

public class WindSlash : AttackBase
{
    [SerializeField] private GameObject objectToShoot;
    [SerializeField] private float speed;
    [SerializeField] private List<Transform> shootLocations;
    [SerializeField] private float maxDistance;

    private void Start()
    {
        if (shootLocations.Count <= 0 || objectToShoot == null)
        {
            Debug.LogWarning(this.gameObject + " Has no object to shoot and / or places to shoot from");
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
         throwObjectComponent.Instantiate(damage, true, null);

         TweenBuild tweenBuild = new TweenBuild(obj);
         Tween tweenPosition = tweenBuild.SetTweenPosition(shootLocations[i].position + (shootLocations[i].forward * maxDistance), .4f, EasingType.Linear);
        
         tweenBuild.DestroyOnFinish = true;
         tweenBuild.StartTween();
        }
    }

}
