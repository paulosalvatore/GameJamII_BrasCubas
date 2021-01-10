using System;
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

    [SerializeField]
    private float gravity;

    public static Player Instance;

    private bool _isColliding;

    [SerializeField]
    private float maxVelocity;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        rb.velocity = new Vector3(
            -Mathf.Min(Mathf.Abs(rb.velocity.x), maxVelocity),
            rb.velocity.y,
            rb.velocity.z
        );

        // Time.timeScale = 1f;

        // if (!_isColliding)
        // {
        //     rb.velocity = new Vector3(
        //         rb.velocity.x + -moveSpeed * Time.deltaTime,
        //         rb.velocity.y,
        //         rb.velocity.z
        //     );
        // }
        //
        // rb.velocity = new Vector3(
        //     rb.velocity.x,
        //     rb.velocity.y - gravity * Time.deltaTime,
        //     rb.velocity.z
        // );

        armPivot.Rotate(Vector3.forward * (rotateSpeed * Time.deltaTime));
    }

    public void OnCollisionEnter(Collision other)
    {
        _isColliding = true;

        // Debug.Log("CollisionEnter: " + other.gameObject.name);
    }

    public void OnCollisionExit(Collision other)
    {
        _isColliding = false;

        // Debug.Log("CollisionExit: " + other.gameObject.name);
    }
}
