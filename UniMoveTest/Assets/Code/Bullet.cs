using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rigidbody;
    public float velocity;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * 100;
    }

    // Update is called once per frame
    void Update()
    {
        //  transform.Translate(Vector3.forward * Time.deltaTime * velocity, Space.World);
        rigidbody.AddForce(transform.forward * velocity);
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.tag == "Asteroid"){
            Debug.Log("I should destroy this");
            Destroy(other.gameObject);
        }
    }
}
