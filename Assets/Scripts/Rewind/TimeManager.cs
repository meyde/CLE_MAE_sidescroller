
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public List<float[]>[] backlog=new List<float[]>[10];
    public List<GameObject> logables;
    private int index = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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


            var encodedList = new List<float[]>();
            foreach (var go in logables)
            { encodedList.Add(Encode(go)); }
            ;

            if (index < 10)
            {
                backlog[index] = encodedList;
                index++;
            }
            else
            {
                var log = backlog;
                Array.Copy(log, 1, backlog, 0, 9);
                backlog[9] = encodedList;
            }
            yield return new WaitForSeconds(1);
        }
    }
    

}
