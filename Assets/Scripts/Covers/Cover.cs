using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    public Color color;
    private void Start()
    {
        color = Color.blue;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawCube(transform.position, Vector3.one * 0.3f);
    }
}
