using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(LayoutElement))]
public class ControllerLobbyTabButton : MonoBehaviour
{
    [SerializeField]
    private Tab tab;
    [SerializeField]
    private ServiceLobby serviceLobby;

    private Button buttonTab;
    private bool isSelected = false;
    private LayoutElement layoutElement;
    private RectTransform rectTransform;


    private void Awake()
    {
        buttonTab = GetComponent<Button>();
        layoutElement = GetComponent<LayoutElement>();
        rectTransform = GetComponent<RectTransform>();

        buttonTab.onClick.AddListener(OnClick);
        serviceLobby.OnSelectionChanged += onSelectionChanged;
    }

    private void onSelectionChanged(Tab selectedTab)
    {
        if(tab != selectedTab && isSelected)
        {
            StartCoroutine(DisableSelection());
        }
        else if(!isSelected && selectedTab == tab)
        {
          StartCoroutine(EnableSelection());
        }
    }

    private IEnumerator EnableSelection()
    {
        yield return null;
        isSelected = true;
        layoutElement.preferredWidth = rectTransform.rect.width;
    }

    private IEnumerator DisableSelection()
    {
        yield return null;
        isSelected = false;
        layoutElement.preferredWidth = -1f;
    }

    private void OnClick()
    {
        serviceLobby.ChangeTab(tab);
    }
}
