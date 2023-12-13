using UnityEngine;
using UnityEngine.SceneManagement;

// Subscriber
public class TutorialStepSubscriber : MonoBehaviour
{
    public DialogueManager dialogueManager;

    // 튜토리얼 2를 위한 플래그
    private bool hasEnteredTrigger = false;

    // Publisher 생성
    TutorialEventPublisher tutorialPublisher = new TutorialEventPublisher();
    
    private void Start()
    {
        // Subscriber 등록
        if (tutorialPublisher != null)
        {
            tutorialPublisher.OnTutorialStepCompleted += HandleTutorialStep;
        }

        if (PlayerPrefs.HasKey("TutorialCompleted") && PlayerPrefs.GetInt("TutorialCompleted") == 0)
        {
            CheckAndPublishTutorialStep();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasEnteredTrigger && PlayerPrefs.GetInt("TutorialCompleted") == 0)
        {
            tutorialPublisher.PublishTutorialStep(2);
            hasEnteredTrigger = true;
        }
    }

    private void CheckAndPublishTutorialStep()
    {
        // 상수 캐싱
        const string gameLobbySceneName = "GameLobby";
        const string mainSceneName = "MainScene";

        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == gameLobbySceneName)
        {
            tutorialPublisher.PublishTutorialStep(1);
        }
        else if (sceneName == mainSceneName)
        {
            tutorialPublisher.PublishTutorialStep(3);
        }
    }

    private void HandleTutorialStep(int step)
    {
        StartTutorial(step);
    }

    void StartTutorial(int step)
    {
        TutorialManager.instance.SetTutorialPanelActive(true);
        
        switch (step)
        {
            case 1:
                Dialogue tutorialDialogue_1 = new()
                {
                    name = "튜토리얼",
                    sentences = new string[]
                    {
                        "몬스터 팜에 오신 것을 환영합니다! 이 곳은 로비입니다.\n스페이스 바를 눌러 대화를 진행하세요.",
                        "A키를 눌러 왼쪽으로, D키를 눌러 오른쪽으로 이동할 수 있습니다.",
                        "오른쪽으로 이동하여 몬스터 관리실로 이동해보세요."
                    }
                };
                dialogueManager.StartDialogue(tutorialDialogue_1);
                break;
            case 2:
                Dialogue tutorialDialogue_2 = new()
                {
                    name = "튜토리얼",
                    sentences = new string[]
                    {
                        "E키를 눌러 몬스터 관리실에 입장하여 몬스터를 관리할 수 있습니다"
                    }
                };
                dialogueManager.StartDialogue(tutorialDialogue_2);
                break;
            case 3:
                Dialogue tutorialDialogue_3 = new()
                {
                    name = "튜토리얼",
                    sentences = new string[]
                    {
                        "몬스터 케이지를 클릭해서 몬스터를 관리할 수 있습니다.",
                        "몬스터의 관리법에는 6가지가 있으며,\n몬스터마다 관리법에 차이가 있습니다.",
                        "선호하는 관리법을 선택하면 높은 확률로 관리에 성공하게 되며,\n반대로 성격과 맞지 않는 관리법을 선택하면\n관리에 실패할 확률이 증가합니다.",
                        "관리에 자주 실패한다면 몬스터가 탈출할 수도 있으니 조심하시길 바랍니다.",
                        "관리법을 선택하며 게임을 진행하세요."
                    }
                };
                dialogueManager.StartDialogue(tutorialDialogue_3);
                break;
            default:
                break;
        }
    }

    void Update()
    {
        // 사용자 입력에 따라 다음 대화 표시 또는 행동 수행
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            dialogueManager.DisplayNextSentence();
        }
    }

    // 메모리 누수 방지를 위한 이벤트 해제
    private void OnDestroy()
    {
        if (tutorialPublisher != null)
        {
            tutorialPublisher.OnTutorialStepCompleted -= HandleTutorialStep;
        }
    }
}
