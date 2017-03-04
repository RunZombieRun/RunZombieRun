using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : BrainBase
{
    public override void StartBrain()
    {
        base.StartBrain();
    }

    public override void StopBrain()
    {
        base.StopBrain();
    }


    void Start()
    {
        StartBrain();
    }
}
