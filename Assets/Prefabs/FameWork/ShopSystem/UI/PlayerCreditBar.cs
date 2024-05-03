using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCreditBar : MonoBehaviour
{
  [SerializeField] Button ShopBtn;
  [SerializeField] UIManager uiManager;
  [SerializeField] CreditComponent creditComp;
  [SerializeField] TextMeshProUGUI creditText;
  private void Start() {
        // Kiểm tra sự kiện đã được thêm vào AddListener chưa
        if (ShopBtn != null && uiManager != null)
        {
            ShopBtn.onClick.AddListener(PullOutShop);
            Debug.Log("Sự kiện PullOutShop đã được thêm vào AddListener.");
        }
        else
        {
            Debug.LogError("ShopBtn hoặc uiManager không được gán. Không thể thêm sự kiện.");
        }
        creditComp.onCreditChange+=UpdateCredit;
        UpdateCredit(creditComp.Credit);
    
  }
  private void UpdateCredit(int credit){
    creditText.SetText(credit.ToString());
  }

  private void PullOutShop(){
        Debug.Log("Btn Down");
        uiManager.SwitchToShop();
  }
}
