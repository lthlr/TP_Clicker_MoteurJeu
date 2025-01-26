using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public List<MonsterInfos> Monsters;
    public List<Upgrade> Upgrades;
    int _currentMonster = 0;
    public Monster monster;
    public GameObject PrefabHitPoint;
    public GameObject PrefabUpgradeUI;
    public GameObject ParentUpgrades;

    private void Start()
    {
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

                GameObject go = GameObject.Instantiate(PrefabHitPoint, monster.canvas.transform, false);
                go.transform.localPosition = UnityEngine.Random.insideUnitCircle * 5;
                go.transform.DOLocalMoveY(150, 0.8f);
                go.GetComponent<TMP_Text>().DOFade(0, 0.8f);
                GameObject.Destroy(go, 0.8f);
                

            }

            if (monster.IsAlive() == false)
            {
                NextMonster();
            }
        }
    }

    private void NextMonster()
    {
        _currentMonster++;
        monster.SetMonster(Monsters[_currentMonster]);
    }
}
