using UnityEngine;

public class Interactable : MonoBehaviour
{
    public PlayerControl player;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.interactingWith = this;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (player.interactingWith == this)
        {
            player.interactingWith = null;
        }
    }
    public virtual void  Interaction()
    {
    }


}
