using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damege;
    public int per;

    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damege, int per, Vector3 dir)
    {
        this.damege = damege;
        this.per = per;

        if (per > -1)
        {
            rigid.velocity = dir * 15f;
        }
    }
    public void DestroyBulletInvoke()
    {
        Invoke(nameof(DestroyBullet), 5f);
    }
    private void DestroyBullet()
    {
        this.gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("CumberMonster") && !collision.CompareTag("Monster") || per == -1)
            return;

        per--;

        if (per == -1 && collision.CompareTag("Monster"))
        {
            //몬스터 갯 컴포넌트로 가져오기, TakeDamage 호출
            MonsterUnit unit = collision.GetComponent<MonsterUnit>();
            unit.TakeDamage(damege);

            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);

            //Destroy(this.gameObject, 3); //3초 뒤, 총알의 삭제
        }
        else if (per == -1 && collision.CompareTag("CumberMonster"))
        {
            CumberMonster unit = collision.GetComponent<CumberMonster>();
            unit.TakeDamage(damege);

            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
