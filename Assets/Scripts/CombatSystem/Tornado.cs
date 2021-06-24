using System.Collections.Generic;
using TweenMachine;
using UnityEngine;

public class Tornado : AttackBase
{
    [SerializeField] private GameObject objectToShoot;
    [SerializeField] private float speed;
    [SerializeField] private List<Transform> shootLocations;
    [SerializeField] private float maxDistance;
    private static readonly int Animation = Animator.StringToHash("Attack");
    [SerializeField] private Animator animator;

    private void Start()
    {
        if (animator == null) animator = GetComponent<Animator>();
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
        
        if (animator != null) animator.SetInteger(Animation, 2);
    }

    public void SpawnAttack()
    {
        for (int i = 0; i < shootLocations.Count; i++)
        {
            //for all the locations we want to instantiate a slash. 
            GameObject obj = Instantiate(objectToShoot);
            Transform trans = shootLocations[i].transform;
            obj.transform.position = trans.position;
         
            ThrowObject throwObjectComponent = obj.AddComponent<ThrowObject>();
            throwObjectComponent.Instantiate(Random.Range(minDamage, maxDamage), true, null);

            TweenBuild tweenBuild = new TweenBuild(obj);
            Tween tweenPosition = tweenBuild.SetTweenPosition(shootLocations[i].position + (shootLocations[i].forward * maxDistance), speed, EasingType.Linear);
        
            tweenBuild.DestroyOnFinish = true;
            tweenBuild.StartTween();
        }
    }

    public void StopAttackAnimation()
    {
        print("STOP!!!!!");
        if (animator != null)
        {
            print("WORKED");
            animator.SetInteger(Animation, 0);
        }
    }

}
