using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;
    SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }


    void FixedUpdate()
    {
        //if (!isLive)
            //return;
        

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;

        void LateUpdate()
        {
            if (!isLive)
                return;
        }
    }

    /*void OnEnavle()
    {
        target = GameMenager.instance.tower.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }*/

    /*public void Init(SpawnData data)
    {
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }*/

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;

        health -= collision.GetComponent<Bullet>().damege;

        if (health > 0)
        {

        }
        else
        {

        }
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
