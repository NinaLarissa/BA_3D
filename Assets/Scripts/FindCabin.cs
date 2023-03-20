using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCabin : MonoBehaviour
{

    public GameObject FindTheCabin;

    // the GameObject that includes the UI text is set inactive at the beginning

    private void Start()
    {
        FindTheCabin.SetActive(false);
    }

    // when the tagged Player is entering the collider object, the UI text will appear for 5s, before it will be destroyed

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindTheCabin.SetActive(true);
            Destroy(gameObject, 10);
        }
    }
}
