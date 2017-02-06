using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseClass : MonoBehaviour
{
	//Сюда записываем только поля, которые будут во всех наследниках
	public int m_Health;
	public int m_Armor;
	public float m_Speed;


	public virtual void DoSomething ()
	{
		Debug.Log ("Something is happened");
	}

	public virtual void ChangeSpeed(float val)
	{
		m_Speed -= val;
	}

}
