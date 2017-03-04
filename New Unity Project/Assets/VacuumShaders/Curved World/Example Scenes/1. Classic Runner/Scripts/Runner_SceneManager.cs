//VacuumShaders 2014
// https://www.facebook.com/VacuumShaders

using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace VacuumShaders
{
    namespace CurvedWorld
    {
        [AddComponentMenu("VacuumShaders/Curved World/Example/Runner/Scene Manager")]
        public class Runner_SceneManager : MonoBehaviour
        {
            //////////////////////////////////////////////////////////////////////////////
            //                                                                          // 
            //Variables                                                                 //                
            //                                                                          //               
            //////////////////////////////////////////////////////////////////////////////
            static public Runner_SceneManager get;

            public float speed = 1;
            public GameObject[] chunks;

            public GameObject[] Zombies;

            public GameObject[] Enviroment;

            static public float chunkSize = 60;
            static public Vector3 moveVector = new Vector3(0, 0, -1);
            static public GameObject lastChunk;

            List<Material> listMaterials;
            //////////////////////////////////////////////////////////////////////////////
            //                                                                          // 
            //Unity Functions                                                           //                
            //                                                                          //               
            //////////////////////////////////////////////////////////////////////////////
            void Awake()
            { 
                get = this;

                
                //Instantiate chunks
                for (int i = 0; i < chunks.Length; i++)
                {
                    GameObject obj = (GameObject)Instantiate(chunks[i]);

                    obj.transform.position = new Vector3(0, 0, i * chunkSize);

                    lastChunk = obj;
                }

                //Instantiate cars
                
            } 

            // Use this for initialization
            void Start()
            {
                Renderer[] renderers = FindObjectsOfType(typeof(Renderer)) as Renderer[];

                listMaterials = new List<Material>();
                foreach (Renderer _renderer in renderers)
                {
                    listMaterials.AddRange(_renderer.sharedMaterials);
                }

                listMaterials = listMaterials.Distinct().ToList();

                StartCoroutine(ZombieSpawner());
               // StartCoroutine(EnviromentSpawner());
            }
            IEnumerator ZombieSpawner()
            {
                
                for (;;)
                {
                    int randSpawnDot = Random.Range(0, 3);
                    yield return new WaitForSeconds(2f);

                    GameObject zmb =  Instantiate(Zombies[0]) as GameObject;
                   


                    if (randSpawnDot == 0)
                    {
                        zmb.transform.position = new Vector3(-3f,0, Random.Range(140, 240));

                    }
                    else if(randSpawnDot == 1)
                    {
                        zmb.transform.position = new Vector3(0f,0, Random.Range(140, 240));
                    }
                    else
                    {
                        zmb.transform.position = new Vector3(3f, 0, Random.Range(140, 240));
                        randSpawnDot = 0;                    
                    }
                    randSpawnDot++;
                }
            }
            IEnumerator EnviromentSpawner()
            {

                for (;;)
                {
                    int randSpawnDot = Random.Range(0, 2);
                    yield return new WaitForSeconds(3f);

                    GameObject zmb = Instantiate(Enviroment[0]) as GameObject;

                    if (randSpawnDot == 0)
                    {
                        zmb.transform.position = new Vector3(-6.6f, 0, Random.Range(140, 240));

                    }
                    else if (randSpawnDot == 1)
                    {
                        zmb.transform.position = new Vector3(6.6f, 0, Random.Range(140, 240));
                        randSpawnDot = 0;
                    }
                    randSpawnDot++;
                }
            }

            //////////////////////////////////////////////////////////////////////////////
            //                                                                          // 
            //Custom Functions                                                          //
            //                                                                          //               
            //////////////////////////////////////////////////////////////////////////////

            public void DestroyChunk(Runner_Chunk moveElement)
            {
                Vector3 newPos = lastChunk.transform.position;
                newPos.z += chunkSize;


                lastChunk = moveElement.gameObject;
                lastChunk.transform.position = newPos;
            }

            public void DestroyCar(Runner_Car car)
            {
                GameObject.Destroy(car.gameObject);

                Instantiate(Zombies[Random.Range(0, Zombies.Length)]);
            }
        }
    }
}