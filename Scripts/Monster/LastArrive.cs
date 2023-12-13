using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastArrive : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            collision.gameObject.GetComponent<MonsterUnit>().ReturnRoom();
            GameManager.instance.HpDecrease();
        }
        else if(collision.gameObject.tag == "CumberMonster")
        {
            GameManager.instance.HpDecrease();
        }
    }
}
