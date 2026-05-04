using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private TimeManager tm;
    public void OnPause(bool pause)
    {
        tm.paused = !tm.paused;
    }
}
