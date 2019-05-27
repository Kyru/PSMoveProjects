using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller class which uses the LightsaberTrail class to show a trail when we move the lightsaber
/// </summary>
[RequireComponent(typeof(LightsaberTrail))]
public class LightsaberTrailController : MonoBehaviour
{

    LightsaberTrail lightsaberTrail;

    void Start()
    {
        lightsaberTrail = GetComponent<LightsaberTrail>();
    }

    void Update()
    {
        lightsaberTrail.Iterate(Time.time);
        lightsaberTrail.UpdateTrail(Time.time, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CollisionCube")
        {
            string materialTrail = GetComponent<Renderer>().material.name;
            string materialCube = other.GetComponent<Renderer>().material.name;

            if (materialTrail == "Lightsaber Blue (Instance)" && materialCube == "CubeBlue (Instance)")
            {
                Messenger.Broadcast(GameEvent.ADD_SCORE);
                Destroy(other.gameObject);
            }
            else if (materialTrail == "Lightsaber Green (Instance)" && materialCube == "CubeGreen (Instance)")
            {
                Messenger.Broadcast(GameEvent.ADD_SCORE);
                Destroy(other.gameObject);
            }
            else if (materialTrail == "Lightsaber Red (Instance)" && materialCube == "CubeRed (Instance)")
            {
                Messenger.Broadcast(GameEvent.ADD_SCORE);
                Destroy(other.gameObject);
            }
            else if (materialTrail == "Lightsaber Purple (Instance)" && materialCube == "CubePurple (Instance)")
            {
                Messenger.Broadcast(GameEvent.ADD_SCORE);
                Destroy(other.gameObject);
            }
        }
    }
}
