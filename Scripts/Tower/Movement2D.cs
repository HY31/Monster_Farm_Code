using UnityEngine;

public class Movemont2D : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.0f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;

    public float  MoveSpeed => moveSpeed;


    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
}
