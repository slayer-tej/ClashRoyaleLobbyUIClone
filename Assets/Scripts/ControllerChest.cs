using System;
using UnityEngine;
using UnityEngine.UI;

public class ControllerChest : MonoBehaviour
{
    public bool unlocked = false;
    public bool isOpened = false;
    [SerializeField]
    private Button _chestButton;

    private void Awake()
    {
        _chestButton = GetComponent<Button>();
        _chestButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
    }

    public void Unlock()
    {
        unlocked = true;
    }
    public void Open(bool status)
    {
        isOpened = status;
    }
}

