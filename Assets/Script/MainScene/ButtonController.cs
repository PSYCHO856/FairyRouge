using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onLevelButotnClick()
    {
        SceneManager.LoadScene("Chapter");
    }
    public void onBattleButtonClick()
    {
        SceneManager.LoadScene("BattleScene");
    }
    public void onBackButton1Click()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void onBackButton2Click()
    {
        SceneManager.LoadScene("MainLine");
    }
}
