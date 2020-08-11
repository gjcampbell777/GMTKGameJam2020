using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    
	public AudioClip[] hit;

    public Sprite secret;

    private float speed;
    private Rigidbody2D rb;
    private ShakeScript shake;

    private AudioSource audioSource;

    void Start()
    {
    	audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    	speed = Random.Range(3.0f, 8.0f);
    	shake = GameObject.Find("Shake Manager").GetComponent<ShakeScript>();
    	shake.CamShake();

      if(Random.Range(0, 100) == 0) this.GetComponent<SpriteRenderer>().sprite = secret;

    }

    void FixedUpdate()
    {

    	rb.velocity = transform.up * speed;

    }

    void OnCollisionEnter2D(Collision2D other)
    {
   
   		if(other.gameObject.tag != "Player" && other.gameObject.tag != "Bullet")
   		{
   			AudioSource.PlayClipAtPoint(
       			hit[Random.Range(0,hit.Length)], new Vector3(0, 0, 0));
   			Destroy(gameObject);
   		}
       
    }

   	void OnBecameInvisible() 
    {
   		Destroy(gameObject);
    }
}
