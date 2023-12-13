using System.Collections.Generic;
using UnityEngine;

public class RoomBuilder : MonoBehaviour
{
    [SerializeField] Room room;

    [Header("Build Position")]
    [SerializeField] Transform[] buildPoints;

    private int monsters;

    public List<Room> rooms = new List<Room>();

    private void Start()
    {
        monsters = MonsterManager.Instance.GetCurMonsterData().Count;
        ManaManager.Instance.ManageMentCount = 0;
        RoomManager roommanager = RoomManager.Instance;
        BuildRooms(monsters);
        roommanager.RoomList = GetRooms();
        roommanager.monsterDatas = MonsterManager.Instance.GetCurMonsterData();
        roommanager.InitializeRoomData();
        roommanager.InitializeRoom();
        roommanager.InitializeGauges();
    }

    private void BuildRooms(int monsters)
    {
        for(int i = 0; i < monsters; i++)
        {
            rooms.Add(Instantiate(room, buildPoints[i]));
            rooms[i].GetComponent<Canvas>().sortingOrder = 6;
            rooms[i].name = "Room_" + i;
        }
    }

    public List<Room> GetRooms()
    {
        return rooms;
    }

}
