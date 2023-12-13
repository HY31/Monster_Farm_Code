using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
//using Unity.PlasticSCM.Editor.WebApi;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private int days = 0;
    [SerializeField] private int maxMana = 30;
    [SerializeField] private int curMana = 0;
    [SerializeField] private bool isSystemEvent = false;
    [SerializeField] private bool isTextEvent = false;
    [SerializeField] private bool isClear = false;
    [SerializeField] private bool isGameOver = false;
    [SerializeField] private int Hp;
    [SerializeField] private UIManager uiManager = null;
    [SerializeField] private ManaManager manaManager = null;

    [SerializeField] public PoolManager poolManager = null;
    [SerializeField] private Tower tower = null;

    [SerializeField] private GameObject[] TagComponent;

    public static GameManager Instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }


    public int GetDays() { return days; }
    public bool GetIsSystemEvent() { return isSystemEvent; }
    public void SetIsSystemEvent_true() { isSystemEvent = true; }
    public void SetIsSystemEvent_false() { isSystemEvent = false; }
    public bool GetIsTextEvent() { return isTextEvent; }
    public void SetIsTextEvent_true() { isTextEvent = true; }
    public void SetIsTextEvent_false() { isTextEvent = false; }
    public bool GetIsClear() { return isClear; }
    public void SetIsClear_false() { isClear = false; }
    public int GetMaxMana() { return maxMana; }
    public int GetCurMana() { return curMana; }

    public GameObject ManagerSetting (string objectName)
    {
        for (int i = 0; i < TagComponent.Length; i++)
        {
            if (objectName == TagComponent[i].name)
            {
                return TagComponent[i];
            }
        }
        throw new Exception(objectName + " 오브젝트를 찾을 수 없음");
    }

    public void ManagerSetting()
    {
        TagComponent = GameObject.FindGameObjectsWithTag("Manager");
        uiManager = ManagerSetting("UIManager").GetComponent<UIManager>();
        manaManager = ManagerSetting("ManaManager").GetComponent<ManaManager>();
        poolManager = ManagerSetting("PoolManager").GetComponent<PoolManager>();
        uiManager.StartObjectSetting();
        manaManager.StartSetting();
        //초기 Hp값
        Hp = 1;
    }
    public void NextDay()
    {
        uiManager.NextDay(days);
    }

    public void DaysClear()
    {
        isClear = true;
        isSystemEvent = true;
        // uiManager.ChangeTimeScale(0);
        uiManager.DaysClearPanelON(days);
        //Player.Instance.Gold += 100;
        days += 1;
        curMana = 0;
        // 다음날 마나 증가
        maxMana = maxMana + days * 10;
    }


    public void MoveGameScene()
    {
        SceneLoad.LoadScene("MainScene");
    }

    public void MoveMonsterSelectScene()
    {
        SceneManager.LoadScene("GameLobby");
    }

    // instance.gameManager.HpDecrease() 사용
    public void HpDecrease()
    {
        Hp -= 1;
        if (Hp == 0)
        {
            GameOver();
        }
    }
    
    public void GameOver()
    {
        uiManager.GameOverPanelON();
        isSystemEvent = true;
        isGameOver = true;

        // 몬스터 리스트 수정 필요
    }
    public bool GetGameOver()
    {
        return isGameOver;
    }
    public void SetGameOver(bool isgameover)
    {
        isGameOver = isgameover;
    }
}
