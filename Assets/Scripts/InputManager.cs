using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    // Define a custom enum to represent the touch/click side
    public enum TouchSide { Left, Right }

    // Define an event with the custom enum as an argument
    public event Action<TouchSide> OnSideTouch;
    public event Action<TouchSide> OnTouchRelease;

    bool isTouching = false; // Flag to track if the screen is currently being touched
    TouchSide initialSide = TouchSide.Left; // To store the initial touch side

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (!isTouching)
        {
            // Check for the first touch
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                Vector2 inputPosition;

                // Check for touch input on mobile devices
                if (Input.touchCount > 0)
                {
                    inputPosition = Input.GetTouch(0).position;
                }
                else // Fallback to mouse input for editor/testing
                {
                    inputPosition = Input.mousePosition;
                }

                initialSide = DetermineTouchSide(inputPosition);
                isTouching = true;

                // Notify listeners about the initial touch
                OnSideTouch?.Invoke(initialSide);
            }
        }
        else
        {
            // Check for touch release
            if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
            {
                Vector2 inputPosition;

                // Check for touch input on mobile devices
                if (Input.touchCount > 0)
                {
                    inputPosition = Input.GetTouch(0).position;
                }
                else // Fallback to mouse input for editor/testing
                {
                    inputPosition = Input.mousePosition;
                }

                TouchSide finalSide = DetermineTouchSide(inputPosition);
                isTouching = false;

                // Notify listeners about the touch release
                OnTouchRelease?.Invoke(finalSide);
            }
        }
    }

    private TouchSide DetermineTouchSide(Vector2 touchPosition)
    {
        // Get the screen width and calculate the center
        float screenWidth = Screen.width;
        float screenCenter = screenWidth / 2f;

        // Compare the touch position to the screen center to determine the side
        if (touchPosition.x < screenCenter)
        {
            return TouchSide.Left;
        }
        else
        {
            return TouchSide.Right;
        }
    }
}
