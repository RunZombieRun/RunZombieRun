using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour {
	public static Action ScoreChange;
	public static Action<GameObject> DeadMinion;
	public List<GameObject> enemies; //Если будем хранить врагов, то пригодится

	public static GameController instance;
    [SerializeField]
    private int m_Health;
    public int Health
    {
        set
        {
            m_Health = value;
           // CheckHP();
        }
        get
        {
            return m_Health;
        }
    }

    [SerializeField]
    private int m_Score;
	public  int Score
	{
		set{
			m_Score = value;
            CheckHP();

        }
		get{
			return m_Score;
		}
	}

    [SerializeField]
    private float m_Stamina;
    public float Stamina
    {
        get
        {
            return m_Stamina;
        }
        set
        {
            m_Stamina = value;
        }
    }

    [SerializeField]
    private float m_Armor;
    public float Armor
    {
        get
        {
            return m_Armor;
        }
        set
        {
            m_Armor = value;
        }
    }

    void Awake()
	{
		if (instance == null) 
		{
			instance = this;
		}
		else 
		{
			Destroy (instance.gameObject);
			instance = this;
		}
	}

	//Подписываем на эвенты
	void OnEnable()
	{
		ScoreChange += DoSomethingWithScore;
		DeadMinion += DeadSound;
	}

	void OnDisable()
	{
		ScoreChange -= DoSomethingWithScore;
		DeadMinion -= DeadSound;
	}

    private void CheckHP()
    {
        if(m_Health <= 0)
        {
            Destroy(Player_Controller.get.gameObject);
        }
    }
	private void DoSomethingWithScore(){
		
		Debug.Log ("ScoreChanges");
	}

	private void DeadSound(GameObject obj){
		enemies.Remove (obj);
		Debug.Log ("Minion is Dead");
	}

    public void Restart()
    {
        
        Application.LoadLevel(0);
    }
}
