using UnityEngine;

public class DestroyingAnomaly : Interactable
{
    [SerializeField] GameObject fissureToRemove;
    public override void Interaction()
    {
        fissureToRemove.SetActive(false);
        gameObject.SetActive(false);
    }



}
