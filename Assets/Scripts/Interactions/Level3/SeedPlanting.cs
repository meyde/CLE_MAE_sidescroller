using UnityEngine;
using UnityEngine.UI;

public class SeedPlanting : Interactable
{
    [SerializeField] private Image inventorySpace;
    [SerializeField] private GameObject tree;
    [SerializeField] private GameObject pot;
    public override void Interaction()
    {
        if (player.level3State != 1f) return;
        player.level3State = 2f;
        inventorySpace.color = Color.white;
        tree.SetActive(true);
        pot.SetActive(false);
    }
}
