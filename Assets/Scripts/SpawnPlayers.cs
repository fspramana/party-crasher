using UnityEngine;
using System.Collections;

public class SpawnPlayers : MonoBehaviour {

    GameObject[] playersSpawn;

    void Start() {
        playersSpawn = GameObject.FindGameObjectsWithTag("PlayerSpawnPoint");
    }

    // Update is called once per frame
    void Update() {

    }
}
