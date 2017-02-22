using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VacuumShaders
{
    namespace CurvedWorld
    {

        [AddComponentMenu("VacuumShaders/Curved World/Example/Runner/Car")]
        public class EnemyFermer : BaseEnemy
        {


            Rigidbody rigidBody;
            public float speed = 1;
            bool zombie = false;


            void Start()
            {
                rigidBody = GetComponent<Rigidbody>();
                              
            }
            private void OnCollisionEnter(Collision collision)
            {
                print("qwe");
                if (collision.gameObject.tag == "Player")
                {
                    Destroy(collision.gameObject);
                }
            }

            private void FixedUpdate()
            {
                rigidBody.MovePosition(transform.position + Runner_SceneManager.moveVector * Runner_SceneManager.get.speed * Time.deltaTime * speed);


                if (transform.position.y < -10)
                {
                    Destroy(this.gameObject);
                }
           }
        }
    }
}