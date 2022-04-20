using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class UnitManager : MonoBehaviour {
    public static UnitManager Instance;

    private List<ScriptableUnit> _units;
    public List<BaseUnit> playerUnits;
    public List<BaseUnit> enemyUnits;

    public BaseHero SelectedHero;

    void Awake() {
        Instance = this;

        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();

    }

    public void SpawnHeroes() {
        var heroCount = 2;

        for (int i = 0; i < heroCount; i++) {
            ScriptableUnit randomUnit= GetRandomUnit(Faction.Hero);
            var randomPrefab = randomUnit.UnitPrefab;
            
            var spawnedHero = Instantiate(randomPrefab);

            playerUnits.Add(spawnedHero.GetComponent<BaseUnit>());

            var randomSpawnTile = GridManager.Instance.GetHeroSpawnTile();
            spawnedHero.GetComponent<BaseUnit>().SetStats(randomUnit);

            randomSpawnTile.SetUnit(spawnedHero);
        }

        GameManager.Instance.ChangeState(GameState.SpawnEnemies);
    }

    public void SpawnEnemies()
    {
        var enemyCount = 4;

        for (int i = 0; i < enemyCount; i++)
        {
            ScriptableUnit randomUnit = GetRandomUnit(Faction.Enemy);
            var randomPrefab = randomUnit.UnitPrefab;
            var spawnedEnemy = Instantiate(randomPrefab);
            enemyUnits.Add(spawnedEnemy.GetComponent<BaseUnit>());

            var randomSpawnTile = GridManager.Instance.GetEnemySpawnTile();
            spawnedEnemy.GetComponent<BaseUnit>().SetStats(randomUnit);

            randomSpawnTile.SetUnit(spawnedEnemy);
        }

        GameManager.Instance.ChangeState(GameState.HeroesTurn);
    }

    private ScriptableUnit GetRandomUnit(Faction faction)  {
        return _units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First();
    }

    public void SetSelectedHero(BaseHero hero) {
        SelectedHero = hero;
        MenuManager.Instance.ShowSelectedHero(hero);
    }

    public void PlayerTurn()
    {
        foreach(BaseHero hero in playerUnits)
        {
            hero.ResetAction();
        }
        
    }
}
