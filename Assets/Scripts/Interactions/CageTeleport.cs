using UnityEngine;

public class CageTeleport : MonoBehaviour
{
    [SerializeField] public Transform tpPosition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerControl>() != null)
        {
            collision.gameObject.transform.position = tpPosition.position;
        }
    }
}
