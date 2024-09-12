using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using InControl;

public class PlayerJoinManager : MonoBehaviour
{
    public static readonly string PLAYER_1_ID = "Player1Id";
    public static readonly string PLAYER_2_ID = "Player2Id";
    public static readonly string PLAYER_3_ID = "Player3Id";
    public static readonly string PLAYER_4_ID = "Player4Id";

    public int CurrentJoinedPlayer;
    public bool CharacterSelection;

    public Button ButtonPlay;

    public Player[] PlayersAvailable;

    private int[] PlayerToControllerIndex;
    private List<int> ControllerIndexAlreadyJoin;

	void Start ()
	{
        CurrentJoinedPlayer = 0;
        PlayerToControllerIndex = new int[4];
        PlayerToControllerIndex[0] = -1;
        PlayerToControllerIndex[1] = -1;
        PlayerToControllerIndex[2] = -1;
        PlayerToControllerIndex[3] = -1;
        ControllerIndexAlreadyJoin = new List<int>();

        foreach (Player Player in PlayersAvailable)
        {
            Player.PlayerStandby();
        }
    }
	
	void Update ()
	{
        if(CharacterSelection)
        {
            int DevicesAvailable = InputManager.Devices.Count;
            for (int i = 0; i < DevicesAvailable; i++)
            {
                int CurrentDeviceIndex = i;
                InputDevice CurrentDevice = InputManager.Devices[CurrentDeviceIndex];
                if (CurrentDevice.Action1.WasReleased)
                {
                    if (!ControllerIndexAlreadyJoin.Contains(CurrentDeviceIndex))
                    {
                        PlayersAvailable[CurrentJoinedPlayer].PlayerJoined();
                        PlayerToControllerIndex[CurrentJoinedPlayer] = CurrentDeviceIndex;
                        ControllerIndexAlreadyJoin.Add(CurrentDeviceIndex);
                        CurrentJoinedPlayer++;
                    }
                }
            }

            if (CurrentJoinedPlayer > 1) ButtonPlay.interactable = true;
            else ButtonPlay.interactable = false;
        }
	}

    public void Reset()
    {
        CurrentJoinedPlayer = 0;
        PlayerToControllerIndex = new int[4];
        PlayerToControllerIndex[0] = -1;
        PlayerToControllerIndex[1] = -1;
        PlayerToControllerIndex[2] = -1;
        PlayerToControllerIndex[3] = -1;
        ControllerIndexAlreadyJoin = new List<int>();

        foreach(Player Player in PlayersAvailable)
        {
            Player.PlayerStandby();
        }
    }

    public void IsCharacterSelect(bool isCharacterSelect)
    {
        CharacterSelection = isCharacterSelect;
    }

    public void SavePlayerJoinAndStartGame()
    {
        if (PlayerToControllerIndex[0] != -1) PlayerPrefs.SetInt(PLAYER_1_ID, PlayerToControllerIndex[0]);
        if (PlayerToControllerIndex[1] != -1) PlayerPrefs.SetInt(PLAYER_2_ID, PlayerToControllerIndex[1]);
        if (PlayerToControllerIndex[2] != -1) PlayerPrefs.SetInt(PLAYER_3_ID, PlayerToControllerIndex[2]);
        if (PlayerToControllerIndex[3] != -1) PlayerPrefs.SetInt(PLAYER_4_ID, PlayerToControllerIndex[3]);
        PlayerPrefs.Save();
        StartGame();
    }

    private void StartGame()
    {
        SceneManager.LoadScene("MovementTest");
    }
}
