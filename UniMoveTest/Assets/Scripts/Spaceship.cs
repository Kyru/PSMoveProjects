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
    public int playerNum;
    private Vector3 startPosition;
    private Quaternion startRotation;

    // cameras
    [SerializeField] private Camera outsideCamera;      // 0   
    [SerializeField] private Camera insideCamera;       // 1
    [SerializeField] private Camera topCamera;          // 2
    [SerializeField] private Camera farCamera;          // 3
    private int activeCamera;

    // bullets
    [SerializeField] private GameObject bulletSpawnerLeft;
    [SerializeField] private GameObject bulletSpawnerRight;
    [SerializeField] private GameObject bullet;

    private float Zrotation = 0;
    [SerializeField] private GameObject cockpit;
    // canvas
    [SerializeField] private GameObject carefulMessage;
    [SerializeField] private GameObject canvas;

    // Start is called before the first frame update
    public void AlternativeStart()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        rigidbody = GetComponent<Rigidbody>();
        canMove = false;

        insideCamera.enabled = false;
        topCamera.enabled = false;
        farCamera.enabled = false;
        outsideCamera.enabled = true;

        activeCamera = 0;

        cockpit.SetActive(false);
        carefulMessage.SetActive(false);
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
            switch (activeCamera)
            {
                case 0:
                    insideCamera.enabled = false;
                    topCamera.enabled = false;
                    farCamera.enabled = false;
                    outsideCamera.enabled = true;
                    canvas.GetComponent<Canvas>().worldCamera = outsideCamera;
                    break;
                case 1:
                    insideCamera.enabled = true;
                    topCamera.enabled = false;
                    farCamera.enabled = false;
                    outsideCamera.enabled = false;
                    canvas.GetComponent<Canvas>().worldCamera = insideCamera;
                    break;
                case 2:
                    insideCamera.enabled = false;
                    topCamera.enabled = false;
                    farCamera.enabled = true;
                    outsideCamera.enabled = false;
                    canvas.GetComponent<Canvas>().worldCamera = farCamera;
                    break;
                case 3:
                    insideCamera.enabled = false;
                    topCamera.enabled = true;
                    farCamera.enabled = false;
                    outsideCamera.enabled = false;
                    canvas.GetComponent<Canvas>().worldCamera = topCamera;
                    break;
            }

            if (insideCamera.enabled) cockpit.SetActive(true);
            else cockpit.SetActive(false);

            activeCamera++;
            if (activeCamera == 4) activeCamera = 0;
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MapLimit")
        {
            Debug.Log("Touching mapLimit");
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "MapAlert")
        {
            carefulMessage.SetActive(true);
        }
        else if (other.gameObject.tag == "Asteroid")
        {
            Messenger<int>.Broadcast(GameEvent.MINUS_LIFE, playerNum);
            respawn();
            // Destroy(this.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MapAlert")
        {
            carefulMessage.SetActive(false);
        }
    }

    void respawn()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}
