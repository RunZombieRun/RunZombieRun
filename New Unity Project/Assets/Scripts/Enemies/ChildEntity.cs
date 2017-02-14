using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChildEntity : BaseEnemy
{

	void Update()
	{
		if (m_Health <= 0) {
			Die(); // Показываем что в наследниках можно вызывать метоеды, даже если они не указаны
		}
	}

	public override void Die ()
	{
		
		base.Die ();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
        }
    }


}
