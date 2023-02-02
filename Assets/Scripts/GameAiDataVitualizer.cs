using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAiDataVitualizer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int monsterKillCount;
    [SerializeField] private int deadCount;
    [SerializeField] private int getHitCount;
    [SerializeField] private Vector2 playerDeadPosition;

    public GameObject[] monsterPrefab;


    void Start()
    {
        GameAi.monsterPrefab = monsterPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        monsterKillCount = GameAi.monsterKillCount;
        deadCount = GameAi.deadCount;
        getHitCount = GameAi.getHitCount;
        playerDeadPosition = GameAi.playerDeadPostion;

    }
}
