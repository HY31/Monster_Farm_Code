using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoad : MonoBehaviour
{
    
    static string nextScene;

    public Slider progressBar;
    public TMP_Text loadingTxt;

    SoundManager soundManager;

    private void Awake()
    {
        soundManager = SoundManager.instance;
    }

    public static void LoadScene(string scene)
    {
        nextScene = scene;
        SceneManager.LoadScene("Loading");
    }

    private void Start()
    {
        soundManager.StopBGM();
        StartCoroutine(LoadGameLobbyScene());
    }

    IEnumerator LoadGameLobbyScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
        operation.allowSceneActivation = false; // 씬을 비동기로 불러들일 때 자동으로 다음 씬으로 넘어갈 지 설정하는 것. false로 하면 90%(0.9f)까지만 동기한 후 기다린다.

        while (!operation.isDone)
        {
            yield return null;
            if(progressBar.value < 1f)
            {
                progressBar.value = Mathf.MoveTowards(progressBar.value, 1f, Time.deltaTime); // 진행에 따라 로딩바가 차오른다
            }
            else
            {
                loadingTxt.text = "Press SpaceBar";
            }

            if(Input.GetKeyDown(KeyCode.Space) && progressBar.value >= 1f && operation.progress >= 0.9f)
            {
                soundManager.PlayLoadingEffect();
                operation.allowSceneActivation = true;
            }
        }
    }
}
