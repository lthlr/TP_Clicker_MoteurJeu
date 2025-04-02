using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MonsterInfos
{
    public string Name;
    public int Life;
    public Sprite Sprite;
}
public class Monster : MonoBehaviour
{
    [SerializeField]int _life;
    public TMP_Text Textlife;
    public Image ImageLife;
    int _lifeMax = 10;
    public GameObject Visual;
    public Canvas canvas;

    public bool IsAlive()
    {
        return _life > 0;
    }

    public void SetMonster(MonsterInfos infos)
    {
        if (infos != null)
        {
            _lifeMax = infos.Life;
            _life = _lifeMax;

            Visual.GetComponent<SpriteRenderer>().sprite = infos.Sprite;
        }
    }
    public void Hit(int damage)
    {
        _life -= damage;
        UpdateLife();
        Visual.transform.DOComplete();
        Visual.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0), 0.3f);
    }
    void UpdateLife()
    {
        Textlife.text = $"{_life}/{_lifeMax}";

        float percent = (float)_life / (float)_lifeMax;
        ImageLife.fillAmount = percent;
    }

    private void Awake()
    {
        UpdateLife();
    }
}


