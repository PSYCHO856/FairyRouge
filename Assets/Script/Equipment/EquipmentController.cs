using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentController : MonoBehaviour
{
    public GameObject layer;
    private int i;
    private bool flag;
    private string str;

    private Text Material1;
    private Text Material2;
    private Text Material3;
    // Start is called before the first frame update
    void Start()
    {
        
        flag = true;
        str = "0";
        Material1 = GameObject.Find("Material1").GetComponentInChildren<Text>();
        EquipmentData._material1 = Forging.GetSheet((int)Forging.SheetName.Sheet1).GetData(EquipmentData._forgingId).Material1;
        EquipmentData._material1Get = EquipmentData.count[RoleData._roleId + 1000];
        EquipmentData._material1Num = Forging.GetSheet((int)Forging.SheetName.Sheet1).GetData(EquipmentData._forgingId).num1;
        Material1.text = EquipmentData._material1Get+"/" +EquipmentData._material1Num;

        Material2 = GameObject.Find("Material2").GetComponentInChildren<Text>();
        EquipmentData._material2 = Forging.GetSheet((int)Forging.SheetName.Sheet1).GetData(EquipmentData._forgingId).Material2;
        EquipmentData._material2Get = EquipmentData.count[RoleData._roleId + 2000];
        EquipmentData._material2Num = Forging.GetSheet((int)Forging.SheetName.Sheet1).GetData(EquipmentData._forgingId).num2;
        Material2.text = EquipmentData._material2Get + "/" + EquipmentData._material2Num;

        Material3 = GameObject.Find("Material3").GetComponentInChildren<Text>();
        EquipmentData._material3 = Forging.GetSheet((int)Forging.SheetName.Sheet1).GetData(EquipmentData._forgingId).Material3;
        EquipmentData._material3Get = EquipmentData.count[RoleData._roleId + 3000];

        EquipmentData._material3Num = Forging.GetSheet((int)Forging.SheetName.Sheet1).GetData(EquipmentData._forgingId).num3;
        Material3.text = EquipmentData._material3Get + "/" + EquipmentData._material3Num;
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
                if(EquipmentData._material1Get/EquipmentData._material1Num>=1&& EquipmentData._material2Get / EquipmentData._material2Num >= 1&&
                    EquipmentData._material3Get / EquipmentData._material3Num >= 1)
                {
                    flag = true;
                    break;
                }
                else
                {
                    
                    flag = false;
                }
                
            }
            else if (int.Parse(EquipmentData._currentForging)%5==1)
            {
                if (EquipmentData._material1Get / EquipmentData._material1Num >= 1 && EquipmentData._material2Get / EquipmentData._material2Num >= 1 &&
                    EquipmentData._material3Get / EquipmentData._material3Num >= 1)
                {
                    flag = true;
                    break;
                }
                else
                {

                    flag = false;
                }
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
            EquipmentData._material1Get -=  EquipmentData._material1Num;
            
            EquipmentData._material2Get -= EquipmentData._material2Num;
            EquipmentData._material3Get -= EquipmentData._material3Num;
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
            UnityEditor.EditorUtility.DisplayDialog("Tips", "不满足条件", "确定");
        }
       
        for (i = 0; i < 5; i++)
        {
            
                Debug.LogError(EquipmentData._hasForging[i] );       
        }
        //保存改造信息
        EquipmentData.equ[RoleData._roleId] = EquipmentData._hasForging;
        EquipmentData.count[RoleData._roleId] = EquipmentData._count;
        EquipmentData.count[RoleData._roleId + 1000] = EquipmentData._material1Get;
        EquipmentData.count[RoleData._roleId + 2000] = EquipmentData._material2Get;
        EquipmentData.count[RoleData._roleId + 3000] = EquipmentData._material3Get;

        EquipmentData.dic[RoleData._roleId]= EquipmentData.equ;
        GameObject.Destroy(layer);
        
    }
}
