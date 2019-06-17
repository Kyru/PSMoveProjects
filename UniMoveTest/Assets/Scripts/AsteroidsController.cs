using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsController : MonoBehaviour
{
    void Start()
    {
        foreach (Transform t in transform)
        {
            t.gameObject.tag = "Asteroid";
        }
    }
}