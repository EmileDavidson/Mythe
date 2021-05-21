using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenRotation : Tween
{
    private Vector3 startRotation;
    private Vector3 targetRotation;
    private Vector3 direction;


    public TweenRotation(GameObject gameObject, Quaternion targetRotation, float speed)
    {
        this.gameObject = gameObject;
        this.startRotation = gameObject.transform.eulerAngles;
        this.targetRotation = new Vector3(targetRotation.x, targetRotation.y, targetRotation.z);
    
        this.direction.x = targetRotation.x - startRotation.x;
        this.direction.y = targetRotation.x - startRotation.y;
        this.direction.z = targetRotation.x - startRotation.z;

        this.speed = speed;
        this.percent = 0;
        this.EaseMethode = Easing.Linear;
    }

    protected override void UpdateTween()
    {
        float easingstep = EaseMethode(percent);
            
        float x = startRotation.x + (direction.x * easingstep);
        float y = startRotation.y + (direction.y * easingstep);
        float z = startRotation.z + (direction.z * easingstep);
            
        Vector3 newRotation = new Vector3(x, y, z);
            
        gameObject.transform.eulerAngles = newRotation;
    }

    protected override void TweenEnd()
    {
        gameObject.transform.eulerAngles = new Vector3(targetRotation.x, targetRotation.y, targetRotation.z);
    }
    
}
