using System;
using System.Collections;
using System.Collections.Generic;
using TweenMachine;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngineInternal;

public class Throw : AttackBase
{
    [SerializeField] private GameObject throwObject;
    [SerializeField] private Transform throwFromLocation;
    [SerializeField] private float speed;
    [SerializeField] private float maxDistance;
    [SerializeField] private float forwardOffset;

    private void Start()
    {
        if (throwFromLocation == null) throwFromLocation = this.transform;
    }

    public override void Use()
    {
        if (throwObject == null) return;

        GameObject obj = Instantiate(throwObject);
        Transform trans = throwFromLocation.transform;
        obj.transform.position = trans.position + (trans.forward * forwardOffset);
        ThrowObject throwObjectComponent = obj.AddComponent<ThrowObject>();
        throwObjectComponent.Instantiate(damage, true, null);

        TweenBuild tweenBuild = new TweenBuild(obj);
        Tween tweenPosition = tweenBuild.SetTweenPosition(throwFromLocation.position + (throwFromLocation.forward * maxDistance), .4f, EasingType.Linear);
        
        tweenBuild.DestroyOnFinish = true;
        tweenBuild.StartTween();
    }
}
