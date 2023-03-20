using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTargetReached : MonoBehaviour
{

    // NOT IN USE BECAUSE I DON'T USE THE SLIDER OF THE GUN


    public float threshold = 0.2f;
    public Transform target;
    public UnityEvent OnReached;
    private bool wasReached = false;

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if(distance < threshold && !wasReached)
        {
            //Reached the target
            OnReached.Invoke();
            wasReached = true;
        }
        else if(distance >= threshold)
        {
            wasReached = false;
        }
    }
}