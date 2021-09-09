using System.Collections;
using System.Collections.Generic;
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

    private bool touchedLastFrame = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }


    void Update()
    {

        if (touchedLastFrame && Input.touchCount == 0)
        {
            touchedLastFrame = false;
        }
        else if (!touchedLastFrame && Input.touchCount > 0)
        {
            Jump();
            touchedLastFrame = true;
        }

        if (rb.velocity.y == 0f)
        {
            isGrounded = true;
        }
        else 
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
        StartCoroutine(Disappear());
    }
    private IEnumerator Disappear()
    {
        boxCollider.enabled = false;
        rb.velocity = Vector3.up * hitForce;
        yield return new WaitForSeconds(0.7f);
        gameObject.SetActive(false);
    }
}
