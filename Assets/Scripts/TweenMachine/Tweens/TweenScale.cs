using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenScale : Tween
{
    private Vector3 startScale;
    private Vector3 targetScale;
    private Vector3 scaleDirection;


    public TweenScale(GameObject gameObject, Vector3 targetScale, float speed)
    {
        this.gameObject = gameObject;
        this.startScale = gameObject.transform.localScale;
        this.targetScale = targetScale;

        scaleDirection = targetScale - startScale;

        this.speed = speed;
        this.percent = 0;
        this.EaseMethode = Easing.Linear;
    }

    protected override void UpdateTween()
    {
        float easingstep = EaseMethode(percent);
        gameObject.transform.localScale = startScale + (scaleDirection * easingstep);
    }

    protected override void TweenEnd()
    {
        gameObject.transform.localScale = targetScale;
    }
}

