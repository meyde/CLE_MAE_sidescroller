using UnityEngine;

public class FinalBtn : Interactable
{
    [SerializeField] private FinalDoorManager fdm;
    [SerializeField] private int id;
    public override void Interaction()
    {
        if (id == 0)
        {
            fdm.leftState = 1f;
            fdm.OnStateChange();
        }
        else
        {
            fdm.rightState = 1f;
            fdm.OnStateChange();
        }
    }
}
