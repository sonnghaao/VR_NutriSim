using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

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
    private XRGrabInteractable grabInteractable;
    private bool isGrabbed = false;
    private Vector3 targetPosition;
    private Quaternion targetRotation;

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Make the Rigidbody kinematic

        
        grabInteractable = GetComponent<XRGrabInteractable>();

        
        grabInteractable.onSelectEntered.AddListener(OnGrab);
        grabInteractable.onSelectExited.AddListener(OnRelease);
    }

    void FixedUpdate()
    {
        
        if (isGrabbed)
        {
            rb.MovePosition(Vector3.Lerp(rb.position, targetPosition, Time.fixedDeltaTime * 10f));
            rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 10f));
        }
        else 
        {
            // Floating effect
            float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
            rb.MovePosition(new Vector3(transform.position.x, newY, transform.position.z));

            // Rotating effect
            rb.MoveRotation(Quaternion.Euler(0f, rotationSpeed * Time.deltaTime, 0f) * rb.rotation);
        }
    }

    void OnGrab(XRBaseInteractor interactor)
    {
        
        isGrabbed = true;

        
        rb.isKinematic = false;

        
        targetPosition = interactor.attachTransform.position;
        targetRotation = interactor.attachTransform.rotation;
    }

    void OnRelease(XRBaseInteractor interactor)
    {
        
        isGrabbed = false;

        
        rb.isKinematic = true;
    }
}