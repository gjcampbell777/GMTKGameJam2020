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

 	private AudioSource audioSource;

 	void Start()
 	{
 		audioSource = GetComponent<AudioSource>();
 		player = GameObject.FindWithTag("Player");
 		speed = Random.Range(0.5f, 3.0f);
 		audioSource.clip = spawnSounds[Random.Range(0,spawnSounds.Length)];
       	audioSource.Play();
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
       
   }
}
