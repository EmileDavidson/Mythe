using UnityEngine;

public class SphereCaster : MonoBehaviour
{
    [SerializeField] private float maxDistance = 20f;
    [SerializeField] private float radius = 5f;

    private void OnDrawGizmos()
    {
        RaycastHit hit;

        bool isHit = Physics.SphereCast(transform.position, radius, transform.forward, out hit, maxDistance);
        if (isHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
            Gizmos.DrawWireSphere(transform.position + transform.forward * hit.distance, radius);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.forward * maxDistance);
            Gizmos.DrawWireSphere(transform.position + transform.forward * maxDistance, radius);
        }
    }
}
