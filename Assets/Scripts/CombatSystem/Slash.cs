using UnityEngine;

public class Slash : AttackBase
{
    [SerializeField] private DamageOnCollision wapen;
    private int _comboCount = 0;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource slash;
    
    //combo system
    private static readonly int Animation = Animator.StringToHash("Attack");
    private readonly string[] _animatorNames = {"attack1", "attack2", "attack3"};
    public bool canClick = true;

    private void Start()
    {
        if (wapen == null)
        {
            Debug.LogError("No DamageOnCollision class found!");
            return;
        }
        wapen.SetDamageValues(minDamage, maxDamage);
        wapen.IsActive = false;
    }

    public override void Use()
    {
        //bugg fix (make sure to check if we are still in the combo)        
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle")) Idle();
        //check if we are in the idle animation
        if (_comboCount == 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("idle")) return;
        
        //standard logic for attack and combo start.
        if (!canClick) return;
        _comboCount++;
        if (_comboCount == 1)
        {
            wapen.IsActive = true;
            animator.SetInteger(Animation, 1);
        }
    }

    public override void ComboCheck()
    {
        canClick = false;

        for (int i = 0; i < _animatorNames.Length; i++)
        {
            //zo lang we nog niet voorbij de max count zijn kunnen we nog checken voor nog een combo en anders terug naar idle.
            if (i >= _animatorNames.Length - 1)
            {
              Idle();
              return;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(_animatorNames[i]) && _comboCount > (i + 1))
            {
                //COMBO
                NextAttack(i);
                return;
            }
        }
        Idle();
    }

    private void Idle()
    {
        animator.SetInteger(Animation, 0);
        _comboCount = 0;
        canClick = true;
        wapen.IsActive = false;
    }

    private void NextAttack(int i)
    {
        wapen.IsActive = true;
        animator.SetInteger(Animation, i + 2); //making sure we do the next move count: 2,3,4,5,6 etc.. move 1 is the first move before this is called
        canClick = true;
    }

    //sounds
    public void PlaySlashSound()
    {
        // slashSound.Play();
        slash.Play();
    }
}
