using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{

    // triggers the next scene, when the Player gets to the Objects (red ball)


    public SceneSwitch sceneSwitch;
    public int scene;

    // Start is called before the first frame update
    void Start()
    {
        sceneSwitch.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sceneSwitch.enabled = true;
            {
                SceneManager.LoadScene(scene);
            }
        }
    }
}
