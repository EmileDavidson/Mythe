using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{

    public Material material;
    public Material startMaterial;
    [SerializeField] private float changeTime = .5f;
    [SerializeField]Health health;
    [SerializeField] private Renderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        if (_renderer == null) _renderer = GetComponent<Renderer>();
        _renderer.enabled = true;
        _renderer.sharedMaterial = startMaterial;
        if (health == null) health = gameObject.GetComponent<Health>();
        

    }

    // Update is called once per frame
    void Update()
    {
        health.OnRemoveHealth?.AddListener(value =>
        {
            if (health.health <= 0) return;
            if(_renderer != null && material != null) StartCoroutine(Change());
        });
    }

    private IEnumerator Change()
    {
        _renderer.sharedMaterial = material;
        yield return new WaitForSeconds(changeTime);
        _renderer.sharedMaterial = startMaterial;
    }
}
