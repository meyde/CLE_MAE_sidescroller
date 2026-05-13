using UnityEditor.AssetImporters;
using UnityEngine;

public class Level2TimeManager : TimeManager
{
    [SerializeField] private TimedDoorManager tmd;
    public override void RewindTime(int time)
    {
        int pastIndex = (index - time - 1);
        if (pastIndex < 0) { pastIndex += logSize; }
        float[] encodedPastPlayer = backlog[pastIndex];
        if (encodedPastPlayer == null) return;
        player.transform.position = new Vector3(encodedPastPlayer[0], encodedPastPlayer[1], player.transform.position.z);
        player.temporality = Mathf.FloorToInt(encodedPastPlayer[2]);
        player.OnChangedTimeline(Mathf.FloorToInt(encodedPastPlayer[2]));
        if (encodedPastPlayer[3] != tmd.currentTime)
        {
            if (encodedPastPlayer[3] == 0)
            {
                tmd.isOpen = false; tmd.door.SetActive(true);
            }
            else
            {
                tmd.isOpen = true; tmd.door.SetActive(false);
            }
            tmd.currentTime = Mathf.FloorToInt(encodedPastPlayer[3]);
        }
    }


    public override float[] EncodePlayer(PlayerControl toEncode)
    {
        var encoded = new float[4] { toEncode.transform.position.x, toEncode.transform.position.y, toEncode.temporality, tmd.currentTime };
        return encoded;
    }
}
