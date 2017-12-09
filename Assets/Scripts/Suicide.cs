using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suicide : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Explosive(){
		Collider[] hit_target_list = Physics.OverlapSphere (this.gameObject.transform.position, 2f, 1 << 8);
		foreach (Collider hit_taget in hit_target_list) {
			if (string.Equals (hit_taget.gameObject.name, "Enemy(Clone)")) {
				Destroy (hit_taget.gameObject);
			}
		}
	}
}
