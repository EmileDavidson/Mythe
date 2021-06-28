using System.Collections.Generic;
using UnityEngine;

public class EnemyInRangeChecker : RangeChecker
{
    [SerializeField] private float forwardOffset = 3;
    [SerializeField] float range = 10f;

    public override GameObject GetFirstGameObjectInRange()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position + (transform.forward * forwardOffset), range, _layer);
        if (cols.Length < 1) return null;
        return cols[0].gameObject;
    }

    public override GameObject[] GetAllObjectsInRange()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position + (transform.forward * forwardOffset), range, _layer);
        if (cols.Length < 1) return null;
        List<GameObject> enemies = new List<GameObject>();
        for (int i = 0; i < cols.Length; i++) enemies.Add(cols[i].gameObject);
        return enemies.ToArray();
    }
    
    public override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (transform.forward * forwardOffset), range);
    }
}