using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    private bool spawnNext = true;
    void Update()
    {
        if (spawnNext) StartCoroutine("SpawnEnemy");
    }

    IEnumerator SpawnEnemy()
    {
            spawnNext = false;
            yield return new WaitForSeconds(3f);
            Instantiate(cube, transform.position, cube.transform.rotation, gameObject.transform);
            spawnNext = true;
    }
}
