using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SettingController : MonoBehaviour
{
    public GameObject Obj;

    //上一个按钮
    private static GameObject previousBtn;
    private Text txt;
    private Image image;

    //当前按钮
    private GameObject btn;
    private Text txtNeo;
    private Image imageNeo;

    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;

    private GameObject CurrentPanel;
    // Start is called before the first frame update
    void Start()
    {
        previousBtn = GameObject.Find("GameButton");
        txt = previousBtn.GetComponentInChildren<Text>();
        image = previousBtn.GetComponent<Image>();
        CurrentPanel = panel1;
        CurrentPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onBackButtonClick()
    {
        GameObject.Destroy(Obj);
    }
    public void onSettingButtonClick()
    {
        btn = EventSystem.current.currentSelectedGameObject;
        if (btn.name == "GameButton")
        {
            if(btn.name== previousBtn.name)
            {
                Debug.LogError(1);
            }
            else
            {
                CurrentPanel.SetActive(false);
                CurrentPanel = panel1;
                CurrentPanel.SetActive(true);
            }
            

        }
        else if (btn.name == "VoiceButton")
        {
            if (btn.name == previousBtn.name)
            {
                Debug.LogError(2);
            }
            CurrentPanel.SetActive(false);
            CurrentPanel = panel2;
            CurrentPanel.SetActive(true);

        }
        else if (btn.name == "OtherButton")
        {
            if (btn.name == previousBtn.name)
            {
                Debug.LogError(3);
            }
            CurrentPanel.SetActive(false);
            CurrentPanel = panel3;
            CurrentPanel.SetActive(true);

        }
        if (btn.name == previousBtn.name)
        {
            Debug.LogError(btn.name);
        }
        else
        {
            txtNeo = btn.GetComponentInChildren<Text>();
            imageNeo = btn.GetComponent<Image>();

            txt.color = Color.black;
            image.color = Color.white;

            txtNeo.color = Color.white;
            imageNeo.color = Color.black;

            previousBtn = btn;

            txt = previousBtn.GetComponentInChildren<Text>();
            image = previousBtn.GetComponent<Image>();
        }    
    }
}
