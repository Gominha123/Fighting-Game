using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public AnimLayer animLayer;
    public enum AnimLayer
    {
        BaseLayer = 0,
        oneHandSword = 1,
        twoHandSword = 2,
        hammer = 3,
    }

    public bool canHit;
    public float damage;
    public float heavyDamage;
}
