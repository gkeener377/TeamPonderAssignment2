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

    [Header("Terrain Tilt")]
    public float tiltSpeed = 5.0f;

    void Update()
    {
        // Steering left and right
        float turn = 0f;

        if (Input.GetKey(KeyCode.D))
            turn = 1f;
        if (Input.GetKey(KeyCode.A))
            turn = -1f;

        transform.Rotate(
            Vector3.up,
            turn * turnSpeed * Time.deltaTime
        );

        // Acceleration and reversing
        float moveDistance = 0f;
        if (Input.GetKey(KeyCode.W))
            moveDistance += speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            moveDistance -= speed * Time.deltaTime;

        Vector3 newPosition =
            transform.position +
            transform.forward * moveDistance;

        //Hovering and quivering
        Terrain terrain = Terrain.activeTerrain;

        if (terrain != null)
        {
            float terrainHeight =
                terrain.SampleHeight(newPosition) +
                terrain.transform.position.y;

            float quiver =
                Mathf.Sin(Time.time * quiverSpeed) *
                quiverAmount;

            newPosition.y =
                terrainHeight +
                hoverHeight +
                quiver;


            //Tilting with terrain
            Vector3 normal =
                terrain.terrainData.GetInterpolatedNormal(
                    (newPosition.x - terrain.transform.position.x)
                    / terrain.terrainData.size.x,

                    (newPosition.z - terrain.transform.position.z)
                    / terrain.terrainData.size.z
                );

            // Find the slope angle
            float slopeX =
                Mathf.Atan2(normal.z, normal.y) *
                Mathf.Rad2Deg;
            float slopeZ =
                Mathf.Atan2(normal.x, normal.y) *
                Mathf.Rad2Deg;

            // Create target rotation
            Quaternion targetRotation =
                Quaternion.Euler(
                    slopeX,
                    transform.eulerAngles.y,
                    -slopeZ
                );

            // Tilts based on aggressiveness of tilt speed value
            transform.rotation =
                Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    tiltSpeed * Time.deltaTime
                );
        }

        transform.position = newPosition;
    }
}