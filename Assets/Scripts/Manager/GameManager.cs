using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static GameManager singleton;

    [HideInInspector]
    public GameObject[] playersSpawnPos;

    [HideInInspector]
    public GameObject[] itemsSpawnPos;

    [HideInInspector]
    public List<GameObject> spawns;

    public float itemSpawnDelayInSeconds = 120f;
    public int itemPerSpawn = 4;
    public GameObject[] items;
    public Transform spawnContainer;

    public List<GameObject> playerRemain;

    public GameEndShower GameEndShower;
    public GameMenuContainer GameMenuContainer;

    void Awake() {
        // iam the only one b*tch
        if ( singleton == null ) {
            singleton = this;
        } else {
            Destroy(gameObject);
        }

        spawns = new List<GameObject>();

        playersSpawnPos = GameObject.FindGameObjectsWithTag("PlayerSpawnPos");
        itemsSpawnPos = GameObject.FindGameObjectsWithTag("ItemsSpawnPos");

        InvokeRepeating("SpawnItem", itemSpawnDelayInSeconds, itemSpawnDelayInSeconds);

        playerRemain = new List<GameObject>();
    }

    void SpawnItem() {
        print("SPAWNING ITEM. . .");

        foreach (GameObject spawn in spawns) Destroy(spawn);
        spawns.Clear();

        List<int> avaiableSpawnPos = new List<int>();
        int count = 0;
        int length = itemsSpawnPos.Length;

        for ( int i = 0; i < length; i++ ) {
            avaiableSpawnPos.Add(count++);
        }

        for ( int i = 0; i < itemPerSpawn; i++ ) {
            GameObject instantiate = Instantiate(items[Random.Range(0, items.Length)], itemsSpawnPos[GetAvaiableSpawnIndex(ref avaiableSpawnPos)].transform.position, Quaternion.Euler(0f, 0f, 270f)) as GameObject;
            spawns.Add(instantiate);
            if (spawnContainer) instantiate.transform.SetParent(spawnContainer);
        }

    }

    int GetAvaiableSpawnIndex(ref List<int> spawnPosList) {
        int random = Random.Range(0, spawnPosList.Count);
        int spawnPosIndex = spawnPosList[random];
        spawnPosList.RemoveAt(random);
        return spawnPosIndex;
    }

    void Update()
    {
        if(playerRemain.Count == 1)
        {
            GameMenuContainer.ShowGameEndContainer();
            int whoWin = int.Parse(playerRemain[0].name.Split(' ')[1]);
            GameEndShower.PlayerWin(whoWin);
        }
    }
}
