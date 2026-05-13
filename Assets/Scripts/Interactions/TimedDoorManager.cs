using System.Collections;
using System.Threading;
using UnityEngine;

public class TimedDoorManager : MonoBehaviour
{
    public GameObject door;
    [SerializeField] private float openTime;
    [SerializeField] private PlayerControl player;
    [SerializeField] private TimeManager tm;
    public bool isOpen = false;
    public int currentTime;
    public void TemporaryOpening()
    {
        if (isOpen) return;
        StartCoroutine(DoorTimer(openTime));
    }

    public IEnumerator DoorTimer(float timeToWait)
    {

        isOpen = true;
        door.SetActive(false);
        while (currentTime < timeToWait)
        {
            if (tm.rewinding ||  tm.paused) { yield return new WaitForSeconds(1); }
            currentTime++;
            yield return new WaitForSeconds(1);
        }
        isOpen = false;
        door.SetActive(true);
        currentTime = 0;

    } 
}
