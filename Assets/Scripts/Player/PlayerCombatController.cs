using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    public List<AttackBase> attacks = new List<AttackBase>();
    public AttackBase currentAttack;
    public Animator animator;
    private static readonly int Hurt = Animator.StringToHash("hurt");
    private Health _health;
    private void Start()
    {
        if (animator == null) animator = GetComponent<Animator>();
        if (_health == null) _health = GetComponent<Health>();
        if (_health != null)
        {
            if (animator != null)
            {
                _health.OnHealthChanged.AddListener(value => SetHurt(true));
            }
        }

        
        currentAttack = attacks[0];
    }

    private void Update()
    {
        if (currentAttack == null) return;
        if (Input.GetKeyDown(KeyCode.Mouse0)) currentAttack.Use();
    }

    /// <summary>
    /// CheckCombo triggers function in child. we use this for animation clip events
    /// </summary>
    public void CheckCombo()
    {
        currentAttack.ComboCheck();
    }

    /// <summary>
    /// are we hurt true or false? -> set animator to state.
    /// </summary>
    /// <param name="value">0 = true, > 1 false</param>
    public void SetHurt(int value)
    {
        animator.SetBool(Hurt, value == 0);
    }

    /// <summary>
    /// are we hurt? -> set animator state.
    /// </summary>
    /// <param name="value">the bool if we are hurt or not.</param>
    public void SetHurt(bool value)
    {
        animator.SetBool(Hurt, value);
    }


}
