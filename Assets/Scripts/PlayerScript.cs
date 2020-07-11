﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    
	public float speed;

	public GameObject bullet;

	private float translation;
    private float rotation;
    private float nextFire = 0.5f;
    private float myTime = 0.0f;
    private GameObject reloaded;
	private Rigidbody2D rb;

	void Awake()
	{
		rb = this.GetComponent<Rigidbody2D>();
		reloaded = GameObject.Find("Reloaded");
	}

    void Update()
    {
     
     	reloaded.SetActive(false);
     	myTime += Time.deltaTime;

     	if(myTime > nextFire)
     	{
     		reloaded.SetActive(true);
     	}

        if(Input.GetButton("Fire1") && myTime > nextFire)
        {
        	nextFire = myTime + 0.5f;
        	GunFire(Random.Range(0.0f, 360.0f));
        	nextFire = nextFire - myTime;
            myTime = 0.0f;
        }

    }

    void FixedUpdate()
    {

    	rb.velocity = 
    	new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);

    }

    void OnCollisionEnter2D(Collision2D other)
   {

   		if(other.gameObject.tag == "Bullet")
   		{
   			Physics2D.IgnoreCollision(
   				other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
   		}

   		if(other.gameObject.tag == "Enemy")
   		{
   			Destroy(gameObject);
   		}
       
   }

   void GunFire(float aim)
   {

   		int gunSelect = 0;
   		GameObject newBullet;
   		float scaleChange = 1.0f;
   		Vector3 pos = PlayerCircle(transform.position);
   		Quaternion rot = Quaternion.FromToRotation(Vector3.down, transform.position-pos);

   		if(Random.Range(0,10) == 0)
   		{

   			gunSelect = Random.Range(1,4);

   		}

   		switch(gunSelect)
   		{

   			//pistol
   			case 0:
   				newBullet = Instantiate(bullet, pos, rot);
    			scaleChange = Random.Range(0.5f, 1.5f);
    			newBullet.transform.localScale = new Vector2(scaleChange, scaleChange);
   				break;
   			//shotgun
   			case 1:
   				newBullet = Instantiate(bullet, pos, rot);
    			scaleChange = Random.Range(0.5f, 1.5f);
    			newBullet.transform.localScale = new Vector2(scaleChange, scaleChange);
    			newBullet = Instantiate(bullet, pos+((Vector3.up+Vector3.right)*0.75f), Quaternion.FromToRotation(Vector3.down, transform.position-(pos+((Vector3.up+Vector3.right)*0.75f))));
    			newBullet.transform.localScale = new Vector2(scaleChange, scaleChange);
    			newBullet = Instantiate(bullet, pos+((Vector3.down+Vector3.left)*0.75f), Quaternion.FromToRotation(Vector3.down, transform.position-(pos+((Vector3.down+Vector3.left)*0.75f))));
    			newBullet.transform.localScale = new Vector2(scaleChange, scaleChange);
   				break;
   			//machinegun
   			case 2:
   				newBullet = Instantiate(bullet, pos, rot);
    			scaleChange = Random.Range(0.5f, 1.5f);
    			newBullet.transform.localScale = new Vector2(scaleChange, scaleChange);
    			for(int i = 1 ; i < 5; i++)
    			{
    				newBullet = Instantiate(bullet, pos+(Vector3.down+Vector3.left)*(0.1f*i), Quaternion.FromToRotation(Vector3.down, transform.position-(pos+(Vector3.down+Vector3.left)*(0.1f*i))));
    				newBullet.transform.localScale = new Vector2(scaleChange, scaleChange);
    			}
   				break;
   			//railgun
   			case 3:
    			scaleChange = Random.Range(0.5f, 1.5f);
    			for(int i = 0 ; i < 100; i++)
    			{
    				newBullet = Instantiate(bullet, pos, rot);
    				newBullet.transform.localScale = new Vector2(scaleChange, scaleChange);
    			}
   				break;

   		}

   		Vector3 PlayerCircle (Vector3 center){
        	float radius = 1.25f;
        	float ang = Random.value * 360;
        	Vector3 circle;

	        circle.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
	        circle.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
	        circle.z = 0;
         
        	return circle;
	    }

   }

}
