using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class RoleDevelopment : MonoBehaviour
{
    //角色名
    private Text RoleName;

    

    //角色等级
    private Text level;
    private Slider levelBar;
    //人物立绘
    private Image roleDrawing;

    //职业
    private Text OccupationName;

    //职业特性
    private Text occupationFeature;
    private int occupationID;
    private string occIntroduction;
    //获得属性信息
    private Text Attack;
    private Text Mana;
    private Text MAttack;
    private Text Dodge;
    private Text Life;

    //获取天赋信息
    private Text Talent;
    private int TalentID;

    //获取技能信息
    private Text skill;
    private int skillID;

    //获取角色羁绊
    private Slider FettersBar;
    private Text Fetters;
    private float fettersNum;
    private float maxFetterNum;
    //获取角色立绘
    private Texture2D m_Tex;
    private string _Path;
    private string _texname ;

    private int i;

    // Start is called before the first frame update
    void Start()
    {
        ExcelDataInit.Init();

        //获取角色名
        RoleName = GameObject.Find("RoleName").GetComponent<Text>();
        RoleData._name = RoleInformation. GetSheet((int)RoleInformation.SheetName.Sheet1).GetData(RoleData._roleId).name;
        RoleName.text = RoleData._name;
        //获取职业名
        OccupationName = GameObject.Find("OccupationName").GetComponent<Text>();
        RoleData._name = Occupation.GetSheet((int)Occupation.SheetName.Occupation).GetData(RoleData._roleId).OccupationName;
        OccupationName.text = RoleData._name;
        //获取角色属性

        Life = GameObject.Find("Life").GetComponentInChildren<Text>();
        RoleData._lifeGrowth = Roleattributes.GetSheet((int)Roleattributes.SheetName.Sheet1).GetData(RoleData._roleId).hpGrowth;
        RoleData._constitution = Roleattributes.GetSheet((int)Roleattributes.SheetName.Sheet1).GetData(RoleData._roleId).body;
        RoleData._lifePoint = Roleattributes.GetSheet((int)Roleattributes.SheetName.Sheet1).GetData(RoleData._roleId).hp;
        Life.text = Convert.ToString(RoleData._lifePoint + RoleData._constitution * (RoleData._level - 1) * RoleData._lifeGrowth);

        Mana = GameObject.Find("Mana").GetComponentInChildren<Text>();
        RoleData._manaGrowth = Roleattributes.GetSheet((int)Roleattributes.SheetName.Sheet1).GetData(RoleData._roleId).mpGrowth;
        RoleData._intelligence = Roleattributes.GetSheet((int)Roleattributes.SheetName.Sheet1).GetData(RoleData._roleId).intelligence;
        RoleData._manaPoint = Roleattributes.GetSheet((int)Roleattributes.SheetName.Sheet1).GetData(RoleData._roleId).mp;
        Mana.text = Convert.ToString(RoleData._manaPoint + RoleData._intelligence * (RoleData._level - 1) * RoleData._manaGrowth);

        Attack = GameObject.Find("Attack").GetComponentInChildren<Text>();
        RoleData._physicalAttackGrowth = Roleattributes.GetSheet((int)Roleattributes.SheetName.Sheet1).GetData(RoleData._roleId).attackGrowth;
        RoleData._powerPoint = Roleattributes.GetSheet((int)Roleattributes.SheetName.Sheet1).GetData(RoleData._roleId).strength;
        RoleData._physicalAttackPoint = Roleattributes.GetSheet((int)Roleattributes.SheetName.Sheet1).GetData(RoleData._roleId).attack;
        Attack.text = Convert.ToString(RoleData._physicalAttackPoint + RoleData._powerPoint * (RoleData._level - 1) * RoleData._physicalAttackGrowth);

        MAttack = GameObject.Find("MAttack").GetComponentInChildren<Text>();
        RoleData._magicAttackGrowth = Roleattributes.GetSheet((int)Roleattributes.SheetName.Sheet1).GetData(RoleData._roleId).MattackGrowth;
        RoleData._magicAttackPoint = Roleattributes.GetSheet((int)Roleattributes.SheetName.Sheet1).GetData(RoleData._roleId).Mattack;
        MAttack.text = Convert.ToString(RoleData._magicAttackPoint + RoleData._magicAttackGrowth * (RoleData._level - 1) * RoleData._intelligence);

        Dodge = GameObject.Find("Dodge").GetComponentInChildren<Text>();
        RoleData._dodgeGrowth = Roleattributes.GetSheet((int)Roleattributes.SheetName.Sheet1).GetData(RoleData._roleId).DodgeGrowth;
        RoleData._agile = Roleattributes.GetSheet((int)Roleattributes.SheetName.Sheet1).GetData(RoleData._roleId).agile;
        RoleData._dodgePoint = Roleattributes.GetSheet((int)Roleattributes.SheetName.Sheet1).GetData(RoleData._roleId).Dodge;
        Dodge.text = Convert.ToString(RoleData._dodgePoint + (RoleData._level - 1) * RoleData._agile * RoleData._dodgeGrowth);

        //羁绊等级
        FettersBar = GameObject.Find("FettersSlider").GetComponent<Slider>();
        Fetters = GameObject.Find("FettersNum").GetComponent<Text>();
        fettersNum = RoleInformation.GetSheet((int)RoleInformation.SheetName.Sheet1).GetData(RoleData._roleId).Fetters;
        maxFetterNum = 1000;
        FettersBar.value = fettersNum / maxFetterNum;
        Fetters.text = Convert.ToString(fettersNum);

        //角色等级
        level = GameObject.Find("LevelPoint").GetComponent<Text>();
        level.text = Convert.ToString(RoleData._level);

        //职业特性
        occupationFeature = GameObject.Find("Effect").GetComponent<Text>();
        occupationID = RoleInformation.GetSheet((int)RoleInformation.SheetName.Sheet1).GetData(RoleData._roleId).occupationID;
        occIntroduction = OccupationFeature.GetSheet((int)OccupationFeature.SheetName.Feature).GetData(occupationID).introduce;
        occupationFeature.text = occIntroduction;

        //天赋
        Talent = GameObject.Find("Talent").GetComponentInChildren<Text>();
        TalentID = RoleInformation.GetSheet((int)RoleInformation.SheetName.Sheet1).GetData(RoleData._roleId).skillID1;
        Talent.text = "攻击造成"+ Effect.GetSheet((int)Effect.SheetName.Sheet1).GetData(TalentID).value+"点"+
            Effect.GetSheet((int)Effect.SheetName.Sheet1).GetData(TalentID).type;
        //技能
        skill = GameObject.Find("Skill").GetComponentInChildren<Text>();
        skillID = RoleInformation.GetSheet((int)RoleInformation.SheetName.Sheet1).GetData(RoleData._roleId).skillID2;
        skill.text = "攻击造成" + Effect.GetSheet((int)Effect.SheetName.Sheet1).GetData(skillID).value + "点" +
            Effect.GetSheet((int)Effect.SheetName.Sheet1).GetData(skillID).type;

        /* //获取角色立绘
         _Path = "Assets/Resources/RoleDrawing/";
         _texname = RoleData._roleId + ".jpg";
         roleDrawing = GameObject.Find("RoleDrawing").GetComponent<Image>();
         LoadFromFile(_Path, _texname);
         Sprite tempSprite = Sprite.Create(m_Tex, new Rect(0, 0, m_Tex.width, m_Tex.height), new Vector2(10, 10));
         roleDrawing.sprite = tempSprite;*/

        EquipmentData._forging1 = new List<string>();
        EquipmentData._forging2 = new List<string>();
        EquipmentData._forging3 = new List<string>();
        EquipmentData._hasForging = new List<string>();
        EquipmentData._equipmentID = RoleInformation.GetSheet((int)RoleInformation.SheetName.Sheet1).GetData(RoleData._roleId).EquipmentID;
        for (i = 0; i < 5; i++)
        {
            EquipmentData._forging1.Add(Equipment.GetSheet((int)Equipment.SheetName.Sheet1).GetData(EquipmentData._equipmentID).forging1[i]);
            EquipmentData._forging2.Add(Equipment.GetSheet((int)Equipment.SheetName.Sheet1).GetData(EquipmentData._equipmentID).forging2[i]);
            EquipmentData._forging3.Add(Equipment.GetSheet((int)Equipment.SheetName.Sheet1).GetData(EquipmentData._equipmentID).forging3[i]);
            EquipmentData._hasForging.Add(Equipment.GetSheet((int)Equipment.SheetName.Sheet1).GetData(EquipmentData._equipmentID).hasForging[i]);
            EquipmentData._material1Get = 4;
            EquipmentData._material2Get = 5;
            EquipmentData._material3Get = 5;
        }
        EquipmentData._count = Equipment.GetSheet((int)Equipment.SheetName.Sheet1).GetData(EquipmentData._equipmentID).forgingNum;
        if (!EquipmentData.equ.ContainsKey(RoleData._roleId)) // True 
        {
            EquipmentData.equ.Add(RoleData._roleId+1000, EquipmentData._forging1);
            EquipmentData.equ.Add(RoleData._roleId+2000, EquipmentData._forging2);
            EquipmentData.equ.Add(RoleData._roleId+3000, EquipmentData._forging3);
            EquipmentData.count.Add(RoleData._roleId, EquipmentData._count);
            EquipmentData.count.Add(RoleData._roleId + 1000, EquipmentData._material1Get);
            EquipmentData.count.Add(RoleData._roleId + 2000, EquipmentData._material2Get);
            EquipmentData.count.Add(RoleData._roleId + 3000, EquipmentData._material3Get);
            EquipmentData.equ.Add(RoleData._roleId, EquipmentData._hasForging);
        }
        if(!EquipmentData.dic.ContainsKey(RoleData._roleId))
        {
            EquipmentData.dic.Add(RoleData._roleId, EquipmentData.equ);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    public void onBackButtonClick()
    {
        SceneManager.LoadScene("RoleSelect");
    }
    public void onMainButtonClick()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void onLevelUpButtonClick()
    {
        SceneManager.LoadScene("LevelUpScene");
    }
    public void onEquipmentButtonClick()
    {
        SceneManager.LoadScene("Equipment forging");
    }
    private void LoadFromFile(string path, string _name)
    {
        m_Tex = new Texture2D(1, 1);
        m_Tex.LoadImage(ReadPNG(path + _name));
    }
    private byte[] ReadPNG(string path)
    {
        FileStream fileStream = new FileStream(path, FileMode.Open, System.IO.FileAccess.Read);

        fileStream.Seek(0, SeekOrigin.Begin);

        byte[] binary = new byte[fileStream.Length]; 
        fileStream.Read(binary, 0, (int)fileStream.Length);

        fileStream.Close();

        fileStream.Dispose();

        fileStream = null;

        return binary;
    }
}
    