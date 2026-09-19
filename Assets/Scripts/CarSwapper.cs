using UnityEngine;

public class CarSwapper : MonoBehaviour
{
    [Header("Hovercraft Fleet")]
    public GameObject[] hovercrafts;

    [Header("Camera Reference")]
    public FollowCamera followCamera;

    private int activeIndex = 0;

    void Start()
    {
        if (hovercrafts == null || hovercrafts.Length == 0)
        {
            Debug.LogError("CarSwapper: Please assign hovercrafts in the Inspector!");
            return;
        }

        // Auto-find FollowCamera if not assigned
        if (followCamera == null && Camera.main != null)
        {
            followCamera = Camera.main.GetComponent<FollowCamera>();
        }

        // Apply initial active state
        UpdateActiveHovercraft();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CycleToNextCar();
        }
    }

    void CycleToNextCar()
    {
        if (hovercrafts.Length == 0) return;

        // Modulus arithmetic: 0 -> 1 -> 2 -> 0
        activeIndex = (activeIndex + 1) % hovercrafts.Length;

        UpdateActiveHovercraft();
    }

    void UpdateActiveHovercraft()
    {
        for (int i = 0; i < hovercrafts.Length; i++)
        {
            if (hovercrafts[i] == null) continue;

            bool isActive = (i == activeIndex);

            // Turn Movement script ON for active car, OFF for all inactive cars
            Movement moveScript = hovercrafts[i].GetComponent<Movement>();
            if (moveScript != null)
            {
                moveScript.enabled = isActive;
            }
        }

        // Direct the follow camera to target the active craft
        if (followCamera != null && hovercrafts[activeIndex] != null)
        {
            followCamera.SetTarget(hovercrafts[activeIndex].transform);
        }

        Debug.Log($"Active Hovercraft: Index {activeIndex} ({hovercrafts[activeIndex].name})");
    }
}