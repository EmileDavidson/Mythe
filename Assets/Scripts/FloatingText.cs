using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class FloatingText : MonoBehaviour
{
    public float destroyTime;
    public Vector3 offset;
    public Vector3 RandomizeIntensity = new Vector3(.4f, 0, 0);
    public Transform lookAtTrans;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
        transform.localPosition += offset;
        transform.localPosition += new Vector3(
            Random.Range(-RandomizeIntensity.x, + RandomizeIntensity.x),
            Random.Range(-RandomizeIntensity.y, + RandomizeIntensity.y),
            Random.Range(-RandomizeIntensity.z, + RandomizeIntensity.z));
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - lookAtTrans.position);
    }
}
