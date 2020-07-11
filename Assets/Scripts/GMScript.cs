using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GMScript : MonoBehaviour
{
    
    public GameObject obstacle;
    public GameObject enemy;

    private float obstacleSpawnTime;
    private float obstacleTime = 0.0f;
    private float enemySpawnTime;
    private float enemyTime = 0.0f;
    private float time = 0.0f;
    private Transform playerPos;
    private Text score;
    private Text timer;
    private Text gameOver;
    private Text newHighScore;
    private Text restartPrompt;
    private Text highScore;

	void Start()
	{

		PlayerPrefs.SetInt("Score", 0);
		obstacleSpawnTime = Random.Range(5.0f, 15.0f);
		enemySpawnTime = Random.Range(5.0f, 15.0f);
		score = GameObject.Find("Score").GetComponent<Text>();
		timer = GameObject.Find("Timer").GetComponent<Text>();
		gameOver = GameObject.Find("Game Over").GetComponent<Text>();
		newHighScore = GameObject.Find("New High Score?").GetComponent<Text>();
		restartPrompt = GameObject.Find("Restart Prompt").GetComponent<Text>();
		highScore = GameObject.Find("High Score").GetComponent<Text>();

	}

    // Update is called once per frame
    void Update()
    {

    	time += Time.deltaTime;
    	obstacleTime += Time.deltaTime;
    	enemyTime += Time.deltaTime;

    	score.text = "Score: " + PlayerPrefs.GetInt("Score");
    	timer.text = "Time: " + time.ToString("F1");

    	if(GameObject.FindWithTag("Player") != null)
    	{

    		playerPos = GameObject.FindWithTag("Player").transform;
    	
    	} else {

    		gameOver.text = "Game Over!";
    		restartPrompt.text = "Press 'R' to \n try again!";

    		if(PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("HighScore")
    			|| (PlayerPrefs.GetInt("Score") == PlayerPrefs.GetInt("HighScore")
    				&& time > PlayerPrefs.GetFloat("HighTime")))
    		{

    			newHighScore.text = "New High Score!";

    			PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));
    			PlayerPrefs.SetFloat("HighTime", time);

    		}

    		highScore.text = "High Score:\n Score: " 
    		+ PlayerPrefs.GetInt("HighScore") 
    		+ "\n Time: " + PlayerPrefs.GetFloat("HighTime").ToString("F1");

    		PlayerPrefs.SetInt("Score", 0);
    		time = 0.0f;

    	}
        
    	if(obstacleTime > obstacleSpawnTime)
    	{
    		
    		GameObject newObstacle = Instantiate(obstacle, ValidSpawn(), 
    			Quaternion.Euler(0,0,Random.Range(0.0f, 360.0f)));
    		newObstacle.transform.localScale = new Vector2(Random.Range(1.0f, 8.0f), 1.0f);
    		obstacleSpawnTime = Random.Range(5.0f, 15.0f);
    		obstacleTime = 0.0f;
    	}

    	if(enemyTime > enemySpawnTime)
    	{
    		
    		GameObject newEnemy = Instantiate(enemy, ValidSpawn(), 
    			Quaternion.identity);
    		float scaleChange = Random.Range(0.5f, 1.5f);
    		newEnemy.transform.localScale = new Vector2(scaleChange, scaleChange);
    		enemySpawnTime = Random.Range(5.0f, 15.0f);
    		enemyTime = 0.0f;
    	}

    }

    Vector2 ValidSpawn()
    {

    	bool valid = false;
    	Vector2 spawn;

    	if(GameObject.FindWithTag("Player") == null)
    	{
    		return new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(-4.0f, 4.0f));
    	}

    	do{

    		spawn = new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(-4.0f, 4.0f));

    		valid = true;

    		if((spawn.x <= playerPos.position.x + 1 && spawn.x >= playerPos.position.x - 1)
    			|| (spawn.y <= playerPos.position.y + 1 && spawn.y >= playerPos.position.y - 1))
    		{
    			valid = false;
    		}

    	}while(!valid);

    	return spawn;

    }
}
