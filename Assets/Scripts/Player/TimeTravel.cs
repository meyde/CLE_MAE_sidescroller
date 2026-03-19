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

    public void OnTimeTravel()
    {
        
        var temporality = (pc.temporality + 1) % 3;
        SceneManager.LoadScene(temporality);
        Instantiate(pc,transform.position, Quaternion.identity );
        pc.temporality = temporality;
    }
}
