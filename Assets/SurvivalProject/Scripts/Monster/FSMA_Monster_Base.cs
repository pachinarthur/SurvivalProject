using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMA_Monster_Base : StateMachineBehaviour
{
    [SerializeField] protected FSMA_Monster_Brain brain = null;
    [SerializeField] protected Color debugColor = Color.black;

    public Color DebugColor => debugColor;

    public void Init(FSMA_Monster_Brain _brain)
    {
        brain = _brain;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        brain.SetColor(debugColor);
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }
}
