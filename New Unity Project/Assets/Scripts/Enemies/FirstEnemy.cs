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

            private void OnTriggerEnter(Collider collision)
            {
                if (collision.gameObject.tag == "Player")
                {
                    if (Player_Controller.get.State != Player_Controller.PLAYERSTATE.Jump)
                    {
                        DealDmg(m_Damage);
                    }
                }
            }

        }
    }
}
