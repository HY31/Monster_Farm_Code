using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriter;
    [SerializeField] Animator animator;

    Rigidbody2D rigid;
    
    Vector2 inputVec;
    public float speed;

    public bool IsMovable = true;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
    }
    void FixedUpdate()
    {
        if(IsMovable)
        {
            Vector2 nextVec = inputVec.normalized * speed * Time.deltaTime;
            rigid.MovePosition(rigid.position + nextVec);
        } 
    }

    private void LateUpdate()
    {
        animator.SetFloat("Speed", inputVec.magnitude);
        if(inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }
}
