using UnityEngine;

public class FinalDoorManager : MonoBehaviour
{
    public float leftState = 0f; //0f: not pushed, 1f: pushed 

    public float rightState = 0f;
    [SerializeField] private GameObject door;

    public void OnStateChange()
    {
        if (leftState == rightState && leftState == 1f)
        {
            door.SetActive(false);
        }
    }
}
