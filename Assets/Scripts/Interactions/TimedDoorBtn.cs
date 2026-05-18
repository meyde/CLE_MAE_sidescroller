using UnityEngine;

public class TimedDoorBtn : Interactable
{
    [SerializeField] TimedDoorManager tdm;
    [SerializeField] Transform[] positions;



    public void ResetPosition()
    {
        transform.position = positions[1].position;
    }
    public void ActivatePosition()
    {
        transform.position = positions[0].position;
    }
    public override void Interaction()
    {
        if (tdm.isOpen) return; 
        ActivatePosition();
        tdm.TemporaryOpening();
    }
    
}
