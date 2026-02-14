using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseDatabaseManager : MonoBehaviour
{
    private DatabaseReference reference;

    private void Awake()
    {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void Start()
    {
        TilemapDetails details = new TilemapDetails(1, 1, TilemapState.Ground);
        WriteDatabase("tile_1_1", details.ToString());
    }

    public void WriteDatabase(string id, string message)
    {
        reference.Child("Users").Child(id).SetValueAsync(message).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Data written successfully.");
            }
            else
            {
                Debug.LogError("Failed to write data: " + task.Exception);
            }
        });
    }

    public void ReadDatabase(string id)
    {
        reference.Child("Users").Child(id).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    string message = snapshot.Value.ToString();
                    Debug.Log("Data read successfully: " + message);
                }
                else
                {
                    Debug.LogWarning("No data found for ID: " + id);
                }
            }
            else
            {
                Debug.LogError("Failed to read data: " + task.Exception);
            }
        }); 
    }
}
