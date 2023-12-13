using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterInfoUI : MonoBehaviour
{
    MonsterData CurMonsterData;

    public Image MonsterThumnail;
    public TMP_Text MonsterName;
    public TMP_Text MonsterGrade;
    public TMP_Text MonsterMana;
    public TMP_Text MonsterDamage;
    public TMP_Text MonsterHP;

    public TMP_Text FireRatio;
    public TMP_Text WaterRatio;
    public TMP_Text WindRatio;
    public TMP_Text ThunderRatio;
    public TMP_Text LightRatio;
    public TMP_Text DarknessRatio;
    public TMP_Text Cur_Familiarity;
    public TMP_Text FamiliarityLevel;
    public TMP_Text ManagementRatio1;
    public TMP_Text ManagementRatio2;
    public TMP_Text ManagementRatio3;

    public GameObject MonsterStatHide;
    public GameObject FireHide;
    public GameObject WaterHide;
    public GameObject WindHide;
    public GameObject ThunderHide;
    public GameObject LightHide;
    public GameObject DarknessHide;
   // public List<GameObject> MonsterManagementHide;1203 문제없을시 삭제

    public GameObject MonsterManagement;
    Room room;//삭제예정1206
    RoomData roomData;
    int cur_roomNum;
    List<GameObject> m_ManagementList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        cur_roomNum = RoomManager.Instance.GetCurSelectRoomNum();
        room = RoomManager.Instance.GetCurSelectRoom();//삭제예정
        roomData = RoomManager.Instance.GetCurSelectRoomData();
        CurMonsterData = RoomManager.Instance.GetCurSelectMonsterData();//시작과 동시에 현재 선택한 room번호에 맞춰서 몬스터 정보 넣어주기
        InitializeCurMonsterInfoUI();
    }

    public void InitializeCurMonsterInfoUI()
    {
        MonsterThumnail.sprite = Resources.Load(CurMonsterData.thumbnailPath, typeof(Sprite)) as Sprite;
        MonsterGrade.text = CurMonsterData.grade.ToString()+"급";
        MonsterMana.text = CurMonsterData.maxSpell.ToString();
        MonsterName.text = CurMonsterData.name;
        MonsterDamage.text = CurMonsterData.attack.ToString();
        MonsterHP.text = CurMonsterData.maxHP.ToString();

        FireRatio.text = SetRatioToString(CurMonsterData.fire);
        WaterRatio.text = SetRatioToString(CurMonsterData.water);
        WindRatio.text = SetRatioToString(CurMonsterData.wind);
        ThunderRatio.text = SetRatioToString(CurMonsterData.thunder);
        LightRatio.text = SetRatioToString(CurMonsterData.light);
        DarknessRatio.text = SetRatioToString(CurMonsterData.darkness);

        Cur_Familiarity.text = roomData.cur_familiarity.ToString();
        SetFamiliarityLevel();   

        SetManageMentList();
        SetHideInfo();

    }
    void SetManageMentList()//관리방법 나옴
    {
        for(int i = 0; i < CurMonsterData.managementList.Length; i++) 
        {
            m_ManagementList.Add(Instantiate(Resources.Load("UI/MonsterManagement")) as GameObject);
            m_ManagementList[i].transform.SetParent(MonsterManagement.transform, false);
            m_ManagementList[i].transform.GetChild(0).GetComponent<TMP_Text>().text = CurMonsterData.managementList[i];
            MonsterManagement monsterManagement = m_ManagementList[i].GetComponent<MonsterManagement>();
            monsterManagement.ManagemnetNumber = i;
            monsterManagement.cur_Familiarity = Cur_Familiarity;
            monsterManagement.familiarityLevel = FamiliarityLevel;
            monsterManagement.managementRatio1 = ManagementRatio1;
            monsterManagement.managementRatio2 = ManagementRatio2;
            monsterManagement.managementRatio3 = ManagementRatio3;
            if (roomData.MonsterManagements.Count == 0)
            {
                for(int j = 0; j < CurMonsterData.managementList.Length; j++)
                roomData.MonsterManagements.Add(true);
            }
            //Debug.Log(roomData.MonsterManagements[i]);
            bool temp = roomData.MonsterManagements[i];
            m_ManagementList[i].transform.GetChild(1).gameObject.SetActive(temp);
            //MonsterManagementHide.Add(m_ManagementList[i].transform.GetChild(1).gameObject);1203
        }
    }
    void SetHideInfo()
    {

        MonsterStatHide.gameObject.SetActive(roomData.MonsterStatHide);
        FireHide.gameObject.SetActive(roomData.FireHide);
        WaterHide.gameObject.SetActive(roomData.WaterHide);
        WindHide.gameObject.SetActive(roomData.WindHide);
        ThunderHide.gameObject.SetActive(roomData.ThunderHide);
        LightHide.gameObject.SetActive(roomData.LightHide);
        DarknessHide.gameObject.SetActive(roomData.DarknessHide);
    }
    void SetFamiliarityLevel()
    {
        if (roomData.familiarityLevel == 2)
        {
            ManagementRatio1.color = new Color(255, 255, 255);
            FamiliarityLevel.text = "친밀도LV" + roomData.familiarityLevel.ToString();
        }
        else if (roomData.familiarityLevel == 3)
        {
            ManagementRatio1.color = new Color(255, 255, 255);
            ManagementRatio2.color = new Color(255, 255, 255);
            FamiliarityLevel.text = "친밀도LV" + roomData.familiarityLevel.ToString();
        }
        else if (roomData.familiarityLevel == 4)
        {
            ManagementRatio1.color = new Color(255, 255, 255);
            ManagementRatio2.color = new Color(255, 255, 255);
            ManagementRatio3.color = new Color(255, 255, 255);
            FamiliarityLevel.text = "친밀도 Max";
        }
        //else
        //{
        //    FamiliarityLevel.text = "친밀도LV1";
        //    ManagementRatio1.color = new Color(101, 101, 101);
        //    ManagementRatio2.color = new Color(101, 101, 101);
        //    ManagementRatio3.color = new Color(101, 101, 101);
        //}
    }

    string SetRatioToString(float ratio)
    {
        if(ratio>=0 && ratio<15)
        {
            return "매우낮음";
        }
        else if(ratio >= 15 && ratio < 40)
        {
            return "낮음";
        }
        else if (ratio >= 40 && ratio < 60)
        {
            return "보통";
        }
        else if (ratio >= 60 && ratio < 85)
        {
            return "높음";
        }
        else if (ratio >= 85 && ratio <=100)
        {
            return "매우높음";
        }
        else
        {
            return "오류";
        }

    }
    
    public void OpenMonsterStat()
    {
        roomData = RoomManager.Instance.GetCurSelectRoomData();
        if (roomData.cur_familiarity >= 8)
        {
            roomData.MonsterStatHide = false;
            roomData.cur_familiarity -= 8;
            Cur_Familiarity.text = roomData.cur_familiarity.ToString();
            MonsterStatHide.gameObject.SetActive(false);
            roomData.familiarityLevel++;
            SetFamiliarityLevel();//친밀도 올라가면 반영
            room.OffInfoHide();
            RoomManager.Instance.RoomDatas[cur_roomNum] = roomData;
        }

    }
    public void OpenFireRatio()
    {
        roomData = RoomManager.Instance.GetCurSelectRoomData();
        if (roomData.cur_familiarity >= 4)
        {
            int temp = 0;
            roomData.cur_familiarity -= 4;
            roomData.FireHide = false;
            Cur_Familiarity.text = roomData.cur_familiarity.ToString();
            bool[] bools = { roomData.FireHide, roomData.WaterHide, roomData.WindHide, roomData.ThunderHide, roomData.LightHide, roomData.DarknessHide };
            RoomManager.Instance.RoomDatas[cur_roomNum] = roomData;
            FireHide.gameObject.SetActive(false);
            for (int i = 0; i < bools.Length; i++)
            {
                if (bools[i] == false)
                    temp++;
            }
            if (temp == 6)
            {
                roomData.familiarityLevel++;
                RoomManager.Instance.RoomDatas[cur_roomNum] = roomData;
                SetFamiliarityLevel();
            }
        }
    }
    public void OpenWaterRatio() 
    {
        roomData = RoomManager.Instance.GetCurSelectRoomData();
        if (roomData.cur_familiarity >= 4)
        {
            int temp = 0;
            roomData.cur_familiarity -= 4;
            roomData.WaterHide = false;
            Cur_Familiarity.text = roomData.cur_familiarity.ToString();
            bool[] bools = { roomData.FireHide, roomData.WaterHide, roomData.WindHide, roomData.ThunderHide, roomData.LightHide, roomData.DarknessHide };
            RoomManager.Instance.RoomDatas[cur_roomNum] = roomData;
            WaterHide.gameObject.SetActive(false);
            for (int i = 0; i < bools.Length; i++)
            {
                if (bools[i] == false)
                    temp++;
            }
            if (temp == 6)
            {
                roomData.familiarityLevel++;
                RoomManager.Instance.RoomDatas[cur_roomNum] = roomData;
                SetFamiliarityLevel();
            }
        }
    }
    public void OpenWindRatio() 
    {
        roomData = RoomManager.Instance.GetCurSelectRoomData();
        if (roomData.cur_familiarity >= 4)
        {
            int temp = 0;
            roomData.cur_familiarity -= 4;
            roomData.WindHide = false;
            Cur_Familiarity.text = roomData.cur_familiarity.ToString();
            bool[] bools = { roomData.FireHide, roomData.WaterHide, roomData.WindHide, roomData.ThunderHide, roomData.LightHide, roomData.DarknessHide };
            RoomManager.Instance.RoomDatas[cur_roomNum] = roomData;
            WindHide.gameObject.SetActive(false);
            for (int i = 0; i < bools.Length; i++)
            {
                if (bools[i] == false)
                    temp++;
            }
            if (temp == 6)
            {
                roomData.familiarityLevel++;
                RoomManager.Instance.RoomDatas[cur_roomNum] = roomData;
                SetFamiliarityLevel();
            }
        }
    }
    public void OpenThunderRatio() 
    {
        roomData = RoomManager.Instance.GetCurSelectRoomData();
        if (roomData.cur_familiarity >= 4)
        {
            int temp = 0;
            roomData.cur_familiarity -= 4;
            roomData.ThunderHide = false;
            Cur_Familiarity.text = roomData.cur_familiarity.ToString();
            bool[] bools = { roomData.FireHide, roomData.WaterHide, roomData.WindHide, roomData.ThunderHide, roomData.LightHide, roomData.DarknessHide };
            RoomManager.Instance.RoomDatas[cur_roomNum] = roomData;
            ThunderHide.gameObject.SetActive(false);
            for (int i = 0; i < bools.Length; i++)
            {
                if (bools[i] == false)
                    temp++;
            }
            if (temp == 6)
            {
                roomData.familiarityLevel++;
                RoomManager.Instance.RoomDatas[cur_roomNum] = roomData;
                SetFamiliarityLevel();
            }
        }
    }
    public void OpenLightRatio() 
    {
        roomData = RoomManager.Instance.GetCurSelectRoomData();
        if (roomData.cur_familiarity >= 4)
        {
            int temp = 0;
            roomData.cur_familiarity -= 4;
            roomData.LightHide = false;
            Cur_Familiarity.text = roomData.cur_familiarity.ToString();
            bool[] bools = { roomData.FireHide, roomData.WaterHide, roomData.WindHide, roomData.ThunderHide, roomData.LightHide, roomData.DarknessHide };
            RoomManager.Instance.RoomDatas[cur_roomNum] = roomData;
            LightHide.gameObject.SetActive(false);
            for (int i = 0; i < bools.Length; i++)
            {
                if (bools[i] == false)
                    temp++;
            }
            if (temp == 6)
            {
                roomData.familiarityLevel++;
                RoomManager.Instance.RoomDatas[cur_roomNum] = roomData;
                SetFamiliarityLevel();
            }
        }
    }
    public void OpenDarknessRatio() 
    {
        roomData = RoomManager.Instance.GetCurSelectRoomData();
        if (roomData.cur_familiarity >= 4)
        {
            int temp = 0;
            roomData.cur_familiarity -= 4;
            roomData.DarknessHide = false;
            Cur_Familiarity.text = roomData.cur_familiarity.ToString();
            bool[] bools = { roomData.FireHide, roomData.WaterHide, roomData.WindHide, roomData.ThunderHide, roomData.LightHide, roomData.DarknessHide };
            RoomManager.Instance.RoomDatas[cur_roomNum] = roomData;
            DarknessHide.gameObject.SetActive(false);
            for (int i = 0; i < bools.Length; i++)
            {
                if (bools[i] == false)
                    temp++;
            }
            if (temp == 6)
            {
                roomData.familiarityLevel++;
                RoomManager.Instance.RoomDatas[cur_roomNum] = roomData;
                SetFamiliarityLevel();
            }
        }
    }


    public void ExitUI()
    {
        UIManagerTest.Instance.RemoveOneUI();
    }
}
