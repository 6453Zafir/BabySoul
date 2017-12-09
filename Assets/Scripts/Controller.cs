using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public List<CampFire>CampFires = new List<CampFire>();

    public int CurrentCampFireIdx { get; private set; }


	enum SKILLSELECTED{
		NONE,
		Granade,
		Spear,
		Suicide
	}
	public GameObject granade_prefab;
	public GameObject spear_prefab;
	SKILLSELECTED skill_selected = SKILLSELECTED.NONE;
    public float moveSpeed = 5f;
    bool isFPCMoving = false;
	bool isDied = false;
	int dieTime = 10;
	// Use this for initialization
	void Start ()
	{
        CurrentCampFireIdx = 0;
        CampFires[CurrentCampFireIdx].Activate();
        SoundManager.Instance.PlayLoop(AudioClass.environment.bird);
        SoundManager.Instance.PlayLoop(AudioClass.environment.wind);
    }
	
	// Update is called once per frame
	void Update () {
		if (isDied) {
			if (skill_selected == SKILLSELECTED.Suicide) {
				Suicide ();
			}
		} else {
            KeyboardListener ();
            if (isFPCMoving)
            {
                SoundManager.Instance.PlayLoop(AudioClass.baby.footstep_normal);
            }
            else {
                SoundManager.Instance.StopSound("footstep_normal");
            }
		}
	}

	void KeyboardListener(){
        isFPCMoving = false;
        if (Input.GetKey (KeyCode.W)) {
			this.transform.position += new Vector3 (0, 0, moveSpeed) * Time.deltaTime;
            isFPCMoving = true;
		}
		if (Input.GetKey (KeyCode.S)) {
			this.transform.position += new Vector3 (0, 0, -moveSpeed) * Time.deltaTime;
            isFPCMoving = true;
		}
		if (Input.GetKey (KeyCode.A)) {
			this.transform.position += new Vector3 (-moveSpeed, 0, 0) * Time.deltaTime;
            isFPCMoving = true;
		}
		if (Input.GetKey (KeyCode.D)) {
			this.transform.position += new Vector3 (moveSpeed, 0, 0) * Time.deltaTime;
            isFPCMoving = true;
		}
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			skill_selected = SKILLSELECTED.Granade;
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			skill_selected = SKILLSELECTED.Spear;
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			skill_selected = SKILLSELECTED.Suicide;
			isDied = true;
		}

		if (Input.GetMouseButtonDown(0))
		{
			switch (skill_selected) {
			case SKILLSELECTED.Granade:
				if (UIController.GranadeLeft > 0) {
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit raycastHit = new RaycastHit();
					if (Physics.Raycast(ray, out raycastHit))
					{
						Vector3 pos = raycastHit.point;
						GameObject armo = Instantiate(granade_prefab, this.transform.position, this.transform.rotation);
						armo.GetComponent<GranadeLogic>().SendMessage("Throw", pos);
					}
					UIController.GranadeLeft -= 1;
				}
				else
				{
					print("没有炸弹了！");
				}
				break;
			case SKILLSELECTED.Spear:
				if (UIController.ArrowLeft > -10) {
					Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
					RaycastHit raycastHit = new RaycastHit ();
					if (Physics.Raycast (ray, out raycastHit, 10000f, 1 << 8)) {
						GameObject armo = Instantiate (spear_prefab, this.transform.position, this.transform.rotation);
						armo.GetComponent<SpearLogic> ().SendMessage ("Throw", raycastHit.transform);
						UIController.ArrowLeft -= 1;
					}
				}
				else
				{
					print("没有长枪了！");
				}
				break;
			default:
				break;
			}
		}
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "CampFire")
        {
            var nearCamp = collider.gameObject.GetComponentInChildren<CampFire>();
            //nearCamp.lighton

            if(nearCamp != CampFires[CurrentCampFireIdx]) LightBlink = StartCoroutine(LightAnim(nearCamp));
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "CampFire")
        {
            var nearCamp = collider.gameObject.GetComponentInChildren<CampFire>();
            //nearCamp.lighton
            if (nearCamp != CampFires[CurrentCampFireIdx])
            {
                StopCoroutine(LightBlink);
                nearCamp.Light();
            }

        }
    }

    private Coroutine LightBlink;
    IEnumerator LightAnim(CampFire fire)
    {
        while (true)
        {
            fire.Blink();
            yield return new WaitForEndOfFrame();
        }
    }
    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "CampFire")
        {
            var nearCamp = collider.gameObject.GetComponentInChildren<CampFire>();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var lastCamp = CampFires[CurrentCampFireIdx];
                
                var nearCampIdx = CampFires.FindIndex(x=>x== nearCamp);

                if (nearCampIdx >= CurrentCampFireIdx)
                {
                    CurrentCampFireIdx = nearCampIdx;
                    lastCamp.Unactivate();
                    nearCamp.Activate();
                    StopCoroutine(LightBlink);
                    UIController.RecoverAllSkill();
                }
            }
        }
    }

	public void Suicide(){
		dieTime--;
		if (dieTime <= 0) {
			Collider[] hit_target_list = Physics.OverlapSphere (this.gameObject.transform.position, 100f, 1 << 8);
			foreach (Collider hit_taget in hit_target_list) {
				if (string.Equals (hit_taget.gameObject.name, "Enemy(Clone)")) {
					Destroy (hit_taget.gameObject);
				}
			}
			dieTime = 10;
			Respawn ();
		}
	}

	public void Respawn(){
		UIController.GranadeLeft = 3;
		UIController.ArrowLeft = 1;
		UIController.BoomLeft = 1;
		this.transform.position = CampFires [CurrentCampFireIdx].transform.position + new Vector3 (0, 0.5f, 1f);
		isDied = false;
	}
}
