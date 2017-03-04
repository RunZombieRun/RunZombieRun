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
                DealDmg(damage);
            }
            private void Update()
            {             
                if (transform.position.y < -10)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}