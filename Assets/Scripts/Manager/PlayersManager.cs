using UnityEngine;
using System.Collections.Generic;

public class PlayersManager : MonoBehaviour {
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    public GameObject player3Prefab;
    public GameObject player4Prefab;

    public bool Player1Joined;
    public bool Player2Joined;
    public bool Player3Joined;
    public bool Player4Joined;

    public int Player1DeviceId = -1;
    public int Player2DeviceId = -1;
    public int Player3DeviceId = -1;
    public int Player4DeviceId = -1;

    public GameObject player1Object;
    public GameObject player2Object;
    public GameObject player3Object;
    public GameObject player4Object;

    public PlayerUI Player1UI;
    public PlayerUI Player2UI;
    public PlayerUI Player3UI;
    public PlayerUI Player4UI;

    void Awake() {
        Player1Joined = PlayerPrefs.HasKey(PlayerJoinManager.PLAYER_1_ID);
        Player2Joined = PlayerPrefs.HasKey(PlayerJoinManager.PLAYER_2_ID);
        Player3Joined = PlayerPrefs.HasKey(PlayerJoinManager.PLAYER_3_ID);
        Player4Joined = PlayerPrefs.HasKey(PlayerJoinManager.PLAYER_4_ID);

        #region playerCheck
        if ( Player1Joined ) {
            Player1DeviceId = PlayerPrefs.GetInt(PlayerJoinManager.PLAYER_1_ID);
            PlayerPrefs.DeleteKey(PlayerJoinManager.PLAYER_1_ID);
        }

        if ( Player2Joined ) {
            Player2DeviceId = PlayerPrefs.GetInt(PlayerJoinManager.PLAYER_2_ID);
            PlayerPrefs.DeleteKey(PlayerJoinManager.PLAYER_2_ID);
        }

        if ( Player3Joined ) {
            Player3DeviceId = PlayerPrefs.GetInt(PlayerJoinManager.PLAYER_3_ID);
            PlayerPrefs.DeleteKey(PlayerJoinManager.PLAYER_3_ID);
        }

        if ( Player4Joined ) {
            Player4DeviceId = PlayerPrefs.GetInt(PlayerJoinManager.PLAYER_4_ID);
            PlayerPrefs.DeleteKey(PlayerJoinManager.PLAYER_4_ID);
        }
        #endregion

        PlayerPrefs.Save();

        SpawnPlayers();
        SetupUI();
    }

    void SpawnPlayers() {
        List<int> avaiableSpawnPos = new List<int>();
        int count = 0;
        int length = GameManager.singleton.playersSpawnPos.Length;

        for ( int i = 0; i < length; i++ ) {
            avaiableSpawnPos.Add(count++);
        }

        if ( Player1Joined ) {
            Vector3 spawnPos = GameManager.singleton.playersSpawnPos[GetAvaiableSpawnIndex(ref avaiableSpawnPos)].transform.position;

            // look to the center of screen (world pos (0,0,0))
            Quaternion rotation = Quaternion.LookRotation(new Vector3(0, 0, 0) - spawnPos);
            player1Object = Instantiate(player1Prefab, spawnPos, Quaternion.Euler(0, rotation.eulerAngles.y, 0)) as GameObject;
            player1Object.GetComponent<CapsuleController>().SetupControllerDevice(Player1DeviceId);
            player1Object.name = "Player 1";

            GameManager.singleton.playerRemain.Add(player1Object);
        }
        if ( Player2Joined ) {
            Vector3 spawnPos = GameManager.singleton.playersSpawnPos[GetAvaiableSpawnIndex(ref avaiableSpawnPos)].transform.position;

            // look to the center of screen (world pos (0,0,0))
            Quaternion rotation = Quaternion.LookRotation(new Vector3(0, 0, 0) - spawnPos);
            player2Object = Instantiate(player2Prefab, spawnPos, Quaternion.Euler(0, rotation.eulerAngles.y, 0)) as GameObject;
            player2Object.GetComponent<CapsuleController>().SetupControllerDevice(Player2DeviceId);
            player2Object.name = "Player 2";

            GameManager.singleton.playerRemain.Add(player2Object);
        }
        if ( Player3Joined ) {
            Vector3 spawnPos = GameManager.singleton.playersSpawnPos[GetAvaiableSpawnIndex(ref avaiableSpawnPos)].transform.position;

            // look to the center of screen (world pos (0,0,0))
            Quaternion rotation = Quaternion.LookRotation(new Vector3(0, 0, 0) - spawnPos);
            player3Object = Instantiate(player3Prefab, spawnPos, Quaternion.Euler(0, rotation.eulerAngles.y, 0)) as GameObject;
            player3Object.GetComponent<CapsuleController>().SetupControllerDevice(Player3DeviceId);
            player3Object.name = "Player 3";

            GameManager.singleton.playerRemain.Add(player3Object);

        }
        if ( Player4Joined ) {
            Vector3 spawnPos = GameManager.singleton.playersSpawnPos[GetAvaiableSpawnIndex(ref avaiableSpawnPos)].transform.position;

            // look to the center of screen (world pos (0,0,0))
            Quaternion rotation = Quaternion.LookRotation(new Vector3(0, 0, 0) - spawnPos);
            player4Object = Instantiate(player4Prefab, spawnPos, Quaternion.Euler(0, rotation.eulerAngles.y, 0)) as GameObject;
            player4Object.GetComponent<CapsuleController>().SetupControllerDevice(Player4DeviceId);
            player4Object.name = "Player 4";

            GameManager.singleton.playerRemain.Add(player4Object);

        }
    }

    private void SetupUI()
    {
        Player1UI.SetIsVisible(Player1Joined);
        Player2UI.SetIsVisible(Player2Joined);
        Player3UI.SetIsVisible(Player3Joined);
        Player4UI.SetIsVisible(Player4Joined);

        if (Player1Joined) Player1UI.SetCharacter(player1Object);
        if (Player2Joined) Player2UI.SetCharacter(player2Object);
        if (Player3Joined) Player3UI.SetCharacter(player3Object);
        if (Player4Joined) Player4UI.SetCharacter(player4Object);
    }

    int GetAvaiableSpawnIndex(ref List<int> spawnPosList) {
        int random = Random.Range(0, spawnPosList.Count);
        int spawnPosIndex = spawnPosList[random];
        spawnPosList.RemoveAt(random);
        print(spawnPosIndex);
        return spawnPosIndex;
    }
}
