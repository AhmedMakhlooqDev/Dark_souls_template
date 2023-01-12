using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimatorBool : StateMachineBehaviour
{

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public string targetBool;
    public bool status;

    public string isRotatingWithRootMotion = "isRotatingWithRootMotion";
    public bool isRotatingWithRootMotionStatus = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(targetBool, status);
        animator.SetBool(isRotatingWithRootMotion, isRotatingWithRootMotionStatus);
    }

}
