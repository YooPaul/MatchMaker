using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;

public class HostGame : MonoBehaviour
{
    
    NetworkMatch networkMatch;
    private List<MatchInfoSnapshot> matchInfo;
    private string _matchName = "SeigMatch";
    private bool _matchDoesExists = false;
    private int index = 0;

    void Awake()
    {
        networkMatch = gameObject.AddComponent<NetworkMatch>();
        matchInfo = new List<MatchInfoSnapshot>();
    }

    private void Start()
    {
        networkMatch.ListMatches(0, 10, "", true, 0, 0, OnMatchList);
        if (!_matchDoesExists)
            networkMatch.CreateMatch(_matchName, 4, true, "", "", "", 0, 0, OnMatchCreate);
        networkMatch.ListMatches(0, 10, "", false, 0, 0, OnMatchList);
        

        //networkMatch.JoinMatch(matchInfo[index].networkId, "", "", "", 0, 0, OnMatchJoined);
    }

    private void Update()
    {
        
        if(!_matchDoesExists)
            networkMatch.ListMatches(0, 10, "", false, 0, 0, OnMatchList);

    }

    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {

        //this.matchInfo = matches.ConvertAll(match => new MatchInfoSnapshot());
        Debug.Log(matches.Count);
        foreach (MatchInfoSnapshot snapshot in matches)
        {
            Debug.Log(snapshot.name);
            matchInfo.Add(snapshot);
            if (snapshot.name == _matchName)
            {
                _matchDoesExists = true;
                index = matchInfo.Count - 1;
            }
        }
    }

    public void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        Debug.Log("Joined");
    }

    public void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        Debug.Log("Created");
    }

}