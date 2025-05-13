using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour
{
    public List<Transform> locations = new List<Transform>();
    public float velocity = 0.1f;
    public bool canMove;
    public int currentTransform = 0;

    private void Update()
    {
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, locations[currentTransform].position, velocity);

            if (transform.position == locations[currentTransform].position)
            {
                if (currentTransform + 1 >= locations.Count)
                {
                    currentTransform = 0;
                }
                else
                {
                    currentTransform++;
                }
            }
        }
    }
}
