using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyActionManager : MonoBehaviour
{
    public static EnemyActionManager Instance;
    private BaseUnit unit;
    public int checkRange = 10;

    private Tile closestHero;
    private BaseUnit heroUnit;

    void Awake()
    {
        Instance = this;
    }

    public async Task EnemyAction( BaseUnit enemy)
    {
       
        Debug.Log("ran enemyaction");
        
        // starting conditions, sets the unit as the current enemy

        closestHero = null;
        unit = null;
        unit = enemy;
        //check if it can attack at the start
        if (AttackInRange() == true)
        {
            MovementManager.Instance.CleanMovementTiles();
            return;
        }
        //else proceeds with the turn
        await MoveInRange();
        AttackInRange();
        MovementManager.Instance.CleanMovementTiles();
        return;
    }

    public async Task MoveInRange()
    {
        // find the closest player if any exist
        
        Tile targetTile = ReturnClosestPlayer(checkRange);
        if (targetTile == null)
        {

            MovementManager.Instance.CleanMovementTiles();
            return;
        }
        MovementManager.Instance.CleanMovementTiles();

        // move towards the enemy

        Tile moveTile = MoveTowardsTile(targetTile);
        // find the tile to move to 
        //moveTile.SetUnit(unit);
        await MoveAnimationManager.Instance.MoveInPath(unit, moveTile);
        MovementManager.Instance.CleanMovementTiles();
        moveTile = null;
    }

    public bool AttackInRange()
    {
        Tile attackTile = ReturnClosestPlayer(unit.attackRange, false);
        if (attackTile == null)
        {
            Debug.Log("attackTile is null");
            MovementManager.Instance.CleanMovementTiles();
            return false;
        }
        Debug.Log("attackTile found by:" + unit.name);
        unit.Attack(attackTile.OccupiedUnit);
        MovementManager.Instance.CleanMovementTiles();
        attackTile = null;
        return true;
    }

    public Tile ReturnClosestPlayer(int _checkRange, bool useBFS = true)
    {
        // Retruns the closest player found
        closestHero = null;
        List<Tile> tiles = new List<Tile>();
        if (useBFS == true) tiles = MovementManager.Instance.ReturnMoveTiles(new Vector2(unit.transform.position.x, unit.transform.position.y), _checkRange, true);
        else tiles = MovementManager.Instance.ReturnAreaTiles(new Vector2(unit.transform.position.x, unit.transform.position.y), _checkRange);

        foreach ( Tile tile in tiles)
        {
            if (tile.OccupiedUnit != null && tile.OccupiedUnit.Faction == Faction.Hero)
            {
                if (closestHero == null)
                {
                    closestHero = tile;
                }
                if ( tile.dist <= closestHero.dist)
                {
                    closestHero = tile;
                }
            }
        }
        tiles.Clear();
        return closestHero;
    }

    
    public Tile MoveTowardsTile(Tile targetTile)
    {
        List<Tile> tiles = MovementManager.Instance.ReturnMoveTiles(new Vector2(unit.transform.position.x, unit.transform.position.y), checkRange);
        int moveCount = 0;
        Tile startingTile = GridManager.Instance.GetTileAtPosition(unit.transform.position);
        Tile tile = targetTile;

        //set by how much the enemy should move backwards from target
        int moveValue = 0;
        if (targetTile.dist - unit.speed - unit.attackRange > 0)
        {
            moveValue = targetTile.dist - unit.speed - unit.attackRange;
        }
        else if (targetTile.dist - unit.speed - unit.attackRange<= 0)
        {
            moveValue = 1;
        }

            while ( moveCount < moveValue)
        {
            // move backwards until it finds the origin
            
            tile = tile.parent;
            moveCount++;
            // check if u arrived back to starting point
            if (tile == startingTile) break;
            if (tile.OccupiedUnit != null) break;
        }

        tiles.Clear();
        

        // check that you didnt land on the target tile itself
        if (tile != targetTile && tile.OccupiedUnit == null)
        {
            return tile;
        }
        else
        {
            return startingTile;
        }
    }
}
