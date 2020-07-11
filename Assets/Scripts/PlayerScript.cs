using System.Collections;
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
	private Rigidbody2D rb;

	void Awake()
	{
		rb = this.GetComponent<Rigidbody2D>();
	}

    void Update()
    {
     
     	myTime += Time.deltaTime;

        if(Input.GetButton("Fire1") && myTime > nextFire)
        {
        	nextFire = myTime + 0.5f;
        	Instantiate(bullet, transform.position, Quaternion.Euler(0,0,Random.Range(0.0f, 360.0f)));
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

}
