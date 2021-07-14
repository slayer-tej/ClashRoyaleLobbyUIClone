using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ControllerChest : MonoBehaviour
{
    [SerializeField] private ChestType Type;
    [SerializeField] private int coins;
    [SerializeField] private int gems;
    [SerializeField] private int timetoOpen;
    [SerializeField] private Button _buttonChest;
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private GameObject _notEnoughGemsPanel;
    [SerializeField] private Button _buttonStartUnlocking;
    [SerializeField] private Button _buttonUnlockWithGems;
    [SerializeField] private Button _buttonClosePanel;
    [SerializeField] private Button _buttonClosePanel2;
    [SerializeField] private TextMeshProUGUI inputField;
    [SerializeField] private TextMeshProUGUI statusField;
    private int remainingTime;

    public bool islocked = true;
    public bool isUnlocking = false;
    public bool isCollected = false;


    private void Awake()
    {
        ServiceChestSystem.Instance.OnSelectionChanged += onTabSelectionChange;
    }

    private void Start()
    {
        remainingTime = timetoOpen;
        UpdateStatus();
        _buttonChest.onClick.AddListener(OnClick);
        _buttonStartUnlocking.onClick.AddListener(StartUnlock);
        _buttonUnlockWithGems.onClick.AddListener(UnlockWithGems);
        _buttonClosePanel.onClick.AddListener(ClosePanel);
        _buttonClosePanel2.onClick.AddListener(ClosePanel2);
    }

    private void UpdateStatus()
    {
        if (islocked && !isUnlocking)
        {
            statusField.text = "Locked";
        }
        else if (!islocked && !isUnlocking)
        {
            statusField.text = "Unlocked";
        }
        else if(!islocked && isCollected)
        {
            Debug.Log("Collected");
            statusField.text = "Collected";
        }
    }

    private void ClosePanel2()
    {
        _notEnoughGemsPanel.SetActive(false);
    }

    private void ClosePanel()
    {
        _optionsPanel.SetActive(false);
    }

    private void UnlockWithGems()
    {
        int gemsNeededToUnlock = (int)Mathf.Ceil( (float)remainingTime/ 600);
        Debug.Log(gemsNeededToUnlock);
        if(ServiceChestSystem.Instance.coins < gemsNeededToUnlock)
        {
            _notEnoughGemsPanel.SetActive(true);
        }
        else
        {
            ServiceChestSystem.Instance.coins -= gemsNeededToUnlock;
            _optionsPanel.SetActive(false);
            ServiceChestSystem.Instance.UpdateUI();
            Unlock();
        }
    }

    public void InitializeValues(Chest random)
    {
        Type = random.Type;
        coins = random.coins;
        gems = random.gems;
        timetoOpen = random.timetoOpen;
    }

    private void StartUnlock()
    {
        _optionsPanel.SetActive(false);
        isUnlocking = true;
        ServiceChestSystem.Instance.StartUnlocking(this);
        UpdateStatus();
        StartCoroutine(Countdown());
    }

    private void OnClick()
    {
        ServiceChestSystem.Instance.ChangeTab(GetInstanceID());
        if(islocked )
        {
            _optionsPanel.SetActive(true);

            if (isUnlocking)
            {
                _buttonStartUnlocking.interactable = false;
            }
        }
        else if (!islocked && !isCollected)
        {
            UpdateStatus();
            UnlockChest();
        }
    }

    private void UnlockChest()
    {
        ServiceChestSystem.Instance.coins += coins;
        ServiceChestSystem.Instance.gems += gems;
        ServiceChestSystem.Instance.UpdateUI();
        isCollected = true;
        ServiceChestSystem.Instance.EndUnlocking(this);
    }

    IEnumerator Countdown()
    {
        while (remainingTime > 0 && islocked)
        {
            inputField.text = remainingTime.ToString();
            yield return new WaitForSeconds(1);
            remainingTime--;
        }
        Unlock();
        UpdateStatus();
    }

    private void onTabSelectionChange(int instanceID)
    {
        if (instanceID == GetInstanceID())
        {
            if (islocked)
            {
                _optionsPanel.SetActive(true);
            }
        }
        else
        {
            _optionsPanel.SetActive(false);
        }
    }

    public void Hold()
    {
        _buttonChest.interactable = false;
    }
    internal void UnHold()
    {
        _buttonChest.interactable = true;
        StartUnlock();
    }

    public void ReleaseSlot()
    {
        ServiceChestSystem.Instance._numOfSlots++;
        Destroy(gameObject);
    }
    public void Unlock()
    {
        islocked = false;
        isUnlocking = false;
    }

    public void Open(bool status)
    {
        isCollected = status;
    }
}

