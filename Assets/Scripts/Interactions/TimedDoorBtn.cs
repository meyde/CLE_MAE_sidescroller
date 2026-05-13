using UnityEngine;

public class TimedDoorBtn : Interactable
{
    [SerializeField] TimedDoorManager tdm;


    public override void Interaction()
    {
        Debug.Log("TimedDoorBtn Pressed");
        tdm.TemporaryOpening();
    }
    
}
