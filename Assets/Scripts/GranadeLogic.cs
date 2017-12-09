using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeLogic : MonoBehaviour {
/*	enum STATE{
		Init,
		Start,
		Throw,
		Explose
	}
	Vector3 currentVelocity = Vector3.one;
	Vector3 target_pos;
	STATE state = STATE.Init;
	public GameObject explosion_prefab;
	float explosion_time;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case STATE.Start:
			this.GetComponent<Rigidbody> ().AddForce (Vector3.up * 10f, ForceMode.Impulse);
			state = STATE.Throw;
			break;
		case STATE.Throw:
			this.transform.position = Vector3.SmoothDamp (this.transform.position, target_pos, ref currentVelocity, 1f);
			explosion_time -= 1.0f * Time.deltaTime;
			if (explosion_time <= 0f) {
				state = STATE.Explose;
			}
			break;
		case STATE.Explose:
			AOE ();
			explosion_time -= 1.0f * Time.deltaTime;
			if (explosion_time <= -0.3f) {
				Destroy (this.gameObject);
			}
			break;
		default:
			break;
		}
	}

	public void Throw(Vector3 pos){
		target_pos = pos;
		state = STATE.Start;
		explosion_time = 2f;
	}

	void AOE(){
		GameObject explosion = Instantiate (explosion_prefab, this.transform);
		Renderer renderer = explosion.GetComponent<Renderer> ();
		renderer.material.color = new Color (0.5f, 0f, 0f, 0.1f);
		Collider[] hit_target_list = Physics.OverlapSphere (this.gameObject.transform.position, 2f);
		foreach (Collider hit_taget in hit_target_list) {
			if (string.Equals (hit_taget.gameObject.name, "Enemy(Clone)")) {
				Destroy (hit_taget.gameObject);
			}
		}
	}*/
	void Start () {

	}


}
