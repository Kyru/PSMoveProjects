using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotator : MonoBehaviour
{
    private int randRotationType;
    private int speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 50;
        randRotationType = Random.Range(0, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (randRotationType == 0)
        {
            transform.Rotate(Vector3.right * speed * Time.deltaTime);
        }
        else if (randRotationType == 1)
        {
            transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
