using System;
using Firebase;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseAnalytics : MonoBehaviour
{
    public static FirebaseAnalytics Instance;
    private bool isFirebaseReady = false;
    private FirebaseApp app;

    private void Awake()
    {
        if (Instance!=null && Instance!=this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available) {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = Firebase.FirebaseApp.DefaultInstance;
                isFirebaseReady = true;
                // Set a flag here to indicate whether Firebase is ready to use by your app.
            } else {
                UnityEngine.Debug.LogError(System.String.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    public void LogLevelComplete(int score)
    {
        if (!isFirebaseReady)return;
        Debug.Log("score "+score);
        Firebase.Analytics.FirebaseAnalytics.LogEvent("level_complete",
            new Firebase.Analytics.Parameter("level_score",score));
    }

    public void LogLevelReset(int score)
    {
        if (!isFirebaseReady)return;
        
        Firebase.Analytics.FirebaseAnalytics.LogEvent("level_reset",
            new Firebase.Analytics.Parameter("level_number",score));
    }
}
