using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    
    private float speed;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    	speed = Random.Range(3.0f, 8.0f);
    }

    void FixedUpdate()
    {

    	rb.velocity = transform.up * speed;

    }

    void OnCollisionEnter2D(Collision2D other)
    {
   
   		if(other.gameObject.tag != "Player" && other.gameObject.tag != "Bullet")
   		{
   			Destroy(gameObject);
   		}
       
    }

   	void OnBecameInvisible() 
    {
   		Destroy(gameObject);
    }
}
