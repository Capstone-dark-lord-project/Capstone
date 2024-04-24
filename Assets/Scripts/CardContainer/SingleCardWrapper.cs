using config;
using events;
using UnityEngine;
using UnityEngine.EventSystems;

public class SingleCardWrapper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler,
    IPointerUpHandler {
    public Vector2 targetPosition;
    private RectTransform rectTransform;
    private Canvas canvas;
    public AnimationSpeedConfig animationSpeedConfig;
    public SingleCardContainer container;
    private bool isHovered;
    private bool isDragged;
    private Vector2 dragStartPos;
    public Card card;
    public int uiLayer;
    public int slot;

    public float width {
        get => rectTransform.rect.width * rectTransform.localScale.x + 2.0f;
    }

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        card = GetComponent<CardDisplay>().card;
    }

    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        UpdateUILayer();
    }

    private void UpdateUILayer() {
        if (!isHovered && !isDragged) {
            canvas.sortingOrder = uiLayer;
        }
    }

    private void UpdatePosition() {
        if (!isDragged) {
            var target = new Vector2(targetPosition.x, targetPosition.y);

            var distance = Vector2.Distance(rectTransform.position, target);
            var repositionSpeed = rectTransform.position.y > target.y || rectTransform.position.y < 0
                ? animationSpeedConfig.releasePosition
                : animationSpeedConfig.position;
            rectTransform.position = Vector2.Lerp(rectTransform.position, target,
                repositionSpeed / distance * Time.deltaTime);
        }
        else {
            var delta = (Vector2)Input.mousePosition + dragStartPos;
            rectTransform.position = new Vector2(delta.x, delta.y);
        }
    }

    public void SetAnchor(Vector2 min, Vector2 max) {
        rectTransform.anchorMin = min;
        rectTransform.anchorMax = max;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (isDragged) {
            return;
        }
        canvas.sortingOrder = 100;
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (isDragged) {
            // Avoid hover events while dragging
            return;
        }
        canvas.sortingOrder = uiLayer;
        isHovered = false;
    }

    public void OnPointerDown(PointerEventData eventData) {
        isDragged = true;
        dragStartPos = new Vector2(transform.position.x - eventData.position.x,
            transform.position.y - eventData.position.y);
        container.OnCardDragStart(this);
    }

    public void OnPointerUp(PointerEventData eventData) {
        isDragged = false;
        container.OnCardDragEnd();
    }
}
