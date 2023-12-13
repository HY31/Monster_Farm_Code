using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading;

public class MonsterSelect : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private MonsterManager monsterManager;

    [SerializeField] private GameObject[] monsterSelectBtns;
    [SerializeField] private TMP_Text[] monsterSelectText;

    [SerializeField] private int num_Monsters;
    [SerializeField] private List<MonsterData> selectMonsters = new List<MonsterData>();

    private void Start()
    {
        gameManager = GameManager.instance;
        monsterManager = MonsterManager.Instance;

        // 선택하는 몬스터의 갯수 num_Monsters 정하기 (초기값 3)
        num_Monsters = 3;

        monsterSelectBtns[num_Monsters - 1].SetActive(true);

        while (selectMonsters.Count < num_Monsters)
        {
            Start:
            MonsterCode monsterCode = (MonsterCode)(UnityEngine.Random.Range(0, Enum.GetNames(typeof(MonsterCode)).Length));

            foreach (MonsterData monster in monsterManager.monsters)
            {
                if (monster.code == monsterCode)
                {
                    monsterCode = (MonsterCode)(UnityEngine.Random.Range(0, Enum.GetNames(typeof(MonsterCode)).Length));
                    goto Start;
                }
            }

            switch (selectMonsters.Count)
            {
                case 2:
                    if (selectMonsters[selectMonsters.Count - 2].code == monsterCode)
                    {
                        Debug.Log(monsterCode);
                        Debug.Log("다시");
                        continue;
                    }
                    goto case 1;
                case 1:
                    if (selectMonsters[selectMonsters.Count - 1].code == monsterCode)
                    {
                        Debug.Log(monsterCode);
                        Debug.Log("다시");
                        continue;
                    }
                    break;
            }

            selectMonsters.Add(monsterManager.GetMonsterData(monsterCode));
            Debug.Log(monsterCode);
            Debug.Log(selectMonsters.Count);
        }

        switch (num_Monsters)
        {
            case 1:
                monsterSelectText[0].text = selectMonsters[0].code.ToString();
                break;
            case 2:
                monsterSelectText[1].text = selectMonsters[0].code.ToString();
                monsterSelectText[2].text = selectMonsters[1].code.ToString();
                break;
            case 3:
                monsterSelectText[3].text = selectMonsters[0].code.ToString();
                monsterSelectText[4].text = selectMonsters[1].code.ToString();
                monsterSelectText[5].text = selectMonsters[2].code.ToString();
                break;
        }
    }

    public void SelectMonster(int monsterNum)
    {
        monsterManager.monsters.Add(selectMonsters[monsterNum]);
        Debug.Log(selectMonsters[monsterNum].code);
        Debug.Log(monsterManager.monsters.Count);
        //++방 초기화해주는 무언가
        MoveGameScene();
    }

    private void MoveGameScene()
    {
        gameManager.MoveGameScene();
    }
}
