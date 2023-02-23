using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newChooseAttackStyleStateData", menuName = "Data/State Data/Choose Attack Style State Data")]
public class D_ChooseAttackStyleState : ScriptableObject
{
    // weight use for problability of attack style decision
    public float weightNA = 0.8f; // weight of Normal Attack
    public float weightSA = 0.2f; // weight of Special Attack
}
