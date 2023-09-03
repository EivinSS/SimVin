using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnEnable()
    {
        InputManager.Instance.OnSideTouch += HandleTouchInput;
        InputManager.Instance.OnTouchRelease += HandleTouchRelease;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnSideTouch -= HandleTouchInput;
        InputManager.Instance.OnTouchRelease -= HandleTouchRelease;
    }

    private void HandleTouchInput(InputManager.TouchSide touchSide)
    {
        Debug.Log(touchSide.ToString());
    }

    private void HandleTouchRelease(InputManager.TouchSide touchSide) 
    {
        Debug.Log("Released: " + touchSide.ToString());
    }
}