using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpForce;

    private Rigidbody rb;
    private BoxCollider boxCollider;

    [SerializeField] private float gravityScale = 5;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float hitForce;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }


    void Update()
    {        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
        if (rb.velocity.y == 0f)
        {
            isGrounded = true;
        } else 
        {
            isGrounded = false;
        }
    }
    private void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * (gravityScale - 1));        
    }

    private void Jump()
    {
        if (isGrounded) rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts.Length > 0)
        {
            ContactPoint contact = collision.contacts[0];

            if (Vector3.Dot(contact.normal, Vector3.right) > 0.5f || Vector3.Dot(contact.normal, Vector3.left) > 0.5f)
            {
                HitByPlatform();
            }
        }
    }
    private void HitByPlatform()
    {
        GameplayController.instance.GameOver();
        boxCollider.enabled = false;
        rb.velocity = Vector3.up * hitForce;
    }

}
