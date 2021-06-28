using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class PauseController : MonoBehaviour
{
   #region SINGLETON PATTERN
          /**
         * make sure there is only of these classes on objects in the scene.
         * if there are none we create one under the object "Managers" if that one doesnt exist we create it as well
         * if you need the object before the script you can also manual put it in the scene but make sure to do it only ones!
         */
   private static PauseController _instance;
   public static PauseController Instance
   {
      get
      {
         if (_instance != null) return _instance;

         _instance = GameObject.FindObjectOfType<PauseController>();
         if (_instance != null) return _instance;

         GameObject parent = GameObject.Find("Managers");
         if (parent == null) parent = new GameObject("Managers");

         GameObject container = new GameObject("PauseController");
         _instance = container.AddComponent<PauseController>();
         _instance.transform.parent = parent.transform;
         return _instance;
      }
   }
   #endregion
   
   [SerializeField] private bool paused;
   [SerializeField] private bool canPause = true;
   [SerializeField] private bool unPauseOnStart = true;
    
   public UnityEvent onPauseChange = new UnityEvent();
   public UnityEvent onPause = new UnityEvent();
   public UnityEvent onPlay = new UnityEvent();

   public MouseLock mouseLocker;

   //getters setters
   public bool Paused
   {
      get => paused;
      set
      {
         if (!canPause) return;
         paused = value;
         onPauseChange.Invoke();
      }
   }

   public bool CanPause
   {
      get => canPause;
      set => canPause = value;
   }
   
   //main functions
   private void PauseChanged()
   {
      if (paused)
      {
         mouseLocker.SetVisibility(true);
         mouseLocker.SetLockMode(CursorLockMode.None);
         Time.timeScale = 0;
         onPause.Invoke();
         return;
      }
      mouseLocker.SetVisibility(false);
      mouseLocker.SetLockMode(CursorLockMode.Locked);
      Time.timeScale = 1;
      onPlay.Invoke();
   }

   //hulp functions
   public void LockMouse()
   {
      mouseLocker.SetVisibility(false);
      mouseLocker.SetLockMode(CursorLockMode.Locked);
   }

   public void UnlockMouse()
   {
      mouseLocker.SetVisibility(true);
      mouseLocker.SetLockMode(CursorLockMode.None);
   }

   //event functions
   public void Start()
   {
      onPauseChange.AddListener(PauseChanged);
      if (unPauseOnStart) Paused = false;
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
         Paused = !Paused;
      }
   }
}
