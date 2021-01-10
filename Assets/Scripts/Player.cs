using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private Transform armPivot;

    [SerializeField]
    private float rotateSpeed;

    [SerializeField]
    private float moveSpeed;

    private void Update()
    {
        rb.velocity = new Vector3(
            -moveSpeed,
            rb.velocity.y,
            rb.velocity.z
        );

        armPivot.Rotate(Vector3.forward * (rotateSpeed * Time.deltaTime));
    }
}
