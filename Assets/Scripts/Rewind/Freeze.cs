using UnityEngine;

public class Freeze : MonoBehaviour
{
    [SerializeField] private TimeManager tm;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerControl>() != null)
        {
            tm.ForcedRewind();
        }
    }
}
