using UnityEngine;

public class SwipeExit : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float swipeThreshold = 50f; 

    void Update()
    {
        DetectSwipe();
        DetectBackButton();
    }

    private void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    endTouchPosition = touch.position;
                    HandleSwipe();
                    break;
            }
        }
    }

    private void HandleSwipe()
    {
        float horizontalSwipeDistance = endTouchPosition.x - startTouchPosition.x;

        if (Mathf.Abs(horizontalSwipeDistance) > swipeThreshold)
        {
            if (horizontalSwipeDistance > 0) 
            {
                ExitApp();
            }
            else if (horizontalSwipeDistance < 0) 
            {
                ExitApp();
            }
        }
    }

    private void DetectBackButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            ExitApp();
        }
    }

    private void ExitApp()
    {
        Debug.Log("Exiting app...");
        Application.Quit();
    }
}
