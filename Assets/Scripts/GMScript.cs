using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GMScript : MonoBehaviour
{
    
    public GameObject obstacle;
    public GameObject enemy;

    private float obstacleSpawnTime;
    private float obstacleTime = 0.0f;
    private float enemySpawnTime;
    private float enemyTime = 0.0f;
    private float time = 0.0f;
    private float gameOverTime;
    private Transform playerPos;
    private Text score;
    private Text timer;
    private Text gameOver;
    private Text newHighScore;
    private Text restartPrompt;
    private Text highScore;
    private Text escapePrompt;

	void Start()
	{

		PlayerPrefs.SetInt("Score", 0);
		obstacleSpawnTime = Random.Range(5.0f, 10.0f);
		enemySpawnTime = Random.Range(5.0f, 10.0f);
		score = GameObject.Find("Score").GetComponent<Text>();
		timer = GameObject.Find("Timer").GetComponent<Text>();
		gameOver = GameObject.Find("Game Over").GetComponent<Text>();
		newHighScore = GameObject.Find("New High Score?").GetComponent<Text>();
		restartPrompt = GameObject.Find("Restart Prompt").GetComponent<Text>();
		highScore = GameObject.Find("High Score").GetComponent<Text>();
		escapePrompt = GameObject.Find("Escape Prompt").GetComponent<Text>();

	}

    // Update is called once per frame
    void Update()
    {

    	if (Input.GetKeyDown("r")) { 
        	SceneManager.LoadScene("Scene"); 
     	}

    	if(GameObject.FindWithTag("Player") != null)
    	{

    		time += Time.deltaTime;
	    	obstacleTime += Time.deltaTime;
	    	enemyTime += Time.deltaTime;

    		playerPos = GameObject.FindWithTag("Player").transform;
    	
    	} else {

    		gameOverTime = time;
    		timer.text = "Time: " + gameOverTime.ToString("F1");
    		gameOver.text = "Game Over!";
    		restartPrompt.text = "Press 'R' to \n try again!";
    		escapePrompt.text = "Hit 'esc' to return to the main menu";

    		if(PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("HighScore")
    			|| (PlayerPrefs.GetInt("Score") == PlayerPrefs.GetInt("HighScore")
    				&& gameOverTime > PlayerPrefs.GetFloat("HighTime")))
    		{

    			newHighScore.text = "New High Score!";

    			PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));
    			PlayerPrefs.SetFloat("HighTime", gameOverTime);

    		}

    		highScore.text = "High Score:\n Score: " 
    		+ PlayerPrefs.GetInt("HighScore") 
    		+ "\n Time: " + PlayerPrefs.GetFloat("HighTime").ToString("F1");

    	}

    	score.text = "Score: " + PlayerPrefs.GetInt("Score");
    	timer.text = "Time: " + time.ToString("F1");
        
    	if(obstacleTime > obstacleSpawnTime)
    	{
    		
    		GameObject newObstacle = Instantiate(obstacle, ValidSpawn(), 
    			Quaternion.Euler(0,0,Random.Range(0.0f, 360.0f)));
    		newObstacle.transform.localScale = new Vector2(Random.Range(1.0f, 8.0f), 1.0f);
    		obstacleSpawnTime = spawnTime();
    		obstacleTime = 0.0f;
    	}

    	if(enemyTime > enemySpawnTime)
    	{
    		
    		GameObject newEnemy = Instantiate(enemy, ValidSpawn(), 
    			Quaternion.identity);
    		float scaleChange = Random.Range(0.5f, 1.5f);
    		newEnemy.transform.localScale = new Vector2(scaleChange, scaleChange);
    		enemySpawnTime = spawnTime();
    		enemyTime = 0.0f;
    	}

    	if(GameObject.FindWithTag("Enemy") == null && enemySpawnTime - enemyTime > 3.0f)
    	{
    		enemySpawnTime = enemyTime + Random.Range(1.0f, 3.0f);
    	}

    }

    float spawnTime()
    {


    	if((PlayerPrefs.GetInt("Score") > 3 && PlayerPrefs.GetInt("Score") < 7)
    		|| (time > 30.0f && time < 45.0f))
    	{

    		return Random.Range(3.0f, 8.0f);

		} else if (PlayerPrefs.GetInt("Score") >= 7 || time >= 45.0f){

			return Random.Range(3.0f, 5.0f);

		}

    	return Random.Range(5.0f, 10.0f);

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
