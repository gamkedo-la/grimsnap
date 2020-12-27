using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetHealth : MonoBehaviour
{
    public TextMeshProUGUI targetNameText;
    public Image targetHealthImage;
    public Image targetHealthImageFill;

    public void ShowTargetHealth(Health target)
    {
        if (target != null)
        {
            if (target.health <= 0)
            {
                HideTargetHealth();
            }
            else
            {
                targetNameText.enabled = true;
                targetHealthImage.enabled = true;
                targetHealthImageFill.enabled = true;
                targetNameText.text = target.name;
                targetHealthImageFill.fillAmount = target.health / target.maxHealth;
            }
        }
    }

    public void HideTargetHealth()
    {
        targetNameText.enabled = false;
        targetHealthImage.enabled = false;
        targetHealthImageFill.enabled = false;
    }
}
