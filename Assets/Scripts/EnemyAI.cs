using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {
	public Transform baby;
	public Transform player;
	float afraid = -1.0f;
	int health = 3;
	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = this.GetComponent<NavMeshAgent> ();
		baby = GameObject.Find ("Baby").transform;
		player = GameObject.Find ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
        afraid -= 1.0f * Time.deltaTime;
		if (afraid >= 0f) {
			
		} else {
			agent.speed = 3;
			agent.acceleration = 2;
			agent.angularSpeed = 60;
			agent.SetDestination (baby.position);
			if (Vector3.Distance (this.transform.position, player.transform.position) <= 3f) {
				health--;
                float pitch = Random.Range(1f, 4f);
                if (pitch < 2)
                {
                    SoundManager.Instance.PlayOneshot(AudioClass.ghost.ghost_afraid, true, pitch, 0.2f);
                }
                else {
                    SoundManager.Instance.PlayOneshot(AudioClass.ghost.ghost_afraid_2, true, pitch, 0.2f);
                }
                afraid = 0.8f;
				agent.speed = 4;
				agent.acceleration = 10;
				agent.angularSpeed = 360;
				agent.SetDestination (2 * this.transform.position - player.position);
			}
            if (Vector3.Distance(this.transform.position, baby.transform.position) <= 3f)
            {
                BabyAI.BabyHealth-=10;
            }
        }
		if (health <= 0) {
            float pitch = Random.Range(0.1f, 2f);
			SoundManager.Instance.PlayOneshot (AudioClass.ghost.ghost_die,true, pitch,pitch);
			Destroy (this.gameObject);
		}
	}
}
