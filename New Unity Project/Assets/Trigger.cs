using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    public  GameObject TriggerObject;
    public string TriggerName;
    private void Start()
    {
        TriggerObject.GetComponentInChildren<Animator>().speed = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TriggerObject.GetComponentInChildren<Animator>().speed = 1f;
        }
    }

    void Up()
    {
        TriggerObject.GetComponent<Animator>().SetTrigger("ZombieUp");

    }

}
