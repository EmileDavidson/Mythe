using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class DamageDisplayer : MonoBehaviour
{
    private Health _health;
    [SerializeField] private GameObject floatingTextObject;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Camera lookCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        _health = gameObject.GetComponent<Health>();
        _health.RemoveHealthEvent?.AddListener(TakeDamage);
        if (lookCamera == null) lookCamera = Camera.main;
    }

    private void TakeDamage(float value)
    {
        if (value < 0) return;
        GameObject go = Instantiate(floatingTextObject, transform.position, Quaternion.identity, canvas.transform);

        FloatingText fText = go.GetComponent<FloatingText>();
        if (fText == null) fText = go.AddComponent<FloatingText>();
        fText.offset = offset;
        fText.lookAtTrans = lookCamera.transform;

        go.GetComponent<TextMesh>().text = "" + value;
    }
}
