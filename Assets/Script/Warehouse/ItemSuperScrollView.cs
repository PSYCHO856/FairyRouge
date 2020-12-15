using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;


public class ItemSuperScrollView : MonoBehaviour
{
    // Start is called before the first frame update
    List<GameObject> item;
    ScrollRect superScrollRect;
    RectTransform superRect;

    [HideInInspector]
    public float itemHeight;
    [HideInInspector]
    public float itemWidth;
    [Header("数据总数")]
    public int itemNum = 30;
    [Header("显示的数据总数")]
    public int maxItemNum = 6;
    int firstIndex;
    int lastIndex;
    [Header("数据分行")]
    public int queue = 2;
    [Header("预设体间隔")]
    public int interval = 10;
    [Header("预加载列数")]
    public int external = 1;
    [Header("预设体（需要有文字组件）")]
    public GameObject _obj;
    [Header("预设体的文字组件名")]
    public string textName = "Text";

    
    private string _Path;
    private string _texname;
    private Texture2D m_Tex;

    //上一个按钮
    private static GameObject previousBtn;
    private Text txt;
    private Image image;
   
     //当前按钮
    private GameObject btn;
    private Text txtNeo;
    private Image imageNeo;
    void Start()
    {
        Check();   
        item = new List<GameObject>();
        superScrollRect = GetComponent<ScrollRect>();
        superRect = superScrollRect.content.transform.GetComponent<RectTransform>();
        superScrollRect.onValueChanged.AddListener((Vector2 vec) => OnScrollMoveWidth(vec));
        itemHeight = _obj.GetComponent<RectTransform>().rect.height+ interval;
        itemWidth = _obj.GetComponent<RectTransform>().rect.width+ interval;
        SetContentWidth();
        InsCountitemWidth();

        previousBtn = GameObject.Find("All");
        txt = previousBtn.GetComponentInChildren<Text>();
        image = previousBtn.GetComponent<Image>();
        image.color = Color.black;
        txt.color = Color.white;

    }
    void Check()
    {
        if (itemNum < maxItemNum)
        {
            return;
        }
        if (queue == 0 || maxItemNum % queue != 0)
        {
            return;
        }
        if (itemNum % maxItemNum != 0)
        {
            int current = itemNum;
            itemNum = (itemNum / maxItemNum + 1) * maxItemNum;
        }
        if (_obj == null)
        {
            return;
        }
        if (maxItemNum / queue * (queue + external) > itemNum)
        {
            int current = external;
            external = (itemNum / queue) - maxItemNum / queue;
        }
        if (_obj.transform.Find(textName) == null)
        {
            return;
        }
    }
    #region 左右进行滚动
    /// <summary>
    /// 设置滚动条content的宽度、锚点以及ScrollView的长宽   
    /// </summary>
    public void SetContentWidth()
    {
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(itemWidth * maxItemNum / queue, (_obj.GetComponent<RectTransform>().sizeDelta.y + 20) * queue);
        superRect.sizeDelta = new Vector2(itemNum * itemWidth / queue , 0);
        superRect.anchorMin = new Vector2(0, 0);
        superRect.anchorMax = new Vector2(0, 1);
    }

    public void InsCountitemWidth()
    {
        int needItemNum = Mathf.Clamp(maxItemNum, 1, itemNum);
        for (int j = 0; j < needItemNum / queue + external; j++)
        {
            for (int i = 0; i < queue; i++)
            {
                GameObject obj = Instantiate(_obj, superRect);
                obj.name = (j * queue + i+1).ToString();
                obj.transform.Find(textName).GetComponent<Text>().text = obj.name;
                RectTransform _rect = obj.transform.GetComponent<RectTransform>();
                _rect.pivot = new Vector2(0, 1);
                _rect.anchorMin = new Vector2(0, 1);
                _rect.anchorMax = new Vector2(0, 1);
                _rect.anchoredPosition = new Vector2(j * itemWidth, -itemHeight * i);
                item.Add(obj);
            }
        }
        lastIndex = needItemNum - 1 + queue;
    }
    private void OnScrollMoveWidth(Vector2 pVec)
    {
        //从左往右
        while (Mathf.Abs(superRect.anchoredPosition.x) > itemWidth * (firstIndex / queue ) && lastIndex < itemNum - 1)
        {
            for (int i = 0; i < queue; i++)
            {
                GameObject _first = item[0];
                RectTransform _firstRect = _first.GetComponent<RectTransform>();
                item.RemoveAt(0);
                item.Add(_first);
                _firstRect.anchoredPosition = new Vector2(((lastIndex + 1) / queue) * itemWidth, _firstRect.anchoredPosition.y);
                firstIndex++;
                lastIndex++;

                _first.name = (lastIndex+1).ToString();
                _first.transform.Find("Text").GetComponent<Text>().text = _first.name;
            }
        }
        //从右往左
        while (Mathf.Abs(superRect.anchoredPosition.x) < itemWidth * firstIndex / queue && firstIndex > 0)
        {
            for (int i = 0; i < queue; i++)
            {
                GameObject _last = item[item.Count - 1];
                RectTransform _lastRect = _last.GetComponent<RectTransform>();
                if(item.Count - 1 == 0) 
                {
                    
                }
                else
                {
                    item.RemoveAt(item.Count - 1);
                }
                
                item.Insert(0, _last);
                _lastRect.anchoredPosition = new Vector2(((firstIndex - 1) / queue) * itemWidth, _lastRect.anchoredPosition.y);

                firstIndex--;
                lastIndex--;
                //修改显示
                _last.name = (firstIndex+1).ToString();
                _last.transform.Find("Text").GetComponent<Text>().text = _last.name;
            }

        }
    }
    #endregion
    #region 上下进行滚动
    /// <summary>
    /// 设置滚动条content的高度
    /// </summary>
    public void SetContentHeight()
    {
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(_obj.GetComponent<RectTransform>().sizeDelta.x + 20, maxItemNum * itemHeight);
        superRect.sizeDelta = new Vector2(0, itemNum * itemHeight);
        superRect.anchorMin = new Vector2(0, 1);
        superRect.anchorMax = new Vector2(1, 1);
    }

    public void InsCountitem()
    {
        int needItemNum = Mathf.Clamp(maxItemNum, 0, itemNum);
        for (int i = 0; i < needItemNum; i++)
        {
            GameObject obj = Instantiate(_obj, superRect);
            obj.name = i.ToString();
            obj.transform.Find("Text").GetComponent<Text>().text = i.ToString();
            RectTransform _rect = obj.transform.GetComponent<RectTransform>();
            _rect.pivot = new Vector2(0.5f, 1);
            _rect.anchorMin = new Vector2(0.5f, 1);
            _rect.anchorMax = new Vector2(0.5f, 1);
            _rect.anchoredPosition = new Vector2(0, -itemHeight * i);
            item.Add(obj);
            lastIndex = i;
        }
    }

    private void OnScrollMove(Vector2 pVec)
    {
        while (superRect.anchoredPosition.y > itemHeight * (firstIndex) && lastIndex != itemNum - 1)
        {
            GameObject _first = item[0];
            RectTransform _firstRect = _first.GetComponent<RectTransform>();
            item.RemoveAt(0);
            item.Add(_first);
            _firstRect.anchoredPosition = new Vector2(_firstRect.anchoredPosition.x, -(lastIndex + 1) * itemHeight);

            firstIndex++;
            lastIndex++;
            //修改显示
            _first.name = lastIndex.ToString();
            _first.transform.Find("Text").GetComponent<Text>().text = _first.name;
        }
        while (superRect.anchoredPosition.y < itemHeight * firstIndex && firstIndex != 0)
        {
            GameObject _last = item[item.Count - 1];
            RectTransform _lastRect = _last.GetComponent<RectTransform>();
            item.RemoveAt(item.Count - 1);
            item.Insert(0, _last);
            _lastRect.anchoredPosition = new Vector2(_lastRect.anchoredPosition.x, -(firstIndex - 1) * itemHeight);

            firstIndex--;
            lastIndex--;
            //修改显示
            _last.name = firstIndex.ToString();
            _last.transform.Find("Text").GetComponent<Text>().text = _last.name;
        }
    }
    #endregion

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
    public void onBackButtonClick()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void onMainSceneButtonClick()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void onAllButtonClick()
    {
        btn = EventSystem.current.currentSelectedGameObject;
        if(btn.name==previousBtn.name)
        {
            Debug.LogError(btn.name);
        }
        else
        {
            txtNeo = btn.GetComponentInChildren<Text>();
            imageNeo = btn.GetComponent<Image>();

            txt.color = Color.black;
            image.color = Color.white;

            txtNeo.color = Color.white;
            imageNeo.color = Color.black;

            previousBtn = btn;

            txt = previousBtn.GetComponentInChildren<Text>();
            image = previousBtn.GetComponent<Image>();
            
        }       
    }
}
