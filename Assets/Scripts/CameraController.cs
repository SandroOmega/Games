using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject Shapeshifter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(Shapeshifter.transform.position.x,Shapeshifter.transform.position.y,this.transform.position.z);
        
	}
}
