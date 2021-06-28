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
   [SerializeField] private bool hideMouseOnStart = false;
    
   public UnityEvent onPauseChange = new UnityEvent();
   public UnityEvent onPause = new UnityEvent();
   public UnityEvent onPlay = new UnityEvent();

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
         Cursor.visible = true;
         Cursor.lockState = CursorLockMode.None;
         Time.timeScale = 0;
         onPause.Invoke();
         return;
      }
      Cursor.visible = false;
      Cursor.lockState = CursorLockMode.Locked;
      Time.timeScale = 1;
      onPlay.Invoke();
   }

   //hulp functions
   public void LockMouse()
   {
      Cursor.visible = false;
      Cursor.lockState = CursorLockMode.Locked;
   }

   public void UnlockMouse()
   {
      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.None;
   }

   public void SetTimeScale(float value)
   {
      Time.timeScale = value;
   }

   //event functions
   public void Start()
   {
      onPauseChange.AddListener(PauseChanged);
      if (unPauseOnStart) Paused = false;
      
      //lock or unlock mouse on start.
      if(hideMouseOnStart) LockMouse();
      else UnlockMouse();
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
         Paused = !Paused;
      }
   }
}
