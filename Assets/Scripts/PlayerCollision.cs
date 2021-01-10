using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Player.Instance.OnCollisionEnter(other);
    }

    private void OnCollisionExit(Collision other)
    {
        Player.Instance.OnCollisionExit(other);
    }
}
