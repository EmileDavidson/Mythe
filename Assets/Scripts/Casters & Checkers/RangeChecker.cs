using UnityEngine;

public abstract class RangeChecker : MonoBehaviour
{
    [SerializeField] protected LayerMask _layer;

    public abstract GameObject GetFirstGameObjectInRange();
    public abstract GameObject[] GetAllObjectsInRange();
    public abstract void OnDrawGizmosSelected();
}
