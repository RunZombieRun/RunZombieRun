using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour {
    public GameObject Player;
    private float zPos;
    public float Multiplier;
    private void Start()
    {
        zPos = gameObject.transform.position.z;
    }
    // Update is called once per frame
    void FixedUpdate ()
    {
        var posPlayer = new Vector3(Player.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, posPlayer, Multiplier * Time.deltaTime);
	}
}
