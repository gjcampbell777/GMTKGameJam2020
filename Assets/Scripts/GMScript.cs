using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMScript : MonoBehaviour
{
    
    public GameObject obstacle;
    public GameObject enemy;

    private float obstacleSpawnTime;
    private float obstacleTime = 0.0f;
    private float enemySpawnTime;
    private float enemyTime = 0.0f;

    private Transform playerPos;

	void Start()
	{

		playerPos = GameObject.FindWithTag("Player").transform;
		obstacleSpawnTime = Random.Range(5.0f, 15.0f);
		enemySpawnTime = Random.Range(5.0f, 15.0f);

	}

    // Update is called once per frame
    void Update()
    {

    	obstacleTime += Time.deltaTime;
    	enemyTime += Time.deltaTime;
        
    	if(obstacleTime > obstacleSpawnTime)
    	{
    		
    		GameObject newObstacle = Instantiate(obstacle, new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(-4.0f, 4.0f)), 
    			Quaternion.Euler(0,0,Random.Range(0.0f, 360.0f)));
    		newObstacle.transform.localScale = new Vector2(Random.Range(1.0f, 8.0f), 1.0f);
    		obstacleSpawnTime = Random.Range(5.0f, 15.0f);
    		obstacleTime = 0.0f;
    	}

    	if(enemyTime > enemySpawnTime)
    	{
    		
    		GameObject newEnemy = Instantiate(enemy, new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(-4.0f, 4.0f)), 
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

    	do{

    		spawn = new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(-4.0f, 4.0f));

    		if((spawn.x > playerPos.position.x + 4 || spawn.x < playerPos.position.x - 4)
    			&& (spawn.y > playerPos.position.y + 4 || spawn.y < playerPos.position.y - 4))
    		{
    			valid = true;
    		}

    	}while(!valid);

    	return spawn;

    }
}
