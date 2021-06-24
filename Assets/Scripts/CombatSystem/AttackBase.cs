using UnityEngine;

public abstract class AttackBase : MonoBehaviour
{
    public int minDamage;
    public int maxDamage;
    public float attackSpeed;
    public float attackCooldown;
    private float _cooldownTime;
    public bool used = false;

    public int attackButtonClickCount = 0;
    
    public abstract void Use();
    public virtual void ComboCheck(){}


    public void Update()
    {
        if (!used) return;
        
        _cooldownTime += Time.deltaTime;
        
        if (_cooldownTime > attackCooldown)
        {
            //can use attack again
            _cooldownTime = 0;
            used = false;
        }
    }
    
    
}
