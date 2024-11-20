using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BotData")]
public class BotDataSO : ScriptableObject
{
    public List<BotData> botDataList = new List<BotData>();
}
