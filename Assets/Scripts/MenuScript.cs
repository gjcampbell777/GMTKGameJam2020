using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
	
	void Update(){

		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	    Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

	    RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

	    if(Input.GetMouseButtonDown(0)){

		    	
	    	if(hit.collider.gameObject.tag == "Play")
	    	{

	    		SceneManager.LoadScene("Game"); 
	    		
	    	} else if(hit.collider.gameObject.tag == "Quit")
	    	{

	    		Application.Quit();
	    	}

		}

	}
}
