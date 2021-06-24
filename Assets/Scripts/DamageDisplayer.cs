using UnityEngine;

[RequireComponent(typeof(Health))]
public class DamageDisplayer : MonoBehaviour
{
    private Health _health;
    [SerializeField] private GameObject floatingTextObject;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 floatingTextSize;
    [SerializeField] private Camera lookCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        _health = gameObject.GetComponent<Health>();
        _health.OnRemoveHealth?.AddListener(TakeDamage);
        if (lookCamera == null) lookCamera = Camera.main;
    }

    private void TakeDamage(int value)
    {
        if (value < 0) return;
        GameObject go = Instantiate(floatingTextObject, transform.position, Quaternion.identity, canvas.transform);

        FloatingText fText = go.GetComponent<FloatingText>();
        if (fText == null) fText = go.AddComponent<FloatingText>();
        fText.offset = offset;
        fText.lookAtTrans = lookCamera.transform;
        floatingTextObject.transform.localScale =
            new Vector3(floatingTextSize.x, floatingTextSize.y, floatingTextSize.z);

        go.GetComponent<TextMesh>().text = "" + value;
    }
}
