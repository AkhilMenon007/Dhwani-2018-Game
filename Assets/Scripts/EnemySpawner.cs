using UnityEngine;
using System.Collections.Generic;
public class EnemySpawner : MonoBehaviour {

    GameController gameController;

    [SerializeField]
    GameObject[] enemyPrefab;
    [SerializeField]
    float spawnCutoff = 100f;
    [SerializeField]
    float spawnRate = 3f;

	// Use this for initialization
	void Start ()
    {
        gameController = GameController.gameController;
        InvokeRepeating("SpawnEnemy", 0f, spawnRate);
    }

    private void SpawnEnemy()
    {
        if (gameController.gameStarted)
        {
            while (true)
            {
                Transform spawnPoint = gameController.GenerateRandomTarget();
                if((spawnPoint.position-gameController.Player.transform.position).magnitude > spawnCutoff)
                {
                    Instantiate(enemyPrefab[Random.Range(0,enemyPrefab.Length)], spawnPoint.position, spawnPoint.rotation);
                    break;
                }
            }
        }
    }

}
