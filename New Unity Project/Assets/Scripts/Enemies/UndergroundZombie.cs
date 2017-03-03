using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VacuumShaders;

namespace VacuumShaders
{
    namespace CurvedWorld
    {
        [AddComponentMenu("VacuumShaders/Curved World/Example/Runner/Car")]
        public class UndergroundZombie : BaseEnemy
        {
            private void OnTriggerEnter(Collider other)
            {
                print("qwe");
                DealDmg(damage);
            }
            private void FixedUpdate()
            {
                GetComponent<Rigidbody>().MovePosition(transform.position + Runner_SceneManager.moveVector * Runner_SceneManager.get.speed * Time.deltaTime * m_Speed);

                if (transform.position.y < -10)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}