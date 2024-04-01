using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image AmtImage;
    [SerializeField] TextMeshProUGUI AmtText;

    public void UpdateHealth(float health,float delta,float maxhealth){
        AmtImage.fillAmount=health/maxhealth;
        AmtText.SetText(health.ToString());
    }
}
