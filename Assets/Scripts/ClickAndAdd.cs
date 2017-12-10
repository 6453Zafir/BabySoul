using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickAndAdd : MonoBehaviour {
	public GameObject obstacle_prefab;
	public GameObject enemy_prefab;

	float time = 3.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		time -= 1.0f * Time.deltaTime;
		if (time <= 0f) {
			time = 3.0f;
			Vector3 rand_pos = new Vector3 (Random.Range (-10f, 10f), 0f, Random.Range (-10f, 10f));
			/*Instantiate (obstacle_prefab, rand_pos, new Quaternion (0, 0, 0, 0));
			rand_pos = new Vector3 (Random.Range (-10f, 10f), 0f, Random.Range (-10f, 10f));*/
			Instantiate (enemy_prefab, rand_pos, new Quaternion (0, 0, 0, 0));
            float pitch = Random.Range(0.2f, 3f);
            SoundManager.Instance.PlayOneshot(AudioClass.ghost.ghost_born, true,pitch,0.3f);
		}
	}
}
