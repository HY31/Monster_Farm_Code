using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterList : MonoBehaviour
{
    private MonsterManager monsterManager;

    public List<MonsterData> monsters;

    public MonsterData monsterData;

    public void Start()
    {
        monsterManager = MonsterManager.Instance;
        monsters = monsterManager.monsters;
    }

    public void AddMonster()
    {
        MonsterCode monsterCode;
        monsterCode = (MonsterCode)(UnityEngine.Random.Range(0, Enum.GetNames(typeof(MonsterCode)).Length));
        Debug.Log(monsterCode);
        monsterData = monsterManager.GetMonsterData(monsterCode);
        Debug.Log(monsterData.name);
        Debug.Log(monsterData.speed);
        monsters.Add(monsterData);
        Debug.Log(monsters.Count);
    }

    public void SaveMonster()
    {
        MonsterManager.Instance.monsters = monsters;
    }
}
