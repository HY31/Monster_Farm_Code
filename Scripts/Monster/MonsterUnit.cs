using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MonsterUnit : MonsterBase
{
    MonsterData m_data;

    protected override void Start()
    {
        base.Start();
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    protected override void Update()
    {
        base.Update();
        if (m_escape == true && isAttacking == false)
        {
            transform.position = transform.position = Vector2.MoveTowards(gameObject.transform.position, TargetPos, speed*Time.deltaTime*10);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Tower")
        {
            StartCoroutine(PreAttack(other));
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Tower" && m_escape == true)
        {
            skeletonAnimator.AnimationState.AddAnimation(0, "Walk", true, 0);
        }
    }

    IEnumerator PreAttack(Collider2D other)
    {
        if (isAttacking == false)
        {
            isAttacking = true;
            Attack();
            yield return new WaitForSeconds(1f);
            if (other != null)
            {
                other.GetComponent<Tower>().TakeDamage(attack);
            }
            isAttacking = false;
        }
    }

    public void InitializeMonsterUnit(MonsterData data, int num)//
    {
        m_data = data;
        monsterNumber = num;
        m_curHP = data.maxHP;
        m_maxHP = data.maxHP;
        speed = data.speed;
        attack = data.attack;

    }

    public MonsterData GetMonsterData()
    {
        return m_data;
    }

    public override void Attack()
    {
        base.Attack();

    }
    public void Escape(GameObject monsterSpwanPos)
    {
        FirstTargetPos = TargetPos;
        HpBar.SetActive(true);
        HP_Bar.fillAmount = m_curHP / m_maxHP;
        PrevPos = this.transform.position;
        if (m_escape) return;
        m_escape = true;
        this.transform.position = monsterSpwanPos.transform.position;
        Flip();
        skeletonAnimator.AnimationState.AddAnimation(0, "Walk", true, 0);

        // 몬스터 탈출시 레이어 변경
        gameObject.layer = LayerMask.NameToLayer("Enemy");
    }
    public void ReturnRoom()
    {
        HpBar.SetActive(false);
        m_die = true;
        m_escape = false;
        skeletonAnimator.AnimationState.AddAnimation(0, "Idle", true, 0);
        this.transform.position = PrevPos;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        RoomManager.Instance.ReturnRageCount(monsterNumber);
        RoomManager.Instance.RoomList[monsterNumber].GetComponent<Room>().OnBtnFunction();
        m_curHP = m_maxHP;
        TargetPos = FirstTargetPos;
    }

    public override void TakeDamage(float damage)
    {
        // 대미지 관련 함수


        base.TakeDamage(damage);
        m_curHP = Mathf.Clamp(m_curHP - damage, 0, m_maxHP);
        HP_Bar.fillAmount = m_curHP / m_maxHP;
        if (m_curHP == 0)
        {
            ReturnRoom();
            //몬스터 제압시 레이어 변경
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}
