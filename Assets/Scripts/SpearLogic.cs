using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearLogic : MonoBehaviour {
	Vector3 move_direction = Vector3.zero;
	Ray ray;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += move_direction * Time.deltaTime;
		ray = new Ray (this.transform.position - move_direction * Time.deltaTime, this.transform.position + move_direction * Time.deltaTime);
		RaycastHit raycastHit = new RaycastHit ();
		if (Physics.Raycast (ray, out raycastHit, 3f, 1 << 8)) {
			Destroy (raycastHit.transform.gameObject);
		}
	}

	public void Throw(Vector3 pos){
		move_direction = 30 * Vector3.Normalize (pos - this.transform.position);
		move_direction.y = 0;
		this.transform.LookAt (pos);
	}
}
