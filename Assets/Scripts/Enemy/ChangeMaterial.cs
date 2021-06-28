using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{

    public Material[] material;
    Renderer rend;
    [SerializeField]Health health;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
        if (health == null) health = gameObject.GetComponent<Health>();
        

    }

    // Update is called once per frame
    void Update()
    {
        health.OnRemoveHealth?.AddListener(value => { StartCoroutine("CChange"); });
    }

    private IEnumerator CChange()
    {
        rend.sharedMaterial = material[1];
        yield return new WaitForSeconds(0.5f);
        rend.sharedMaterial = material[0];
    }
}
