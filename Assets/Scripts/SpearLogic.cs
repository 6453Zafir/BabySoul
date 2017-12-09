using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearLogic : MonoBehaviour {
	Vector3 move_direction = Vector3.zero;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += move_direction * Time.deltaTime;
	}

	public void Throw(Vector3 pos){
		move_direction = 30 * Vector3.Normalize (pos - this.transform.position);
		this.transform.LookAt (pos);
	}
}
