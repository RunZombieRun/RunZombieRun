using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : ArmorBase
{
    public override void StartArmor()
    {
        base.StartArmor();
    }

    public override void StopArmor()
    {
        base.StopArmor();
    }


    void Start()
    {
        StartArmor();
    }
}
