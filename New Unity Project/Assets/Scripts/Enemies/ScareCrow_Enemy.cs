using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace VacuumShaders
{
    namespace CurvedWorld
    {
        public class ScareCrow_Enemy : BaseEnemy
        {

            private void OnTriggerEnter(Collider other)
            {
                if (other.gameObject.tag == "Player")
                {
                    Destroy(this.gameObject);
                    DealDmg(m_Damage);
                }
            }
            //Then delete and place crow in scene
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
