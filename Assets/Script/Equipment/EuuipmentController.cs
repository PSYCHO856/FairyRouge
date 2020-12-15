using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EuuipmentController : MonoBehaviour
{
    public GameObject layer;
    private int i;
    private bool flag;
    private string str;

    private Text Material1;
    // Start is called before the first frame update
    void Start()
    {
        flag = true;
        str = "0";
        Material1 = GameObject.Find("Material1").GetComponentInChildren<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void closed()
    {
        GameObject.Destroy(layer);
    }
    public void onForgingButtonClick()
    {
        EquipmentData._preconditions = Convert.ToString(Forging.GetSheet((int)Forging.SheetName.Sheet1).GetData(int.Parse(EquipmentData._currentForging)).Preconditions);
        //循环遍历前置条件
        for(i=0;i<5;i++)
        {
            //判断是否满足前置条件或是第一个改造
            if(EquipmentData._hasForging[i].Equals(EquipmentData._preconditions))
            {
                flag = true;
                break;
            }
            else if (int.Parse(EquipmentData._currentForging)%5==1)
            {
                flag = true;
                break;
            }
            else
            {
                flag = false;
            }
        }
        
        if (flag)
        {
            GameObject.Find(EquipmentData._currentForging).GetComponent<Image>().color = Color.black;
            EquipmentData._count -= 1;
            GameObject.Find("Point").GetComponent<Text>().text = Convert.ToString(EquipmentData._count);
            GameObject.Find(EquipmentData._currentForging).GetComponent<Button>().enabled = false;
        }
        
        if (flag)
        {
            for (i = 0; i < 5; i++)
            {
                if (EquipmentData._hasForging[i].Equals(str))
                {
                    EquipmentData._hasForging.RemoveAt(i);
                    EquipmentData._hasForging.Add(EquipmentData._currentForging);
                    break;
                }
            }
        }
        else
        {
            UnityEditor.EditorUtility.DisplayDialog("Tips", "不满足条件，未解锁前置技能", "确定");
        }
       
        for (i = 0; i < 5; i++)
        {
            
                Debug.LogError(EquipmentData._hasForging[i] );       
        }
        //保存改造信息
        EquipmentData.equ[RoleData._roleId] = EquipmentData._hasForging;
        EquipmentData.count[RoleData._roleId] = EquipmentData._count;
        EquipmentData.dic[RoleData._roleId]= EquipmentData.equ;
        GameObject.Destroy(layer);
        
    }
}
