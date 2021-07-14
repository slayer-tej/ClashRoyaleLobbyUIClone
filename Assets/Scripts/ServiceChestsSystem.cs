using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServiceChestsSystem: MonoBehaviour
{
    [SerializeField] private Button _generatorChestButton;
    private int _numOfSlots = 4;
    [SerializeField]
    private GameObject _chestPrefab;
    // Start is called before the first frame update
    void Start()
    {
        _generatorChestButton.onClick.AddListener(GenerateRandomChest);
    }

    private void GenerateRandomChest()
    {
       while(_numOfSlots > 0)
        {
            int RandomChest = UnityEngine.Random.Range(0, 4);
            LoadChest(RandomChest);
            _numOfSlots--;
        }
    }

    private void LoadChest(int randomChest)
    {
        Instantiate(_chestPrefab,gameObject.transform);
        Chest chest = new Chest(randomChest);
        Debug.Log("ChestType: " + chest.Type + " Coins: " + chest.coins + " Gems: "+chest.gems);
    }
}

public struct Chest
{
    public ChestType Type;
    public int coins;
    public int gems;

    public Chest(int Randomnumber)
    {
        Type = (ChestType)Randomnumber;
        coins = 0;
        gems = 0;
        switch (Type)
        {
            case ChestType.Common:
                coins = UnityEngine.Random.Range(100, 201);
                gems = UnityEngine.Random.Range(10, 21);
                break;
            case ChestType.Rare:
                coins = UnityEngine.Random.Range(300, 501);
                gems = UnityEngine.Random.Range(20, 41);
                break;
            case ChestType.Epic:
                coins = UnityEngine.Random.Range(600, 801);
                gems = UnityEngine.Random.Range(45, 61);
                break;
            case ChestType.Legendary:
                coins = UnityEngine.Random.Range(1000, 1201);
                gems = UnityEngine.Random.Range(80, 100);
                break;
        }
    }
}

public enum ChestType
{
    Common,
    Rare,
    Epic,
    Legendary
}
