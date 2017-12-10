using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public static int GranadeLeft = 3, ArrowLeft = 1, BoomLeft = 1;
    public GameObject GGPanel;
    public GameObject GranadePanel, ShootPanel, BoomPanel;
    public Text ThrowInfo, ShootInfo, BoomInfo;
    public static bool isThrowingGa = false, isShooting = false, isBooming = false;
    public static bool isGameOver = false;
    bool isUsingSkill;
    // Use this for initialization
    void Start () {
       
    }

    // Update is called once per frame
    void Update () {

        if (isThrowingGa&&GranadeLeft>0) {
            GranadePanel.GetComponent<Image>().color = new Color(255, 255, 255, 1); 
        }
        else
        {
            GranadePanel.GetComponent<Image>().color = new Color(255, 255, 255, 0.3f);
        }
        if (isShooting&&ArrowLeft>0)
        {
            ShootPanel.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }
        else {
            ShootPanel.GetComponent<Image>().color = new Color(255, 255, 255, 0.3f);
        }
        if (isBooming&&BoomLeft>0)
        {
            BoomPanel.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }
        else {
            BoomPanel.GetComponent<Image>().color = new Color(255, 255, 255, 0.3f);
        }
        ThrowInfo.text = GranadeLeft.ToString();
        ShootInfo.text = ArrowLeft.ToString();
        BoomInfo.text = BoomLeft.ToString();


        if (BabyAI.BabyHealth<=0) {
            isGameOver = true;
            GGPanel.SetActive(true);

        }
    }

    public static void RecoverAllSkill()
    {
        GranadeLeft = 3;
        ArrowLeft = 1;
        BoomLeft = 1;
    }
    public void chooseSkill(GameObject other) {
        if (isUsingSkill) {
            switch (other.name)
            {
                case "Throw":
                    if (GranadeLeft > 0)
                    {
                        GranadeLeft -= 1;
                        isThrowingGa = true;
                        print("投掷了一个炸弹");
                    }
                    else
                    {
                        print("没有炸弹了");
                    }
                    ThrowInfo.text = GranadeLeft.ToString();
                    break;
                case "Shoot":
                    if (ArrowLeft > 0)
                    {
                        ArrowLeft -= 1;
                        print("射出一个快速攻击物品");
                    }
                    else
                    {
                        print("没有箭矢了");
                    }
                    ShootInfo.text = ArrowLeft.ToString();
                    break;
                case "Boom":
                    if (BoomLeft > 0)
                    {
                        BoomLeft -= 1;
                        print("自爆了！");
                    }
                    else
                    {
                        print("自爆机会已用过");
                    }
                    BoomInfo.text = BoomLeft.ToString();
                    break;
                default:
                    print("There is sth wrong");
                    break;
            }
        }
      


    }
}
