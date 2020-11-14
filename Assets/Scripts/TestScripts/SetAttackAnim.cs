using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAttackAnim : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Attack1", false);
    }
}
