using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainBase : EnvironmentBase
{
    public int m_AddStamina;


    virtual public void StartBrain()
    {

    }


    virtual public void StopBrain()
    {

    }

    public override void DoSomething()
    {
        GameController.instance.Stamina += m_AddStamina;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DoSomething();
        }
    }
}
