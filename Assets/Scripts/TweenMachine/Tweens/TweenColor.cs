using UnityEngine;
using UnityEngine.Events;

public class TweenColor : Tween
{
    private Color targetColor;
    private Color startingColor;
    private Renderer _renderer;

    private float _directionR;
    private float _directionG;
    private float _directionB;
    private float _directionA;

    //constructor
    public TweenColor(GameObject gameObject, Color targetColor, float speed)
    {
        this.gameObject = gameObject;
        this.targetColor = targetColor;
        this.speed = speed;

        _renderer = gameObject.GetComponent<Renderer>();
        startingColor = _renderer.material.color;

        _directionR = targetColor.r - startingColor.r;
        _directionG = targetColor.g - startingColor.g;
        _directionB = targetColor.b - startingColor.b;
        _directionA = targetColor.a - startingColor.a;

        percent = 0;
        EaseMethode = Easing.Linear;
    }

    //update

    protected override void UpdateTween()
    {
        float easingstep = EaseMethode(percent);
        
        float r = startingColor.r + (_directionR * easingstep);
        float g = startingColor.g + (_directionG * easingstep);
        float b = startingColor.b + (_directionB * easingstep);
        float a = startingColor.a + (_directionA * easingstep);
        
        _renderer.material.color = new Color(r, g, b, a);
    }

    protected override void TweenEnd()
    {
        _renderer.material.color = targetColor;
    }
    
}