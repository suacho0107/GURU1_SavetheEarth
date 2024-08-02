using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : StateMachineBehaviour
{
    // 공격을 위해선, 공격 가능한 거리만큼 가까워져야함 (Follow -> Attack)
    Transform target;
    public float speed = 1f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // create movement: state update our enemy(be able to chase player)
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, target.position, speed * Time.deltaTime);
        Vector2 _lookDir = target.transform.position - animator.transform.position;
        animator.transform.up = new Vector2(_lookDir.x, _lookDir.y);
        float _distance = Vector2.Distance(animator.transform.position, target.position);

        if (_distance > 5)
        {
            animator.SetBool("isFollow", false);
        }
        //if (_distance < 1.5)
        //{
        //    // Attack 애니메이션이 종료된 뒤, Follow 애니메이션 출력
        //    animator.SetTrigger("Attack");
        //}

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // reset attack trigger
        // animator.ResetTrigger("Attack");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
