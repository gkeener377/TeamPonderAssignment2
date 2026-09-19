using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 3f, -7f);
    public float followSpeed = 10f;
    public float rotationSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // Calculate offset position behind target in world space
        Vector3 targetPosition = target.position + target.rotation * offset;

        // Smooth position follow
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Smooth rotation follow
        Quaternion targetRotation = Quaternion.LookRotation(target.forward, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}