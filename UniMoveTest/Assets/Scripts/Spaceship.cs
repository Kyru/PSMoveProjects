using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spaceship : MonoBehaviour
{
    private UniMoveController move;
    private Rigidbody rigidbody;
    public float actualVelocity = 100f;
    public float standardVelocity = 100f;
    public float turboVelocity = 1000f;
    private float Zrotation = 0;
    private bool canMove;
    public int playerNum;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private bool isGameOver;
    private string player;
    private bool circleButtonDown;
    private bool isReloading;

    // explosion
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject spaceshipBody;
    [SerializeField] private GameObject pyramid;

    // cameras
    [SerializeField] private Camera outsideCamera;      // 0   
    [SerializeField] private Camera insideCamera;       // 1
    [SerializeField] private Camera topCamera;          // 2
    [SerializeField] private Camera farCamera;          // 3
    private int activeCamera;
    [SerializeField] private GameObject cockpit;

    // bullets
    [SerializeField] private GameObject bulletSpawnerLeft;
    [SerializeField] private GameObject bulletSpawnerRight;
    [SerializeField] private GameObject bullet;

    // canvas
    [SerializeField] private GameObject carefulMessage;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject shootBar;
    [SerializeField] private GameObject turboBar;
    private float maxBar = 1f;


    void Awake()
    {
        Messenger.AddListener(GameEvent.GAME_OVER, gameOver);
    }

    public void AlternativeStart()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        rigidbody = GetComponent<Rigidbody>();
        canMove = false;
        isGameOver = false;

        insideCamera.enabled = false;
        topCamera.enabled = false;
        farCamera.enabled = false;
        outsideCamera.enabled = true;
        canvas.GetComponent<Canvas>().worldCamera = outsideCamera;

        activeCamera = 0;

        cockpit.SetActive(false);
        carefulMessage.SetActive(false);
        explosion.SetActive(false);

        if (playerNum == 1) player = "Player1";
        else if (playerNum == 2) player = "Player2";

        shootBar.GetComponent<Image>().fillAmount = maxBar;
        turboBar.GetComponent<Image>().fillAmount = maxBar;
        circleButtonDown = false;
        isReloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver && move != null)
        {
            if (move.GetButtonDown(PSMoveButton.Move))
            {
                move.ResetOrientation();
                canMove = true;
            }

            if (move.GetButtonDown(PSMoveButton.Circle))
            {
                circleButtonDown = true;
            }

            if (circleButtonDown)
            {
                turboBar.GetComponent<Image>().fillAmount -= 0.2f;
                actualVelocity = turboVelocity;
                if (turboBar.GetComponent<Image>().fillAmount <= 0)
                {
                    actualVelocity = standardVelocity;
                    circleButtonDown = false;
                }
            }

            turboBar.GetComponent<Image>().fillAmount += 0.01f;

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
                if (!isReloading)
                {
                    if (shootBar.GetComponent<Image>().fillAmount > 0)
                    {
                        shootBar.GetComponent<Image>().fillAmount -= 0.01f;
                        Debug.Log("fill amount trigger " + shootBar.GetComponent<Image>().fillAmount);
                        Instantiate(bullet, bulletSpawnerLeft.transform.position, bulletSpawnerLeft.transform.rotation);
                        Instantiate(bullet, bulletSpawnerRight.transform.position, bulletSpawnerRight.transform.rotation);
                    }
                }
            }

            if (shootBar.GetComponent<Image>().fillAmount <= 0 && !isReloading)
            {
                StartCoroutine("reloadBar");
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
    }

    IEnumerator reloadBar()
    {
        isReloading = true;
        yield return new WaitForSeconds(3f);
        shootBar.GetComponent<Image>().fillAmount = maxBar;
        isReloading = false;
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
            Messenger<int>.Broadcast(GameEvent.MINUS_LIFE, playerNum);
            triggerExplosion();
        }
        else if (other.gameObject.tag == "MapAlert")
        {
            carefulMessage.SetActive(true);
        }
        else if (other.gameObject.tag == "Asteroid")
        {
            Messenger<int>.Broadcast(GameEvent.MINUS_LIFE, playerNum);
            triggerExplosion();
        }
        else if (other.gameObject.tag == "BulletP1")
        {
            if (this.gameObject.name != "Player1")
            {
                Messenger<int>.Broadcast(GameEvent.MINUS_LIFE, playerNum);
                triggerExplosion();
            }
        }
        else if (other.gameObject.tag == "BulletP2")
        {
            if (this.gameObject.name != "Player2")
            {
                Messenger<int>.Broadcast(GameEvent.MINUS_LIFE, playerNum);
                triggerExplosion();
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        // The one who manages collisions between players is Player1
        if (other.gameObject.name == "Player1" && this.gameObject.name != "Player1")
        {
            Messenger.Broadcast(GameEvent.MINUS_LIFE_BOTH);
            triggerExplosion();
            other.gameObject.GetComponent<Spaceship>().triggerExplosion();
        }
        /*
        else if (other.gameObject.name == "Player2" && this.gameObject.name != "Player2")
        {
            Messenger.Broadcast(GameEvent.MINUS_LIFE_BOTH);
            triggerExplosion();
            Invoke("respawn", 2f);
        } */
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MapAlert")
        {
            carefulMessage.SetActive(false);
        }
    }

    public void triggerExplosion()
    {
        canMove = false;
        rigidbody.velocity = new Vector3(0, 0, 0);

        spaceshipBody.SetActive(false);
        explosion.SetActive(true);
        pyramid.SetActive(false);

        foreach (Collider c in GetComponents<Collider>())
        {
            c.enabled = false;
        }

        Invoke("respawn", 2f);
    }

    void respawn()
    {
        foreach (Collider c in GetComponents<Collider>())
        {
            c.enabled = true;
        }

        spaceshipBody.SetActive(true);
        explosion.SetActive(false);
        pyramid.SetActive(true);

        transform.position = startPosition;
        transform.rotation = startRotation;

        shootBar.GetComponent<Image>().fillAmount = maxBar;
        turboBar.GetComponent<Image>().fillAmount = maxBar;

        canMove = false;
    }

    void gameOver()
    {
        rigidbody.velocity = new Vector3(0, 0, 0);
        isGameOver = true;
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.GAME_OVER, gameOver);
    }
}
