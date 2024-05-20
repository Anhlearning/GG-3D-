using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]Button StartBtn;
    [SerializeField]Button ControlsBtn;
    [SerializeField]Button BackBtn;
    [SerializeField]CanvasGroup FrontUi;
    [SerializeField]CanvasGroup ControlsUI;
    [SerializeField]LevelManager levelManager;

    private void Start() {
        Debug.Log("START");
        StartBtn.onClick.AddListener(StartGame);
        ControlsBtn.onClick.AddListener(SwtichControlsUI);
        BackBtn.onClick.AddListener(SwtichToFrontUI);

    }
    private void StartGame(){
        Debug.Log("LOADGAME Start");
        levelManager.LoadFirstLevel();
    }
    private void SwtichControlsUI(){
        ControlsUI.blocksRaycasts=true;
        ControlsUI.alpha=1;

        FrontUi.blocksRaycasts=false;
        FrontUi.alpha=0;

    }
    private void SwtichToFrontUI(){
        ControlsUI.blocksRaycasts=false;
        ControlsUI.alpha=0;

        FrontUi.blocksRaycasts=true;
        FrontUi.alpha=1;
    }
    public void CLick(){
        Debug.Log("DA CLICK");
    }

}
