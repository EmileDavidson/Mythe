using System.Collections.Generic;
using TweenMachine;
using UnityEngine;

namespace TweenMachine
{
    public class TweenController : MonoBehaviour
    {
        #region SINGLETON PATTERN
        /**
         * make sure there is only of these classes on objects in the scene.
         * if there are none we create one under the object "Managers" if that one doesnt exist we create it as well
         * if you need the object before the script you can also manual put it in the scene but make sure to do it only ones!
         */
        private static TweenController _instance;
        public static TweenController Instance
        {
            get
            {
                if (_instance != null) return _instance;

                _instance = GameObject.FindObjectOfType<TweenController>();
                if (_instance != null) return _instance;

                GameObject parent = GameObject.Find("Managers");
                if (parent == null) parent = new GameObject("Managers");

                GameObject container = new GameObject("TweenController");
                _instance = container.AddComponent<TweenController>();
                _instance.transform.parent = parent.transform;
                return _instance;
            }
        }
        #endregion

        public bool debugging = false;
        public List<TweenBuild> _acitveTweens = new List<TweenBuild>();
        private List<TweenBuild> _doneTweens = new List<TweenBuild>();
        private bool paused = false;
    
        private void Update()
        {
            if (_acitveTweens.Count <= 0) Destroy(this.gameObject);
            if (paused) return;
            
            UpdateActiveTweens();
        }
    
        private void UpdateActiveTweens()
        {
            for (int i = 0; i < _acitveTweens.Count; i++)
            {
                if (_acitveTweens[i].GameObject == null)
                {
                    if(debugging) Debug.Log("Removed a tween because the object we tween is removed.");
                    _acitveTweens.RemoveAt(i);
                    i--;
                    continue;
                }
                _acitveTweens[i].UpdateTween(Time.deltaTime);
                if (_acitveTweens[i].tweenBuildFinished)
                {
                    if(_acitveTweens[i].DestroyOnFinish) Destroy(_acitveTweens[i].GameObject);
                    _acitveTweens.RemoveAt(i);
                    i--;
                }
            }
        }

        public void SetPaused()
        {
            if (_acitveTweens.Count >= 1)
            {
                paused = true;
                return;
            }
            //there are no tweens to be paused
            Destroy(gameObject);
        }
    }
}
