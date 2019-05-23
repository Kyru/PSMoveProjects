using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCube : MonoBehaviour
{
    [SerializeField] List<Material> listMaterials;
    public float speed;

    void Start(){
        int randMaterial = Random.Range(0,4);
        GetComponent<Renderer>().material = listMaterials[randMaterial];
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed);
    }
}
