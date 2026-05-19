using System;
using UnityEngine;

public class Level1TimeManager : TimeManager
{
    [SerializeField] private Lever level1Lever;
    [SerializeField] private GameObject level1Cage;
    public override void RewindTime(int time)
    {
        int pastIndex = (index - time - 1);
        if (pastIndex < 0) { pastIndex += logSize; }
        float[] encodedPastPlayer = backlog[pastIndex];
        if (encodedPastPlayer == null) return;
        player.transform.position = new Vector3(encodedPastPlayer[0], encodedPastPlayer[1], player.transform.position.z);
        player.temporality = Mathf.FloorToInt(encodedPastPlayer[2]);
        player.OnChangedTimeline(Mathf.FloorToInt(encodedPastPlayer[2]));
        if (encodedPastPlayer[3] != player.level1LeverActive)
        {
            level1Lever.activated = false;
            level1Lever.GetComponent<SpriteRenderer>().flipX = true;
            level1Cage.SetActive(true);
            player.level1LeverActive = 0f;
        }
    }

    public override float[] EncodePlayer(PlayerControl toEncode)
    {
        var encoded = new float[4] { toEncode.transform.position.x, toEncode.transform.position.y, toEncode.temporality, toEncode.level1LeverActive }; 
        return encoded;
    }
}
