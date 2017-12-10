using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearLogic : MonoBehaviour {
	Transform move_direction;
	Ray ray;
	int stopTime = 20;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (move_direction == null) {
			Time.timeScale = 1f;
			Destroy (this.gameObject);
		}
		if (Vector3.Distance (this.transform.position, move_direction.position) < 1.0f) {
			move_direction.gameObject.GetComponent<EnemyAI> ().enabled = false;
			this.transform.position += (move_direction.position - this.transform.position) * 2f * Time.deltaTime;
			if (stopTime > 0) {
				stopTime--;
				Time.timeScale = 0.1f;
			} else {
				Time.timeScale = 1f;
				SoundManager.Instance.PlayOneshot (AudioClass.player.bingo);
				Destroy (move_direction.gameObject);
				move_direction.gameObject.GetComponent<EnemyAI> ().SendMessage ("GetDamaged", 10);
				Destroy (this.gameObject);
			}
		} else {
			this.transform.position += (move_direction.position - this.transform.position) * 10f * Time.deltaTime;
			//this.transform.LookAt (move_direction);
		}
	}

	public void Throw(Transform pos){
		SoundManager.Instance.PlayOneshot (AudioClass.player.shoot);
		move_direction = pos;
		//this.transform.LookAt (move_direction);
	}
}
