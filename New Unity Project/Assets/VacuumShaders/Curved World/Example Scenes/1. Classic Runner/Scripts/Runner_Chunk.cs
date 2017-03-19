//VacuumShaders 2014
// https://www.facebook.com/VacuumShaders

using UnityEngine;
using System.Collections;


[AddComponentMenu("VacuumShaders/Curved World/Example/Runner/Move Element")]
public class Runner_Chunk : MonoBehaviour
{
    //////////////////////////////////////////////////////////////////////////////
    //                                                                          // 
    //Unity Functions                                                           //                
    //                                                                          //               
    //////////////////////////////////////////////////////////////////////////////

    void Update()
    {
        transform.Translate(Runner_SceneManager.moveVector * Runner_SceneManager.get.speed * Time.deltaTime);
    }

    void FixedUpdate()
    {
     //   if (transform.position.z < -100)
      //      Runner_SceneManager.get.DestroyChunk(this);
    }
}