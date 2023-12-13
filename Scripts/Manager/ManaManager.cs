using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Spine;

public class ManaManager : MonoBehaviour
{
    private static ManaManager m_instance = null;
    public static ManaManager Instance
    {
        get
        {
            if(!m_instance)
            {
                m_instance = FindObjectOfType(typeof(ManaManager)) as ManaManager;
            }
            return m_instance;
        }
    }

    [SerializeField] private Slider manaBar;
    [SerializeField] private TMP_Text manaText;
    [SerializeField] private int maxMana;
    [SerializeField] private int curMana;
    [SerializeField] private GameObject berserkGage1;
    [SerializeField] private GameObject berserkGage2;
    [SerializeField] private GameObject berserkGage3;
    [SerializeField] private GameObject berserkGage4;
    [SerializeField] private TMP_Text berserkCountText;

    [SerializeField] private int berserkCount;
    [SerializeField] private int manageMentCount;
    [SerializeField] private bool isBerserkEvent;

    public int ManageMentCount//이거 이용해서 
    {
        get { return manageMentCount; }
        set { manageMentCount = value; }
    }

    public int BerserkCount
    {
        get { return berserkCount; }
        set { berserkCount = value; }
    }

    [SerializeField] private GameManager gameManager;
    [SerializeField] private EventMessage eventMessage;
    private CumberSpawner cumberSpawner;
    private int cur_day;

    private void Awake()
    {
        if (null == m_instance)
        {
            m_instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        manaBar.value = Mathf.Lerp(manaBar.value, (float)curMana / (float)maxMana, Time.deltaTime * 10);
        manaText.text = curMana.ToString() + "/" + maxMana.ToString();
        //if (curMana >= maxMana && !gameManager.GetIsClear()) gameManager.DaysClear();
    }

    public void StartSetting()
    {
        gameManager = GameManager.instance;
        eventMessage = gameManager.ManagerSetting("EventMessage").GetComponent<EventMessage>();
        manaBar = gameManager.ManagerSetting("ManaSlider").GetComponent<Slider>();
        manaText = gameManager.ManagerSetting("ManaText").GetComponent<TMP_Text>();
        GameObject berserkGages = gameManager.ManagerSetting("BerserkGageBackGroundImg").gameObject;
        berserkGage1 = berserkGages.transform.GetChild(0).gameObject;
        berserkGage2 = berserkGages.transform.GetChild(1).gameObject;
        berserkGage3 = berserkGages.transform.GetChild(2).gameObject;
        berserkGage4 = berserkGages.transform.GetChild(3).gameObject;
        berserkCountText = gameManager.ManagerSetting("BerserkCountText").GetComponent<TMP_Text>();
        maxMana = gameManager.GetMaxMana();
        curMana = gameManager.GetCurMana();
        manaText.text = curMana.ToString() + "/" + maxMana.ToString();
        berserkCount = 0;
        manageMentCount = 0;
        cur_day = gameManager.GetDays();
        isBerserkEvent = false;
    }

    public void GettingMana(int successMana)
    {
        curMana += successMana;
        manaText.text = curMana.ToString() + "/" + maxMana.ToString();
        if (curMana >= maxMana && !gameManager.GetIsClear()) gameManager.NextDay();
    }
    public int GetCurMana()
    {
        return curMana;
    }
    public void SetCumberSpawner(CumberSpawner spawner)
    {
        cumberSpawner = spawner;
    }

    public void BerserkGageUp()
    {
        if (!isBerserkEvent)
        {
            if (BerserkCount < 4)
            {
                BerserkCount++;

                if (BerserkCount == 4)
                {
                    BerserkEventStart();
                    eventMessage.EventMessageStart("몬스터 폭주게이지가 가득 찼습니다.");
                    // 코루틴으로 폭주 주의 상태 해제
                    // 새로운 폭주 이벤트가 필요
                    // StartCoroutine("BerserkEvent_End");
                    BerserkEventEnd();
                }
            }
            BerserkGage();
        }
    }

    public void BerserkEventStart()
    {
        isBerserkEvent = true;
        RoomManager.Instance.StartBerserkEvent();
        cumberSpawner.SpawnCumberMonster();
    }

    IEnumerator BerserkEvent_End()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        BerserkEventEnd();
    }

    public void BerserkEventEnd()
    {
        isBerserkEvent = false;
        berserkCount = 0;
        BerserkGage();
    }

    public void BerserkGage()
    {
        switch (BerserkCount)
        {
            case 0:
                berserkCountText.text = "0";
                berserkGage1.SetActive(false);
                berserkGage2.SetActive(false);
                berserkGage3.SetActive(false);
                berserkGage4.SetActive(false);
                break;
            case 1:
                berserkCountText.text = "1";
                berserkGage1.SetActive(true);
                berserkGage2.SetActive(false);
                berserkGage3.SetActive(false);
                berserkGage4.SetActive(false);
                break;
            case 2:
                berserkCountText.text = "2";
                berserkGage1.SetActive(true);
                berserkGage2.SetActive(true);
                berserkGage3.SetActive(false);
                berserkGage4.SetActive(false);
                break;
            case 3:
                berserkCountText.text = "3";
                berserkGage1.SetActive(true);
                berserkGage2.SetActive(true);
                berserkGage3.SetActive(true);
                berserkGage4.SetActive(false);
                break;
            case 4:
                berserkCountText.text = "4";
                berserkGage1.SetActive(true);
                berserkGage2.SetActive(true);
                berserkGage3.SetActive(true);
                berserkGage4.SetActive(true);
                Debug.Log("몬스터 폭주 상황 발생");
                break;
            default:
                Debug.Log("폭주 게이지 문제");
                break;
        }
    }
}
