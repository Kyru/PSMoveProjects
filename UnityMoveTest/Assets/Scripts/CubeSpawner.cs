using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    private bool spawnNext;
    private bool gameStarted;

    void Start()
    {
        spawnNext = true;
        gameStarted = false;
        Messenger.AddListener(GameEvent.START_GAME, startGame);
    }

    void startGame()
    {
        gameStarted = true;
    }

    void Update()
    {
        if (gameStarted)
        {
            if (spawnNext) StartCoroutine("SpawnEnemy");
        }
    }

    IEnumerator SpawnEnemy()
    {
        spawnNext = false;
        yield return new WaitForSeconds(3f);
        Instantiate(cube, transform.position, cube.transform.rotation, gameObject.transform);
        spawnNext = true;
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.START_GAME, startGame);
    }
}
