using System.Runtime.InteropServices;
using UnityEngine;

public class TweenPosition : Tween
{
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Vector3 direction;

    //constructor
    public TweenPosition(GameObject gameObject, Vector3 targetPos, float speed)
    {
        this.gameObject = gameObject;
        this.targetPosition = targetPos;
        this.speed = speed;

        this.startPosition = gameObject.transform.position;
        this.direction = targetPos - startPosition;
        this.percent = 0;
        
        this.EaseMethode = Easing.Linear;
    }

    protected override void UpdateTween()
    {
        float easingstep = EaseMethode(percent);
        gameObject.transform.position = startPosition + (direction * easingstep);
    }

    protected override void TweenEnd()
    {
        gameObject.transform.position = targetPosition;

    }


    public void ChangeTarget(Vector3 targetPos, float sp)
    {
        this.targetPosition = targetPos;
        this.speed = sp;

        this.startPosition = gameObject.transform.position;
        this.direction = targetPos - startPosition;
        this.percent = 0;
    }
}
