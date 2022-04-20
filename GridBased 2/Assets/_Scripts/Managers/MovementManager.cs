using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public static MovementManager Instance;
    
    
    void Awake()
    {
        Instance = this;
    }

    public void SetAreaTiles(Vector2 pos, int movement, Color color)
    {
        Dictionary<Vector2, Tile> tiles = GridManager.Instance._tiles;
        //select tiles in range
        List<Tile> area = tiles.Where(t => (Mathf.Abs(t.Key.x - pos.x) < movement && Mathf.Abs(t.Key.y - pos.y) < movement) ).ToDictionary(t => t.Key, t => t.Value).Values.ToList();
        //makes tiles inRange true
        foreach (Tile tile in area)
        {
            tile.inRange = true;
            tile.rangeHighlight.SetActive(true);
            tile.rangeHighlight.GetComponent<SpriteRenderer>().color = color;
        }

    }

    public void SetMovementTiles(Vector2 pos, int movement, Color color)
    {
        
        Dictionary<Vector2, Tile> tiles = GridManager.Instance._tiles;
        int moveCount = 0;
        //select tiles in range
        List<Tile> area = new List<Tile>();
        area.Add(GridManager.Instance.GetTileAtPosition(pos));
        
        while ( moveCount < movement)
        {
            

            foreach (Tile tile in area.ToList() )
            {
                //movement BFS
                Vector2 tilePos = tile.transform.position;
                
                if (tile.Walkable == true || tilePos == pos && tile.isCheck == false)
                {
                    
                    // add for directions
                    if (GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x + 1, tilePos.y)) != null && GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x + 1, tilePos.y)).isCheck == false)
                    {
                        var nextTile = GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x + 1, tilePos.y));
                        area.Add(nextTile);
                        nextTile.parent = tile;
                        if (nextTile.dist == -1) nextTile.dist = moveCount + 1;
                    }

                    if (GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x - 1, tilePos.y)) != null && GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x - 1, tilePos.y)).isCheck == false)
                    {
                        var nextTile = GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x - 1, tilePos.y));
                        area.Add(nextTile);
                        nextTile.parent = tile;
                        if (nextTile.dist == -1) nextTile.dist = moveCount + 1;
                    }

                    if (GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x, tilePos.y + 1)) != null && GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x, tilePos.y + 1)).isCheck == false)
                    {
                        var nextTile = GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x, tilePos.y + 1));
                        area.Add(nextTile);
                        nextTile.parent = tile;
                        if (nextTile.dist == -1) nextTile.dist = moveCount + 1;
                    }

                    if (GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x + 1, tilePos.y -1)) != null && GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x, tilePos.y -1)).isCheck == false)
                    {
                        var nextTile = GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x, tilePos.y - 1));
                        area.Add(nextTile);
                        nextTile.parent = tile;
                        if (nextTile.dist == -1) nextTile.dist = moveCount +1;
                    }

                    
                    tile.isCheck = true;
                }
            
  
            }
            moveCount++;
        }
        //makes tiles inRange true
        foreach (Tile tile in area.ToList())
        {
            if (tile.Walkable == true)
            {
                tile.inRange = true;
                tile.rangeHighlight.SetActive(true);
                tile.rangeHighlight.GetComponent<SpriteRenderer>().color = color;

                
                tile.isCheck = true;
            }
        }
    }

    public void CleanMovementTiles()
    {
        Dictionary<Vector2, Tile> tiles = GridManager.Instance._tiles;

        //makes tiles inRange false
        foreach (Tile tile in tiles.Values)
        {
            tile.inRange = false;
            tile.rangeHighlight.SetActive(false);

            tile.isCheck = false;
            tile.parent = tile;
            tile.dist = -1;
        }

    }

    public bool CheckRangeTiles()
    {
        Dictionary<Vector2, Tile> tiles = GridManager.Instance._tiles;
        foreach (Tile tile in tiles.Values)
        {
            if (tile.inRange == true)
            {
                if (tile.OccupiedUnit != null && tile.OccupiedUnit.Faction == Faction.Enemy)
                {
                    return true;
                }
            }

        }
        return false;
    }

    public void DrawPath( Tile target)
    {
        LineRenderer path = UnitManager.Instance.SelectedHero.path;
        path.positionCount = 1;
        path.SetPosition(0, target.transform.position);
        Tile tile = target;
 
            for (int i = 0; i < target.dist; i++)
            {

                path.positionCount++;
                tile = tile.parent;
                path.SetPosition(i +1 , tile.transform.position);



            }

    }
    public void CleanPath()
    {
        LineRenderer path = UnitManager.Instance.SelectedHero.path;
        path.positionCount = 0;
    }

    public List<Tile> ReturnMoveTiles(Vector2 pos, int movement,bool includeUnits = false)
    {
        Dictionary<Vector2, Tile> tiles = GridManager.Instance._tiles;
        int moveCount = 0;
        //select tiles in range
        List<Tile> area = new List<Tile>();
        area.Add(GridManager.Instance.GetTileAtPosition(pos));

        while (moveCount < movement)
        {


            foreach (Tile tile in area.ToList())
            {
                //movement BFS
                Vector2 tilePos = tile.transform.position;

                if (tile.Walkable == true || tilePos == pos && tile.isCheck == false)
                {

                    // add for directions
                    if (GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x + 1, tilePos.y)) != null && GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x + 1, tilePos.y)).isCheck == false)
                    {
                        var nextTile = GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x + 1, tilePos.y));
                        area.Add(nextTile);
                        nextTile.parent = tile;
                        if (nextTile.dist == -1) nextTile.dist = moveCount + 1;
                    }

                    if (GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x - 1, tilePos.y)) != null && GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x - 1, tilePos.y)).isCheck == false)
                    {
                        var nextTile = GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x - 1, tilePos.y));
                        area.Add(nextTile);
                        nextTile.parent = tile;
                        if (nextTile.dist == -1) nextTile.dist = moveCount + 1;
                    }

                    if (GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x, tilePos.y + 1)) != null && GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x, tilePos.y + 1)).isCheck == false)
                    {
                        var nextTile = GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x, tilePos.y + 1));
                        area.Add(nextTile);
                        nextTile.parent = tile;
                        if (nextTile.dist == -1) nextTile.dist = moveCount + 1;
                    }

                    if (GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x + 1, tilePos.y - 1)) != null && GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x, tilePos.y - 1)).isCheck == false)
                    {
                        var nextTile = GridManager.Instance.GetTileAtPosition(new Vector2(tilePos.x, tilePos.y - 1));
                        area.Add(nextTile);
                        nextTile.parent = tile;
                        if (nextTile.dist == -1) nextTile.dist = moveCount + 1;
                    }


                    tile.isCheck = true;
                }


            }
            moveCount++;
        }
        if (includeUnits == true)
        {
            return area;
        }
        List<Tile> finalArea = new List<Tile>();
        foreach (Tile tile in area.ToList())
        {
            if (tile.Walkable == true)
            {
                finalArea.Add(tile);
            }
        }
        return finalArea;
    }
    public List<Tile> ReturnAreaTiles(Vector2 pos, int range)   
    {
        Dictionary<Vector2, Tile> tiles = GridManager.Instance._tiles;
        //select tiles in range
        List<Tile> area = tiles.Where(t => (Mathf.Abs(t.Key.x - pos.x) < range && Mathf.Abs(t.Key.y - pos.y) < range)).ToDictionary(t => t.Key, t => t.Value).Values.ToList();
        return area;
    }

    public static implicit operator MovementManager(MoveAnimationManager v)
    {
        throw new NotImplementedException();
    }
}
