using UnityEngine;

public class FinaleAnomalyKill : Interactable
{
    [SerializeField] private GameObject endText;
    public override void Interaction()
    {
        endText.SetActive(false);
    }
}
