using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerFarmController : MonoBehaviour
{
    public Tilemap tm_Ground;
    public Tilemap tm_Grass;
    public Tilemap tm_Tree;

    public TileBase tb_Ground; 
    public TileBase tb_Grass; 
    public TileBase tb_Tree;


    private void Update()
    {
        HandleFarmAction();
    }


    public void HandleFarmAction()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Vector3Int cellPos = tm_Ground.WorldToCell(transform.position);

            TileBase currTileBase = tm_Grass.GetTile(cellPos);

            if(currTileBase == tb_Grass)
            {
                tm_Grass.SetTile(cellPos, null);
            }
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Vector3Int cellPos = tm_Ground.WorldToCell(transform.position);

            TileBase currTileBase = tm_Grass.GetTile(cellPos);

            if (currTileBase == null)
            {
                tm_Tree.SetTile(cellPos, tb_Tree);
            }
        }

    }
}
