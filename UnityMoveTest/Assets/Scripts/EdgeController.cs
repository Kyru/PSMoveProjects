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

            if (materialEdge == "Lightsaber Blue (Instance)" && materialCube == "CubeBlue (Instance)")
            {
                Messenger.Broadcast(GameEvent.ADD_SCORE);
                Destroy(other.gameObject);
            }
            else if (materialEdge == "Lightsaber Green (Instance)" && materialCube == "CubeGreen (Instance)")
            {
                Messenger.Broadcast(GameEvent.ADD_SCORE);
                Destroy(other.gameObject);
            }
            else if (materialEdge == "Lightsaber Red (Instance)" && materialCube == "CubeRed (Instance)")
            {
                Messenger.Broadcast(GameEvent.ADD_SCORE);
                Destroy(other.gameObject);
            }
            else if (materialEdge == "Lightsaber Purple (Instance)" && materialCube == "CubePurple (Instance)")
            {
                Messenger.Broadcast(GameEvent.ADD_SCORE);
                Destroy(other.gameObject);
            }
        }
    }
}
