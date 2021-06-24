using UnityEngine;

public abstract class RangeChecker : MonoBehaviour
{
    [SerializeField] protected LayerMask _layer;

    public abstract Enemy GetFirstEnemyInRange();
    public abstract Enemy[] GetAllEnemiesInRange();
    public abstract GameObject[] GetAllEniemiesObjectsInRange();
    public abstract void OnDrawGizmosSelected();
}
