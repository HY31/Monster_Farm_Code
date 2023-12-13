using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private MonsterList monsterList;
    [SerializeField] private TalkManager talkManager;
    [SerializeField] private TimeScale timeScale;
    [SerializeField] private EventMessage eventMessage;
    [SerializeField] private NextDay nextDay;

    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject daysClearPanel;
    [SerializeField] private GameObject talkPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text daysClearText;
    [SerializeField] private TMP_Text talkText;

    [SerializeField] private int talkId = 0;
    [SerializeField] private int talkIndex = 0;

    [SerializeField] private int timeScaleValue = 0;

    [SerializeField] private Stack<GameObject> panels = new Stack<GameObject>();

    [SerializeField] private Slider MasterSlider;
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider SFXSlider;


    private void Start()
    {
        SceneStart();
    }

    private void Update()
    {
        // 세팅 단축키
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameManager.GetIsSystemEvent() && !gameManager.GetIsTextEvent())
            {
                PanelOn(settingPanel);
            }
            else
            {
                PanelOff();
            }
        }


        // 시간 조절 단축키
        if (!gameManager.GetIsSystemEvent() && !gameManager.GetIsTextEvent())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                soundManager.PlayClickEffect();

                if (timeScaleValue != 0)
                {
                    ChangeTimeScale(TimeScale.TimeScaleValue.PAUSE);
                }
                else
                {
                    ChangeTimeScale(TimeScale.TimeScaleValue.ONE);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                soundManager.PlayClickEffect();
                ChangeTimeScale(TimeScale.TimeScaleValue.ONE);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                soundManager.PlayClickEffect();
                ChangeTimeScale(TimeScale.TimeScaleValue.TWO);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                soundManager.PlayClickEffect();
                ChangeTimeScale(TimeScale.TimeScaleValue.THREE);
            }
        }

        // 대화 넘기기 키
        if (gameManager.GetIsTextEvent())
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Talk(talkId);
            }
        }
    }

    private void SceneStart()
    {
        gameManager = GameManager.instance;
        soundManager = SoundManager.instance;
        gameManager.ManagerSetting();
        StartSetting();
    }

    public void StartObjectSetting()
    {
        monsterList = gameManager.ManagerSetting("MonsterList").GetComponent<MonsterList>();
        talkManager = gameManager.ManagerSetting("TalkManager").GetComponent<TalkManager>();
        timeScale = gameManager.ManagerSetting("Time").GetComponent<TimeScale>();
        eventMessage = gameManager.ManagerSetting("EventMessage").GetComponent<EventMessage>();
        nextDay = gameManager.ManagerSetting("NextDay").GetComponent<NextDay>();
        GameObject panels = gameManager.ManagerSetting("Panels");
        daysClearPanel = panels.transform.GetChild(0).gameObject;
        talkPanel = panels.transform.GetChild(1).gameObject;
        settingPanel = panels.transform.GetChild(2).gameObject;
        gameOverPanel = panels.transform.GetChild(3).gameObject;
        daysClearText = daysClearPanel.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        //talkText = talkPanel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
    }

    private void StartSetting()
    {
        gameManager.SetIsClear_false();
        gameManager.SetIsTextEvent_false();
        gameManager.SetIsSystemEvent_false();
        eventMessage.SetEventMessageOff();
        nextDay.SetNextDayOff();

        ChangeTimeScale(TimeScale.TimeScaleValue.ONE);
        settingPanel.SetActive(false);
        daysClearPanel.SetActive(false);
        talkPanel.SetActive(false);
        soundManager.SetSlider(MasterSlider, BGMSlider, SFXSlider);
    }

    public void ChangeTimeScale(TimeScale.TimeScaleValue timeScaleValue)
    {
        this.timeScaleValue = (int)timeScaleValue;
        Time.timeScale = this.timeScaleValue;

        timeScale.ChangeTimeScale(timeScaleValue);
    }

    public void PanelOn(GameObject panel)
    {
        soundManager.PlayClickEffect();
        if (panels.Count == 0)
        {
            soundManager.PauseBGM();
            ChangeTimeScale(TimeScale.TimeScaleValue.PAUSE);
            gameManager.SetIsSystemEvent_true();
        }
        panels.Push(panel);
        panels.Peek().SetActive(true);
        Debug.Log(panels.Peek().ToString());
        Debug.Log(panels.Count);
    }

    public void PanelOff()
    {
        soundManager.PlayCancelEffect();
        Debug.Log(panels.Peek().ToString());
        Debug.Log(panels.Count);
        panels.Peek().SetActive(false);
        panels.Pop();
        if (panels.Count == 0)
        {
            soundManager.ResumeBGM();
            ChangeTimeScale(TimeScale.TimeScaleValue.ONE);
            gameManager.SetIsSystemEvent_false();
        }
    }

    public void NextDay(int days)
    {
        soundManager.PlayStageClear();
        nextDay.NextDayStart(days);
    }

    public void NextDayBtn()
    {
        soundManager.PauseBGM();
        soundManager.PlayClickEffect();
        InvenManager.Instance.AddGold(ManaManager.Instance.GetCurMana());
        ChangeTimeScale(TimeScale.TimeScaleValue.PAUSE);
        gameManager.DaysClear();
    }

    public void DaysClearPanelON(int days)
    {
        daysClearPanel.SetActive(true);
        daysClearText.text = days.ToString() + "일차 클리어!";
    }

    public void GameOverPanelON()
    {
        soundManager.PlayGameOver();
        ChangeTimeScale(TimeScale.TimeScaleValue.PAUSE);
        gameOverPanel.SetActive(true);
    }

    public void DaysClearBtn()
    {
        eventMessage.EventMessageStop();
        ChangeTimeScale(TimeScale.TimeScaleValue.ONE);
        monsterList.SaveMonster();
        gameManager.MoveMonsterSelectScene();
    }

    public void TalkStart(int talkId)
    {
        if (!gameManager.GetIsTextEvent())
        {
            ChangeTimeScale(TimeScale.TimeScaleValue.PAUSE);
            gameManager.SetIsTextEvent_true();
            talkPanel.SetActive(true);
            this.talkId = talkId;
            talkIndex = 0;
            Talk(talkId);
        }
        else Debug.Log("텍스트이벤트 켜져있음");
    }

    private void Talk(int id)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if(talkData == null)
        {
            TalkEnd();
            return;
        }
        talkText.text = talkData;
        talkIndex++;
    }

    private void TalkEnd()
    {
        ChangeTimeScale(TimeScale.TimeScaleValue.ONE);
        gameManager.SetIsTextEvent_false();
        talkPanel.SetActive(false);
    }

    public void ExitGame()
    {
        soundManager.PlayClickEffect();
        Application.Quit();
    }

    public void GoStartScene()
    {
        // 저장 관련
        soundManager.PlayClickEffect();
        eventMessage.EventMessageStop();
        ChangeTimeScale(TimeScale.TimeScaleValue.ONE);
        SceneManager.LoadScene("StartScene");
    }

    public void GoLobbyScene()
    {
        soundManager.PlayClickEffect();
        eventMessage.EventMessageStop();
        ChangeTimeScale(TimeScale.TimeScaleValue.ONE);
        InvenManager.Instance.AddGold(ManaManager.Instance.GetCurMana());
        SceneManager.LoadScene("GameLobby");
    }

}
