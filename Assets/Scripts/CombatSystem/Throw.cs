using System;
using TweenMachine;
using UnityEngine;
using Random = UnityEngine.Random;

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
        
        //FIX OBJECT SCRIPT
        ThrowObject throwObjectComponent = obj.AddComponent<ThrowObject>();
        throwObjectComponent.Instantiate(Random.Range(minDamage, maxDamage), true, null);

        TweenBuild tweenBuild = new TweenBuild(obj);
        Tween tweenPosition = tweenBuild.SetTweenPosition(throwFromLocation.position + (throwFromLocation.forward * maxDistance), speed, EasingType.Linear);
        
        tweenBuild.DestroyOnFinish = true;
        tweenBuild.StartTween();
    }
}
