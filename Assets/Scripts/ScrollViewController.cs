using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollViewController : MonoBehaviour
{
    [Header("UI Components")]
    public ScrollRect scrollRect; // Reference to your ScrollRect component
    public Button leftButton;     // Reference to your left button
    public Button rightButton;    // Reference to your right button

    [Header("Scroll Settings")]
    [Range(0, 1)]
    public float scrollAmount = 0.1f; // Amount to scroll (0 to 1)
    public float scrollSpeed = 0.5f;  // Duration of the scrolling in seconds

    public int selection = 0;

    private Coroutine scrollCoroutine;
    private bool isScrolling;

    private void Start()
    {
        // Add listeners to the buttons
        leftButton.onClick.AddListener(() => OnScrollButtonClicked(-scrollAmount));
        rightButton.onClick.AddListener(() => OnScrollButtonClicked(scrollAmount));
    }

    /// <summary>
    /// Called when a scroll button is clicked.
    /// </summary>
    /// <param name="amount">The amount to scroll. Positive for right, negative for left.</param>
    private void OnScrollButtonClicked(float amount)
    {

        // Stop any ongoing scrolling coroutine
        //if (scrollCoroutine != null)
        //{
        //    StopCoroutine(scrollCoroutine);
        //}

        if (!isScrolling)
        {
            isScrolling = true;
            // Start a new scrolling coroutine
            scrollCoroutine = StartCoroutine(ScrollCoroutine(amount));
        }
    }

    /// <summary>
    /// Coroutine that smoothly scrolls the ScrollRect.
    /// </summary>
    /// <param name="amount">The normalized amount to scroll.</param>
    /// <returns></returns>
    private IEnumerator ScrollCoroutine(float amount)
    {
        if (amount < 0)
        {
            selection--;
        }
        else
        {
            selection++;
        }

        float elapsedTime = 0f;
        float startPosition = scrollRect.horizontalNormalizedPosition;
        float targetPosition = Mathf.Clamp01(startPosition + amount);

        while (elapsedTime < scrollSpeed)
        {
            elapsedTime += Time.deltaTime;
            float newPosition = Mathf.Lerp(startPosition, targetPosition, elapsedTime / scrollSpeed);
            scrollRect.horizontalNormalizedPosition = newPosition;
            yield return null;
        }

        // Ensure the final position is set
        scrollRect.horizontalNormalizedPosition = targetPosition;
        scrollCoroutine = null;
        isScrolling = false;
    }
}
