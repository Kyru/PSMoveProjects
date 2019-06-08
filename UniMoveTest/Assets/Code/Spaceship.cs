using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    private UniMoveController move;
    private Rigidbody rigidbody;
    public float velocity = 10f;
    private bool canMove;

    float Zrotation = 0;

    // Start is called before the first frame update
    public void AlternativeStart()
    {
        rigidbody = GetComponent<Rigidbody>();
        canMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (move.GetButtonDown(PSMoveButton.Move))
        {
            Debug.Log("Hola amigos");
            move.ResetOrientation();
            canMove = true;
        }
        if (move.GetButtonDown(PSMoveButton.Circle))
        {
            velocity = velocity * 10;
        }
        if (move.GetButtonUp(PSMoveButton.Circle))
        {
            velocity = 10f;
        }
        /*
        // transform.Translate(Vector3.forward * velocity * Time.deltaTime);
        // rigidbody.AddForce(transform.forward * velocity);
        if (canMove)
        {
            rigidbody.velocity = transform.forward * velocity;

            Debug.Log("transform rotation start " + transform.rotation);
            Debug.Log("move Orientation start " + move.Orientation);

            Vector3 moveOrientation = move.Orientation.eulerAngles;
            Vector3 newRotation = transform.rotation.eulerAngles;

            Debug.Log("move Orientation euler angles " + moveOrientation);
            Debug.Log("transform rotation euler angles" + newRotation);

            float y = Mathf.Round(move.Orientation.y * 100f) / 100f;

            Debug.Log("y not rounded: " + move.Orientation.y);
            Debug.Log("y rounded " + y);

            // if (y > 0 || y <= 0.1f)
            // {
            // Debug.Log("entro dentro del if");
            newRotation.x += (y * 15);
            // }
            // else if (y == 0.2f) transform.rotation = Quaternion.Euler(rotation * 1.5f);

            Debug.Log("newRotation after if " + newRotation);

            transform.rotation = Quaternion.Euler(newRotation);

            Debug.Log("transform rotation al final " + transform.rotation);

            // transform.rotation = move.Orientation;
        }
        */

        if (canMove)
        {
            rigidbody.velocity = transform.forward * velocity;

            float Ymove = move.Orientation.y; //probar mas adelante con el Round a ver si funciona mejor o no
            float Xrotation = transform.rotation.x;

            if (move.GetButtonDown(PSMoveButton.Square))
            {
                Zrotation = 1;
            }
            if (move.GetButtonDown(PSMoveButton.Triangle))
            {
                Zrotation = -1;
            }
            if (move.GetButtonUp(PSMoveButton.Triangle) || move.GetButtonUp(PSMoveButton.Square))
            {
                Zrotation = 0;
            }

            transform.Rotate(move.Orientation.x * 10f, move.Orientation.y * 10f, Zrotation);

        }
    }

    public UniMoveController Move
    {
        get { return move; }
        set { move = value; }
    }

}
