using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CumberMonster : MonsterBase
{

    protected override void Start()
    {
        base.Start();
        InitializeMonsterUnit();
        Flip();
    }

    protected override void Update()
    {
        base.Update();
        if (isAttacking == false)
        {
            transform.position = transform.position = Vector2.MoveTowards(gameObject.transform.position, TargetPos, speed * Time.deltaTime * 10);
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
        if (collision.tag == "Tower")
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
            yield return new WaitForSeconds(1f);//½Ã°£ Ä³½Ì
            if (other != null)
            {
                other.GetComponent<Tower>().TakeDamage(attack);
            }
            isAttacking = false;
        }
    }

    public void InitializeMonsterUnit()//
    {
        m_maxHP = 90f;
        m_curHP = 90f;
        speed = 0.1f;
        attack = 10;
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        skeletonAnimator.AnimationState.AddAnimation(0, "Walk", true, 0);
    }

    public override void Attack()
    {
        base.Attack();

    }

    public override void TakeDamage(float damage)
    {

        base.TakeDamage(damage);
        m_curHP = Mathf.Clamp(m_curHP - damage, 0, m_maxHP);
        HP_Bar.fillAmount = m_curHP / m_maxHP;
        if (m_curHP == 0)
        {
            Destroy(this.gameObject);
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}
