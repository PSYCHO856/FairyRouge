using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
public class ItemSelect : MonoBehaviour
{
  
    private Texture2D m_Tex;
    public GameObject Obj1;
    public GameObject Obj2;
    private GameObject testObj;
    // Start is called before the first frame update
    void Start()
    {
        ExcelDataInit.Init();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ontestButtonClick()
    {
        testObj = GameObject.Find("ConsumablesPanel(Clone)");
        
        
        if(testObj==null)
        {
            if(int.Parse(gameObject.name)%2==0)
            {
                 GameObject.Instantiate(Obj1, gameObject.transform.parent.parent.parent.parent.transform);
            }
            else
            {
                GameObject.Instantiate(Obj2, gameObject.transform.parent.parent.parent.parent.transform);
            }
        }
        
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
    