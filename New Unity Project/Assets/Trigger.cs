using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    public  GameObject TriggerObject;
    public GameObject Parent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("qwe");
            TriggerObject.GetComponent<Animator>().SetTrigger("ZombieUp");
        

        }
    }

    void Up()
    {
        TriggerObject.GetComponent<Animator>().SetTrigger("ZombieUp");
        Parent.transform.localPosition = new Vector3(0, -1, Parent.transform.localPosition.z);
        TriggerObject.transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("qwe");
          
          
        }
    }

}
