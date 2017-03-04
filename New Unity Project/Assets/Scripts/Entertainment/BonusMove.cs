using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMove : MonoBehaviour
{

    public GameObject EnemyPrefab;
    public Transform CakeSartPoint;
    GameObject EnemyObject;

    public int AddMoney;

    //public bool rotate;
    //public float m_MovementSpeed = 1f;

    public bool isNegative;
    public bool isWandering;

    public bool spawnConrol = true;
    // Use this for initialization

    

    void Start()
    {
        if (GetComponent<Rigidbody>())
        {
            if (!GetComponent<Rigidbody>().useGravity)
            {
                Vector3 Pos = transform.position;
                Pos.y = 0f;
                transform.position = Pos;
            }
        }


        EnemyObject = Instantiate(EnemyPrefab, CakeSartPoint.position, CakeSartPoint.rotation) as GameObject;
        EnemyObject.transform.parent = CakeSartPoint;


    }

    /*void FixedUpdate()
    {
        if (rotate)
        {
            transform.RotateAround(transform.position, Vector3.up, 1f);
        }
    }


    private void Update()
    {

        transform.Translate(Vector3.back * m_MovementSpeed * Time.deltaTime, Space.World);
    }*/
}