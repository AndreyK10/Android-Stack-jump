using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Platform : MonoBehaviour
{
    private Rigidbody rb;
    private PlatformMovement platformMovement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        platformMovement = GetComponent<PlatformMovement>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            rb.isKinematic = true;
            platformMovement.enabled = false;

        }
    }
}
