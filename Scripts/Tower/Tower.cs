using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    public Scanner scanner;
    [SerializeField]
    Image HPBar;
    [SerializeField]
    SpriteRenderer TowerSprite;
    [SerializeField]
    private float m_maxTowerHP;
    [SerializeField]
    private float m_curTowerHP;

    item_data TowerData;
    

    Rigidbody2D rigid;
    SpriteRenderer spriter;

    [SerializeField] private GameObject arrangementPoint;

    void Awake()
    {
        m_curTowerHP = m_maxTowerHP;
        scanner = GetComponent<Scanner>();
    }
    private void Start()
    {
        if (arrangementPoint == null)
        {
            arrangementPoint = gameObject.transform.parent.gameObject;
        }
    }

    public void TakeDamage(int damage)
    {
        m_curTowerHP = Mathf.Clamp(m_curTowerHP - damage, 0, m_maxTowerHP);
        HPBar.fillAmount = m_curTowerHP / m_maxTowerHP;
        if (m_curTowerHP == 0)
        {
            arrangementPoint.GetComponent<BoxCollider2D>().enabled = true;
            Destroy(this.gameObject);
        }
    }
    public void SetTowerInfo(item_data itemdata)
    {
        TowerData = itemdata;
        TowerSprite.sprite = TowerData.Item_Sprite;
        m_curTowerHP = TowerData.HP;
        m_maxTowerHP = TowerData.HP;
        this.gameObject.transform.GetChild(1).GetComponent<TowerGun>().setTowerGun(TowerData.Damage);
    }
}

