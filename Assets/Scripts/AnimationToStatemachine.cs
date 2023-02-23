using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStatemachine : MonoBehaviour
{
    public AttackState attackState;

    public void TriggerAttack()
    {
        attackState.TriggerAttack();
    }
    
    public void FinishAttack()
    {
        attackState.FinishAttack();
    }
}
