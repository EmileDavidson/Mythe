using System;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.Events;

public abstract class Tween
{
    //variable declaration 
    protected Func<float, float> EaseMethode;
    protected float speed;
    protected float percent;
    protected GameObject gameObject;
    
    //actions
    private Action _onTweenStart;
    private Action _onTweenFinish;
    private Action _onTweenUpdate;

    public bool IsFinished
    {
        get { return percent >= 1; }
    }
    
    protected bool HasStarted
    {
        get { return percent > 0; }
    }
    
    //functions
    public void Update(float dt)
    {
        //invoke tweenstart action if not started yet
        if(!HasStarted) OnTweenStart?.Invoke();
        
        //invoke update action
        OnTweenUpdate?.Invoke();
        
        percent += dt / speed;
        if (!IsFinished)
        {
            UpdateTween();
            return;
        }
        //invoke finshed action
        OnTweenFinish?.Invoke();
        
        Debug.unityLogger.Log("finished");
        TweenEnd();
    }

    protected abstract void UpdateTween();
    protected abstract void TweenEnd();

    //getters & setters
    public virtual Tween SetEasing(Func<float, float> func)
    {
        EaseMethode = func;
        return this; 
    }

    public Action OnTweenFinish
    {
        get => _onTweenFinish;
        set => _onTweenFinish = value;
    }

    public Action OnTweenStart
    {
        get => _onTweenStart;
        set => _onTweenStart = value;
    }

    public Action OnTweenUpdate
    {
        get => _onTweenUpdate;
        set => _onTweenUpdate = value;
    }
}
