using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScript : MonoBehaviour
{
    public Animator animator;

    public void CamShake()
    {

    	switch(Random.Range(0,4))
    	{
    		case 0:
    			animator.SetTrigger("Shake1");
    			break;
    		case 1:
    			animator.SetTrigger("Shake2");
    			break;
    		case 2:
    			animator.SetTrigger("Shake3");
    			break;
    		case 3:
    			animator.SetTrigger("Shake4");
    			break;
    	}

    }

}
