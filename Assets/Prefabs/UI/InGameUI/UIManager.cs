using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;

public class UIManager : MonoBehaviour
{
  [SerializeField]CanvasGroup GameplayCtrl;
  [SerializeField] CanvasGroup GamePlayMenu;
  [SerializeField] CanvasGroup Shop;

  List<CanvasGroup>Allchildren= new List<CanvasGroup>();

     CanvasGroup currentGrp;
   private void Start() {
     List<CanvasGroup>children=new List<CanvasGroup>();
     GetComponentsInChildren(true,children);
     foreach(CanvasGroup child in children){
          if(child.transform.parent==transform){
               Allchildren.Add(child);
               SetGroupActive(child,false,false);
          }
     }
     if(Allchildren.Count!=0){
          SetCurrentActiveGrp(Allchildren[0]);
     }
   }
   private void SetCurrentActiveGrp(CanvasGroup child){
     if(currentGrp !=null){
          SetGroupActive(currentGrp,false,false);
     }
     currentGrp=child;
     SetGroupActive(currentGrp,true,true);
   }
    public void SwitchToGamePlayUI(){
        SetCurrentActiveGrp(GameplayCtrl);
    }
   private void SetGroupActive(CanvasGroup child,bool interactable,bool visible){
     child.interactable=interactable;
     child.blocksRaycasts=interactable;
     child.alpha=visible ? 1 :0;
   }
   public void SetGamePlayControlEnabled(bool enable){
        SetCanvasGroupEnabled(GameplayCtrl,enable);
   }
   public void SetGamePlayMenuEnabled(bool enable){
        SetCanvasGroupEnabled(GamePlayMenu,enable);
   }

     public void SwitchToShop(){
          SetCurrentActiveGrp(Shop);
     }
   public void SetCanvasGroupEnabled(CanvasGroup group,bool enable){
        group.interactable=enable;
        group.blocksRaycasts=enable;
   }
}
