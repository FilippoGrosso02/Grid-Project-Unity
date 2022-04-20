using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    public static MenuManager Instance;

    [SerializeField] private GameObject selectedHeroPanel, selectedHeroName, selectedHeroHealth;
    

    [SerializeField] private GameObject tileUnitPanel, tileUnitName, tileUnitHealth;

    [Space(10)]
    [SerializeField] private GameObject tilePanel;
    void Awake() {
        Instance = this;
        selectedHeroPanel.SetActive(false);
        tileUnitPanel.SetActive(false);
        tilePanel.SetActive(false);
    }

    public void ShowTileInfo(Tile tile) {
        //EMPTY SPACE
        if (tile == null)
        {
            tilePanel.SetActive(false);
            tileUnitPanel.SetActive(false);
            return;
        }
        //TILE
        tilePanel.GetComponentInChildren<Text>().text = tile.TileName;
        tilePanel.SetActive(true);
        
        //OCCUPIED UNIT
        if (tile.OccupiedUnit) {
            tileUnitName.GetComponentInChildren<Text>().text = tile.OccupiedUnit.UnitName;
            tileUnitHealth.GetComponentInChildren<Text>().text = tile.OccupiedUnit.currentHealth.ToString();
            tileUnitPanel.SetActive(true);
        }
    }

    public void ShowSelectedHero(BaseHero hero) {
        if (hero == null) {
            selectedHeroPanel.SetActive(false);
            return;
        }
        //HERO
        selectedHeroPanel.GetComponentInChildren<Text>().text = hero.UnitName;
        selectedHeroHealth.GetComponentInChildren<Text>().text = hero.currentHealth.ToString();
        selectedHeroPanel.SetActive(true);
    }

    public void TurnPass()
    {
        GameManager.Instance.ChangeState(GameState.EnemiesTurn);
        
    }
}
