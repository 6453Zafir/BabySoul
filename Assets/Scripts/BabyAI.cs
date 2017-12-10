using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BabyAI : MonoBehaviour {
	public Transform[] target_list;
    public static float BabyHealth = 300;
    public GameObject player;
	NavMeshAgent agent;
	int index = 0;

	// Use this for initialization
	void Start () {
		agent = this.GetComponent<NavMeshAgent> ();
		agent.SetDestination (target_list [index].position);
	}

    // Update is called once per frame
    void Update() {
        print("宝宝血量：" + BabyHealth);
        if (transform.position.x < -3) {
            UIController.isFinished = true;
        }
        transform.LookAt(Camera.main.transform.position, Vector3.up);
        if (Vector3.Distance(this.transform.position, target_list[index].position) <= 3.0f && index < target_list.Length - 1) {
            index++;
            agent.SetDestination(target_list[index].position);
        }
        if (Vector3.Distance(this.transform.position, player.transform.position) <= 3f)
        {
            SoundManager.Instance.PlayLoop(AudioClass.baby.cry,false,true,0.5f,1);
            BabyHealth -= 1;
        }else{
            BabyHealth += 0.2f;
            SoundManager.Instance.StopSound("cry");
        }
    }
}
