using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ServiceChestSystem: MonoSingletonGeneric<ServiceChestSystem>
{
    public event Action<int> OnSelectionChanged;

    public int _numOfSlots = 4;
    [SerializeField] private Button _generatorChestButton;
    [SerializeField] private GameObject _chestPrefab;
    [SerializeField] private TextMeshProUGUI textFieldCoins;
    [SerializeField] private TextMeshProUGUI textFieldGems;
    [SerializeField] private List<ControllerChest> chestsList = new List<ControllerChest>();

    public int coins;
    public int gems;

    void Start()
    {
        _generatorChestButton.onClick.AddListener(GenerateRandomChest);
        UpdateUI();
    }

    private void GenerateRandomChest()
    {
       if(_numOfSlots > 0)
        {
            int RandomChest = UnityEngine.Random.Range(0, 4);
            LoadChest(RandomChest);
            _numOfSlots--;
        }
    }

    internal void StartUnlocking(ControllerChest controller)
    {
        foreach(ControllerChest chest in chestsList)
        {
            if(chest.GetInstanceID() != controller.GetInstanceID())
            {
                chest.Hold();
            }
        }
    }

    internal void EndUnlocking(ControllerChest controller)
    {
        for(int i = 0; i < chestsList.Count; i++)
        {
            if(chestsList[i].GetInstanceID() == controller.GetInstanceID())
            {
                chestsList[i].ReleaseSlot();
                chestsList[i + 1].UnHold();
            }
        }
        //foreach(ControllerChest chest in chestsList)
        //{
        //    if(chest.GetInstanceID() == controller.GetInstanceID())
        //    {
        //        chest.Hold();
        //    }
        //    else
        //    {
        //        chest.UnHold();
        //    }
        //}
    }

    public void UpdateUI()
    {
        textFieldCoins.text = coins.ToString();
        textFieldGems.text = gems.ToString();
    }

    private void LoadChest(int randomChest)
    {
        Chest Randomchest = new Chest(randomChest);
        Debug.Log("ChestType: " + Randomchest.Type + " Coins: " + Randomchest.coins + " Gems: " + Randomchest.gems);
        GameObject Loot =  Instantiate(_chestPrefab,gameObject.transform);
        ControllerChest controllerChest = Loot.GetComponent<ControllerChest>();
        controllerChest.InitializeValues(Randomchest);
        chestsList.Add(controllerChest);
    }

    internal void ChangeTab(int instanceID)
    {
        OnSelectionChanged.Invoke(instanceID);
    }
}

public struct Chest
{
    public ChestType Type;
    public int coins;
    public int gems;
    public int timetoOpen;

    public Chest(int Randomnumber)
    {
        Type = (ChestType)Randomnumber;
        coins = 0;
        gems = 0;
        timetoOpen = 0;
        switch (Type)
        {
            case ChestType.Common:
                coins = UnityEngine.Random.Range(100, 201);
                gems = UnityEngine.Random.Range(10, 21);
                timetoOpen = 900;
                break;
            case ChestType.Rare:
                coins = UnityEngine.Random.Range(300, 501);
                gems = UnityEngine.Random.Range(20, 41);
                timetoOpen = 1800;
                break;
            case ChestType.Epic:
                coins = UnityEngine.Random.Range(600, 801);
                gems = UnityEngine.Random.Range(45, 61);
                timetoOpen = 3600;
                break;
            case ChestType.Legendary:
                coins = UnityEngine.Random.Range(1000, 1201);
                gems = UnityEngine.Random.Range(80, 100);
                timetoOpen = 10800;
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
