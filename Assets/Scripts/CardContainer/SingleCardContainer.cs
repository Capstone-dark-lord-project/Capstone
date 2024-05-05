using System.Collections.Generic;
using System.Linq;
using config;
using events;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class SingleCardContainer : MonoBehaviour {
    [Header("Managers")]
	[SerializeField]
	public PlayerManager playerManager;
    public CraftingManager craftingManager;

    [Header("Events")]
    [SerializeField]
    //private SingleCardEventsConfig eventsConfig;
    public UnityEvent<int, SingleCardWrapper> OnCardRemoveFromSlot;

    [SerializeField]
    private AnimationSpeedConfig animationSpeedConfig;

    [SerializeField]
    private CraftingUIConfig craftingUIConfig;
    
    [Header("Card List")]
    [SerializeField]
    public List<SingleCardWrapper> cardInSlot;

    private RectTransform rectTransform;
    private SingleCardWrapper currentDraggedCard;
    private int previousChildCount;
    public int slot;

    private void Start() {
        //eventsConfig.OnCardRemoveFromSlot.AddListener(SingleCardDestroyer.OnCardRemoveFromSlot());
        cardInSlot.Clear();
        rectTransform = GetComponent<RectTransform>();
        previousChildCount = transform.childCount;
    }

    private void Update() {
        if (transform.childCount > previousChildCount)
        {
            Debug.LogWarning("A child has been added to the container!");
            previousChildCount = transform.childCount;
            InitCards();
        }
        if (transform.childCount < previousChildCount)
        {
            Debug.LogWarning("A child has been remove from the container!");
            previousChildCount = transform.childCount;
        }

        if (cardInSlot.Count == 0) {
            return;
        }
        
        SetCardsPosition();
    }

    private void InitCards() {
        SetUpCards();
        // SetCardsAnchor();
    }

    void SetUpCards() {
        cardInSlot.Clear();
        foreach (Transform card in transform) {
            var wrapper = card.GetComponent<SingleCardWrapper>();
            if (wrapper == null) {
                wrapper = card.gameObject.AddComponent<SingleCardWrapper>();
            }

            cardInSlot.Add(wrapper);

            AddOtherComponentsIfNeeded(wrapper);

            wrapper.animationSpeedConfig = animationSpeedConfig;
            wrapper.container = this;
        }
    }

	// Add needed components
    private void AddOtherComponentsIfNeeded(SingleCardWrapper wrapper) {
        var canvas = wrapper.GetComponent<Canvas>();
        if (canvas == null) {
            canvas = wrapper.gameObject.AddComponent<Canvas>();
        }

        canvas.overrideSorting = true;

        if (wrapper.GetComponent<GraphicRaycaster>() == null) {
            wrapper.gameObject.AddComponent<GraphicRaycaster>();
        }
    }

    private void SetCardsPosition() {
        var cardsTotalWidth = cardInSlot.Sum(card => card.width * card.transform.lossyScale.x);
        var containerWidth = rectTransform.rect.width * transform.lossyScale.x;
        DistributeChildrenToFitContainer(cardsTotalWidth);
    }

    private void DistributeChildrenToFitContainer(float childrenTotalWidth) {
        // Get the width of the container
        var width = rectTransform.rect.width * transform.lossyScale.x;
        // Get the distance between each child
        var distanceBetweenChildren = (width - childrenTotalWidth) / (cardInSlot.Count - 1);
        // Set all children's positions to be evenly spaced out
        var currentX = transform.position.x - width / 2;
        foreach (SingleCardWrapper child in cardInSlot) {
            var adjustedChildWidth = child.width * child.transform.lossyScale.x;
            child.targetPosition = new Vector2(currentX + adjustedChildWidth / 2, transform.position.y);
            currentX += adjustedChildWidth + distanceBetweenChildren;
        }
    }

    public void OnCardDragStart(SingleCardWrapper cardObj) {
        currentDraggedCard = cardObj;
        if (IsCursorInCraftArea1()) {
            slot = 1;
        } else if (IsCursorInCraftArea2()) {
            slot = 2;
        } else if (IsCursorInResultArea()) {
            slot = 3;
        }
    }

    public void OnCardDragEnd() {

        // Crafting functions
        if (!IsCursorInCraftArea1() && slot == 1) {
            Debug.LogWarning("Out of Craft Area 1");
            CardRemoved(slot, currentDraggedCard);
        }

        if (!IsCursorInCraftArea2() && slot == 2) {
            Debug.LogWarning("Out of Craft Area 2");
            CardRemoved(slot, currentDraggedCard);
        }

        if (!IsCursorInResultArea() && slot == 3) {
            Debug.LogWarning("Out of Result Area");
            CardRemoved(slot, currentDraggedCard);
        }

        currentDraggedCard = null;
    }

    public void CardRemoved(int slot, SingleCardWrapper card) {
        Debug.LogWarning("Destroy Method");
        OnCardRemoveFromSlot?.Invoke(slot, card);
        playerManager.UpdateHandCountUI();
    }

    // Area Checks
    private bool IsCursorInCraftArea1() {
        if (craftingUIConfig.craftArea1 == null) return false;
        
        var cursorPosition = Input.mousePosition;
        var craftArea1 = craftingUIConfig.craftArea1;
        var craftAreaCorners = new Vector3[4];
        craftArea1.GetWorldCorners(craftAreaCorners);
        return cursorPosition.x > craftAreaCorners[0].x &&
               cursorPosition.x < craftAreaCorners[2].x &&
               cursorPosition.y > craftAreaCorners[0].y &&
               cursorPosition.y < craftAreaCorners[2].y;
    }

    private bool IsCursorInCraftArea2() {
        if (craftingUIConfig.craftArea2 == null) return false;
        
        var cursorPosition = Input.mousePosition;
        var craftArea2 = craftingUIConfig.craftArea2;
        var craftAreaCorners = new Vector3[4];
        craftArea2.GetWorldCorners(craftAreaCorners);
        return cursorPosition.x > craftAreaCorners[0].x &&
               cursorPosition.x < craftAreaCorners[2].x &&
               cursorPosition.y > craftAreaCorners[0].y &&
               cursorPosition.y < craftAreaCorners[2].y;
    }

    private bool IsCursorInResultArea() {
        if (craftingUIConfig.resultArea == null) return false;
        
        var cursorPosition = Input.mousePosition;
        var resultArea = craftingUIConfig.resultArea;
        var resultAreaCorners = new Vector3[4];
        resultArea.GetWorldCorners(resultAreaCorners);
        return cursorPosition.x > resultAreaCorners[0].x &&
               cursorPosition.x < resultAreaCorners[2].x &&
               cursorPosition.y > resultAreaCorners[0].y &&
               cursorPosition.y < resultAreaCorners[2].y;
    }
}

