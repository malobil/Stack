using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_UIManager : MonoBehaviour
{
    public static Script_UIManager Instance { get; private set; }
    public Transform bottomUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetNewCard(Transform newCard)
    {
        newCard.SetParent(bottomUI);
    }
}
