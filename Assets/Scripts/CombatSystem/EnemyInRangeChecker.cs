using System.Collections.Generic;
using UnityEngine;

public class EnemyInRangeChecker : RangeChecker
{
    [SerializeField] private float forwardOffset = 3;
    [SerializeField] float range = 10f;

    public override Enemy GetFirstEnemyInRange()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position + (transform.forward * forwardOffset), range, _layer);
        foreach (var c in cols)
        {
            if (cols.Length < 1) return null;
            return cols[0].GetComponent<Enemy>();
        }
        return null;
    }

    public override Enemy[] GetAllEnemiesInRange()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position + (transform.forward * forwardOffset), range, _layer);
        if (cols.Length < 1) return null;
        Enemy[] enemies = new Enemy[cols.Length];
        for (int i = 0; i < cols.Length; i++) enemies[i] = cols[i].GetComponent<Enemy>();
        return enemies;
    }

    public override GameObject[] GetAllEniemiesObjectsInRange()
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