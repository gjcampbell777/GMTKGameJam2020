using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsctaleScript : MonoBehaviour
{

	private float deathTime;
    private float myTime = 0.0f;

	void Start()
	{

		deathTime = Random.Range(5.0f, 15.0f);

	}

    // Update is called once per frame
    void Update()
    {

    	myTime += Time.deltaTime;

    	if(myTime > deathTime)
    	{
    		Destroy(gameObject);
    	}

    }
}
