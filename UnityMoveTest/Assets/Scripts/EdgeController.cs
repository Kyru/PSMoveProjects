using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeController : MonoBehaviour
{
    [SerializeField] int player;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CollisionCube")
        {
            string materialEdge = GetComponent<Renderer>().material.name;
            string materialCube = other.GetComponent<Renderer>().material.name;

            if (materialEdge == "Lightsaber Blue (Instance)" && materialCube == "CubeBlue (Instance)")
            {
                Messenger<int>.Broadcast(GameEvent.ADD_SCORE, player);
                other.gameObject.GetComponent<LightCube>().cubeDestroyed();
            }
            else if (materialEdge == "Lightsaber Green (Instance)" && materialCube == "CubeGreen (Instance)")
            {
                Messenger<int>.Broadcast(GameEvent.ADD_SCORE, player);
                other.gameObject.GetComponent<LightCube>().cubeDestroyed();
            }
            else if (materialEdge == "Lightsaber Red (Instance)" && materialCube == "CubeRed (Instance)")
            {
                Messenger<int>.Broadcast(GameEvent.ADD_SCORE, player);
                other.gameObject.GetComponent<LightCube>().cubeDestroyed();
            }
            else if (materialEdge == "Lightsaber Purple (Instance)" && materialCube == "CubePurple (Instance)")
            {
                Messenger<int>.Broadcast(GameEvent.ADD_SCORE, player);
                other.gameObject.GetComponent<LightCube>().cubeDestroyed();
            }
        }
    }
}
