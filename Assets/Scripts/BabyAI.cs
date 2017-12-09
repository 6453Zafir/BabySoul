using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BabyAI : MonoBehaviour {
	public Transform[] target_list;
    public static int BabyHealth = 200;
	NavMeshAgent agent;
	int index = 0;

	// Use this for initialization
	void Start () {
		agent = this.GetComponent<NavMeshAgent> ();
		agent.SetDestination (target_list [index].position);
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
        BabyHealth += 2;
        if (Vector3.Distance (this.transform.position, target_list [index].position) <= 1.0f && index < target_list.Length - 1) {
			index++;
			agent.SetDestination (target_list [index].position);
		}
	}
}
