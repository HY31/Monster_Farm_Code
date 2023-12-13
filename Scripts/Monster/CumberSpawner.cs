using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CumberSpawner : MonoBehaviour
{
    [SerializeField]
    List<Transform> SpawnPoses = new List<Transform>();
    [SerializeField]
    List<Transform> TargetPoses = new List<Transform>();
    [SerializeField]
    GameObject CumberMonster;
    // Start is called before the first frame update
    void Start()
    {
        ManaManager.Instance.SetCumberSpawner(this);
    }
    public void SpawnCumberMonster()
    {
        int day = GameManager.instance.GetDays();
        if (day >= 0)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject cumberMonster = Instantiate(CumberMonster);
                cumberMonster.transform.position = SpawnPoses[0].position;
                cumberMonster.GetComponent<CumberMonster>().SetTargetPos(TargetPoses[0].position);
            }
        }
        if(day >= 2)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject cumberMonster = Instantiate(CumberMonster);
                cumberMonster.transform.position = SpawnPoses[1].position;
                cumberMonster.GetComponent<CumberMonster>().SetTargetPos(TargetPoses[1].position);
            }
        }
        if (day >= 4)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject cumberMonster = Instantiate(CumberMonster);
                cumberMonster.transform.position = SpawnPoses[2].position;
                cumberMonster.GetComponent<CumberMonster>().SetTargetPos(TargetPoses[2].position);
            }
        }
        if (day >= 7)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject cumberMonster = Instantiate(CumberMonster);
                cumberMonster.transform.position = SpawnPoses[3].position;
                cumberMonster.GetComponent<CumberMonster>().SetTargetPos(TargetPoses[3].position);
            }
        }
        if (day >= 10)
        {
            for (int i = 0; i < 7; i++)
            {
                GameObject cumberMonster = Instantiate(CumberMonster);
                cumberMonster.transform.position = SpawnPoses[4].position;
                cumberMonster.GetComponent<CumberMonster>().SetTargetPos(TargetPoses[4].position);
            }
        }
        if (day >= 13)
        {
            for (int i = 0; i < 7; i++)
            {
                GameObject cumberMonster = Instantiate(CumberMonster);
                cumberMonster.transform.position = SpawnPoses[5].position;
                cumberMonster.GetComponent<CumberMonster>().SetTargetPos(TargetPoses[5].position);
            }
        }


    }
}
