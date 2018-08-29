using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public Player player;
    public GameObject scoreAreaPrefab;
    public GameObject obstaclePrefab;
    public GameObject obstacleContainer;
    public float holeSize;
    public float randomHoleOffset;

    public float spawnDistance;
    public float spawnOffset;
    public Text titleText;
    public Text instructionText;
    public Text scoreText;
    public Text gameOverText;

    private float spawnPointer = 0f;
    private int score = 0;
    private bool GameOver;

	// Use this for initialization
	void Start () {
        spawnPointer = -spawnDistance * 2;
        titleText.gameObject.SetActive(true);
        instructionText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);

        player.OnScore += OnScore;
        player.OnKill += OnKill;
	}
	
	// Update is called once per frame
	void Update () {
        // spawn
        if (player != null)
        {
            if (player.GetComponent<Rigidbody2D>().velocity.x > 0)
            {
                titleText.gameObject.SetActive(false);
                instructionText.gameObject.SetActive(false);
                scoreText.gameObject.SetActive(true);
            }
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
        if (GameOver) {
            if (Input.GetAxis("Fire1") == 1f)
            {
                SceneManager.LoadScene("Game");
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
        obstacleTop.transform.localEulerAngles = new Vector3(0, 0, 180);

        GameObject obstacleBottom = Instantiate(obstaclePrefab);
        obstacleBottom.transform.SetParent(obstacleContainer.transform);
        obstacleBottom.transform.position = new Vector2(
            x,
            -holeSize / 2 + holeOffset);

        GameObject scoreArea = Instantiate(scoreAreaPrefab);
        scoreArea.transform.SetParent(obstacleContainer.transform);
        scoreArea.transform.position = new Vector2(x, holeOffset);
    }

    void OnScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }
    void OnKill()
    {
        scoreText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        gameOverText.text = string.Format(gameOverText.text, score);

        GameOver = true;
    }
}
