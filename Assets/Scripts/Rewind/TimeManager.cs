
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeManager : MonoBehaviour
{
    public float[][] backlog;
    [SerializeField] PlayerControl player;
    private int index = 0;
    private int logSize = 12;
    private int charged = 0;

    public bool paused = false ;
    public bool rewinding = false ;
    private int timeRewinded;

    static void timer(bool flag, float timeToWait)
    {
        var currentTime = 0f;
        while (currentTime < timeToWait)
        {
            currentTime += Time.deltaTime;
        }
        flag = true;
    }

    
    private void Awake()
    {
        backlog= new float[logSize][];
    }
    void Start()
    {
        StartCoroutine(Loging());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float[] EncodePlayer(PlayerControl toEncode)
    {
        var encoded = new float[3] { toEncode.transform.position.x, toEncode.transform.position.y, toEncode.temporality };
        return encoded;
    }

    public IEnumerator Loging()
    {
        while (true)
        {
            if (paused || rewinding) { yield return new WaitForSeconds(1); }
            else
            {
                float[] encodedPlayer = EncodePlayer(player);
                backlog[index] = encodedPlayer;
                index = (index + 1) % logSize;
                charged = Mathf.Clamp(charged + 1, 0, logSize);
                yield return new WaitForSeconds(1);
            }
        }
    }

    public void OnRewinding( InputValue value)
    {
        if (!rewinding) return;
        if (paused) return;
        if (charged < 1) return;
        float vlue = value.Get<float>();
        Debug.Log(charged.ToString());
        if (vlue < 0) { timeRewinded = Mathf.Clamp(timeRewinded + 1, 0, charged); }
        if (vlue > 0) { timeRewinded = Mathf.Clamp(timeRewinded - 1, 0, charged); }
        Debug.Log(timeRewinded);
        RewindTime(timeRewinded);
    }   

    public void OnRewind()
    {
        if (paused) return;
        if (!rewinding)
        {
            timeRewinded = 0;
            rewinding = true;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
        else
        {
            rewinding = false;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            charged -= timeRewinded;
            index = (index-timeRewinded) % logSize;
        }
        
    }

    public void ForcedRewind()
    {
        OnRewind();
    }
    public void RewindTime(int time)
    {
        float[] encodedPastPlayer = backlog[(index + logSize-time) % logSize];
        if (encodedPastPlayer == null) return;
        player.transform.position = new Vector3(encodedPastPlayer[0], encodedPastPlayer[1], player.transform.position.z);
        player.temporality = Mathf.FloorToInt(encodedPastPlayer[2]);
        player.OnChangedTimeline(Mathf.FloorToInt(encodedPastPlayer[2]));

    }
}
