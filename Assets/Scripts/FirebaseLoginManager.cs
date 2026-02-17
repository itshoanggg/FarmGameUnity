using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseLoginManager : MonoBehaviour
{
    public InputField ipRegisterEmail;
    public InputField ipRegisterPassword;

    public Button btnRegister;

    private FirebaseAuth auth;

    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        btnRegister.onClick.AddListener(RegisterFirebase);
    }

    public void RegisterFirebase()
    {
        string email = ipRegisterEmail.text;
        string password = ipRegisterPassword.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("Register was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("Register encountered an error: " + task.Exception);
                return;
            }
            if(task.IsCompleted)
            {
                Debug.Log("User registered successfully"); 
            }
        });
    }
}
