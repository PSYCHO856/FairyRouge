using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Security.Cryptography;

public class MainScene : MonoBehaviour
{
    private Text PlayerLevel;
    private Text PlayerId;
    private Text PlayerName;
    private Text DiamondsNum;
    private Text GoldCoinNum;

    public GameObject setting;
    private string ButtonName;
    // Start is called before the first frame update
    void Start()
    {
        PlayerLevel = GameObject.Find("PlayerLevel").GetComponentInChildren<Text>();
        PlayerData._playerLevel = 1214;
        PlayerLevel.text = Convert.ToString(PlayerData._playerLevel);

        PlayerId = GameObject.Find("PlayerId").GetComponent<Text>();
        PlayerData._playerId = 15795638;
        PlayerId.text = Convert.ToString(PlayerData._playerId);

        PlayerName = GameObject.Find("PlayerName").GetComponent<Text>();
        PlayerData._playerName = "陈Sir";
        PlayerName.text = PlayerData._playerName;

        DiamondsNum = GameObject.Find("Diamonds").GetComponentInChildren<Text>();
        PlayerData._DiamondsNum = 130000;
        DiamondsNum.text = Convert.ToString(PlayerData._DiamondsNum);

        GoldCoinNum = GameObject.Find("GoldCoin").GetComponentInChildren<Text>();
        PlayerData._GoldCoinNum = 15000;
        GoldCoinNum.text = Convert.ToString(PlayerData._GoldCoinNum);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void onMailButtonClick()
    {
        Debug.Log("邮件");
    }
    public void onSettingButtonClick()
    {
        GameObject.Instantiate(setting.gameObject.transform);
    }
    public void onAnnouncementButtonClick()
    {
        var button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        ButtonName = button.name;
        string result = System.Text.RegularExpressions.Regex.Replace(ButtonName, @"[^0-9]+", "");
        Debug.Log("当前是活动" + result);
    }
    public void onBattleButtonClick()
    {
        SceneManager.LoadScene("MainLine");
    }
    public void onRaidButtonClick()
    {
        Debug.Log("副本");
    }
    public void onAcitvityButtonClick()
    {
        Debug.Log("活动");
    }
    public void onDrawButtonClick()
    {
        Debug.Log("抽卡");
    }
    public void onRoleSelectButtonClick()
    {
        SceneManager.LoadScene("RoleSelect");
    }
    public void onStoreHouseButtonClick()
    {
        SceneManager.LoadScene("WarehouseScene");
    }
    public void onBaseButtonClick()
    {
        Debug.Log("基地");
    }
    public void onTaskButtonClick()
    {
        Debug.Log("任务");
    }
    public void onIllustrationButtonClick()
    {
        Debug.Log("图鉴");
    }
    public void onShopButtonClick()
    {
        Debug.Log("商店");
    }
    public void onDiamondsNumAddButtonClick()
    {
        Debug.Log("钻石增加");
    }
    public void onGoldCoinButtonClick()
    {
        Debug.Log("金币增加");
    }
}
