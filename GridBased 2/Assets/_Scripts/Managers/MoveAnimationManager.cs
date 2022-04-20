using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MoveAnimationManager : MonoBehaviour
{
    public static MoveAnimationManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public async Task MoveInPath(BaseUnit unit, Tile targetTile)
    {
        
        int dist = targetTile.dist;
        Stack<Vector3> positions = new Stack<Vector3>();
        positions.Push(targetTile.transform.position);

        Tile tile = targetTile;

        for (int i = 0; i < dist; i++)
        {

            
            tile = tile.parent;
            positions.Push(tile.transform.position);



        }

        Vector3 pos = new Vector3();
        int count = 0;
        while( count <= dist)
        {
            await Task.Delay(System.TimeSpan.FromSeconds(0.1f));
            pos = positions.Pop();
            unit.transform.position = pos;

            count++;

        }

        targetTile.SetUnit(unit);
        
    }
    

}
