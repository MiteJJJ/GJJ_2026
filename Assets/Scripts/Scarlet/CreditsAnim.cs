using UnityEngine;

public class CreditsScroll : MonoBehaviour
{
    [SerializeField] private float startY = 0f;
    [SerializeField] private float endY = 2000f;
    [SerializeField] private float duration = 30f;

    private RectTransform rectTransform;
    private float elapsedTime = 0f;
    private bool isScrolling = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, startY);
        isScrolling = true;
    }

    void Update()
    {
        if (isScrolling)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            if (t >= 1f)
            {
                t = 1f;
                isScrolling = false;
            }

            float currentY = Mathf.Lerp(startY, endY, t);
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, currentY);
        }
    }
}