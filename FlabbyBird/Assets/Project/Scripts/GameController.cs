using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Player player;
    public GameObject obstaclePrefab;
    public GameObject obstacleContainer;
    public float holeSize;
    public float randomHoleOffset;

	// Use this for initialization
	void Start () {
        SpawnObstacle(10);
	}
	
	// Update is called once per frame
	void Update () {
		
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
