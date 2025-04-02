using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public List<MonsterInfos> Monsters;
    int _currentMonster = 0;
    public Monster monster;
    public GameObject PrefabHitPoint;
    public GameObject PrefabUpgradeUI;
    public GameObject ParentUpgrades;
    public Canvas Canvas;
    public List<Upgrade> Upgrades;

    private void Start()
    {
        if (Monsters.Count > 0) Monster.UpdateMonster(Monsters[_currentMonster]);
        if(Upgrades.Count>0 && PrefabUpgradeUI != null && ParentUpgrades != null)
        {
            foreach (var upgrade in Upgrades)
            {
                GameObject go = GameObject.Instantiate(PrefabUpgradeUI, ParentUpgrades.transform);
                UpgradeUI upgradeUI = upgradeInstance.GetComponent<UpgradeUI>();
                upgrade.Icon.sprite = upgrade.Icon;
                upgradeUI.CostText.text = $"{upgrade.Cost}$";
                upgradeUI.DescriptionText.text = $"{upgrade.Name}\"
            }

        }
        monster.SetMonster(Monsters[_currentMonster]);
        foreach (var upgrade in Upgrades)
        {
            GameObject go = GameObject.Instantiate(PrefabUpgradeUI, ParentUpgrades.transform, false);
            go.transform.localPosition = Vector3.zero;
            go.GetComponent<UpgradeUI>().Initialize(upgrade);
        }
    }

    

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(world, Vector2.zero);

            if (hit.collider != null)
            {
                Monster monster = hit.collider.GetComponent<Monster>();
                monster.Hit(1);

                SpawnFeedback();
                //GameObject go = GameObject.Instantiate(PrefabHitPoint, monster.canvas.transform, false);
                //go.transform.localPosition = UnityEngine.Random.insideUnitCircle * 5;
                //go.transform.DOLocalMoveY(150, 0.8f);
                //go.GetComponent<TMP_Text>().DOFade(0, 0.8f);
                //GameObject.Destroy(go, 0.8f);
                

            }

            if (monster.IsAlive() == false)
            {
                NextMonster();
            }
        }
    }

    private void NextMonster()
    {
        _currentMonster = (_currentMonster + 1) % Monsters.Count;
        monster.SetMonster(Monsters[_currentMonster]);
    }

    private void SpawnFeedback()
    {
        GameObject feedback = Instantiate(PrefabHitPoint, Canvas.transform, false);
        feedback.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        feedback.transform.position = new Vector3(feedback.transform.position.x, feedback.transform.position.y, 0);
        feedback.transform.DOLocalMoveY(150, 0.8f);
        feedback.GetComponent<TMP_Text>().DOFade(0, 0.8f);
        Destroy(feedback, 0.8f);
    }
}
