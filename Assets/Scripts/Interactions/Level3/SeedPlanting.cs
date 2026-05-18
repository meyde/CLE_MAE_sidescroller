using UnityEngine;
using UnityEngine.UI;

public class SeedPlanting : Interactable
{
    [SerializeField] private Image inventorySpace;
    [SerializeField] private GameObject tree;
    [SerializeField] private GameObject pot;
    [SerializeField] private Sprite potPlanted;
    public override void Interaction()
    {
        if (player.level3State != 1f) return;
        player.level3State = 2f;
        inventorySpace.gameObject.SetActive(false);
        tree.SetActive(true);
        pot.GetComponent<SpriteRenderer>().sprite = potPlanted;
    }
}
