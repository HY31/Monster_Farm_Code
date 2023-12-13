using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBase : MonoBehaviour
{
    protected bool m_die = false;
    public bool m_escape = false;

    protected SkeletonAnimation skeletonAnimator;

    protected float m_curHP;
    protected float m_maxHP;

    protected float speed;
    protected int attack;

    protected int m_curRageCount;
    protected int m_maxRageCount;
    protected int monsterNumber;

    protected bool isAttacking;

    protected Vector2 PrevPos;
    protected Vector2 TargetPos;
    protected Vector2 FirstTargetPos;

    [SerializeField]
    protected Image HP_Bar;
    [SerializeField]
    protected GameObject HpBar;


    protected virtual void Start()
    {
        skeletonAnimator = GetComponent<SkeletonAnimation>();
        
    }
    protected virtual void Update()
    {

    }


    public virtual void Attack() 
    {
        skeletonAnimator.AnimationState.AddAnimation(0, "Attack", false, 0);
    }


    public virtual void TakeDamage(float damage)
    {
        
        
        if (m_die) return;

    }

    public void Flip()
    {
        Vector2 thisPos = transform.position;//1203
        Vector2 newPos = TargetPos - thisPos;
        float roty = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, roty, 0);//1203
    }

    public bool IsDie()
    {
        return m_die;
    }

    public bool IsEscape()
    {
        return m_escape;
    }

    // HP, Damage
    public int GetCurHP()
    {
        return (int)m_curHP;
    }

    public float GetHPPercent()
    {
        return m_curHP / m_maxHP;
    }
    public Vector2 GetTargetPos()
    {
        return TargetPos;
    }

    public void SetTargetPos(Vector2 targetPos)
    {
        TargetPos = targetPos;
    }

}
