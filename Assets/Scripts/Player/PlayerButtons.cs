using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerButtons : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] PlayerData data;

    public static Action<int> OnNumberPressed;

    public void QSave(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        SaveSystem.Save(data);
    }

    public void QLoad(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        Destroy(gameObject);
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void QuestList(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if (data.viewingDialogue || data.viewingInventory) return;
        if (QuestWindow.instance.gameObject.activeInHierarchy)
        {
            QuestWindow.instance.gameObject.SetActive(false);
            data.viewingQuestList = false;
        } else
        {
            QuestWindow.instance.ViewQuests(data.questsCache);
            data.viewingQuestList = true;
            NotEnoughSpaceText.instance.Hide();
        }
    }

    public void Inventory(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if (data.viewingDialogue || data.viewingQuestList) return;
        if (data.viewingInventory)
        {
            data.viewingInventory = false;
            InventoryWindow.instance.gameObject.SetActive(false);
        } else
        {
            data.viewingInventory = true;
            InventoryWindow.instance.View(data.GetMoney(), data.GetMaxWeight());
            NotEnoughSpaceText.instance.Hide();
        }
    }

    public void CurrentQuest(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (data.viewingDialogue || data.viewingInventory || data.viewingQuestList) return;
            CurrentQuestUI.instance.ViewQuest(data.GetCurrentQuest());
            NotEnoughSpaceText.instance.Hide();
        } else if (ctx.canceled)
        {
            CurrentQuestUI.instance.gameObject.SetActive(false);
        }
    }

    public void Interact(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if (data.viewingDialogue || data.viewingInventory || data.viewingQuestList) return;
        if (!Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, 3)) return;
        if (hit.collider.gameObject.TryGetComponent(out Entity entity)) entity.Interact(data);
        if (hit.collider.gameObject.TryGetComponent(out Collectable collectable)) collectable.Collect();
    }

    public void Escape(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if (data.viewingDialogue)
        {
            if (DialogueWindow.instance.dialogue.escapable)
            {
                DialogueWindow.instance.CloseDialogue();
            }
        }
        if (data.viewingInventory)
        {
            InventoryWindow.instance.gameObject.SetActive(false);
            data.viewingInventory = false;
        }
        if (data.viewingQuestList)
        {
            data.viewingQuestList = false;
            QuestWindow.instance.gameObject.SetActive(false);
        }
    }

    public void Number(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        int num = int.Parse(ctx.control.displayName);
        OnNumberPressed?.Invoke(num);
    }

}
