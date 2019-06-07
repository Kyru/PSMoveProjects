using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    private UniMoveController move;
    private Rigidbody rigidbody;
    private float velocity = 2f;

    // Start is called before the first frame update
    public void AlternativeStart()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (move.GetButtonDown(PSMoveButton.Move))
        {
            move.ResetOrientation();
        }

        // transform.Translate(Vector3.forward * velocity * Time.deltaTime);
        rigidbody.AddForce(transform.forward * velocity);

        transform.rotation = move.Orientation;
    }

    public UniMoveController Move
    {
        get { return move; }
        set { move = value; }
    }

}
