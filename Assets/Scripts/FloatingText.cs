using UnityEngine;
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

    
    /// <summary>
    /// make sure this gameobject looks at the camera at all time. 
    /// </summary>
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - lookAtTrans.position);
    }
}
