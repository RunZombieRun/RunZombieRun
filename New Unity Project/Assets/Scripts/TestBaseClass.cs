using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestBaseClass : MonoBehaviour {
	public static Action ScoreChange;
	public static Action<GameObject> DeadMinion;
	public List<GameObject> enemies; //Если будем хранить врагов, то пригодится

	public static TestBaseClass instance;


	private int m_Score;
	public  int Score
	{
		set{
			m_Score = value;			
		}
		get{
			return m_Score;
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

	private void DoSomethingWithScore(){
		
		Debug.Log ("ScoreChanges");
	}

	private void DeadSound(GameObject obj){
		enemies.Remove (obj);
		Debug.Log ("Minion is Dead");
	}
}
