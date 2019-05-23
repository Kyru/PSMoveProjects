using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeController : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "CollisionCube"){
            Debug.Log("he chocado con el collisioncube");
        }    
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.tag == "CollisionCube"){
            Debug.Log("He salido del collisionCube");
        }
    }
}
