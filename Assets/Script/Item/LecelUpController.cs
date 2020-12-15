using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// 玩家升级界面
/// </summary>
public class LecelUpController : MonoBehaviour
{
    public GameObject Item1;
    public GameObject Item2;
    public GameObject Item3;
    public GameObject Item4;
    public GameObject UsedItem1;
    public GameObject UsedItem2;
    public GameObject UsedItem3;
    public GameObject UsedItem4;

    private Text Item1Num;
    private GameObject Item1Used;
    private Text UseNum1;
    private Text useNum1;

    private Text Item2Num;
    private GameObject Item2Used;
    private Text UseNum2;
    private Text useNum2;

    private Text Item3Num;
    private GameObject Item3Used;
    private Text UseNum3;
    private Text useNum3;

    private Text Item4Num;
    private GameObject Item4Used;
    private Text UseNum4;
    private Text useNum4;

    private Text LevelPoint;
    private Text NowExp;
    private Text NeedExp;
    private Slider sli;
    private Text TotalExperience;
    private Text Percentage;
    private Text Money;

    private Text NowLife;
    private Text NextLife;
    private float nextLife;
    private Text NowMana;
    private Text NextMana;
    private float nextMana;
    private Text NowAttack;
    private Text NextAttack;
    private float nextAttack;
    private Text NowMAttack;
    private Text NextMAttack;
    private float nextMAttack;
    private Text NowDodge;
    private Text NextDodge;
    private float nextDodge;

    // Start is called before the first frame update
    void Start()
    {
        
        Item1.SetActive(false);
        Item1Num = Item1.transform.GetChild(3).GetComponent<Text>();
        Item1Num.text = "100";
        ItemData._UseNum1 = 0;
        ItemData._ItemMaxNum1 = int.Parse(Item1Num.text);
        if(ItemData._ItemMaxNum1>0)
        {
            Item1.SetActive(true);
        }
        Item1Used = Item1.transform.GetChild(4).gameObject;
        UseNum1 = Item1Used.GetComponentInChildren<Text>();
        Item1Used.SetActive(false);
        useNum1 = UsedItem1.GetComponentInChildren<Text>();

        Item2.SetActive(false);
        Item2Num = Item2.transform.GetChild(3).GetComponent<Text>();
        Item2Num.text = "20";
        ItemData._UseNum2 = 0;
        ItemData._ItemMaxNum2 = int.Parse(Item2Num.text);
        if(ItemData._ItemMaxNum2>0)
        {
            Item2.SetActive(true);
        }
        Item2Used = Item2.transform.GetChild(4).gameObject;
        UseNum2 = Item2Used.GetComponentInChildren<Text>();
        Item2Used.SetActive(false);
        useNum2 = UsedItem2.GetComponentInChildren<Text>();

        Item3.SetActive(false);
        Item3Num = Item3.transform.GetChild(3).GetComponent<Text>();
        Item3Num.text = "30";
        ItemData._UseNum3 = 0;
        ItemData._ItemMaxNum3 = int.Parse(Item3Num.text);
        if(ItemData._ItemMaxNum3>0)
        {
            Item3.SetActive(true);
        }
        Item3Used = Item3.transform.GetChild(4).gameObject;
        UseNum3 = Item3Used.GetComponentInChildren<Text>();
        Item3Used.SetActive(false);
        useNum3 = UsedItem3.GetComponentInChildren<Text>();

        Item4.SetActive(false);
        Item4Num = Item4.transform.GetChild(3).GetComponent<Text>();
        Item4Num.text = "0";
        ItemData._UseNum4 = 0;
        ItemData._ItemMaxNum4 = int.Parse(Item4Num.text);
        if(ItemData._ItemMaxNum4>0)
        {
            Item4.SetActive(true);
        }
        Item4Used = Item4.transform.GetChild(4).gameObject;
        UseNum4 = Item4Used.GetComponentInChildren<Text>();
        Item4Used.SetActive(false);
        useNum4 = UsedItem4.GetComponentInChildren<Text>();


        sli = GameObject.Find("ExperienceBar").GetComponent<Slider>();
        LevelPoint = GameObject.Find("Level").GetComponent<Text>();
        LevelPoint.text = Convert.ToString(RoleData._level);
        NowExp = GameObject.Find("NowExp").GetComponent<Text>();
        RoleData._nowExp = int.Parse(NowExp.text);
        NeedExp = GameObject.Find("NeedExp").GetComponent<Text>();
        RoleData._needExp = int.Parse(NeedExp.text);
        sli.value = RoleData._nowExp / RoleData._needExp;
        Percentage = GameObject.Find("Percentage").GetComponent<Text>();
        Percentage.text = Convert.ToString(sli.value * 100) + "%";
        TotalExperience = GameObject.Find("TotalExperience").GetComponent<Text>();
        TotalExperience.text = "+0";
        Money = GameObject.Find("Money").GetComponent<Text>();
        ItemData._money = 0;
        Money.text = "0";

        NowLife = GameObject.Find("LifePanel").transform.GetChild(2).GetComponent<Text>();
        NextLife = GameObject.Find("LifePanel").transform.GetChild(1).GetComponent<Text>(); 
        nextLife = RoleData._lifePoint + RoleData._constitution * RoleData._lifeGrowth;            
        NowLife.text = Convert.ToString(RoleData._lifePoint);
        NextLife.text = Convert.ToString(nextLife);

        NowMana = GameObject.Find("ManaPanel").transform.GetChild(2).GetComponent<Text>();
        NextMana = GameObject.Find("ManaPanel").transform.GetChild(1).GetComponent<Text>();
        nextMana = RoleData._manaPoint + RoleData._intelligence * RoleData._manaGrowth;
        NowMana.text = Convert.ToString(RoleData._manaPoint);
        NextMana.text = Convert.ToString(nextMana);

        NowAttack = GameObject.Find("AttackPanel").transform.GetChild(2).GetComponent<Text>();
        NextAttack = GameObject.Find("AttackPanel").transform.GetChild(1).GetComponent<Text>();
        nextAttack = RoleData._physicalAttackPoint + RoleData._physicalAttackGrowth * RoleData._powerPoint;
        NowAttack.text = Convert.ToString(RoleData._physicalAttackPoint);
        NextAttack.text = Convert.ToString(nextAttack);

        NowMAttack = GameObject.Find("MAttackPanel").transform.GetChild(2).GetComponent<Text>();
        NextMAttack = GameObject.Find("MAttackPanel").transform.GetChild(1).GetComponent<Text>();
        nextMAttack = RoleData._magicAttackPoint + RoleData._intelligence * RoleData._magicAttackGrowth;
        NowMAttack.text = Convert.ToString(RoleData._magicAttackPoint);
        NextMAttack.text = Convert.ToString(nextMAttack);

        NowDodge = GameObject.Find("DodgePanel").transform.GetChild(2).GetComponent<Text>();
        NextDodge = GameObject.Find("DodgePanel").transform.GetChild(1).GetComponent<Text>();
        nextDodge = RoleData._dodgePoint + RoleData._dodgeGrowth * RoleData._agile;
        NowDodge.text = Convert.ToString(RoleData._dodgePoint);
        NextDodge.text = Convert.ToString(nextDodge);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void onItem1AddButtonClick()
    {
        //使用道具
        if (ItemData._UseNum1 - ItemData._ItemMaxNum1 < 0)
        { 
            ItemData._UseNum1++;
            RoleData._totalExperience += 1000;
            ItemData._money += 1000;
        }
        
        else
        {
            ItemData._UseNum1=ItemData._ItemMaxNum1;
        }

        if(!UsedItem1.activeSelf)
        {
            UsedItem1.SetActive(true);
        }
        Item1Used.SetActive(true);
        UseNum1.text = Convert.ToString(ItemData._UseNum1);
        TotalExperience.text = "+"+Convert.ToString(RoleData._totalExperience);
        Money.text =  Convert.ToString(ItemData._money);
        useNum1.text = "X"+Convert.ToString(ItemData._UseNum1);
        
    }
    public void onItem1ReduceButtonClick()
    {
        //取消使用
        if (ItemData._UseNum1 - 1 > 0)
        {
            ItemData._UseNum1--;
            UseNum1.text = Convert.ToString(ItemData._UseNum1);
            useNum1.text = "X" + Convert.ToString(ItemData._UseNum1);
        }
        else if (ItemData._UseNum1 - 1 == 0)
        {
            ItemData._UseNum1 = 0;
            Item1Used.SetActive(false);
            UsedItem1.SetActive(false);
        }
        RoleData._totalExperience = RoleData._totalExperience-1000;
        ItemData._money = ItemData._money - 1000;
        TotalExperience.text = "+"+Convert.ToString(RoleData._totalExperience);
        Money.text = Convert.ToString(ItemData._money);
    }

    public void onItem2AddButtonClick()
    {
        //使用道具
        if (ItemData._UseNum2 - ItemData._ItemMaxNum2 < 0)
        {
            ItemData._UseNum2++;
            RoleData._totalExperience += 500;
            ItemData._money += 500;
        }
        else
        {
            ItemData._UseNum2 = ItemData._ItemMaxNum2;
        }
        if(!UsedItem2.activeSelf)
        {
            UsedItem2.SetActive(true);
            
        }
        
        Item2Used.SetActive(true);
        UseNum2.text = Convert.ToString(ItemData._UseNum2);
        TotalExperience.text = "+" + Convert.ToString(RoleData._totalExperience);
        Money.text = Convert.ToString(ItemData._money);
        useNum2.text = "X" + Convert.ToString(ItemData._UseNum2);
    }
    public void onItem2ReduceButtonClick()
    {

        //取消使用
        if (ItemData._UseNum2 - 1 > 0)
        {
            ItemData._UseNum2--;
            UseNum2.text = Convert.ToString(ItemData._UseNum2);
            useNum2.text = "X" + Convert.ToString(ItemData._UseNum2);
        }
        else if (ItemData._UseNum2 - 1 == 0)
        {
            ItemData._UseNum2 = 0;
            Item2Used.SetActive(false);
            UsedItem2.SetActive(false);
        }
        RoleData._totalExperience = RoleData._totalExperience - 500;
        ItemData._money = ItemData._money - 500;
        TotalExperience.text = "+" + Convert.ToString(RoleData._totalExperience);
        Money.text = Convert.ToString(ItemData._money);
    }
    public void onItem3AddButtonClick()
    {
        //使用道具
        if (ItemData._UseNum3 - ItemData._ItemMaxNum3 < 0)
        {
            ItemData._UseNum3++;
            RoleData._totalExperience += 250;
            ItemData._money += 250;
        }
        else
        {
            ItemData._UseNum3 = ItemData._ItemMaxNum3;
        }
        if(!UsedItem3.activeSelf)
        {
            UsedItem3.SetActive(true);
        }
        useNum3.text = "X" + Convert.ToString(ItemData._UseNum3);
        Item3Used.SetActive(true);
        UseNum3.text = Convert.ToString(ItemData._UseNum3);
        TotalExperience.text = "+" + Convert.ToString(RoleData._totalExperience);
        Money.text = Convert.ToString(ItemData._money);
    }
    public void onItem3ReduceButtonClick()
    {
        //取消使用
        if (ItemData._UseNum3 - 1 > 0)
        {
            ItemData._UseNum3--;
            UseNum3.text = Convert.ToString(ItemData._UseNum3);
            useNum3.text = "X" + Convert.ToString(ItemData._UseNum3);
        }
        else if (ItemData._UseNum3 - 1 == 0)
        {
            ItemData._UseNum3 = 0;
            Item3Used.SetActive(false);
            UsedItem3.SetActive(false);
        }
        RoleData._totalExperience = RoleData._totalExperience - 250;
        ItemData._money = ItemData._money - 250;
        TotalExperience.text = "+" + Convert.ToString(RoleData._totalExperience);
        Money.text = Convert.ToString(ItemData._money);
    }
    public void onItem4AddButtonClick()
    {
        //使用道具
        if (ItemData._UseNum4 - ItemData._ItemMaxNum4 < 0)
        {
            ItemData._UseNum4++;
            RoleData._totalExperience += 125;
            ItemData._money += 125;
        }
        else
        {
            ItemData._UseNum4 = ItemData._ItemMaxNum4;
        }
        if(!UsedItem4.activeSelf)
        {
            UsedItem4.SetActive(true);
        }
        Item4Used.SetActive(true);
        UseNum4.text = Convert.ToString(ItemData._UseNum4);
        TotalExperience.text = "+" + Convert.ToString(RoleData._totalExperience);
        Money.text = Convert.ToString(ItemData._money);
        useNum4.text = "X" + Convert.ToString(ItemData._UseNum4);
    }
    public void onItem4ReduceButtonClick()
    {
        //取消使用
        if (ItemData._UseNum4 - 1 > 0)
        {
            ItemData._UseNum4--;
            UseNum4.text = Convert.ToString(ItemData._UseNum4);
            useNum4.text = "X" + Convert.ToString(ItemData._UseNum4);
        }
        else if (ItemData._UseNum4 - 1 == 0)
        {
            ItemData._UseNum4 = 0;
            Item4Used.SetActive(false);
            UsedItem4.SetActive(false);
        }
        RoleData._totalExperience = RoleData._totalExperience - 125;
        ItemData._money = ItemData._money - 125;
        TotalExperience.text = "+" + Convert.ToString(RoleData._totalExperience);
        Money.text = Convert.ToString(ItemData._money);
    }

    public void onBackButtpnClick()
    {
        SceneManager.LoadScene("RoleDevelopment");
        RoleData._totalExperience = 0;
    }
    public void onMainButtonClick()
    {
        SceneManager.LoadScene("MainScene");
        RoleData._totalExperience = 0;
        
    }
  
    public void onResetButtonClick()
    {
        //重置数据
        if (Item1.activeSelf)
        {
            if(Item1Used.activeSelf)
            {
                Item1Used.SetActive(false);
            }
            if(UsedItem1.activeSelf)
            {
                UsedItem1.SetActive(false);
                ItemData._UseNum1 = 0;
            }
            
        }
        if (Item2.activeSelf)
        {
            if (UsedItem2.activeSelf)
            {
                UsedItem2.SetActive(false);
            }
            if (Item2Used.activeSelf)
            {
                Item2Used.SetActive(false);
                ItemData._UseNum2 = 0;
            }
            
        }
        if (Item3.activeSelf)
        {
            if (UsedItem3.activeSelf)
            {
                UsedItem3.SetActive(false);
            }
            if (Item3Used.activeSelf)
            {
                Item3Used.SetActive(false); 
                ItemData._UseNum3 = 0;
            }
           
        }
        if (Item4.activeSelf)
        {
            if (UsedItem4.activeSelf)
            {
                UsedItem4.SetActive(false);
            }
            if (Item4Used.activeSelf)
            {
                Item4Used.SetActive(false);
                ItemData._UseNum4 = 0;
            }
            
        }
        RoleData._totalExperience = 0;
        ItemData._money = 0;
        TotalExperience.text = Convert.ToString(RoleData._totalExperience);
        Money.text = Convert.ToString(ItemData._money);
    }

    public void onConfirmButtonClick()
    {
        if(Item1.activeSelf)
        {

            Item1Used.SetActive(false);
            ItemData._ItemMaxNum1 -= ItemData._UseNum1; 
            ItemData._UseNum1 = 0;
            if(ItemData._ItemMaxNum1==0)
            {
                Item1.SetActive(false);
            }
            Item1Num.text= Convert.ToString(ItemData._ItemMaxNum1);
            UsedItem1.SetActive(false);
        }
        if (Item2.activeSelf)
        {
            Item2Used.SetActive(false);
            ItemData._ItemMaxNum2 -= ItemData._UseNum2;
            ItemData._UseNum2 = 0;
            if (ItemData._ItemMaxNum2 == 0)
            {
                Item2.SetActive(false);
            }
            Item2Num.text = Convert.ToString(ItemData._ItemMaxNum2);
            UsedItem2.SetActive(false);
        }

        if (Item3.activeSelf)
        {
            Item3Used.SetActive(false);
            ItemData._ItemMaxNum3 -= ItemData._UseNum3;
            ItemData._UseNum3 = 0;
            if (ItemData._ItemMaxNum3 == 0)
            {
                Item3.SetActive(false);
            }
            Item3Num.text = Convert.ToString(ItemData._ItemMaxNum3);
            UsedItem3.SetActive(false);
        }
        if (Item4.activeSelf)
        {
            Item4Used.SetActive(false);
            ItemData._ItemMaxNum4 -= ItemData._UseNum4;
            ItemData._UseNum4 = 0;
            if (ItemData._ItemMaxNum4 == 0)
            {
                Item4.SetActive(false);
            }
            Item4Num.text = Convert.ToString(ItemData._ItemMaxNum4);
            UsedItem4.SetActive(false);
        }
        RoleData._nowExp += RoleData._totalExperience;
        while (true)
        {
            //等级提升
            if (RoleData._nowExp >= RoleData._needExp)
            {
                RoleData._nowExp -= RoleData._needExp;
                RoleData._level += 1;
                RoleData._lifePoint = nextLife;
                nextLife = RoleData._lifePoint + RoleData._powerPoint * RoleData._lifeGrowth;
                NowLife.text = Convert.ToString(RoleData._lifePoint);
                NextLife.text = Convert.ToString(nextLife);

                RoleData._manaPoint = nextMana;
                nextMana = RoleData._manaPoint + RoleData._intelligence * RoleData._manaGrowth;
                NowMana.text = Convert.ToString(RoleData._manaPoint);
                NextMana.text = Convert.ToString(nextMana);

                RoleData._physicalAttackPoint = nextAttack;
                nextAttack = RoleData._physicalAttackPoint + RoleData._powerPoint * RoleData._physicalAttackGrowth;
                NowAttack.text = Convert.ToString(RoleData._physicalAttackPoint);
                NextAttack.text = Convert.ToString(nextAttack);

                RoleData._magicAttackPoint = nextMAttack;
                nextMAttack = RoleData._magicAttackPoint + RoleData._intelligence * RoleData._magicAttackGrowth;
                NowMAttack.text = Convert.ToString(RoleData._magicAttackPoint);
                NextMAttack.text = Convert.ToString(nextMAttack);

                RoleData._dodgePoint = nextDodge;
                nextDodge = RoleData._agile * RoleData._dodgeGrowth + RoleData._dodgePoint;
                NowDodge.text = Convert.ToString(RoleData._dodgePoint);
                NextDodge.text = Convert.ToString(nextDodge);
            }
            else
                break;
        }
        NowExp.text = Convert.ToString(RoleData._nowExp);
        sli.value = RoleData._nowExp / RoleData._needExp;
        LevelPoint.text = Convert.ToString(RoleData._level);
        RoleData._totalExperience = 0;
        ItemData._money = 0;
        TotalExperience.text = "+" + Convert.ToString(RoleData._totalExperience);
        Money.text = Convert.ToString(ItemData._money);
        Percentage.text = Convert.ToString(sli.value * 100) + "%";
        
        
    }
}
