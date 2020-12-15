using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScene : MonoBehaviour {

    void Start () {
        ExcelDataInit.Init();

    }

    void Update () {

    }

    /// <summary>
    /// 开始游戏
    /// </summary>
    public void startGame () {
        SceneManager.LoadScene ("LoadScene");
    }

    /// <summary>
    /// 关闭游戏
    /// </summary>
    public void exitGame () {
        Application.Quit();
    }
}