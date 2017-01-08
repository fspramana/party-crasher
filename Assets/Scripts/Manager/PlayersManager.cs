using UnityEngine;
using System.Collections;

public class PlayersManager : MonoBehaviour
{

    public bool Player1Joined;
    public bool Player2Joined;
    public bool Player3Joined;
    public bool Player4Joined;

    public int Player1DeviceId = -1;
    public int Player2DeviceId = -1;
    public int Player3DeviceId = -1;
    public int Player4DeviceId = -1;

	void Start ()
	{
        Player1Joined = PlayerPrefs.HasKey(PlayerJoinManager.PLAYER_1_ID);
        Player2Joined = PlayerPrefs.HasKey(PlayerJoinManager.PLAYER_2_ID);
        Player3Joined = PlayerPrefs.HasKey(PlayerJoinManager.PLAYER_3_ID);
        Player4Joined = PlayerPrefs.HasKey(PlayerJoinManager.PLAYER_4_ID);

        if (Player1Joined)
        {
            Player1DeviceId = PlayerPrefs.GetInt(PlayerJoinManager.PLAYER_1_ID);
            PlayerPrefs.DeleteKey(PlayerJoinManager.PLAYER_1_ID);
        }

        if (Player2Joined)
        {
            Player2DeviceId = PlayerPrefs.GetInt(PlayerJoinManager.PLAYER_2_ID);
            PlayerPrefs.DeleteKey(PlayerJoinManager.PLAYER_2_ID);
        }

        if (Player3Joined)
        {
            Player3DeviceId = PlayerPrefs.GetInt(PlayerJoinManager.PLAYER_3_ID);
            PlayerPrefs.DeleteKey(PlayerJoinManager.PLAYER_3_ID);
        }

        if (Player4Joined)
        {
            Player4DeviceId = PlayerPrefs.GetInt(PlayerJoinManager.PLAYER_4_ID);
            PlayerPrefs.DeleteKey(PlayerJoinManager.PLAYER_4_ID);
        }

        PlayerPrefs.Save();
    }
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
