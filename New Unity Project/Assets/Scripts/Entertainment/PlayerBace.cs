﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBace : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            print("Игрок печатает");
            //SetDamage(collision.gameObject);
            Destroy(collision.gameObject, 0.01f);
            //Destroy(this.gameObject, 1f);
        }
    }
}