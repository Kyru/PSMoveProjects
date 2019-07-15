using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    [SerializeField] private GameObject target;
    private float speed  = 5f;
    private Quaternion lookRotation;
    private Vector3 direction;

    // Update is called once per frame
    void Update()
    {
        //find the vector pointing from our position to the target
        direction = (target.transform.position - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        lookRotation = Quaternion.LookRotation(direction);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
    }
}
