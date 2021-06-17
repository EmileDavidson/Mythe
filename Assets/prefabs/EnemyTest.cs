using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public Animator anim;
    bool isWalking;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("IsWalking", true);
            isWalking = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("IsWalking", false);
            isWalking = false;
        }

    }
}
