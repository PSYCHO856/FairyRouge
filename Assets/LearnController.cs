using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LearnController : MonoBehaviour
{
    public GameObject LearnPanel;
    public GameObject AllPanel;

    private GameObject LearnButton;
    private GameObject AllButton;

    // Start is called before the first frame update
    void Start()
    {
        LearnPanel.SetActive(true);
        LearnButton = GameObject.Find("Learn");
        AllButton = GameObject.Find("All");
        LearnButton.GetComponent<Image>().color = Color.black;
        LearnButton.GetComponentInChildren<Text>().color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onAllButtonClick()
    {
        if(AllPanel.activeSelf)
        {

        }
        else
        {
            LearnButton.GetComponent<Image>().color = Color.white;
            LearnButton.GetComponentInChildren<Text>().color = Color.black;
            AllButton.GetComponent<Image>().color = Color.black;
            AllButton.GetComponentInChildren<Text>().color = Color.white;
            LearnPanel.SetActive(false);
            AllPanel.SetActive(true);
        }
        
    }
    public void onLearnButtonClick()
    {
        if(LearnPanel.activeSelf)
        {

        }
        else
        {
            LearnButton.GetComponent<Image>().color = Color.black;
            LearnButton.GetComponentInChildren<Text>().color = Color.white;
            AllButton.GetComponent<Image>().color = Color.white;
            AllButton.GetComponentInChildren<Text>().color = Color.black;
            AllPanel.SetActive(false);
            LearnPanel.SetActive(true);
        }
    }
}
