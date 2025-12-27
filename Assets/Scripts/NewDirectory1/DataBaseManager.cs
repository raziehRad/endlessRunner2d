//using Firebase.Database;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataBaseManager : MonoBehaviour
{
   [SerializeField] private TMP_InputField Name;
   [SerializeField] private TMP_InputField Gold;
    
    private string userID;
    //private DatabaseReference dbReference;
    void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        //dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        Debug.Log("dbReference");
    }

   public void CreateUser()
    {
        User newUser = new User(Name.text, int.Parse(Gold.text));
        string json = JsonUtility.ToJson(newUser);

       // dbReference.Child("user").Child(userID).SetRawJsonValueAsync(json);
    }
    void Update()
    {
        
    }
}
