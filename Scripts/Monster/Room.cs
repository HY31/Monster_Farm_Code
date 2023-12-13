using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public Button Btn1;
    [SerializeField]
    TMP_Text monsterName;
    public TMP_Text MonsterName
    {
        get { return monsterName; }
        set { monsterName = value; }
    }
    [SerializeField]
    TMP_Text rageCountText;
    public TMP_Text RageCountText
    {
        get { return rageCountText; }
        set { rageCountText = value; }
    }
    [SerializeField]
    int rageCount;
    public int RageCount
    {
        get { return rageCount; }
        set { rageCount = value; }
    }
    [SerializeField]
    TMP_Text berserkCount;
    public TMP_Text BerserkCount
    {
        get { return berserkCount; }
        set { berserkCount = value;}
    }
    GameObject monster;
    public GameObject Monster
    {
        get { return monster; }
        set { monster = value; }
    }
    [SerializeField]
    SpellGauge spellGauge;//serializeField로 넣어주는게 나은지 아니면 start문에서 직접 getchild 이용해서 넣어주는게 나은지 물어보기
    public SpellGauge SpellGauge
    {
        get { return spellGauge; }
        set { spellGauge = value; }
    }
    MonsterData roomMonsterData;
    public MonsterData RoomMonsterData
    { 
        get { return roomMonsterData; }
        set { roomMonsterData = value; }
    }
    [SerializeField]
    private GameObject escapeSpawnPos;
    public GameObject EscapeSpawnPos
    {
        get { return escapeSpawnPos; }
        set { escapeSpawnPos = value; }
    }
    [SerializeField]
    GameObject RageCountHide;
    [SerializeField]
    GameObject NameHide;
    [SerializeField]
    Image DangerImage;
    [SerializeField]
    GameObject DangerOutline;
    [SerializeField]
    GameObject BerserkCountObj;
    [SerializeField]
    GameObject BerserkGage;
    [SerializeField]
    Image Gage;
    public Transform SpawnPos;
    public Vector2 TargetPos;
    public int roomNum;
    public bool isBeserk = false;

    Coroutine runningCoroutine = null;
    WaitForSeconds wait = new WaitForSeconds(0.5f);

    Color red = new Color(245 / 255f, 204 / 255f, 198 / 255f);
    Color green = new Color(208 / 255f, 245 / 255f, 198 / 255f);


    // Start is called before the first frame update
    void Start()
    {
        SetCurRoomNum();
        monster.transform.position = SpawnPos.position;
        monster.GetComponent<MonsterUnit>().SetTargetPos(TargetPos);
        Btn1.onClick.AddListener(OpenTakeManaUI);
        
        //Btn1.onClick.AddListener();
    }
    public void BerserkEvent()
    {
        if (isBeserk == false)
        {
            isBeserk = true;
            BerserkCountObj.SetActive(true);
            BerserkGage.SetActive(true);
            runningCoroutine = StartCoroutine(DangerCount());
        }
    }
    public void StopBerserkEvent()
    {
        if (isBeserk == true)
        {
            if (runningCoroutine != null)
            StopCoroutine(runningCoroutine);

            BerserkCountObj.SetActive(false);
            BerserkGage.SetActive(false);
            DangerOutline.SetActive(false);
            DangerImage.color = new Color(208 / 255f, 245 / 255f, 198 / 255f);
            isBeserk = false;
        }
    }

    IEnumerator DangerCount()
    {
        for(float i = 60; i>0; i--)
        {
            Gage.fillAmount = (i / 60);
            DangerImage.color = red;
            DangerOutline.SetActive(true);
            yield return wait;
            DangerImage.color = green;
            DangerOutline.SetActive(false);
            BerserkCount.text = i.ToString();
            yield return wait;
        }
        isBeserk = false;
        BerserkCountObj.SetActive(false);
        BerserkGage.SetActive(false);
        rageCount = 0;
        rageCountText.text = rageCount.ToString();
        OffBtnFunction();
        Monster.GetComponent<MonsterUnit>().Escape(escapeSpawnPos);
    }
    public void OffInfoHide()
    {
        RageCountHide.SetActive(false);
        NameHide.SetActive(false);
    }

    public void SetCurRoomNum()
    {
        string[] temp = this.name.Split('_');
        roomNum = int.Parse(temp[1]);
    }
    
    public void OpenTakeManaUI()
    {
        UIManagerTest.Instance.RemoveOneUI();
        RoomManager.Instance.cur_SelectRoomNum = roomNum;
        UIManagerTest.Instance.AddUI(UIPrefab.TakeManaUI);
    }
    public void OpenMonsterInfoUI()
    {
        RoomManager.Instance.cur_SelectRoomNum = roomNum;
        UIManagerTest.Instance.AddUI(UIPrefab.MonsterInfoUI);
    }
    public void OffBtnFunction()
    {
        Btn1.interactable = false;
    }
    public void OnBtnFunction()
    {
        Btn1.interactable = true;
    }

}
