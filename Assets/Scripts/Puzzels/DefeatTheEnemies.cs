using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DefeatTheEnemies : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public int enemyCount = 0;

    public UnityEvent onFinish = new UnityEvent();

    private void Start()
    {
        enemyCount = enemies.Count;
        
        foreach (GameObject gameObject in enemies)
        {
            Health health = gameObject.GetComponent<Health>();
            health.dieEvent.AddListener(() => { enemyCount--; });
        }
    }

    void Update()
    {
        if (enemyCount <= 0)
        {
            //they all died (removed)
            onFinish.Invoke();
        }
    }
    
    //some functions that we can call for the finish event
    
    public void DestroyThisObject()
    {
        Destroy(this.gameObject);
    }
}
