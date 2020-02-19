using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card",menuName = "Card")]
public class Scriptable_Card : ScriptableObject
{
    public string name;
    public float range;
    public float attack;
    public float attackSpeed;
    public float life;

    public List<Fusion> fusionList;
}

[System.Serializable]
public class Fusion
{
    public Scriptable_Card materia;
    public Scriptable_Card result;
}