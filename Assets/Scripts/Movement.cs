using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 60f;
    public float turnSpeed = 50f;
    void Start()
    {
    }
    void Update()
    {
        // Hint: The global static variable "Terrain.activeTerrain" 
        // may be helpful or have useful methods for user here or in
        // other scripts.
        Terrain terrain = Terrain.activeTerrain;

        //Translate or Rotate position of craft depending on keys pressed
        if (Input.GetKey(KeyCode.W))
            transform.Translate(0, 0, speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(0, 0, -(speed) * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(0, -(turnSpeed) * Time.deltaTime, 0);

        Vector3 position = transform.position;

        //Puts the hover in hovercraft, setting a value to how high the craft sits above the terrain
        float hoverHeight = 5.0f;

        // Tracks the position of the terrain so the hovercraft can adapt as it moves
        float terrainHeight = terrain.SampleHeight(position);

        //Takes the set hover height and adjusts the craft's position to match as it moves across terrain
        position.y = terrainHeight + hoverHeight;

        // set the game object's translation (not an increment)
        transform.position = position;

    }
}
