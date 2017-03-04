﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeBase : EnvironmentBase
{
 
    public int m_AddScore;


    virtual public void StartCake()
    {
		
	}

    
    virtual public void StopCake ()
    {
		
	}
    public override void DoSomething()
    {
        GameController.instance.Score += m_AddScore;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DoSomething();
        }
    }
}
