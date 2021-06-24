using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{
        #region SINGLETON PATTERN
        /**
         * make sure there is only of these classes on objects in the scene.
         * if there are none we create one under the object "Managers" if that one doesnt exist we create it as well
         * if you need the object before the script you can also manual put it in the scene but make sure to do it only ones!
         */
        private static SceneController _instance;
        public static SceneController Instance
        {
            get
            {
                if (_instance != null) return _instance;

                _instance = GameObject.FindObjectOfType<SceneController>();
                if (_instance != null) return _instance;

                GameObject parent = GameObject.Find("Managers");
                if (parent == null) parent = new GameObject("Managers");

                GameObject container = new GameObject("SceneManager");
                _instance = container.AddComponent<SceneController>();
                _instance.transform.parent = parent.transform;
                return _instance;
            }
        }
        #endregion

        public void LoadScene(Scene scene)
        {
            SceneManager.LoadScene((int) scene);
        }
        
        public void LoadScene(int scene)
        {
            SceneManager.LoadScene((int) scene);
        }
}
