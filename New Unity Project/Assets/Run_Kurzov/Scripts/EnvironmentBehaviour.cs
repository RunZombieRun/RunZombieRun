using System.Collections;
using UnityEngine;

public class EnvironmentBehaviour : EnvironmentBase {

	public float m_Speed;
	public float m_Turbo;

	public int m_Health;
	public int m_Armor;
	public float m_Stamina;

	public int m_Damage;

	private int m_Score;
	public int Score;
	public GameObject[] Cakes;

    public float EffectTime = 20f;


    public override void StartEnvironment()
    {
      
    }

    public override void StopEnvironment()
    {
    
    }

    //public override void ChangeSpeed ()
    //{

    //}
    // Use this for initialization
    void Start ()
    {
        StartEnvironment();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
