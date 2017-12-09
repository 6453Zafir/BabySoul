using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public List<CampFire> CampFires = new List<CampFire>();

    public int CurrentCampleFireIdx { get; private set; }


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
    bool isBabyMoving = false;
	// Use this for initialization
	void Start ()
	{
        CurrentCampleFireIdx = 0;
        CampFires[CurrentCampleFireIdx].Activate();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.W)) {
			this.transform.position += new Vector3 (0, 0, moveSpeed) * Time.deltaTime;
            isBabyMoving = true;
        }
		if (Input.GetKey (KeyCode.S)) {
			this.transform.position += new Vector3 (0, 0, -moveSpeed) * Time.deltaTime;
            isBabyMoving = true;
        }
		if (Input.GetKey (KeyCode.A)) {
			this.transform.position += new Vector3 (-moveSpeed, 0, 0) * Time.deltaTime;
            isBabyMoving = true;
        }
		if (Input.GetKey (KeyCode.D)) {
			this.transform.position += new Vector3 (moveSpeed, 0, 0) * Time.deltaTime;
            isBabyMoving = true;
        }
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			skill_selected = SKILLSELECTED.Granade;
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			skill_selected = SKILLSELECTED.Spear;
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			skill_selected = SKILLSELECTED.Suicide;
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
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit raycastHit = new RaycastHit();
					if (Physics.Raycast(ray, out raycastHit))
					{
						Vector3 pos = raycastHit.point;
						pos.y = 0.5f;
						GameObject armo = Instantiate(spear_prefab, this.transform.position, this.transform.rotation);
						armo.GetComponent<SpearLogic> ().SendMessage ("Throw", pos);
					}
					UIController.ArrowLeft -= 1;
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

            if(nearCamp != CampFires[CurrentCampleFireIdx]) LightBlink = StartCoroutine(LightAnim(nearCamp));
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "CampFire")
        {
            var nearCamp = collider.gameObject.GetComponentInChildren<CampFire>();
            //nearCamp.lighton
            if (nearCamp != CampFires[CurrentCampleFireIdx])
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
                var lastCamp = CampFires[CurrentCampleFireIdx];
                
                var nearCampIdx = CampFires.FindIndex(x=>x== nearCamp);

                if (nearCampIdx >= CurrentCampleFireIdx)
                {
                    lastCamp.Unactivate();
                    nearCamp.Activate();
                    StopCoroutine(LightBlink);
                    UIController.RecoverAllSkill();
                }
            }
        }
    }
}
