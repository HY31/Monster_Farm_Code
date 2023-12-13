using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    public List<GameObject> MonsterSpwanPos = new List<GameObject>();//roommanager에 넣어주기위해 저장해둘것들
    public List<GameObject> MonsterTargetPos = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        RoomManager roomManager = RoomManager.Instance;
        roomManager.MonsterSpwanPos = MonsterSpwanPos;
        roomManager.MonsterTargetPos = MonsterTargetPos;
    }

}
