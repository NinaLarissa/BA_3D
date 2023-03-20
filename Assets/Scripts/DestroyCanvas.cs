using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCanvas : MonoBehaviour
{

    //the canvas that will show "try to survive" will appear in the beginning of the game, before it will disappear after 5s

    public float lifetime = 5f;

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifetime);
    }
}