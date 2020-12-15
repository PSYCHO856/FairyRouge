using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
public class RoleSuperScrollView : MonoBehaviour
{
    // Start is called before the first frame update
    List<GameObject> item;//
    ScrollRect superScrollRect;//
    RectTransform superRect;//

    [HideInInspector]
    public float itemHeight;//数据的高度(竖着)
    [HideInInspector]
    public float itemWidth;//数据的宽度(横着)
    [Header("您一共想要多少数据")]
    public int itemNum = 30;//总数据的个数
    [Header("您想显示多少数据")]
    public int maxItemNum = 6;//显示多少个数据
    int firstIndex;
    int lastIndex;
    [Header("您想将显示的数据分为几行")]
    public int queue = 2;
    [Header("您想把预设体的间隔设置为多大")]
    public int interval = 10;
    [Header("您想多加载几列呢")]
    public int external = 1;
    [Header("预设体（需要有文字组件）")]
    public GameObject _obj;



    private string _Path;
    private string _texname;
    private Texture2D m_Tex;


    void Start()
    {
        ExcelDataInit.Init();
        Check();

        item = new List<GameObject>();
        superScrollRect = GetComponent<ScrollRect>();
        superRect = superScrollRect.content.transform.GetComponent<RectTransform>();
        superScrollRect.onValueChanged.AddListener((Vector2 vec) => OnScrollMoveWidth(vec));
        itemHeight = _obj.GetComponent<RectTransform>().rect.height + interval;
        itemWidth = _obj.GetComponent<RectTransform>().rect.width + interval;

        SetContentWidth();
        InsCountitemWidth();
    }
    void Check()
    {
        if (itemNum < maxItemNum)
        {
            Debug.LogError("总数据小于了显示数据了!请重新修改，以保证总数据数量大于显示数据");
            return;
        }
        if (queue == 0 || maxItemNum % queue != 0)
        {
            Debug.LogError("无法将显示数据以" + queue + "行的形式分配，请重新修改");
            return;
        }
        if (itemNum % maxItemNum != 0)
        {
            int current = itemNum;
            itemNum = (itemNum / maxItemNum + 1) * maxItemNum;
            Debug.LogWarning("您填写的总数据<color=red>" + current + "</color>并不能刚好被分配，已经修改成总数为:<color=green>" + itemNum + "</color>");
        }
        if (_obj == null)
        {
            Debug.LogError("背包格子的预设体是空，拒绝运行！");
            return;
        }
        if (maxItemNum / queue * (queue + external) > itemNum)
        {
            int current = external;
            external = (itemNum / queue) - maxItemNum / queue;
            Debug.LogWarning("您填写的额外列数<color=red>" + current + "</color>在生成后已经大于了总数据个数了，已经修改为:<color=green>" + external + "</color>");
        }
      
        //以上是校验数据
    }
    #region 左右进行滚动
    /// <summary>
    /// 设置滚动条content的宽度、锚点以及ScrollView的长宽
    /// </summary>
    public void SetContentWidth()
    {
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(itemWidth * maxItemNum / queue, (_obj.GetComponent<RectTransform>().sizeDelta.y + 20) * queue);
        superRect.sizeDelta = new Vector2(itemNum * itemWidth / queue, 0);
        superRect.anchorMin = new Vector2(0, 0);
        superRect.anchorMax = new Vector2(0, 1);
    }

    public void InsCountitemWidth()
    {
        int needItemNum = Mathf.Clamp(maxItemNum, 0, itemNum);
        for (int j = 0; j < needItemNum / queue + external; j++)
        {
            for (int i = 0; i < queue; i++)
            {


                GameObject obj = Instantiate(_obj, superRect);
                obj.name = (j * queue + i+1).ToString();
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
        while (Mathf.Abs(superRect.anchoredPosition.x) > itemWidth * (firstIndex / queue) && lastIndex < itemNum - 1)
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
                //修改显示
                _first.name = (lastIndex+1).ToString();
                

               /* RoleData._roleId = lastIndex + 1;*/
               /* _Path = "Assets/Resources/RoleDrawing/";
                _texname = RoleData._roleId + ".jpg";
                LoadFromFile(_Path, _texname);
                Sprite tempSprite = Sprite.Create(m_Tex, new Rect(0, 0, m_Tex.width, m_Tex.height), new Vector2(10, 10));
                _first.transform.Find("Drawing").GetComponent<Image>().sprite = tempSprite;*/


            }
        }
        //从右往左
        while (Mathf.Abs(superRect.anchoredPosition.x) < itemWidth * firstIndex / queue && firstIndex > 0)
        {
            for (int i = 0; i < queue; i++)
            {
                GameObject _last = item[item.Count - 1];
                RectTransform _lastRect = _last.GetComponent<RectTransform>();
                item.RemoveAt(item.Count - 1);
                item.Insert(0, _last);
                _lastRect.anchoredPosition = new Vector2(((firstIndex - 1) / queue) * itemWidth, _lastRect.anchoredPosition.y);

                firstIndex--;
                lastIndex--;
                //修改显示
                _last.name = (firstIndex+1).ToString();
               

               /* RoleData._roleId = firstIndex + 1;*/
                /*_Path = "Assets/Resources/RoleDrawing/";
                _texname = RoleData._roleId + ".jpg";
                LoadFromFile(_Path, _texname);
                Sprite tempSprite = Sprite.Create(m_Tex, new Rect(0, 0, m_Tex.width, m_Tex.height), new Vector2(10, 10));
                _last.transform.Find("Drawing").GetComponent<Image>().sprite = tempSprite;*/
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

}
