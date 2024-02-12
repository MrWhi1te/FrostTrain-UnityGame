using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Text LoadText;
    public Slider LoadSlide;

    //public static int Score; //

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            LoadSlide.value = async.progress;
            if (async.progress == 0.3f)
            {
                LoadText.text = "������������ ������ �� ��������";
            }
            if (async.progress == 0.6f)
            {
                LoadText.text = "��������� ����� �� �������";
            }
            if (async.progress == 0.8f)
            {
                LoadText.text = "��������� �� ������� � ����";
            }
            if (async.progress == 0.9f)
            {
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
