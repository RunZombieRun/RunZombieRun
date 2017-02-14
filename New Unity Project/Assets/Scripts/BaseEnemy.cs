using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseEnemy : MonoBehaviour
{
	//Сюда записываем только поля, которые будут во всех наследниках
	public int m_Health = 1;
	public int m_Shield;
	public int damage;
	public float m_Speed;

    public void Start()
    {
        TestBaseClass.instance.enemies.Add(this.gameObject);
    }





	public virtual void Die ()
	{
		Destroy (this.gameObject);
	}

	public virtual void ChangeSpeed(float val)
	{
		m_Speed += val;
	}

	public virtual void GetDamage(int damage)
	{
		//Не уверен что нам понадобится этот метод, но пусть будет
		int dmg = m_Shield - damage;
		if (dmg < 0)
			m_Health += dmg;
	}

}
