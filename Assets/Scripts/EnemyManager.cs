using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
	GameObject baby;
	public GameObject manager;
	Transform[] EnemySpawnPoint;
	public GameObject enemy_prefab_1;
	public GameObject enemy_prefab_2;
	int index;

	// Use this for initialization
	void Start () {
		baby = GameObject.Find ("Baby");
		List<Component> components = new List<Component>(manager.GetComponentsInChildren(typeof(Transform)));
		List<Transform> transforms = components.ConvertAll(c => (Transform)c);
		transforms.Remove(manager.transform);
		transforms.Sort (delegate(Transform a, Transform b) {
			return a.name.CompareTo (b.name);
		});
		EnemySpawnPoint = transforms.ToArray ();
		index = 0;

	}
	
	// Update is called once per frame
	void Update () {
		if (index >= EnemySpawnPoint.Length) {
			return;
		}
		if (Mathf.Abs (EnemySpawnPoint [index].position.x - baby.transform.position.x) < 2.0f) {
			Vector3 rand_pos = EnemySpawnPoint [index].position;
			float pitch = Random.Range (0.2f, 3f);
			if (pitch >= 1.2f) {
				Instantiate (enemy_prefab_1, rand_pos, new Quaternion (0, 0, 0, 0));
			} else {
				Instantiate (enemy_prefab_2, rand_pos, new Quaternion (0, 0, 0, 0));
			}
			SoundManager.Instance.PlayOneshot (AudioClass.ghost.ghost_born, true, pitch, 0.3f);
			index++;
		}
	}
}
