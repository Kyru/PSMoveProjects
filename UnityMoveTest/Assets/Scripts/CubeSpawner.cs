using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private List<Vector3> positions;
    [SerializeField] private List<Vector3> rotations;
    private bool spawnNext;
    private bool gameStarted;

    void Awake()
    {
        Messenger.AddListener(GameEvent.START_GAME, startGame);
    }

    void Start()
    {
        spawnNext = true;
        gameStarted = false;
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
        int randPosition = Random.Range(0, positions.Count);
        Debug.Log(randPosition);
        Instantiate(cube, positions[randPosition], transform.rotation, gameObject.transform);     // Quaternion.Euler(rotations[randPosition])
        // Instantiate(cube, transform.position, transform.rotation, gameObject.transform);
        spawnNext = true;
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.START_GAME, startGame);
    }
}
