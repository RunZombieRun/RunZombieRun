using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ChildEntity : BaseClass
{

	void Update()
	{
		m_Health--; 
		if (m_Health <= 0) {
			TestBaseClass.DeadMinion.Invoke (this.gameObject); // Делаем инвок того что моб уме (( звук, минус колчиество общее, место в гонке)
			ChangeSpeed ( -5f); // Показываем что в наследниках можно вызывать метоеды, даже если они не указаны
		}
	}

	public override void DoSomething ()
	{
		Destroy (this.gameObject);
	}

	 
}
