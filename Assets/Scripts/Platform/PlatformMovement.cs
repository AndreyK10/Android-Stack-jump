using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlatformMovement : MonoBehaviour
{

    private List<Vector3> listVects = new List<Vector3>();

    [Range(0, 1)]
    [SerializeField] private float t;
    [SerializeField] private float pathTime;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        pathTime = Random.Range(2.5f, 3.5f);
        SetPath();

        StartCoroutine(StartMoving(0, 1, pathTime));
    }
    private void Update()
    {
        transform.position = Bezier.GetPoint(listVects.ToArray(), t);
        transform.rotation = Quaternion.LookRotation(Bezier.GetFirstDerivative(listVects.ToArray(), t));
    }

    //private void OnDrawGizmos()
    //{
    //    int segmentsNumber = 20;
    //    Vector3 previousPoint = listVects[0];

    //    for (int i = 0; i < segmentsNumber + 1; i++)
    //    {
    //        float parameter = (float)i / segmentsNumber;
    //        Vector3 point = Bezier.GetPoint(listVects.ToArray(), parameter);
    //        Gizmos.DrawLine(previousPoint, point);
    //        previousPoint = point;
    //    }
    //}

    private void SetPath()
    {
        transform.parent.gameObject.TryGetComponent(out SpawnPoints spawnPoint);
        foreach (Transform point in spawnPoint.transform)
        {
            if (point.TryGetComponent(out Point p))
            {
                listVects.Add(point.transform.position);
            }
        }
        transform.parent = null;
    }


    private IEnumerator StartMoving(float initialValue, float finalValue, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            t = Mathf.Lerp(initialValue, finalValue, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        t = finalValue;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            if (collision.contacts.Length > 0)
            {
                ContactPoint contact = collision.contacts[0];

                if (Vector3.Dot(contact.normal, Vector3.up) > 0.5f)
                {
                    ScoreManager.instance.IncreaseScore();
                }
            }

            rb.isKinematic = true;
            this.enabled = false;

        }
    }
}
