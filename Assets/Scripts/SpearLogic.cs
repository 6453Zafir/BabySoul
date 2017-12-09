using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearLogic : MonoBehaviour {
	Transform move_direction;
	Ray ray;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += (move_direction.position - this.transform.position) * 10f * Time.deltaTime;
		this.transform.LookAt (move_direction);
		if (Vector3.Distance (this.transform.position, move_direction.position) < 1.0f) {
			SoundManager.Instance.PlayOneshot (AudioClass.player.bingo);
			Destroy (move_direction.gameObject);
			Destroy (this.gameObject);
		}
	}

	public void Throw(Transform pos){
		SoundManager.Instance.PlayOneshot (AudioClass.player.shoot);
		move_direction = pos;
		this.transform.LookAt (move_direction);
	}
}
