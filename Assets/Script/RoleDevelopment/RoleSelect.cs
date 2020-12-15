using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;

public class RoleSelect : MonoBehaviour
{
    private Image roleDrawing;
    private string ButtonName;
    private string _Path;
    private string _texname;
    private Texture2D m_Tex;
    private string CurrentName;
    private Text roleName;
    private Text roleLevel;
    
    // Start is called before the first frame update
    void Start()
    {
        ExcelDataInit.Init();

        roleLevel = transform.GetChild(3).GetComponent<Text>();
        roleDrawing = transform.GetChild(0).GetComponent<Image>();
        roleName = transform.GetChild(3).GetComponent<Text>();

        //加载头像
        CurrentName = gameObject.name;
        RoleData._roleId = int.Parse(CurrentName);
        roleLevel.text = Convert.ToString(RoleData._level);
       /* _Path = "Assets/Resources/RoleDrawing/";
        _texname = RoleData._roleId + ".jpg";
        LoadFromFile(_Path, _texname);
        Sprite tempSprite = Sprite.Create(m_Tex, new Rect(0, 0, m_Tex.width, m_Tex.height), new Vector2(10, 10));
        roleDrawing.sprite = tempSprite;*/

        //加载角色名
       // roleName.text = RoleInformation.GetSheet((int)RoleInformation.SheetName.Sheet1).GetData(RoleData._roleId).name;

       /* _Path = "Assets/Resources/other/";
        _texname = RoleInformation.
            GetSheet((int)RoleInformation.SheetName.Sheet1).GetData(RoleData._roleId).occupationID + ".jpg";
        LoadFromFile(_Path, _texname);
        Sprite neoSprite = Sprite.Create(m_Tex, new Rect(0, 0, m_Tex.width, m_Tex.height), new Vector2(10, 10));
        transform.Find("Icon").GetComponent<Image>().sprite = neoSprite;*/
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ontestButtonClick()
    {
        RoleData._roleId = int.Parse(gameObject.name);
        RoleData._level = int.Parse(gameObject.name);
        SceneManager.LoadScene("RoleDevelopment");
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
    