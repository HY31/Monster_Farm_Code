using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
//using Microsoft.Unity.VisualStudio.Editor;

public struct RoomData//1129
{
    public int cur_familiarity;
    public int familiarityLevel;

    public bool MonsterStatHide;  
    public bool FireHide;
    public bool WaterHide;
    public bool WindHide;
    public bool ThunderHide;
    public bool LightHide;
    public bool DarknessHide;

    public List<bool> MonsterManagements;
}

public class RoomManager : MonoBehaviour
{
    private static RoomManager m_instance = null;
    public static RoomManager Instance
    {
        get
        {
            if(!m_instance)
            {
                m_instance = FindObjectOfType(typeof(RoomManager)) as RoomManager;
            }
            return m_instance;
        }
    }
    public List<Room> RoomList;
    public List<SpellGauge> spellGauges;
    public List<RoomData> RoomDatas = new List<RoomData>();
    public List<bool> Cur_MonsterManageData = new List<bool>();

    public List<GameObject> MonsterSpwanPos = new List<GameObject>();//1203 씬바뀌면 초기화돼서 수정해야함.
    public List<GameObject> MonsterTargetPos = new List<GameObject>();
    private item_data Select_Item;

    //[SerializeField] RoomBuilder roomBuilder;

    public int cur_SelectRoomNum;
    public List<MonsterData> monsterDatas;
    float cur_TypeRatio;
    string cur_TypeName;

    private ManaManager manaManager;

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


    // Start is called before the first frame update
    void Start()
    {
        manaManager = ManaManager.Instance;
        //RoomList = roomBuilder.GetRooms();

        //monsterDatas = MonsterManager.Instance.GetCurMonsterData();

        //InitializeRoom();
        //InitializeRoomData();
        //InitializeGauges();
    }
    public void InitializeRoomData()
    {
        RoomData roomData = new RoomData
        {
            cur_familiarity = 0,
            familiarityLevel = 1,
            MonsterStatHide = true,
            FireHide = true,
            WaterHide = true,
            WindHide = true,
            ThunderHide = true,
            LightHide = true,
            DarknessHide = true,
            MonsterManagements = new List<bool>()
        };
        RoomDatas.Add(roomData);
    }

    public void InitializeRoom()
    {
        spellGauges.Clear();
        for (int i = 0; i < RoomList.Count; i++)
        {
            if (RoomDatas[i].MonsterStatHide == false)
                RoomList[i].OffInfoHide();   
            
            RoomList[i].TargetPos = MonsterTargetPos[i].transform.position;//1203
            RoomList[i].MonsterName.text = monsterDatas[i].name;
            RoomList[i].RageCount = monsterDatas[i].rageCount;
            RoomList[i].RageCountText.text = monsterDatas[i].rageCount +"";
            RoomList[i].RoomMonsterData = monsterDatas[i];
            RoomList[i].EscapeSpawnPos = MonsterSpwanPos[i];//1212
            RoomList[i].Monster = Instantiate(Resources.Load(monsterDatas[i].prefabPath) as GameObject);//수정대기**
            RoomList[i].Monster.GetComponent<MonsterUnit>().InitializeMonsterUnit(monsterDatas[i], i);
            spellGauges.Add(RoomList[i].SpellGauge);
        }
    }

    public void InitializeGauges()
    {
        
        for(int i = 0; i < spellGauges.Count; i++)
        {
            //Debug.Log(i+"번");
            spellGauges[i].CreateGauges(monsterDatas[i].maxSpell);
        }

    }
    public void TakeSpell()//마나추출 도중 다시 추출못하게 하는 방법 질문하기(작업중에는 버튼 자체가 꺼지게 할까 고민중)
    {
        int temp = cur_SelectRoomNum;
        RoomList[temp].GetComponent<Room>().StopBerserkEvent();
        RoomList[temp].GetComponent<Room>().OffBtnFunction();
        spellGauges[temp].ClearGauge();
        StartCoroutine("DelayTakeSpell");
    }
    IEnumerator DelayTakeSpell()
    {
        int curselectRoomNum = cur_SelectRoomNum;
        int maxspell = monsterDatas[curselectRoomNum].maxSpell;
        string[] hatelist = monsterDatas[curselectRoomNum].hateList;
        string selectItemName = GetSelectItem();
        int success = 0;
        int fail = 0;
        float m_probability = cur_TypeRatio + (RoomDatas[curselectRoomNum].familiarityLevel - 1) * 2 + CheckItemProbability();


        SpellGauge spellGauge = spellGauges[curselectRoomNum];

        InvenManager.Instance.UseMyFood(Select_Item);//아이템사용 1206

        var check1 = Array.Exists(hatelist, x => x == cur_TypeName);
        var check2 = Array.Exists(hatelist, y => y == selectItemName);

        for (int i = maxspell-1; i >= 0; i--)
        {
            yield return new WaitForSeconds(1f);

            float spellRandom = Random.Range(0, 100);
            if (spellRandom <= m_probability && m_probability != 0)
            {
                spellGauge.IncreaseGauage(i);
                success++;
            }
            else
            {
                spellGauge.DecreaseGauge(i);
                fail++;
            }
        }
        if(success<fail)
        {
            if (check1 == true||check2 == true)
            {
                RoomList[curselectRoomNum].RageCount = 0;
                RoomList[curselectRoomNum].RageCountText.text = RoomList[curselectRoomNum].RageCount.ToString();
                RoomList[curselectRoomNum].Monster.GetComponent<MonsterUnit>().Escape(MonsterSpwanPos[curselectRoomNum]);
            }
            else
            {
                RoomList[curselectRoomNum].RageCount--;
                RoomList[curselectRoomNum].RageCountText.text = RoomList[curselectRoomNum].RageCount.ToString();
                if (RoomList[curselectRoomNum].RageCount == 0)
                    RoomList[curselectRoomNum].Monster.GetComponent<MonsterUnit>().Escape(MonsterSpwanPos[curselectRoomNum]);
            }
        }
        manaManager.GettingMana(success);
        RoomData roomData = RoomDatas[curselectRoomNum];
        roomData.cur_familiarity += success;
        RoomDatas[curselectRoomNum] = roomData;//1129
        //RoomList[cur_SelectRoomNum].cur_familiarity += success;
        success = 0;
        fail = 0;
        manaManager.ManageMentCount++;
        manaManager.BerserkGageUp();
        if(RoomList[curselectRoomNum].Monster.GetComponent<MonsterUnit>().IsEscape()==false)
        RoomList[curselectRoomNum].GetComponent<Room>().OnBtnFunction();
    }
    public void StartBerserkEvent()//1212
    {
        if(RoomList.Count == 1)
        {
            if (RoomList[0].Monster.GetComponent<MonsterUnit>().IsEscape() == true)
                return;
            else
            RoomList[0].GetComponent<Room>().BerserkEvent();
        }
        else
        {
            List<int> temp = Shuffle();
            for (int i = 0; i < temp.Count / 2; i++)//수정
            {
                int num = temp[i];
                if (RoomList[num].Monster.GetComponent<MonsterUnit>().IsEscape() == true)
                    return;
                else
                    RoomList[num].GetComponent<Room>().BerserkEvent();
            }

        }
    }

    List<int> Shuffle()
    {
        List<int> temp = new List<int>();
        for (int i = 0; i < RoomList.Count; i++)
        temp.Add(i);
        
        for(int i = temp.Count - 1; i > 0; i--)
        {
            int rnd = Random.Range(0, i);

            int val = temp[i];
            temp[i] = temp[rnd];
            temp[rnd] = val;
        }
        return temp;
    }
    string GetSelectItem()
    {
        if (Select_Item != null)
            return Select_Item.name;
        else return null;
    }
    float CheckItemProbability()
    {
        if(Select_Item == null)
            return 0;
        else if(Select_Item.Type == cur_TypeName)
            return Select_Item.UpRatio;
        else if(Select_Item.Type == "All")
            return Select_Item.UpRatio;
        else 
            return 0;
    }
    public void SetSelectItem(item_data select_item)
    {
        Select_Item = select_item;
    }
    public int GetCurSelectRoomNum()
    {
        return cur_SelectRoomNum;
    }

    public MonsterData GetCurSelectMonsterData()//몬스터 정보UI에서 몬스터를 가져오기 위한
    {
        return monsterDatas[cur_SelectRoomNum];
    }

    public Room GetCurSelectRoom()//현재 선택한 방 정보 불러오기
    {
        return RoomList[cur_SelectRoomNum];
    }
    public RoomData GetCurSelectRoomData()
    {
        return RoomDatas[cur_SelectRoomNum];
    }
    public void ReturnRageCount(int num)//탈출한 몬스터 카운터복구
    {
        RoomList[num].RageCount = monsterDatas[num].rageCount;
        RoomList[num].RageCountText.text = monsterDatas[num].rageCount + "";
    }

    public void SetFire()
    {
        cur_TypeRatio = monsterDatas[cur_SelectRoomNum].fire;
        cur_TypeName = "Fire";
    }
    public void SetWater()
    {
        cur_TypeRatio = monsterDatas[cur_SelectRoomNum].water;
        cur_TypeName = "Water";
    }
    public void SetWind()
    {
        cur_TypeRatio = monsterDatas[cur_SelectRoomNum].wind;
        cur_TypeName = "Wind";
    }
    public void SetThunder()
    {
        cur_TypeRatio = monsterDatas[cur_SelectRoomNum].thunder;
        cur_TypeName = "Thunder";
    }
    public void SetLight()
    {
        cur_TypeRatio = monsterDatas[cur_SelectRoomNum].light;
        cur_TypeName = "Light";
    }
    public void SetDarkness()
    {
        cur_TypeRatio = monsterDatas[cur_SelectRoomNum].darkness;
        cur_TypeName = "Darkness";
    }
}
