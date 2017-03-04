using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using VacuumShaders;


namespace VacuumShaders
{
    namespace CurvedWorld
    {
        public class FirstEnemy : BaseEnemy
        {
            void Update()
            {
                if (m_Health <= 0) {
                    Die(); }
            }

            public override void Die()
            {
                base.Die();
            }

            private void OnCollisionEnter(Collision collision)
            {
                if (collision.gameObject.tag == "Player")
                {
                    var player = collision.gameObject.GetComponent<Runner_Player>();
                    player.Life -= m_Damage;
                    GetComponent<Collider>().enabled = false;
                }
            }

        }
    }
}
