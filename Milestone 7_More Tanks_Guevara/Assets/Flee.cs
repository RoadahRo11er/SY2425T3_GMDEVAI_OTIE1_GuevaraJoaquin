using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Flee : NPCBaseFSM
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (opponent != null)
        {
            var direction = NPC.transform.position - opponent.transform.position;
            direction.y = 0;

            NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation,
                                                      Quaternion.LookRotation(direction),
                                                      rotspeed * Time.deltaTime);
            NPC.transform.Translate(0, 0, Time.deltaTime * speed);
        }
    }
}
