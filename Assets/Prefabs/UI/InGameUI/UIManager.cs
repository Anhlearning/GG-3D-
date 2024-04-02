using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
  [SerializeField]CanvasGroup GameplayCtrl;
  [SerializeField] CanvasGroup GamePlayMenu;

   public void SetGamePlayControlEnabled(bool enable){
        SetCanvasGroupEnabled(GameplayCtrl,enable);
   }
   public void SetGamePlayMenuEnabled(bool enable){
        SetCanvasGroupEnabled(GamePlayMenu,enable);
   }

   public void SetCanvasGroupEnabled(CanvasGroup group,bool enable){
        group.interactable=enable;
        group.blocksRaycasts=enable;
   }
}
