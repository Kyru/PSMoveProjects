using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    private UniMoveController move;
    private Rigidbody rigidbody;
    public float actualVelocity = 100f;
    public float standardVelocity = 100f;
    public float turboVelocity = 1000f;
    private bool canMove;
    [SerializeField] private Camera insideCamera;
    [SerializeField] private Camera outsideCamera;
    [SerializeField] private GameObject bulletSpawnerLeft;
    [SerializeField] private GameObject bulletSpawnerRight;
    [SerializeField] private GameObject bullet;

    float Zrotation = 0;

    // Start is called before the first frame update
    public void AlternativeStart()
    {
        rigidbody = GetComponent<Rigidbody>();
        canMove = false;
        insideCamera.enabled = false;
        outsideCamera.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (move.GetButtonDown(PSMoveButton.Move))
        {
            move.ResetOrientation();
            canMove = true;
        }
        if (move.GetButtonDown(PSMoveButton.Circle))
        {
            actualVelocity = turboVelocity;
        }
        if (move.GetButtonUp(PSMoveButton.Circle))
        {
            actualVelocity = standardVelocity;
        }
        if (move.GetButtonDown(PSMoveButton.Cross))
        {
            insideCamera.enabled = !insideCamera.enabled;
            outsideCamera.enabled = !outsideCamera.enabled;
        }
        if (move.Trigger > 0)
        {
            // Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent);
            Instantiate(bullet, bulletSpawnerLeft.transform.position, bulletSpawnerLeft.transform.rotation);
            Instantiate(bullet, bulletSpawnerRight.transform.position, bulletSpawnerRight.transform.rotation);
        }

        if (canMove)
        {
            rigidbody.velocity = transform.forward * actualVelocity;

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
