using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearLogic : MonoBehaviour {
	Vector3 move_direction;

	// Use this for initialization
	void Start () {
		Throw (new Vector3 (1, 0, 2));
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
