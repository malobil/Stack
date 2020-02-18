using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Script_DataManager : MonoBehaviour
{
    public static Script_DataManager Instance { get; private set; }
    public TextAsset jsonFile;
    public CardList cardData;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        ImportCard();
    }

    void ImportCard()
    {
        string text = jsonFile.text;
        cardData = JsonUtility.FromJson<CardList>(text);
    }

   public Card GetCard(string cardName)
    {
        foreach(Card cards in cardData.Card)
        {
            if(cardName == cards.Name)
            {
                return cards;
            }
        }

        return null;
    }
}

[Serializable]
public class CardList
{
    public List<Card> Card;
}

[Serializable]
public class Card
{
    public string Name;
    public float Range;
    public int Attack;
    public float AttackSpeed;
    public int Life;
    public List<Fusions> Fusions;
}

[Serializable]
public class Fusions
{
    public string Result;
    public string Materia;
}


