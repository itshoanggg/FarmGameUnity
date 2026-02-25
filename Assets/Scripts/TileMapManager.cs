using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{

    public Tilemap tm_Ground; 

    private Map map;

    private FirebaseDatabaseManager databaseManager;
    private FirebaseUser user;

    private void Start()
    {
        map = new Map();
        databaseManager = GameObject.Find("DatabaseManager").GetComponent<FirebaseDatabaseManager>();
        user = FirebaseAuth.DefaultInstance.CurrentUser;

        WriteAllTileMapToFirebase();
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
}

