using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Hovercraft Performance Stats")]
    public float speed = 60f;
    public float turnSpeed = 50f;
    public float hoverHeight = 5.0f;

    [Header("Hover Vibration Effect")]
    public float quiverAmount = 0.05f;
    public float quiverSpeed = 15f;

    void Update()
    {
        // Steering
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);

        // Forward / Backward
        float moveDistance = 0f;
        if (Input.GetKey(KeyCode.W))
            moveDistance += speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            moveDistance -= speed * Time.deltaTime;

        Vector3 newPosition = transform.position + (transform.forward * moveDistance);

        // Terrain Hovering
        Terrain terrain = Terrain.activeTerrain;
        if (terrain != null)
        {
            float terrainHeight = terrain.SampleHeight(newPosition) + terrain.transform.position.y;
            float quiver = Mathf.Sin(Time.time * quiverSpeed) * quiverAmount;
            newPosition.y = terrainHeight + hoverHeight + quiver;
        }

        transform.position = newPosition;
    }
}