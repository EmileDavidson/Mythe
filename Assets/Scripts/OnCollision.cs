using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCollision : MonoBehaviour
{
    public UnityEvent onCollision = new UnityEvent();

    public List<int> layerTypes = new List<int>();
    public List<String> tagTypes = new List<string>();

    public void OnTriggerEnter(Collider other)
    {
        if (!(layerTypes.Contains(other.gameObject.layer)) && layerTypes.Count >= 1) return;
        if (!(tagTypes.Contains(other.gameObject.tag)) && tagTypes.Count >= 1) return;
        onCollision.Invoke();
    }

    public void OnCollisionEnter(Collision other)
    {
        if (!(layerTypes.Contains(other.gameObject.layer)) && layerTypes.Count >= 1) return;
        if (!(tagTypes.Contains(other.gameObject.tag)) && tagTypes.Count >= 1) return;       
        onCollision.Invoke();
    }
    
    //some build in functions
    public void ActiveObject(GameObject target)
    {
        target.SetActive(true);
    }
    
    public void DeactivateObject(GameObject target)
    {
        target.SetActive(false);
    }

    public void DestroyObject(GameObject target)
    {
        Destroy(target);
    }
}
