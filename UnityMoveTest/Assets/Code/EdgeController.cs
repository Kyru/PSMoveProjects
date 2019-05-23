using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CollisionCube")
        {
            string materialEdge = GetComponent<Renderer>().material.name;
            string materialCube = other.GetComponent<Renderer>().material.name;

            Debug.Log("materialedge = " + materialEdge + ", materialCube = " + materialCube);

            if (materialEdge == "Lightsaber Blue (Instance)" && materialCube == "CubeBlue (Instance)")
            {
                Destroy(other.gameObject);
            }
            else if (materialEdge == "Lightsaber Green (Instance)" && materialCube == "CubeGreen (Instance)")
            {
                Destroy(other.gameObject);
            }
            else if (materialEdge == "Lightsaber Red (Instance)" && materialCube == "CubeRed (Instance)")
            {
                Destroy(other.gameObject);
            }
            else if (materialEdge == "Lightsaber Purple (Instance)" && materialCube == "CubePurple (Instance)")
            {
                Destroy(other.gameObject);
            }
        }
    }
}
