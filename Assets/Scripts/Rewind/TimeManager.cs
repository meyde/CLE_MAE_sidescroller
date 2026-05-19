
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class TimeManager : MonoBehaviour
{
    public float[][] backlog;
    public PlayerControl player;
    [SerializeField] private Image watch;
    [SerializeField] private Sprite[] watchCharges;
    public int index = 0;
    public int logSize = 12;
    private int charged = 0;

    public bool paused = false ;
    public bool rewinding = false ;
    private int timeRewinded;
    

    
    private void Awake()
    {
        backlog= new float[logSize][];
    }
    void Start()
    {
        StartCoroutine(Loging());
    }

    public virtual float[] EncodePlayer(PlayerControl toEncode)
    {
        //var encoded = new float[3] { toEncode.transform.position.x, toEncode.transform.position.y, toEncode.temporality}; //, toEncode.level1LeverActive, level2tdm.currentTime
        //return encoded;
        Debug.Log("Warning: using the parent function it should have been overriden");
        return new float[3]; // this should never happen
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
                charged = Mathf.Clamp(charged + 1, 0, logSize-1);
                watch.sprite = watchCharges[charged];
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
        if (vlue < 0) { timeRewinded = Mathf.Clamp(timeRewinded + 1, 0, charged); }
        if (vlue > 0) { timeRewinded = Mathf.Clamp(timeRewinded - 1, 0, charged); }
        RewindTime(timeRewinded);
    }   

    public void OnRewind()
    {
        if (paused) return;
        if (!rewinding)
        {
            Debug.Log("RewindStarted");
            timeRewinded = 0;
            rewinding = true;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
        else
        {
            Debug.Log("Rewind Over");
            rewinding = false;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            charged -= timeRewinded;
            watch.sprite = watchCharges[charged];
            index -= timeRewinded;
            if (index < 0) { index += logSize; }
        }
        
    }

    public void ForcedRewind()
    {
        Debug.Log("Rewind forcé");
        timeRewinded = 0;
        rewinding = true;
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
    public virtual void RewindTime(int time)
    {
    }
}
