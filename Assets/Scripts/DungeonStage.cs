using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonStage : MonoBehaviour
{
    public int StageLevel { get; set; }

    // Use this for initialization
    void Start()
    {
        StageLevel = 0;
    }

    public void NextDungeonLevel(int amount)
    {
        StageLevel += amount;
        UIEventHandler.DungeonLevelChanged(StageLevel);
    }

    public int GetDungeonLevel()
    {
        return StageLevel;
    }
}
