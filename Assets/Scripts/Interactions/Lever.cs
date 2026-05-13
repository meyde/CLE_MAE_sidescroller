using UnityEngine;

public class Lever : Interactable
{
    [SerializeField] private GameObject cage;
    public bool activated = false;

    public override void Interaction()
    {
        if (activated) return;
        cage.SetActive(false);
        activated = true;
        this.player.level1LeverActive = 1f;
    }
}
