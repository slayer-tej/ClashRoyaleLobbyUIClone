using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Tab
{
    None,
    Battle,
    Cards,
    Clan,
    Store,
    Tournament
}

public class ServiceLobby : MonoBehaviour
{
    public event Action<Tab> OnSelectionChanged;

    [SerializeField]
    private Tab selectedTab = Tab.None;

    private void Start()
    {
        ChangeTab(Tab.Battle);
    }

    public void ChangeTab(Tab tab)
    {
        if(tab != selectedTab)
        {
            Debug.Log("Changing Tab from " + selectedTab + " to " + tab);
            selectedTab = tab;
            OnSelectionChanged?.Invoke(selectedTab);
        }
    }
}
