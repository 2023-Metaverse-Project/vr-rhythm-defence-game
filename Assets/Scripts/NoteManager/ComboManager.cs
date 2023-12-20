using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ComboManager : MonoBehaviour
{
    [SerializeField]
    GameObject goComboImage = null;
    [SerializeField]
    TextMeshProUGUI textCombo = null;

    int currentCombo;

    [SerializeField] Animator comboAnimator = null;
    [SerializeField] Animator comboTextAnimator = null;

    [SerializeField]
    private UnityEvent<int> onComboAchieve;

    private void Start()
    {
        textCombo.gameObject.SetActive(false);
        goComboImage.SetActive(false);
    }

    public void IncreaseCombo(int p_num = 1)
    {
        currentCombo += p_num;
        textCombo.text = string.Format("{0:#,##0}", currentCombo);

        if(currentCombo > 9)
        {
            textCombo.gameObject.SetActive(true);
            goComboImage.SetActive(true);
            comboAnimator?.SetTrigger("Hit");
            comboTextAnimator?.SetTrigger("Hit");
        }

        // combo 를 10번 달성 시 마다 onComboAchieve 이벤트 발생
        if (currentCombo % 10 == 0)
        {
            int comboLevel = currentCombo / 10;
            onComboAchieve?.Invoke(comboLevel);
        }
    }

    public int GetCurrentCombo()
    {
        return currentCombo;
    }

    public void ResetCombo()
    {
        currentCombo = 0;
        textCombo.text = "0";
        textCombo.gameObject.SetActive(false);
        goComboImage.SetActive(false);
    }

    //public void ComboEffect()
    //{
    //    comboAnimator.SetTrigger("Hit");
    //}
}
