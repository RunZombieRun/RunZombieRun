using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeSmall : CakeBase
{


    public override void StartCake()
    {
        base.StartCake();
    }

    public override void StopCake()
    {
        base.StopCake();
    }


    void Start ()
    {
        StartCake();
	}
	
	void Update ()
    {
		
	}
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        DoSomething();
    //    }
    //}
}
