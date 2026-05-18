using UnityEngine;
using UnityEngine.UI;

public class SeedPickup : Interactable
{
    [SerializeField] private Image inventorySpace;
    public override void Interaction()
    {
        if (player.level3State != 0f) return;
        player.level3State = 1f;
        inventorySpace.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
