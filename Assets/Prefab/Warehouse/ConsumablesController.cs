using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class ConsumablesController : MonoBehaviour
{ 
    public GameObject layer;
    public GameObject next;

    public Text UseNum;
    public Text HoldNum;

    //使用数量
    private int useNum;
    private int holdNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  

    /// <summary>
    /// 删除本体
    /// </summary>
    public void closed()
    {
        GameObject.Destroy(layer);
    }
    public void onUseButtonClick()
    {
        GameObject.Instantiate(next, transform);
    }
    
}
