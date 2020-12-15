using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class EquipmentForging : MonoBehaviour
{
    public GameObject forging;
    private int i;
    private string str;
    public GameObject btn1; 
    public GameObject btn2;
    public GameObject btn3;
    public GameObject btn4;
    public GameObject btn5;

    public GameObject btn6;
    public GameObject btn7;
    public GameObject btn8;
    public GameObject btn9;
    public GameObject btn10;

    public GameObject btn11;
    public GameObject btn12;
    public GameObject btn13;
    public GameObject btn14;
    public GameObject btn15;

    // Start is called before the first frame update
    void Start()
    {

        EquipmentData.equ = EquipmentData.dic[RoleData._roleId];
        EquipmentData._forging1 = EquipmentData.equ[RoleData._roleId+1000];
        EquipmentData._forging2 = EquipmentData.equ[RoleData._roleId + 2000];
        EquipmentData._forging3 = EquipmentData.equ[RoleData._roleId + 3000];
        EquipmentData._count = EquipmentData.count[RoleData._roleId];
        EquipmentData._hasForging = EquipmentData.equ[RoleData._roleId];
        ExcelDataInit.Init();
        str = "0";
        
        btn1.name = EquipmentData._forging1[0];
        btn2.name = EquipmentData._forging1[1];
        btn3.name = EquipmentData._forging1[2];
        btn4.name = EquipmentData._forging1[3];
        btn5.name = EquipmentData._forging1[4];

        btn6.name = EquipmentData._forging2[0];
        btn7.name = EquipmentData._forging2[1];
        btn8.name = EquipmentData._forging2[2];
        btn9.name = EquipmentData._forging2[3];
        btn10.name = EquipmentData._forging2[4];

        btn11.name = EquipmentData._forging3[0];
        btn12.name = EquipmentData._forging3[1];
        btn13.name = EquipmentData._forging3[2];
        btn14.name = EquipmentData._forging3[3];
        btn15.name = EquipmentData._forging3[4];

        for (i=0;i<5;i++)
        {
            if(!EquipmentData._hasForging[i].Equals(str))
            {
                GameObject.Find(EquipmentData._hasForging[i]).GetComponent<Image>().color = Color.black;
                EquipmentData._count--;
                GameObject.Find(EquipmentData._hasForging[i]).GetComponent<Button>().enabled = false;
            }
        }
        GameObject.Find("Point").GetComponent<Text>().text = Convert.ToString(EquipmentData._count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onEquipmentForgingClick()
    {
        var forgingName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        EquipmentData._currentForging = forgingName.name;
        EquipmentData._forgingId = int.Parse(forgingName.name);
        if (EquipmentData._count > 0)
        {

            GameObject.Instantiate(forging, gameObject.transform);
        }
        else
            UnityEditor.EditorUtility.DisplayDialog("Tips", "无可用改造点数", "确定");


    }
    public void onBackButtonClick()
    {
        SceneManager.LoadScene("RoleDevelopment");
    }
}
