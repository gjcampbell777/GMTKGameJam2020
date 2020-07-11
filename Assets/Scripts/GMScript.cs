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

	void Start()
	{

		PlayerPrefs.SetInt("Score", 0);
		obstacleSpawnTime = Random.Range(5.0f, 15.0f);
		enemySpawnTime = Random.Range(5.0f, 15.0f);
		score = GameObject.Find("Score").GetComponent<Text>();

	}

    // Update is called once per frame
    void Update()
    {

    	score.text = "Score: " + PlayerPrefs.GetInt("Score");

    	if(GameObject.FindWithTag("Player") != null)
    	{
    		playerPos = GameObject.FindWithTag("Player").transform;
    	}

    	time += Time.deltaTime;
    	obstacleTime += Time.deltaTime;
    	enemyTime += Time.deltaTime;
        
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
