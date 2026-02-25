using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{

    public Tilemap tm_Ground;
    public Tilemap tm_Grass;
    public Tilemap tm_Tree;

    public TileBase tb_Tree; 

    private Map map;

    private FirebaseDatabaseManager databaseManager;
    private FirebaseUser user;

    private DatabaseReference reference;

    private void Start()
    {
        map = new Map();
        databaseManager = GameObject.Find("DatabaseManager").GetComponent<FirebaseDatabaseManager>();
        user = FirebaseAuth.DefaultInstance.CurrentUser;

        // WriteAllTileMapToFirebase();
        FirebaseApp app = FirebaseApp.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        LoadMapForUser();
    }

    public void WriteAllTileMapToFirebase()
    {
        List<TilemapDetails> tilemaps = new List<TilemapDetails>();
        for(int x = tm_Ground.cellBounds.min.x; x < tm_Ground.cellBounds.max.x; x++)
        {
            for(int y = tm_Ground.cellBounds.min.y; y < tm_Ground.cellBounds.max.y; y++)
            {
                TilemapDetails tilemapDetails = new TilemapDetails(x,y,TilemapState.Grass);
                tilemaps.Add(tilemapDetails);
            }
        }
        map = new Map(tilemaps);
        Debug.Log("Tilemap Details: " + map.ToString());

        databaseManager.WriteDatabase(user.UserId + "/Maps" ,map.ToString());
    }

    public void LoadMapForUser()
    {
        reference.Child("Users")
            .Child(user.UserId + "/Maps")
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    Debug.LogError("Failed to load map data: " + task.Exception);
                    return;
                }

                DataSnapshot snapshot = task.Result;

                if (snapshot.Value != null)
                {
                    map = JsonConvert.DeserializeObject<Map>(
                        snapshot.Value.ToString()
                    );

                    Debug.Log("Loaded Map: " + map.ToString());
                    MapToUI(map);
                }
                else
                {
                    Debug.Log("No map data found.");
                }
            });
    }

    public void TileMapDetailToTileBase(TilemapDetails tilemapDetail)
    {
        Vector3Int cellPos = new Vector3Int(tilemapDetail.x, tilemapDetail.y, 0);
        if (tilemapDetail.tilemapState == TilemapState.Ground)
        {
            tm_Grass.SetTile(cellPos, null);
            tm_Tree.SetTile(cellPos, null);
        }
        else if(tilemapDetail.tilemapState == TilemapState.Grass)
        {
            tm_Tree.SetTile(cellPos, null);
        }
        else if (tilemapDetail.tilemapState == TilemapState.Tree)
        {
            tm_Grass.SetTile(cellPos, null);
            tm_Tree.SetTile(cellPos, tb_Tree);
        }
    }

    public void MapToUI(Map map) 
    {
        Debug.Log("Mapping Map to UI...");
        for (int i = 0; i < map.GetMapSize(); i++)
        {
            TileMapDetailToTileBase(map._listTileMapDetail[i]);
        }
    }   

    public void SetStateForTilemapDetail(int x, int y, TilemapState state)
    {
        for(int i = 0; i < map.GetMapSize(); i++)
        {
            if(map._listTileMapDetail[i].x == x && map._listTileMapDetail[i].y == y)
            {
                map._listTileMapDetail[i].tilemapState = state;
                databaseManager.WriteDatabase(user.UserId + "/Maps", map.ToString());
                Debug.Log("Updated Tilemap Detail: " + map._listTileMapDetail[i].ToString());
            }
        }
    }
}

