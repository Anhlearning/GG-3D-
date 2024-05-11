using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IngameMenu : MonoBehaviour
{
   [SerializeField] Button ResumeBtn;
   [SerializeField] Button RestartBtn;
   [SerializeField] Button MainMenu;
   
   [SerializeField] UIManager uIManager;
   private void Start() {
      ResumeBtn.onClick.AddListener(ResumeGame);
      RestartBtn.onClick.AddListener(RestartLevel);
      MainMenu.onClick.AddListener(BackToMainMenu);
   }
   private void ResumeGame(){
      uIManager.SwitchToGamePlayUI();
   }

   private void RestartLevel(){

   }

   private void BackToMainMenu(){
   }

}
