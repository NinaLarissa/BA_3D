using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationRutine : MonoBehaviour
{
    
    // Activates the pointer to better get an idea of where to shoot the Enemy
    
    public GameObject objectToActivate;

    // When the game starts, it should take 5s before the pointer appears (as long as the "try to survive" text is shown)

    void Start()
    {

        StartCoroutine(StartFunction());
    }

    private IEnumerator StartFunction()
    {
        // Wait for 5s
        yield return new WaitForSeconds(5);

        // turn my game object that is set to false (off) to true (on)
        objectToActivate.SetActive(true);
    }
}