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
	public GameObject explosion_prefab;
	Vector3 start_position;
	Vector3 target_position;
	Vector3[] half_position;
	int maxLength;
	int n_max;
	int _index;
	int index{
		get{
			return _index;
		}
		set{
			if (value <= 0) {
				_index = 0;
			} else if (value >= maxLength + 5) {
				_index = maxLength + 5;
			} else {
				_index = value;
			}
		}
	}
	int explosion_time;

	void Start () {

	}

	void Update(){
		if (index <= maxLength) {
			this.transform.position = Beizer (index++, n_max);
		}
		if (index == 3) {
			SoundManager.Instance.PlayOneshot (AudioClass.player.throw_match);
		}
		if (index == maxLength) {
			SoundManager.Instance.PlayOneshot (AudioClass.player.drop_match);
		}
		if (index >= maxLength && index < maxLength + 2) {
			Vector3 force = (target_position - start_position) / maxLength;
			force.y = -0.3f;
			this.GetComponent<Rigidbody> ().AddForce (force,ForceMode.Impulse);
			index++;
		}
		explosion_time--;
		if (explosion_time == 7) {
			AOE ();
		} else if (explosion_time < 0) {
			Destroy (this.gameObject);
		}
	}

	public void Throw(Vector3 pos){
		SoundManager.Instance.PlayOneshot (AudioClass.player.draw_match);
		n_max = 4;
		start_position = this.transform.position;
		target_position = pos;
		target_position.y = 0.1f;
		float distance = Vector3.Distance (start_position, target_position);
		half_position = new Vector3[n_max];
		half_position [0] = start_position;
		half_position [n_max - 1] = target_position;

		half_position [1] = (start_position + target_position) / 2;
		half_position [1].y += distance * 0.4f;
		half_position [2] = start_position / 3 + target_position * 2 / 3;
		half_position [2].y += distance * 0.4f;

		maxLength = (int)(distance * 2);
		explosion_time = 57;
		index = 0;
	}

	void AOE(){
		GameObject explosion = Instantiate (explosion_prefab, this.transform);
		Renderer renderer = explosion.GetComponent<Renderer> ();
		renderer.material.color = new Color (0.5f, 0f, 0f, 0.1f);
		Collider[] hit_target_list = Physics.OverlapSphere (this.gameObject.transform.position, 4f);
		foreach (Collider hit_taget in hit_target_list) {
			if (string.Equals (hit_taget.gameObject.name, "Enemy(Clone)")) {
				Destroy (hit_taget.gameObject);
			}
		}
	}

	Vector3 Beizer(int x, int n){
		float t = x / (float)maxLength;
		Vector3 ans = Vector3.zero;
		n--;
		for (int i = 0; i < n + 1; i++) {
			ans += C(n,i) * Mathf.Pow (t, i) * Mathf.Pow (1 - t, n - i) * half_position [i];
		}
		return ans;
	}

	int C(int n,int m){
		int ans = 1;
		for (int i = 0; i < m; i++) {
			ans *= (n - i);
		}
		for (int i = 0; i < m; i++) {
			ans /= (i + 1);
		}
		return ans;
	}
}
