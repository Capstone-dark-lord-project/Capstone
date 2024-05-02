using System.Collections;
using System.Collections.Generic;
using System.Linq;
using config;
using events;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardContainer : MonoBehaviour {
	[Header("Managers")]
	[SerializeField]
	public PlayerManager playerManager;
    public CraftingManager craftingManager;

    [Header("Events")]
    [SerializeField]
    private EventsConfig eventsConfig;

    [Header("Constraints")]
    [SerializeField]
    private bool forceFitContainer;

    [Header("Alignment")]
    [SerializeField]
    private CardAlignment alignment = CardAlignment.Center;

    private enum CardAlignment {
        Left, Center, Right
    }

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

    [SerializeField]
    private DiscardConfig discardConfig;
    [SerializeField]
    private CraftingUIConfig craftingUIConfig;
    
    [Header("Card List")]
    [SerializeField]
    public List<CardWrapper> cardOnHandUI = new();

    private RectTransform rectTransform;
    private CardWrapper currentDraggedCard;
    
    [Header("Card Action Display")]
    private GameObject actionUI;
    public Canvas UIcanvas;
    public float scaleSpeed;
    public Vector3 finalScale;
    public GameObject actionCardPrefab;

    private void Start() {
        rectTransform = GetComponent<RectTransform>();
        InitCards();
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

	// Add needed components
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
            playerManager.hand.RemoveAt(originalCardIdx);
            if (newCardIdx > originalCardIdx && newCardIdx < cardOnHandUI.Count - 1) {
                newCardIdx--;
            }
            cardOnHandUI.Insert(newCardIdx, currentDraggedCard);
			playerManager.hand.Insert(newCardIdx, currentDraggedCard.card);
        }
        currentDraggedCard.transform.SetSiblingIndex(newCardIdx);
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
		// Discard function
        if (IsCursorInTrashArea() && discardConfig.trashArea.gameObject.activeSelf) 
        {
            Card card = currentDraggedCard.card;
            if(card is ActionCard actionCard)
            {
                playerManager.TaskVariableUpdate(ref playerManager.ActionTrashed);
                if (actionCard.actionName == ActionName.Bomb || actionCard.actionName == ActionName.Weapon)
                {
                    playerManager.TaskVariableUpdate(ref playerManager.weaponOrBombTrashed);
                }
            }
            else if (card is ItemCard)
            {
                playerManager.TaskVariableUpdate(ref playerManager.ItemTrashed);
            }
            eventsConfig?.OnCardDiscard?.Invoke(new CardEvent(currentDraggedCard));
            playerManager.UpdateHandCountUI();
            Debug.Log("Trash Area");
            //MethodToInvoke.Invoke(Arguments) >> public UnityEvent<CardDiscard> OnCardDiscard; which means invoke the OnCardDiscard event with the CardDiscard Argument.
        }

        // Play Card function
        if (IsCursorInPlayArea() && cardPlayConfig.playArea.gameObject.activeSelf && currentDraggedCard.tag == "ActionCards") 
        {
            var playedCard = currentDraggedCard.card;
            StartCoroutine(PlayCardAfterInstantiate(playedCard));

            if (cardPlayConfig.destroyOnPlay) 
            {
                Debug.LogWarning("Destroying");
                DestroyCard(currentDraggedCard);
            }
        }

        // Crafting functions
        if (IsCursorInCraftArea1() && craftingUIConfig.craftArea1.gameObject.activeSelf && craftingManager.cardSlot1 == null && currentDraggedCard.tag == "ResourceCards") 
        {
            Debug.Log("Craft Area 1");
            // Recasting to ResourceCard from Card
            ResourceCard resourceInput = currentDraggedCard.card as ResourceCard;
            eventsConfig?.OnCraftSlotInput?.Invoke(1, resourceInput);
            DestroyCard(currentDraggedCard);
            if (craftingManager.cardSlot2 != null) {
                Debug.Log("Initiate Crafting");
                CraftCard(craftingManager.cardSlot1, craftingManager.cardSlot2);
            }
        }

        if (IsCursorInCraftArea2() && craftingUIConfig.craftArea2.gameObject.activeSelf &&
            craftingManager.cardSlot2 == null && currentDraggedCard.tag == "ResourceCards") {
            Debug.Log("Craft Area 2");
            // Recasting to ResourceCard from Card
            ResourceCard resourceInput = currentDraggedCard.card as ResourceCard;
            eventsConfig?.OnCraftSlotInput?.Invoke(2, resourceInput);
            DestroyCard(currentDraggedCard);
            if (craftingManager.cardSlot1 != null) {
                Debug.Log("Initiate Crafting");
                CraftCard(craftingManager.cardSlot1, craftingManager.cardSlot2);
            }
        }

        currentDraggedCard = null;
    }
    
    public void DestroyCard(CardWrapper card) {
        eventsConfig.OnCardDestroy?.Invoke(new CardEvent(card));
        playerManager.UpdateHandCountUI();
    }

    public void CraftCard(ResourceCard slot1, ResourceCard slot2) {
        eventsConfig.OnBothSlotFull?.Invoke(slot1, slot2);
    }

    // Area Checks
    private bool IsCursorInPlayArea()
    {
        if (cardPlayConfig.playArea == null) return false;
        
        var cursorPosition = Input.mousePosition;
        var playArea = cardPlayConfig.playArea;
        var playAreaCorners = new Vector3[4];
        playArea.GetWorldCorners(playAreaCorners);
        return cursorPosition.x > playAreaCorners[0].x &&
               cursorPosition.x < playAreaCorners[2].x &&
               cursorPosition.y > playAreaCorners[0].y &&
               cursorPosition.y < playAreaCorners[2].y; // 2d clipping
    }

    private bool IsCursorInTrashArea()
    {
        if (discardConfig.trashArea == null) return false;
        
        var cursorPosition = Input.mousePosition;
        var trashArea = discardConfig.trashArea;
        var trashAreaCorners = new Vector3[4];
        trashArea.GetWorldCorners(trashAreaCorners);
        return cursorPosition.x > trashAreaCorners[0].x &&
               cursorPosition.x < trashAreaCorners[2].x &&
               cursorPosition.y > trashAreaCorners[0].y &&
               cursorPosition.y < trashAreaCorners[2].y;
    }

    private bool IsCursorInCraftArea1()
    {
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

    private bool IsCursorInCraftArea2()
    {
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

    private IEnumerator PlayCardAfterInstantiate(Card card) // Chain 1
    {
        yield return StartCoroutine(InstantiateActionCard(card)); //Instantiate -> Scale -> Destroy -> finish = FINISH
        
        if (card is ICardPlayable cardPlayable)
        {
            StartCoroutine(cardPlayable.Play()); // Effect
        }
    }

    public IEnumerator InstantiateActionCard(Card card) // Chain 2
    {
        GameObject CardPrefab = actionCardPrefab;
        Vector3 center = UIcanvas.transform.position;
        actionUI = Instantiate(CardPrefab, center, Quaternion.identity, UIcanvas.transform);

        actionUI.gameObject.AddComponent<Canvas>();
        Canvas actionCanvas = actionUI.GetComponent<Canvas>();
        actionCanvas.overrideSorting = true;
        actionCanvas.sortingOrder = 30;

        // Card Display
        CardDisplay cardDisplay = actionUI.GetComponent<CardDisplay>();
        if (cardDisplay != null)
        {
            cardDisplay.card = card;
            cardDisplay.DisplayCardInfo();
        }
        else
        {
            Debug.LogWarning("CardDisplay component not found on the instantiated object.");
        }
        
        Debug.LogWarning($"Instantiating action Card {card.cardName}");

        yield return StartCoroutine(ScaleObject());
    }

    private IEnumerator ScaleObject() // Chain 3
    {
        while (actionUI.transform.localScale != finalScale)
        {
            actionUI.transform.localScale = Vector3.MoveTowards(actionUI.transform.localScale, finalScale, scaleSpeed * Time.deltaTime);
            yield return null;
        }

        yield return StartCoroutine(DestroyAfterSeconds(2));
    }

    private IEnumerator DestroyAfterSeconds(int seconds) // Chain 4
    {
        yield return new WaitForSecondsRealtime(seconds);

        Destroy(actionUI);
    }
}
