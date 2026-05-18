using System;
using UnityEngine;

public class FinaleTimeManager : TimeManager
{
    [SerializeField] private FinalDoorManager fdm;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject[] levers;
    public override void RewindTime(int time)
    {
        int pastIndex = (index - time - 1);
        if (pastIndex < 0) { pastIndex += logSize; }
        float[] encodedPastPlayer = backlog[pastIndex];
        if (encodedPastPlayer == null) return;
        player.transform.position = new Vector3(encodedPastPlayer[0], encodedPastPlayer[1], player.transform.position.z);
        player.temporality = Mathf.FloorToInt(encodedPastPlayer[2]);
        player.OnChangedTimeline(Mathf.FloorToInt(encodedPastPlayer[2]));
        if (encodedPastPlayer[3] != fdm.leftState)
        {
            door.SetActive(true);
            encodedPastPlayer[3] = 0f;
            levers[0].GetComponent<SpriteRenderer>().flipX = false;

        }
        if (encodedPastPlayer[4] != fdm.leftState)
        {
            door.SetActive(true);
            encodedPastPlayer[4] = 0f;
            levers[0].GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public override float[] EncodePlayer(PlayerControl toEncode)
    {
        var encoded = new float[5] { toEncode.transform.position.x, toEncode.transform.position.y, toEncode.temporality, fdm.leftState, fdm.rightState };
        return encoded;
    }
}
