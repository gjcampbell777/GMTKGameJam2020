using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    
    private float speed;
 	private GameObject player;

 	void Start()
 	{
 		player = GameObject.FindWithTag("Player");
 		speed = Random.Range(0.5f, 3.0f);
 	}

    // Update is called once per frame
    void Update()
    {
        
    	if(player != null)
    	{
    		transform.position = Vector2.MoveTowards(
    			transform.position, player.transform.position, speed * Time.deltaTime);
    	}

    }

    void OnCollisionEnter2D(Collision2D other)
   {

   		if(other.gameObject.tag == "Bullet" || other.gameObject.tag == "Player")
   		{
   			Destroy(gameObject);
   		}
       
   }
}
