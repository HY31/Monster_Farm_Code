using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterManagement : MonoBehaviour
{
    RoomManager roomManager;
    public bool monsterManagementHide;
    public TMP_Text cur_Familiarity;

    public TMP_Text familiarityLevel;
    public TMP_Text managementRatio1;
    public TMP_Text managementRatio2;
    public TMP_Text managementRatio3;
    //public GameObject MonsterManagementHide;
    int cur_roomNum;
    public int ManagemnetNumber;
    RoomData roomData;
    // Start is called before the first frame update
    void Start()
    {
        roomManager = RoomManager.Instance;
        cur_roomNum = roomManager.GetCurSelectRoomNum();

        //if (roomData.MonsterManagements != null)
        //MonsterManagementHide.SetActive(roomData.MonsterManagements[ManagemnetNumber]);
    }

    public void OpenMonsterManagement()
    {
        int temp = 0;
        roomData = roomManager.GetCurSelectRoomData();
        if (roomData.cur_familiarity >= 5 && ManagemnetNumber == 0)
        {
            roomData.MonsterManagements[ManagemnetNumber] = false;
            roomData.cur_familiarity -= 5;
            cur_Familiarity.text = roomData.cur_familiarity.ToString();
            this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            RoomManager.Instance.RoomDatas[cur_roomNum] = roomData;
        }
        else if(roomData.cur_familiarity >= 5 && roomData.MonsterManagements[ManagemnetNumber - 1] == false)
        {
            roomData.MonsterManagements[ManagemnetNumber] = false;
            roomData.cur_familiarity -= 5;
            cur_Familiarity.text = roomData.cur_familiarity.ToString();
            this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            RoomManager.Instance.RoomDatas[cur_roomNum] = roomData;
        }
        for(int i = 0; i < roomData.MonsterManagements.Count; i++) 
        {
            if (roomData.MonsterManagements[i] == false) 
                temp++;
            if (temp == roomData.MonsterManagements.Count)
            {
                roomData.familiarityLevel++;
                SetFamiliarityLevel();
                RoomManager.Instance.RoomDatas[cur_roomNum] = roomData;
            }
        }
    }
    void SetFamiliarityLevel()
    {
        if (roomData.familiarityLevel == 2)
        {
            managementRatio1.color = new Color(255, 255, 255);
            familiarityLevel.text = "친밀도LV" + roomData.familiarityLevel.ToString();
        }
        else if (roomData.familiarityLevel == 3)
        {
            managementRatio2.color = new Color(255, 255, 255);
            familiarityLevel.text = "친밀도LV" + roomData.familiarityLevel.ToString();
        }
        else if (roomData.familiarityLevel == 4)
        {
            managementRatio3.color = new Color(255, 255, 255);
            familiarityLevel.text = "친밀도LV" + roomData.familiarityLevel.ToString();
        }

    }
}
