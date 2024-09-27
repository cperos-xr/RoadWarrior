using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScrollViewController : MonoBehaviour
{
    [Header("UI Components")]
    public ScrollRect scrollRect; // Reference to your ScrollRect component
    public Button leftButton;     // Reference to your left button
    public Button rightButton;    // Reference to your right button
    public RectTransform content; // The content of the ScrollRect

    [Header("Scroll Settings")]
    public float scrollSpeed = 0.5f;  // Duration of the scrolling in seconds

    public List<GameObject> items; // List of items in the scroll view

    private Coroutine scrollCoroutine;
    private bool isScrolling;
    private float scrollAmount; // Dynamic scroll amount

    private void Start()
    {
        // Calculate the scroll amount dynamically based on the number of items
        UpdateScrollAmount();

        // Add listeners to the buttons
        leftButton.onClick.AddListener(() => OnScrollButtonClicked(-scrollAmount));
        rightButton.onClick.AddListener(() => OnScrollButtonClicked(scrollAmount));
    }

    /// <summary>
    /// Updates the scroll amount based on the number of items.
    /// </summary>
    public void UpdateScrollAmount()
    {
        // Ensure there are enough items to scroll
        if (items.Count > 1)
        {
            // Divide the total scroll range by the number of scroll steps needed
            scrollAmount = 1.0f / (items.Count - 1);
        }
        else
        {
            // Default to zero if there is only one item or none
            scrollAmount = 0f;
        }
    }

    /// <summary>
    /// Called when a scroll button is clicked.
    /// </summary>
    /// <param name="amount">The amount to scroll. Positive for right, negative for left.</param>
    private void OnScrollButtonClicked(float amount)
    {
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
        UpdateActiveItem();
        scrollCoroutine = null;
        isScrolling = false;
    }

    /// <summary>
    /// Updates the active item based on the current scroll position.
    /// </summary>
    private void UpdateActiveItem()
    {
        // Calculate the width of each item based on the content and number of items
        float contentWidth = content.rect.width;
        float itemWidth = contentWidth / items.Count;

        // Determine the index of the item closest to the center of the viewport
        float viewportWidth = scrollRect.viewport.rect.width;
        float scrollPosition = scrollRect.horizontalNormalizedPosition * (contentWidth - viewportWidth);

        // Calculate the index of the closest item
        int activeIndex = Mathf.RoundToInt(scrollPosition / itemWidth);

        // Clamp index to ensure it's within bounds
        activeIndex = Mathf.Clamp(activeIndex, 0, items.Count - 1);

        // Access the active item
        GameObject activeItem = items[activeIndex];
        Debug.Log($"Active Item: {activeItem.name}");

        ScrollImage scrollImage = activeItem.GetComponent<ScrollImage>();
        if (scrollImage != null)
        {
            // Perform additional actions based on the active item
            Debug.Log($"Active Item Part: {scrollImage.part.partName}");
        }
        // Perform additional actions if needed, like updating UI or triggering events
    }
}
