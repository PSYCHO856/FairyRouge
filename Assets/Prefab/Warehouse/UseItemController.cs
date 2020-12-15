using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;
public class UseItemController : MonoBehaviour
{
    private Text UseNum;
    private Text HoldNum;

    //使用数量
    private int useNum;
    private int holdNum;
    // Start is called before the first frame update
    void Start()
    {
        UseNum = GameObject.Find("ItemNum").GetComponent<Text>();
        HoldNum = GameObject.Find("HoleNum").GetComponent<Text>();
        useNum = 1;
        holdNum = 11;
        UseNum.text = Convert.ToString(useNum);
        HoldNum.text = Convert.ToString(holdNum);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onCancelButtonClick()
    {
        GameObject.Destroy(gameObject);
    }

    public void onAddButtonClick()
    {
        if (useNum - holdNum < 0)
            useNum++;
        else
            useNum = holdNum;
        UseNum.text = Convert.ToString(useNum);
    }
    public void onReduceButtonClick()
    {
        if (useNum - 2 < 0)
            useNum = 1;
        else
            useNum--;
        UseNum.text = Convert.ToString(useNum);
    }
    public void onMaxButtonClick()
    {
        useNum = holdNum;
        UseNum.text = Convert.ToString(useNum);
    }
    public void onMinButtonClick()
    {
        useNum = 1;
        UseNum.text = Convert.ToString(useNum);
    }
}
