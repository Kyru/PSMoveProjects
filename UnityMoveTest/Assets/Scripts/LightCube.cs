using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCube : MonoBehaviour
{
    [SerializeField] List<Material> listMaterials;
    public float speed;
    private Material currentMaterial;
    private int cubesInRow = 3;
    private float cubeSize = 2;

    // <F> For explosion
    float cubesPivotDistance;
    Vector3 cubesPivot;
    public int explosionForce;
    public int explosionRadius;
    public float explosionUpward;

    void Start()
    {
        int randMaterial = Random.Range(0, 4);
        GetComponent<Renderer>().material = listMaterials[randMaterial];
        currentMaterial = GetComponent<Renderer>().material;

        cubesPivotDistance = cubeSize * cubesInRow / 2;
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CubeDestroyer")
        {
            Messenger.Broadcast(GameEvent.MINUS_LIFE);
            Destroy(this.gameObject);
        }
    }

    public void cubeDestroyed()
    {
        gameObject.SetActive(false);
        for (int i = 0; i < cubesInRow; i++)
        {
            for (int j = 0; j < cubesInRow; j++)
            {
                for (int k = 0; k < cubesInRow; k++)
                {
                    createCube(i, j, k);
                }
            }
        }

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach(Collider hit in colliders){
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if(rb != null){
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
            }
        }

        Destroy(gameObject);

    }

    void createCube(int x, int y, int z)
    {
        GameObject cube;
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        cube.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        cube.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Rigidbody>().mass = cubeSize;
        cube.GetComponent<Renderer>().material = currentMaterial;
    }
}
