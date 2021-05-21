using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Cinemachine;
using UnityEngine;

namespace TweenMachine
{
    public class TweenBuild
    {
        private GameObject _gameObject;
        //tweens
        private List<Tween> tweens = new List<Tween>();
        private bool _destroyOnFinish = false;
        
        //complete tweens
        public Action OnTweenBuildFinish;
        public Action OnTweenBuildUpdate;
        public Action OnTweenBuildStart;
        
        public bool tweenBuildFinished = false;

        public TweenBuild(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        //position
        public Tween SetTweenPosition(Vector3 targetPos, float speed)
        {
            Tween newTween = new TweenPosition(_gameObject, targetPos, speed);
            tweens.Add(newTween);
            return newTween;
        }
        
        public Tween SetTweenPosition(Vector3 target, float speed, EasingType easingType)
        {
            Tween tween =  new TweenPosition(_gameObject, target, speed).SetEasing(EasingDictonary.dict[easingType]);
            tweens.Add(tween);
            return tween;
        }

        public void RemoveAllPositionTweens()
        {
            foreach (Tween tween in tweens)
            {
                if (tween is TweenPosition)
                {
                    tweens.Remove(tween);
                }
            }
        }
        
        //rotation
        public Tween SetTweenRotation(Quaternion target, float speed)
        {
            Tween tween = new TweenRotation(_gameObject, target, speed);
            tweens.Add(tween);
            return tween;
        }
        
        public Tween SetTweenRotation(Quaternion target, float speed, EasingType easingType)
        {
            Tween tween = new TweenRotation(_gameObject, target, speed).SetEasing(EasingDictonary.dict[easingType]);
            tweens.Add(tween);
            return tween;
        }
        
        public void RemoveAllTweenRotations()
        {
            foreach (Tween tween in tweens)
            {
                if (tween is TweenRotation)
                {
                    tweens.Remove(tween);
                }
            }
        }
        
        //scale
        public Tween SetTweenScale(Vector3 target, float speed)
        {
            Tween tween = new TweenScale(_gameObject, target, speed);
            tweens.Add(tween);
            return tween;
        }
        
        public Tween SetTweenScale(Vector3 target, float speed, EasingType easingType)
        {
            Tween tween = new TweenScale(_gameObject, target, speed).SetEasing(EasingDictonary.dict[easingType]);
            tweens.Add(tween);
            return tween;
        }
        
        public void RemoveAllTweenScale()
        {
            foreach (Tween tween in tweens)
            {
                if (tween is TweenScale)
                {
                    tweens.Remove(tween);
                }
            }
        }
        
        //color
        public Tween SetTweenColor(Color target, float speed)
        {
            Tween tween = new TweenColor(_gameObject, target, speed);
            tweens.Add(tween);
            return tween;
        }
        
        public Tween SetTweenColor(Color target, float speed, EasingType easingType)
        {
            Tween tween = new TweenColor(_gameObject, target, speed).SetEasing(EasingDictonary.dict[easingType]);
            tweens.Add(tween);
            return tween;
        }
        
        public void RemoveAllTweenColor()
        {
            foreach (Tween tween in tweens)
            {
                if (tween is TweenColor)
                { 
                    tweens.Remove(tween);
                }
            }
        }

        public bool DestroyOnFinish
        {
            get => _destroyOnFinish;
            set => _destroyOnFinish = value;
        }

        public GameObject GameObject
        {
            get => _gameObject;
            set => _gameObject = value;
        }
        
        //functional functions
        public void UpdateTween(float dt)
        {
            OnTweenBuildUpdate?.Invoke();
            
            if (tweenBuildFinished) return;
            
            foreach (Tween tween in tweens){
                if(!tween.IsFinished) tween.Update(dt);
            }

            CheckComplete();
        }
        
        public void StartTween()
        {
            TweenController.Instance._acitveTweens.Add(this);
            OnTweenBuildStart?.Invoke();
        }

        private void CheckComplete()
        {
            foreach (Tween tween in tweens)
            {
                if (!tween.IsFinished) return;
            }

            OnTweenBuildFinish?.Invoke();
            tweenBuildFinished = true;
        }
    }
}

