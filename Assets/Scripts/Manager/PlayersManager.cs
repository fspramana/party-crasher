using UnityEngine;
using System.Collections.Generic;

public class PlayersManager : MonoBehaviour {
    public GameObject playerPrefab;

    public bool Player1Joined;
    public bool Player2Joined;
    public bool Player3Joined;
    public bool Player4Joined;

    public int Player1DeviceId = -1;
    public int Player2DeviceId = -1;
    public int Player3DeviceId = -1;
    public int Player4DeviceId = -1;

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

        SpawnPlayers();

        PlayerPrefs.Save();
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
            GameObject obj = Instantiate(playerPrefab, spawnPos, Quaternion.Euler(0, rotation.eulerAngles.y, 0)) as GameObject;
            obj.GetComponent<CapsuleController>().SetupControllerDevice(Player1DeviceId);
            obj.name = "Player1";
        }
        if ( Player2Joined ) {
            Vector3 spawnPos = GameManager.singleton.playersSpawnPos[GetAvaiableSpawnIndex(ref avaiableSpawnPos)].transform.position;

            // look to the center of screen (world pos (0,0,0))
            Quaternion rotation = Quaternion.LookRotation(new Vector3(0, 0, 0) - spawnPos);
            GameObject obj = Instantiate(playerPrefab, spawnPos, Quaternion.Euler(0, rotation.eulerAngles.y, 0)) as GameObject;
            obj.GetComponent<CapsuleController>().SetupControllerDevice(Player2DeviceId);
            obj.name = "Player2";
        }
        if ( Player3Joined ) {
            Vector3 spawnPos = GameManager.singleton.playersSpawnPos[GetAvaiableSpawnIndex(ref avaiableSpawnPos)].transform.position;

            // look to the center of screen (world pos (0,0,0))
            Quaternion rotation = Quaternion.LookRotation(new Vector3(0, 0, 0) - spawnPos);
            GameObject obj = Instantiate(playerPrefab, spawnPos, Quaternion.Euler(0, rotation.eulerAngles.y, 0)) as GameObject;
            obj.GetComponent<CapsuleController>().SetupControllerDevice(Player3DeviceId);
            obj.name = "Player3";

        }
        if ( Player4Joined ) {
            Vector3 spawnPos = GameManager.singleton.playersSpawnPos[GetAvaiableSpawnIndex(ref avaiableSpawnPos)].transform.position;

            // look to the center of screen (world pos (0,0,0))
            Quaternion rotation = Quaternion.LookRotation(new Vector3(0, 0, 0) - spawnPos);
            GameObject obj = Instantiate(playerPrefab, spawnPos, Quaternion.Euler(0, rotation.eulerAngles.y, 0)) as GameObject;
            obj.GetComponent<CapsuleController>().SetupControllerDevice(Player4DeviceId);
            obj.name = "Player4";

        }
    }

    int GetAvaiableSpawnIndex(ref List<int> spawnPosList) {
        int random = Random.Range(0, spawnPosList.Count);
        int spawnPosIndex = spawnPosList[random];
        spawnPosList.RemoveAt(random);
        print(spawnPosIndex);
        return spawnPosIndex;
    }
}
