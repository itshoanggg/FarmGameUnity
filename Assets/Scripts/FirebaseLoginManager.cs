using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirebaseLoginManager : MonoBehaviour
{
    [Header("Register")]
    public InputField ipRegisterEmail;
    public InputField ipRegisterPassword;
    public Button btnRegister;

    [Header("Login")]
    public InputField ipLoginEmail;
    public InputField ipLoginPassword;
    public Button btnLogin;

    private FirebaseAuth auth;


    [Header("Switch Form")]
    public Button btnMoveToLogin;
    public Button btnMoveToRegister; 

    public GameObject registerForm;
    public GameObject loginForm;

    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        btnRegister.onClick.AddListener(RegisterFirebase);
        btnLogin.onClick.AddListener(LoginFirebase);

        btnMoveToRegister.onClick.AddListener(SwitchForm);
        btnMoveToLogin.onClick.AddListener(SwitchForm);
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

    public void LoginFirebase()
    {
        string email = ipLoginEmail.text;
        string password = ipLoginPassword.text;
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("Login was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("Login encountered an error: " + task.Exception);
                return;
            }
            if(task.IsCompleted)
            {
                Debug.Log("User logged in successfully");
                FirebaseUser user = task.Result.User;

                SceneManager.LoadScene("PlayScene");
            }
        });
    }

    public void SwitchForm()
    {
        loginForm.SetActive(!loginForm.activeSelf);
        registerForm.SetActive(!registerForm.activeSelf);
    }
}
