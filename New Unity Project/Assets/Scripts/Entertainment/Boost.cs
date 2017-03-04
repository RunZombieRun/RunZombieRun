using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour {

    public int time = 2;
    public float BoostMultiplier;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
  public void boost()
    {
        GameController.instance.SpeedBoost(time, BoostMultiplier);
    }
}
