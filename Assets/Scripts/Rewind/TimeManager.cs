
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public List<float[]>[] backlog;
    public List<GameObject> logables;
    private int index = 0;
    private int logSize = 10;


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
        print(backlog[0][0][0]);
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
            var encodedList = new List<float[]>();
            foreach (var go in logables)
            { encodedList.Add(Encode(go)); }
            ;

            backlog[index % logSize] = encodedList;
            index++;
            yield return new WaitForSeconds(1);
        }
    }
    public void OnRewind()
    {
        var encodedList = backlog[(index + 5) % logSize];
        for (int i = 0; i < backlog.Length; i++)
        {
            var go = logables[i];
            var encoded = encodedList[i];
            go.transform.position = new Vector3(encoded[0], encoded[1], 0);
            go.GetComponent<StateManager>().activated = encoded[2];
        }
    }


}
