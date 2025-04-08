using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Attacks/Normal attack")]
public class AttackSO : ScriptableObject
{
    public AnimatorOverrideController animOV;
    public float damage;
    public float animationsLayer;
    /*
     * visual effect
     * knockback
     * ...
     */
}
