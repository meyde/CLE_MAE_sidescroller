
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class TimeManager : MonoBehaviour
{
    public float[][] backlog;
    [SerializeField] public PlayerControl player;
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
            Debug.Log("RewindStarted");
            timeRewinded = 0;
            rewinding = true;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            player.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            Debug.Log("Rewind Over");
            rewinding = false;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            player.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            charged -= timeRewinded;
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
        player.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
    }
    public virtual void RewindTime(int time)
    {
        //int pastIndex = (index - time-1);
        //if (pastIndex < 0) { pastIndex += logSize; }
        //float[] encodedPastPlayer = backlog[pastIndex];
        //if (encodedPastPlayer == null) return;
        //player.transform.position = new Vector3(encodedPastPlayer[0], encodedPastPlayer[1], player.transform.position.z);
        //player.temporality = Mathf.FloorToInt(encodedPastPlayer[2]);
        //player.OnChangedTimeline(Mathf.FloorToInt(encodedPastPlayer[2]));
        //if (encodedPastPlayer[3] != player.level1LeverActive)
        //{
        //    level1Lever.activated = false;
        //    level1Cage.SetActive(true);
        //    player.level1LeverActive = 0f;
        //}
        //if (encodedPastPlayer[4] != level2tdm.currentTime)
        //{
        //    if (encodedPastPlayer[4] == 0) 
        //    { 
        //        level2tdm.isOpen = false; level2tdm.door.SetActive(true); 
        //    }
        //    else 
        //    { 
        //        level2tdm.isOpen = true; level2tdm.door.SetActive(false); 
        //    }
        //    level2tdm.currentTime = Mathf.FloorToInt(encodedPastPlayer[4]);

        //}

    }
}
