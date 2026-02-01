using UnityEngine;

public class ImageFloat : MonoBehaviour
{
    [SerializeField] private float floatRangeX = 20f;
    [SerializeField] private float floatRangeY = 20f;
    [SerializeField] private float speedX = 1f;
    [SerializeField] private float speedY = 1.5f;

    private RectTransform rectTransform;
    private Vector2 startPos;
    private float timeX;
    private float timeY;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
        timeX = Random.Range(0f, 100f);
        timeY = Random.Range(0f, 100f);
    }

    void Update()
    {
        timeX += Time.deltaTime * speedX;
        timeY += Time.deltaTime * speedY;

        float offsetX = Mathf.Sin(timeX) * floatRangeX;
        float offsetY = Mathf.Sin(timeY) * floatRangeY;

        rectTransform.anchoredPosition = startPos + new Vector2(offsetX, offsetY);
    }
}