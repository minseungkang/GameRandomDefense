using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor.Experimental.UIElements;
using UnityEngine;
using Random = System.Random;

public class GachaManager : MonoBehaviour
{
private Dictionary<int, List<Dictionary<string, string>>> UnitDictByRank = new Dictionary<int, List<Dictionary<string, string>>>();

    private const string TowerInfoFileName = "TowerInfo";

    void Awake()
    {

        Dictionary<int, Dictionary<string, string>> tempDictIdForKey = new Dictionary<int, Dictionary<string, string>>();
        //Load Data from XML
        TextAsset elems = Resources.Load<TextAsset>(TowerInfoFileName);
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(elems.text);

        XmlNodeList children = xml.GetElementsByTagName("Tower");
        foreach (XmlNode row in children)
        {
            Dictionary<string, string> temp = new Dictionary<string, string>();
            foreach (var key in Enum.GetValues(typeof(TowerAttributes.Attributes)))
            {
                temp.Add(key.ToString(), row.Attributes[key.ToString()].Value);
            }

            ;
            tempDictIdForKey.Add(Convert.ToInt32(row.Attributes["UnitId"].Value), temp);
        }

        foreach (var elem in tempDictIdForKey.Values)
        {
            int rank = Convert.ToInt32(elem["Rank"]);
            if (!UnitDictByRank.ContainsKey(rank))
            {
                UnitDictByRank.Add(rank, new List<Dictionary<string, string>>());
            }
            UnitDictByRank[rank].Add(elem);
        }
        Debug.Log(UnitDictByRank);
    }

    public Dictionary<string, string> gacha(int rank)
    {
        List<Dictionary<string, string>> elems = UnitDictByRank[rank];

        if (elems == null)
        {
            return null;
        }

        Random random = new Random();
        return elems[random.Next(elems.Count)];
    }

}
