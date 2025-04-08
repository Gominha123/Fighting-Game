using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public AnimatorOverrideController animOV;
    public AnimLayer animLayer;
    public enum AnimLayer
    {
        BaseLayer = 0,
        oneHandSword = 1,
        twoHandSword = 2,
        hammer = 3,
    }
    
    public float damage;
    public float heavyDamage;

}
