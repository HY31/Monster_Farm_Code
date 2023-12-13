using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    public Image panel;
    public GameObject textBox;

    private void Awake()
    {
        if(!PlayerPrefs.HasKey("TutorialCompleted"))
        {
            PlayerPrefs.SetInt("TutorialCompleted", 0);
        }
        instance = this;
        InIt();
    }

    private void Start()
    {
        if(GameManager.instance != null)
        {
            if (GameManager.instance.GetDays() >= 1)
            {
                // 튜토리얼 끝난거 PlayerPrefs로 저장하기
                PlayerPrefs.SetInt("TutorialCompleted", 1);
            }
            else
            {
                return;
            }
            
        }
        else
        {
            return;
        }
        if (PlayerPrefs.GetInt("TutorialCompleted") >= 1)
        {
            CompletedAllTutorials();
        }
    }
    public void SetTutorialPanelActive(bool isActive)
    {
        panel.gameObject.SetActive(isActive);
        textBox.gameObject.SetActive(isActive);
    }

    public void InIt()
    {
        SetTutorialPanelActive(false);
    }

    public void CompletedAllTutorials()
    {
        Destroy(gameObject);
    }
}
