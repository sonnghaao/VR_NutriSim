using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FloatingPickable : MonoBehaviour
{
    // Floating parameters
    public float floatSpeed = 1f;
    public float floatHeight = 0.5f;

    // Rotation parameters
    public float rotationSpeed = 30f;

    private Vector3 startPos;
    private Rigidbody rb;

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Make the Rigidbody kinematic
    }

    void Update()
    {
        // Floating effect
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Rotating effect
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}