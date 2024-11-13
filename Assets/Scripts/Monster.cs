using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public int Life;
    public TMP_Text Textlife;
    public Image ImageLife;
    int _lifeMax;

    void UpdateLife()
    {
        Textlife.text = $"{Life}/{_lifeMax}";

        float percent = (float)Life / (float)_lifeMax;
        ImageLife.fillAmount = percent;
    }

    private void Awake()
    {
        UpdateLife();
    }
}
