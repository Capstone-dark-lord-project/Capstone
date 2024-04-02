using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public GameObject CraftingUI;
    public GameObject TrashUI;
    public TMP_Text CraftingButtonText;
    public TMP_Text TrashButtonText;

    private bool craftingUIActive = false;
    private bool trashUIActive = false;
    private string closeText = "Close";
    private string CraftingText = "Crafting Table";
    private string trashText = "Trash";

    void Start()
    {
        Crafting_UpdateButtonTextAndUIState();
        Trash_UpdateButtonTextAndUIState();
    }

    public void Crafting_OnButtonClick()
    {
        craftingUIActive = !craftingUIActive;

        Crafting_UpdateButtonTextAndUIState();
    }

    private void Crafting_UpdateButtonTextAndUIState()
    {
        if (craftingUIActive)
        {
            CraftingButtonText.text = closeText;
            CraftingUI.SetActive(true);
        }
        else
        {
            CraftingButtonText.text = CraftingText;
            CraftingUI.SetActive(false);
        }
    }

    public void Trash_OnButtonClick()
    {
        trashUIActive = !trashUIActive;

        Trash_UpdateButtonTextAndUIState();
    }

    private void Trash_UpdateButtonTextAndUIState()
    {
        if (trashUIActive)
        {
            TrashButtonText.text = closeText;
            TrashUI.SetActive(true);
        }
        else
        {
            TrashButtonText.text = trashText;
            TrashUI.SetActive(false);
        }
    }
}
