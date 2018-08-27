using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Player player;
    public GameObject obstaclePrefab;
    public GameObject obstacleContainer;
    public float holeSize;
    public float randomHoleOffset;

    public float spawnDistance;
    public float spawnOffset;

    private float spawnPointer = 0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        // spawn
        if (player != null)
        {
            if (spawnPointer - player.transform.position.x < spawnDistance)
            {
                spawnPointer += spawnDistance;
                SpawnObstacle(spawnPointer + spawnOffset);
            }
            // clear
            for (int i = 0; i < obstacleContainer.transform.childCount; i++)
            {
                GameObject currentObstacle = obstacleContainer.transform.GetChild(i).gameObject;
                if (player.transform.position.x - currentObstacle.transform.position.x > spawnOffset)
                {
                    Destroy(currentObstacle);
                }
            }
        }
	}

    void SpawnObstacle(float x)
    {
        float holeOffset = Random.Range(-randomHoleOffset, randomHoleOffset);

        GameObject obstacleTop = Instantiate(obstaclePrefab);
        obstacleTop.transform.SetParent(obstacleContainer.transform);
        obstacleTop.transform.position = new Vector2(
            x,
            holeSize / 2 + holeOffset);

        GameObject obstacleBottom = Instantiate(obstaclePrefab);
        obstacleBottom.transform.SetParent(obstacleContainer.transform);
        obstacleBottom.transform.position = new Vector2(
            x,
            -holeSize / 2 + holeOffset);
    }
}
