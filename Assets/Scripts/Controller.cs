using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
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
	void Start () {
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
		if (Input.GetKey (KeyCode.Alpha1)) {
			skill_selected = SKILLSELECTED.Granade;
		}
		if (Input.GetKey (KeyCode.Alpha2)) {
			skill_selected = SKILLSELECTED.Spear;
		}
		if (Input.GetKey (KeyCode.Alpha3)) {
			skill_selected = SKILLSELECTED.Suicide;
		}


        if (Input.GetMouseButtonDown(0) && UIController.isThrowingGa )
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
				if (UIController.ArrowLeft > 0) {
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit raycastHit = new RaycastHit();
					if (Physics.Raycast(ray, out raycastHit))
					{
						Vector3 pos = raycastHit.point;
						GameObject armo = Instantiate(spear_prefab, this.transform.position, this.transform.rotation);
						armo.GetComponent<SpearLogic>().SendMessage("Throw", pos);
					}
					UIController.GranadeLeft -= 1;
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
}
