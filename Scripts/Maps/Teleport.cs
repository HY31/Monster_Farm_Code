using System.Collections;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject target;
    public GameObject arrived;
    public GameObject nextPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Monster"))
        {
            target = collision.gameObject;
            StartCoroutine(TeleportRoutine());
        }
        else if(collision.gameObject.CompareTag("CumberMonster"))
        {
            target = collision.gameObject;
            StartCoroutine(TeleportRoutine2());
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if(collision.gameObject.CompareTag("Monster"))
    //    {
    //        StartCoroutine(TeleportRoutine());
    //    }
    //    else if (collision.gameObject.CompareTag("CumberMonster"))//1210
    //    {
    //        StartCoroutine(TeleportRoutine2());
    //    }
    //}

    IEnumerator TeleportRoutine()
    {
        yield return null;
        target.transform.position = arrived.transform.position;
        target.GetComponent<MonsterUnit>().SetTargetPos(nextPos.transform.position);
        target.GetComponent<MonsterUnit>().Flip();
    }
    IEnumerator TeleportRoutine2()
    {
        yield return null;
        target.transform.position = arrived.transform.position;
        target.GetComponent<CumberMonster>().SetTargetPos(nextPos.transform.position);
        target.GetComponent<CumberMonster>().Flip();
    }

}
