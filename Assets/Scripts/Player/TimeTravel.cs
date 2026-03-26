using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TimeTravel : MonoBehaviour
{
    private Vector3 _position;
    [SerializeField] private PlayerControl pc;
    private static TimeTravel _instance;

    void Awake()
    {
        this.Instantiatemanager();
    }

    private void Instantiatemanager()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != _instance)
        {
            Destroy(this.gameObject);
        }
    }
}
