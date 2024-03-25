using System.Collections.Generic;
using System.Linq;
using config;
using DefaultNamespace;
using events;
using UnityEngine;
using UnityEngine.UI;

public class CardContainer : MonoBehaviour {
	[Header("Managers")]
	[SerializeField]
	public PlayerManager playerManager;

    [Header("Constraints")]
    [SerializeField]
    private bool forceFitContainer;

    [Header("Alignment")]
    [SerializeField]
    private CardAlignment alignment = CardAlignment.Center;

    [SerializeField]
    private bool allowCardRepositioning = true;

    [Header("Rotation")]
    [SerializeField]
    [Range(0f, 90f)]
    private float maxCardRotation;

    [SerializeField]
    private float maxHeightDisplacement;

    [SerializeField]
    private ZoomConfig zoomConfig;

    [SerializeField]
    private AnimationSpeedConfig animationSpeedConfig;

    [SerializeField]
    private CardPlayConfig cardPlayConfig;
    
    [Header("Events")]
    [SerializeField]
    private EventsConfig eventsConfig;
    
    [Header("CardList")]
    [SerializeField]
    public List<CardWrapper> cardOnHandUI = new();
    public List<Card> handList;

    private RectTransform rectTransform;
    private CardWrapper currentDraggedCard;

    private void Start() {
        rectTransform = GetComponent<RectTransform>();
        InitCards();
		// Get list from playerManager
        if (playerManager != null) {
            handList = playerManager.hand;
        } else {
            Debug.LogError("PlayerManager reference is not set in CardContainer!");
        }
    }

    void Update() {
        UpdateCards();
    }

	// Initiate cards and anchors
    private void InitCards() {
        SetUpCards();
        SetCardsAnchor();
    }

	// Associate a rotation based on the index in the cards list, first and last at max rotation
    private void SetCardsRotation() {
        for (var i = 0; i < cardOnHandUI.Count; i++) {
            cardOnHandUI[i].targetRotation = GetCardRotation(i);
            cardOnHandUI[i].targetVerticalDisplacement = GetCardVerticalDisplacement(i);
        }
    }

	// Associate a vertical displacement based on the index in the cards list with center card as the highest displacement
    private float GetCardVerticalDisplacement(int index) {
        if (cardOnHandUI.Count < 3) return 0;
        return maxHeightDisplacement *
               (1 - Mathf.Pow(index - (cardOnHandUI.Count - 1) / 2f, 2) / Mathf.Pow((cardOnHandUI.Count - 1) / 2f, 2));
    }

	// Associate a rotation based on the index in the cards list, first and last at max rotation
    private float GetCardRotation(int index) {
        if (cardOnHandUI.Count < 3) return 0;
        return -maxCardRotation * (index - (cardOnHandUI.Count - 1) / 2f) / ((cardOnHandUI.Count - 1) / 2f);
    }

	// Give wrapper component to Initiated Object from PlayerManager.cs
    void SetUpCards() {
        cardOnHandUI.Clear();
        foreach (Transform card in transform) {
            var wrapper = card.GetComponent<CardWrapper>();
            if (wrapper == null) {
                wrapper = card.gameObject.AddComponent<CardWrapper>();
            }

            cardOnHandUI.Add(wrapper);

            AddOtherComponentsIfNeeded(wrapper);

            wrapper.zoomConfig = zoomConfig;
            wrapper.animationSpeedConfig = animationSpeedConfig;
            wrapper.eventsConfig = eventsConfig;
            wrapper.container = this;
        }
    }

	// Add need components
    private void AddOtherComponentsIfNeeded(CardWrapper wrapper) {
        var canvas = wrapper.GetComponent<Canvas>();
        if (canvas == null) {
            canvas = wrapper.gameObject.AddComponent<Canvas>();
        }

        canvas.overrideSorting = true;

        if (wrapper.GetComponent<GraphicRaycaster>() == null) {
            wrapper.gameObject.AddComponent<GraphicRaycaster>();
        }
    }

	// Update card displaying infos
    private void UpdateCards() {
        if (transform.childCount != cardOnHandUI.Count) {
            InitCards();
        }

        if (cardOnHandUI.Count == 0) {
            return;
        }

        SetCardsPosition();
        SetCardsRotation();
        SetCardsUILayers();
        UpdateCardOrder();
    }

	// Set UI layer
    private void SetCardsUILayers() {
        for (var i = 0; i < cardOnHandUI.Count; i++) {
            cardOnHandUI[i].uiLayer = zoomConfig.defaultSortOrder + i;
        }
    }

	// Get the index of the dragged card depending on its position
    private void UpdateCardOrder() {
        if (!allowCardRepositioning || currentDraggedCard == null) return;

        var newCardIdx = cardOnHandUI.Count(card => currentDraggedCard.transform.position.x > card.transform.position.x);
        var originalCardIdx = cardOnHandUI.IndexOf(currentDraggedCard);
        if (newCardIdx != originalCardIdx) {
            cardOnHandUI.RemoveAt(originalCardIdx);
            handList.RemoveAt(originalCardIdx);
            if (newCardIdx > originalCardIdx && newCardIdx < cardOnHandUI.Count - 1) {
                newCardIdx--;
            }
            cardOnHandUI.Insert(newCardIdx, currentDraggedCard);
			handList.Insert(newCardIdx, currentDraggedCard.card);
        }
        currentDraggedCard.transform.SetSiblingIndex(newCardIdx);
		playerManager.hand = handList;
    }

	// Set card positioning based on container width
    private void SetCardsPosition() {
        // Compute the total width of all the cards in global space
        var cardsTotalWidth = cardOnHandUI.Sum(card => card.width * card.transform.lossyScale.x);
        // Compute the width of the container in global space
        var containerWidth = rectTransform.rect.width * transform.lossyScale.x;
        if (forceFitContainer && cardsTotalWidth > containerWidth) {
            DistributeChildrenToFitContainer(cardsTotalWidth);
        }
        else {
            DistributeChildrenWithoutOverlap(cardsTotalWidth);
        }
    }

	// Force fit container = true
    private void DistributeChildrenToFitContainer(float childrenTotalWidth) {
        // Get the width of the container
        var width = rectTransform.rect.width * transform.lossyScale.x;
        // Get the distance between each child
        var distanceBetweenChildren = (width - childrenTotalWidth) / (cardOnHandUI.Count - 1);
        // Set all children's positions to be evenly spaced out
        var currentX = transform.position.x - width / 2;
        foreach (CardWrapper child in cardOnHandUI) {
            var adjustedChildWidth = child.width * child.transform.lossyScale.x;
            child.targetPosition = new Vector2(currentX + adjustedChildWidth / 2, transform.position.y);
            currentX += adjustedChildWidth + distanceBetweenChildren;
        }
    }

	// Force fit container = false
    private void DistributeChildrenWithoutOverlap(float childrenTotalWidth) {
        var currentPosition = GetAnchorPositionByAlignment(childrenTotalWidth);
        foreach (CardWrapper child in cardOnHandUI) {
            var adjustedChildWidth = child.width * child.transform.lossyScale.x;
            child.targetPosition = new Vector2(currentPosition + adjustedChildWidth / 2, transform.position.y);
            currentPosition += adjustedChildWidth;
        }
    }

	// Anchor position of the hand
    private float GetAnchorPositionByAlignment(float childrenWidth) {
        var containerWidthInGlobalSpace = rectTransform.rect.width * transform.lossyScale.x;
        switch (alignment) {
            case CardAlignment.Left:
                return transform.position.x - containerWidthInGlobalSpace / 2;
            case CardAlignment.Center:
                return transform.position.x - childrenWidth / 2;
            case CardAlignment.Right:
                return transform.position.x + containerWidthInGlobalSpace / 2 - childrenWidth;
            default:
                return 0;
        }
    }

	// Set anchor of each card
    private void SetCardsAnchor() {
        foreach (CardWrapper child in cardOnHandUI) {
            child.SetAnchor(new Vector2(0, 0.5f), new Vector2(0, 0.5f));
        }
    }

	// Store the correct GameObject on Card drag start
    public void OnCardDragStart(CardWrapper cardObj) {
        currentDraggedCard = cardObj;
        Debug.Log($"Current Dragged Card Index: {cardOnHandUI.IndexOf(currentDraggedCard)}");
    }

    public void OnCardDragEnd() {
        // If card is in play area, play it!
		// Temp function
        if (IsCursorInPlayArea()) {
            eventsConfig?.OnCardPlayed?.Invoke(new CardPlayed(currentDraggedCard));
            if (cardPlayConfig.destroyOnPlay) {
                DestroyCard(currentDraggedCard);
            }
        }

        currentDraggedCard = null;
    }
    
    public void DestroyCard(CardWrapper card) {
        eventsConfig.OnCardDestroy?.Invoke(new CardDestroy(card));
    }

    private bool IsCursorInPlayArea() {
        if (cardPlayConfig.playArea == null) return false;
        
        var cursorPosition = Input.mousePosition;
        var playArea = cardPlayConfig.playArea;
        var playAreaCorners = new Vector3[4];
        playArea.GetWorldCorners(playAreaCorners);
        return cursorPosition.x > playAreaCorners[0].x &&
               cursorPosition.x < playAreaCorners[2].x &&
               cursorPosition.y > playAreaCorners[0].y &&
               cursorPosition.y < playAreaCorners[2].y;
        
    }
}
