using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public BotDataSO botDataSO;
    public List<BotData> listBotData;

    private void Awake()
    {
        listBotData = botDataSO.botDataList;
    }

    public BotData GetBotData(BotType botType)
    {
        //List<BotData> bots = listBotData;
        //for (int i = 0; i < bots.Count; i++)
        //{
        //    if (botType == bots[i].botType)
        //    {
        //        return bots[i];
        //    }
        //}

        return listBotData[(int)botType];

        //return null;
    }
}
