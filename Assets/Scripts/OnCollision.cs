using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCollision : MonoBehaviour
{
    public UnityEvent onCollision = new UnityEvent();

    public List<int> layerTypes = new List<int>();
    public List<String> tagTypes = new List<string>();

    /// <summary>
    /// Unity OnTrigger event (check if we collide with one of the given layers / tag types if we do trigger event.
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
    {
        if (!(layerTypes.Contains(other.gameObject.layer)) && layerTypes.Count >= 1) return;
        if (!(tagTypes.Contains(other.gameObject.tag)) && tagTypes.Count >= 1) return;
        onCollision.Invoke();
    }

    /// <summary>
    /// Unity OnCollision event (check if we collide with one of the given layers / tag types if we do trigger event.
    /// </summary>
    /// <param name="other"></param>
    public void OnCollisionEnter(Collision other)
    {
        if (!(layerTypes.Contains(other.gameObject.layer)) && layerTypes.Count >= 1) return;
        if (!(tagTypes.Contains(other.gameObject.tag)) && tagTypes.Count >= 1) return;       
        onCollision.Invoke();
    }
    
    /// <summary>
    /// Active Gameobject.
    /// </summary>
    /// <param name="target">Gameobject to active.</param>
    public void ActiveObject(GameObject target)
    {
        target.SetActive(true);
    }
    
    /// <summary>
    /// DeActive gameobject
    /// </summary>
    /// <param name="target">object to DeActive</param>
    public void DeactivateObject(GameObject target)
    {
        target.SetActive(false);
    }

    /// <summary>
    /// Destroy given gameobject
    /// </summary>
    /// <param name="target">GameObject to destroy</param>
    public void DestroyObject(GameObject target)
    {
        Destroy(target);
    }
}
