using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> TabButtons;
    public TabButton SelectedTab;
    public List<GameObject> PagesToSwap;

    public List<GameObject> SettingsButtons;

    

    public void Subscribe(TabButton button) {
        if (TabButtons == null) {
            TabButtons = new List<TabButton>();
        }
        TabButtons.Add(button);
    }

    public void ResetTabs() {
        foreach(TabButton button in TabButtons) {
            if (SelectedTab != null && button == SelectedTab) {
                continue;
            }
            button.idleTab.SetActive(true);
            button.hoverTab.SetActive(false);
            button.chosenTab.SetActive(false);
        }
    }


    public void ResetTabs2() {
        foreach(TabButton button in TabButtons) {
            //if (SelectedTab != null && button == SelectedTab) 
            //    continue;
            
            button.idleTab.SetActive(true);
            button.hoverTab.SetActive(false);
            button.chosenTab.SetActive(false);
        }
    }

}
