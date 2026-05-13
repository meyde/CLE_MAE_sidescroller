using System;
using UnityEngine;
using UnityEngine.UI;

public class Level3TimeManager : TimeManager
{
    [SerializeField] private Image inventory;
    [SerializeField] private GameObject tree;
    [SerializeField] private GameObject pot;
    public override void RewindTime(int time)
    {
        int pastIndex = (index - time - 1);
        if (pastIndex < 0) { pastIndex += logSize; }
        float[] encodedPastPlayer = backlog[pastIndex];
        if (encodedPastPlayer == null) return;
        player.transform.position = new Vector3(encodedPastPlayer[0], encodedPastPlayer[1], player.transform.position.z);
        player.temporality = Mathf.FloorToInt(encodedPastPlayer[2]);
        player.OnChangedTimeline(Mathf.FloorToInt(encodedPastPlayer[2]));
        if (encodedPastPlayer[3] != player.level3State)
        {
            switch (encodedPastPlayer[3])
            {
                case 0:
                    inventory.color = Color.white;
                    tree.SetActive(false);
                    pot.SetActive(true);
                    break;
                case 1:
                    inventory.color = Color.red;
                    tree.SetActive(false);
                    pot.SetActive(true);
                    break;
                case 2:
                    inventory.color = Color.white;
                    tree.SetActive(true);
                    pot.SetActive(false);
                    break;
            }
            player.level3State = encodedPastPlayer[3];
        }
    }


    public override float[] EncodePlayer(PlayerControl toEncode)
    {
        var encoded = new float[4] { toEncode.transform.position.x, toEncode.transform.position.y, toEncode.temporality, toEncode.level3State };
        return encoded;
    }
}
