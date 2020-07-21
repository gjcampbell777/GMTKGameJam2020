using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
	public GameObject particleEffect;
    public AudioClip[] spawnSounds;
    public AudioClip[] defeatSounds;

    private float speed;
 	private GameObject player;
 	private Vector2 movement;
 	private Rigidbody2D rb;
	private Animator animator;

 	private AudioSource audioSource;

  private Vector3 previousPosition;
  private Vector3 currentMovementDirection;

 	void Start()
 	{
 		rb = GetComponent<Rigidbody2D>();
 		animator = GetComponent<Animator>();
 		audioSource = GetComponent<AudioSource>();
 		player = GameObject.FindWithTag("Player");
 		speed = Random.Range(0.5f, 3.0f);
 		audioSource.clip = spawnSounds[Random.Range(0,spawnSounds.Length)];
       	audioSource.Play();
 	}

    // Update is called once per frame
    void Update()
    {

    	movement = rb.velocity;
        
    	if(player != null)
    	{
    		transform.position = Vector2.MoveTowards(
    			transform.position, player.transform.position, speed * Time.deltaTime);
    	}

      if(previousPosition != transform.position) {
        currentMovementDirection = (transform.position - previousPosition);
        previousPosition = transform.position;
      }

    	animator.SetFloat("Horizontal", currentMovementDirection.y);
    	animator.SetFloat("Vertical", currentMovementDirection.x);
    	animator.SetBool("Speed", true);

    }

    void OnCollisionEnter2D(Collision2D other)
   {

   		if(other.gameObject.tag == "Bullet")
   		{
   			PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score")+1);
       		AudioSource.PlayClipAtPoint(
       			defeatSounds[Random.Range(0,defeatSounds.Length)], new Vector3(0, 0, 0));
       		Instantiate(particleEffect, transform.position, transform.rotation);
   			Destroy(gameObject);
   		}

   		if(other.gameObject.tag == "Player")
   		{
       		AudioSource.PlayClipAtPoint(
       			defeatSounds[Random.Range(0,defeatSounds.Length)], new Vector3(0, 0, 0));
       		Instantiate(particleEffect, transform.position, transform.rotation);
   			Destroy(gameObject);
   		}

   		animator.SetBool("Speed", false);
       
   }
}
