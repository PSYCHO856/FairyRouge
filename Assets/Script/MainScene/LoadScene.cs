using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private Text Progress;

    private Slider slider;

    private float ProgressValue;

    public string nextSceneName;

    private AsyncOperation async = null;
    // Start is called before the first frame update
    void Start()
    {
        Progress = GetComponent<Text>();
        slider = transform.GetComponentInParent<Slider>();
        StartCoroutine(LoadAsyncScene());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LoadAsyncScene()
    {
        async = SceneManager.LoadSceneAsync(nextSceneName);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            if (async.progress < 0.9f)
                ProgressValue = async.progress;
            else
                ProgressValue = 1.0f;
            slider.value = ProgressValue;
            Progress.text = (int)(slider.value * 100) + " %";


            if (ProgressValue >= 0.9)
            {
                Progress.text = "按任意键继续";

                if (Input.anyKeyDown)
                {
                    async.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}
