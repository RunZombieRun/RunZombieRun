using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeBig : CakeBase
{

    public bool rotate;
    public float m_MovementSpeed = 1f;

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
	
    void FixedUpdate()
    {
        if (rotate)
        {
            transform.RotateAround(transform.position, Vector3.up, 1f);
        }
    }


    private void Update()
    {

        transform.Translate(Vector3.back * m_MovementSpeed * Time.deltaTime, Space.World);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        Debug.Log("Hello");
    //        DoSomething();
    //        //Destroy(gameObject);
    //    }
    //}
}
