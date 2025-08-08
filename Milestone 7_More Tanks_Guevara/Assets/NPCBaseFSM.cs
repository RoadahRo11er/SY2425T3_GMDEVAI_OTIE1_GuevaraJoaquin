using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPCBaseFSM : StateMachineBehaviour
{
    public GameObject NPC;
    public GameObject opponent;
    public float speed = 2.0f;
    public float rotspeed = 1f;
    public float accuracy = 3.0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        opponent = NPC.GetComponent<TankAI>().GetPlayer();
    }
}
