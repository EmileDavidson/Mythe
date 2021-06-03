using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Slash : AttackBase
{
    [SerializeField] protected RangeChecker _rangeChecker;

    public Slash(EnemyInRangeChecker checker)
    {
        _rangeChecker = checker;
    }

    public override void Use()
    {
        //making sure there are enemies to hit.
        List<GameObject> enemies = _rangeChecker.GetAllEniemiesObjectsInRange()?.ToList();
        if (enemies == null || enemies?.Count < 0) return;
        foreach (GameObject enemy in enemies)
        {
            //wat willen we doen met de enemy gameobject.
            enemy.GetComponent<Health>().RemoveHealth(Random.Range(minDamage, maxDamage));
        }

        // this.used = true;
    }
}
