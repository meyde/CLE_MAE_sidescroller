using UnityEngine;

public class DestroyingAnomaly : Interactable
{
    [SerializeField] GameObject borderToOpen;
    [SerializeField] GameObject borderToClose;
    public override void Interaction()
    {
        borderToOpen.SetActive(true);
        borderToClose.SetActive(false);
        gameObject.SetActive(false);
    }



}
