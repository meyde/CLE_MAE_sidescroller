
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeManager : MonoBehaviour
{
    public List<float[]>[] backlog;
    public List<GameObject> logables;
    private int index = 0;
    private int logSize = 12;
    private int charged = 0;

    public bool paused = false ;
    public bool rewinding = false ;
    private int timeRewinded;

    static void timer(int timeToWait)
    {
        var currentTime = 0f;
        while (currentTime < timeToWait)
        {
            currentTime += Time.deltaTime;
        }
    }

    private void Awake()
    {
        backlog= new List<float[]>[logSize];
    }
    void Start()
    {
        StartCoroutine(Loging());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float[] Encode(GameObject toEncode)
    {
        var encoded = new float[3] { toEncode.transform.position.x, toEncode.transform.position.y, toEncode.GetComponent<StateManager>().activated };
        return encoded;
    }

    public IEnumerator Loging()
    {
        while (true)
        {
            if (paused || rewinding) { yield return new WaitForSeconds(1); }
            else
            {
                var encodedList = new List<float[]>();
                foreach (var go in logables)
                { encodedList.Add(Encode(go)); }
                backlog[index] = encodedList;
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
            foreach (var go in logables)
            {
                Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.bodyType = RigidbodyType2D.Static;
                }
            }
        }
        else
        {
            rewinding = false;
            foreach (var go in logables)
            {
                Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.bodyType = RigidbodyType2D.Dynamic;
                }
            }
            charged -= timeRewinded;
            index -= timeRewinded;
        }
        
    }

    public void ForcedRewind()
    {
        OnRewind();
    }
    public void RewindTime(int time)
    {
        var encodedList = backlog[(index + logSize-time) % logSize];
        if (encodedList == null) return;
        for (int i = 0; i < logables.Count; i++)
        {
            var go = logables[i];
            var encoded = encodedList[i];
            go.transform.position = new Vector3(encoded[0], encoded[1], 0);
            go.GetComponent<StateManager>().activated = encoded[2];
        }
    }
}
