using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBase : EnvironmentBase
{
    public int m_AddArmor;


    virtual public void StartArmor()
    {

    }


    virtual public void StopArmor()
    {

    }

    public override void DoSomething()
    {
        GameController.instance.Armor += m_AddArmor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DoSomething();
        }
    }
}
