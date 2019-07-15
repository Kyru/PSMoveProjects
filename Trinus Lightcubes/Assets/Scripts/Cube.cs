using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{

    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material.SetColor("_Color", Color.red);
        Debug.Log(gameObject.tag);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sphere")
        {
            renderer.material.SetColor("_Color", Color.green);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Sphere")
        {
            renderer.material.SetColor("_Color", Color.red);
        }
    }
}
