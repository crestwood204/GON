﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyCastleManager : MonoBehaviour
{

    private GameObject DataObj;
    private GameObject outcomePanelObj;

    private Data data;
    private GUIController guiController;

    // player stats
    private int curPlayer;
    private int soldiersPlayer;
    private int wealthPlayer;

    // castle stats
    private Dictionary<string, string> curCastle;
    private Text soldiersCastleText;
    private Text wealthCastleText;
    private Text generalCastleText;
    private Text nameCastleText;
    private Text tollCastleText;
    private Text nameHouseCastleText;
    
    void Start()
    {
        DataObj = GameObject.Find("GUI Controller");
        data = DataObj.GetComponent<Data>();
        guiController = DataObj.GetComponent<GUIController>();

        curPlayer = guiController.getPlayerNum();
        soldiersPlayer = int.Parse(data.getPlayerAttribute(curPlayer, "soldiers"));
        wealthPlayer = int.Parse(data.getPlayerAttribute(curPlayer, "wealth"));

        curCastle = data.getEvent(guiController.getCurUnit().PathLocation);

        // start castle name
        nameCastleText = GameObject.Find("nameCastle Text").GetComponent<Text>();
        nameCastleText.text = curCastle["name"];

        // start info1 text
        nameHouseCastleText = GameObject.Find("nameHouseCastle Text").GetComponent<Text>();
        nameHouseCastleText.text = "House " + curCastle["house"];
        generalCastleText = GameObject.Find("nameGeneralCastle Text").GetComponent<Text>();
        generalCastleText.text = "Guarded by " + curCastle["general"];

        // start info2 text 
        soldiersCastleText = GameObject.Find("soldiersCastle Text").GetComponent<Text>();
        soldiersCastleText.text = "Soldiers: " + curCastle["general"];
        wealthCastleText = GameObject.Find("wealthCastle Text").GetComponent<Text>();
        wealthCastleText.text = "Wealth: " + curCastle["wealth"];
        tollCastleText = GameObject.Find("tollCastle Text").GetComponent<Text>();
        tollCastleText.text = "Toll: " + getToll(float.Parse(curCastle["wealth"])).ToString();

        // start outcome panel
        outcomePanelObj = GameObject.Find("Canvas").transform.GetChild(2).gameObject;

    }


    void Update()
    {

    }
    

    public int getToll(float castleWealth)
    {
        return (int)Math.Ceiling(Math.Max(100, castleWealth * 0.2));
    }
    
    public void onPayToll()
    {
        data.setPlayerNumericAttribute(curPlayer, "wealth", 0 - getToll(float.Parse(curCastle["wealth"])));
        data.setPlayerNumericAttribute(data.getPlayerByHouse(curCastle["house"]), "wealth", getToll(float.Parse(curCastle["wealth"])));

        outcomePanelObj.SetActive(true);
        string tollStr = "You paid " + getToll(float.Parse(curCastle["wealth"])).ToString() + "to House " + curCastle["house"];
        outcomePanelObj.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = tollStr;

        //guiController.EndTurn();
        //SceneManager.LoadScene(1);
    }
}