//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using PlayFab;
//using PlayFab.ClientModels;
//using System.IO;

//public class LoginManager : MonoBehaviour
//{



//    private string gameDataFileName = "data.json";

//    public string filePath;

//    public SaveData saveData;

//    public bool saveGameLoaded, kongPlayer;



//    public string playerID;


//    public bool IsLoggedIn
//    {
//        get
//        {
//            return PlayFabClientAPI.IsClientLoggedIn();
//        }
//    }
//    private static LoginManager instance;

//    public static LoginManager Instance
//    {
//        get
//        {


//            return instance;
//        }
//    }



//    private void Awake()
//    {
//        filePath = Application.dataPath + Path.DirectorySeparatorChar + gameDataFileName;


//        //TESTTESTTESTETSETETS
//       // PlayerPrefs.DeleteAll();

//        if (instance == null)

//        {
//            instance = this;

//            DontDestroyOnLoad(this.gameObject);

//        }

//        else
//        {
//            Destroy(gameObject);

//        }
//    }


//    void Start()
//    {
//        saveData = new SaveData();

//        LoginWithDeviceID();

//    }



//    //void Update()
//    //{

//    //    if (Input.GetKeyDown(KeyCode.S))
//    //    {
//    //        GameManager.Instance.SavePlayerData();
//    //    }
//    //    if (Input.GetKeyDown(KeyCode.L))
//    //    {

//    //        LoadUserData(loadFromPlayfab: false);
//    //    }
//    //}

//    //replaced by kong
//    private void LoginWithDeviceID()
//    {
//        if (IsLoggedIn) return;

//        var request = new LoginWithCustomIDRequest
//        {
//            TitleId = "8888",
//            CreateAccount = true,
//            CustomId = GetPlayerID(),
//        };

//        PlayFabClientAPI.LoginWithCustomID(request, OnLoggedIn, OnLoginError);

//        ////if has a kongAccount
//        //{
//        //    kongplayer = true;

//        //    //check if name is in my database
//        //    //if yes, load SavaData
//        //}
//        //else
//        //{
//        //  // get string from playerprefs     
//        //if player prefs has key, load from file
//        //  thus load from file bool}
//    }





//    private string GetPlayerID()
//    {
//        string newPlayerID = "";

//        if (PlayerPrefs.HasKey("playerID"))
//        {
//            newPlayerID = PlayerPrefs.GetString("playerID");
//            saveGameLoaded = true;
//            LoadUserData(loadFromPlayfab:false);

//        }
//        else
//        {

//            string glyphs = "abcdefghijklmnopqrstuvwxyz";



//            for (int i = 0; i < 25; i++)
//            {
//                newPlayerID += glyphs[Random.Range(0, glyphs.Length)];
//            }

//            PlayerPrefs.SetString("playerID", newPlayerID);
//        }



//        return newPlayerID;
//    }




//    public void UploadPLayerData(SaveData saveData)
//    {

//        Dictionary<string, string> dataAsJson;

//        dataAsJson = new Dictionary<string, string>();


//        dataAsJson.Add("jsonData", JsonUtility.ToJson(saveData));





//        var dataRequest = new UpdateUserDataRequest
//        {
//            Data = dataAsJson,


//        };
//        PlayFabClientAPI.UpdateUserData(dataRequest, (result) =>
//        {
//        }, (error) =>
//        {
//            Debug.Log(error.ErrorDetails);
//        });
//    }



//    public void LoadUserData(bool loadFromPlayfab)
//    {

//        string dataAsJson ="";

//        if (loadFromPlayfab)
//        {
//            PlayFabClientAPI.GetUserData(new GetUserDataRequest()
//            {
//                // PlayFabId = PlayFabID,


//            }, result =>
//            {
//                //old Player
//                if (result.Data != null && result.Data.ContainsKey("jsondata"))
//                {
//                    dataAsJson = result.Data["jsonData"].Value;                
//                    Debug.Log("No Ancestor key");
//                }
//                else Debug.Log("whatcouldbecometrue: " + result.Data["jsonData"].Value);
//            }, (error) =>
//            {
//                Debug.Log("Got error retrieving user data:");
//                Debug.Log(error.GenerateErrorReport());
//            });

//        }
//        //load from File
//        else
//        {
//            dataAsJson = File.ReadAllText(filePath);
//        }

//        saveData = JsonUtility.FromJson<SaveData>(dataAsJson);
//        GameManager.Instance.LoadPlayerData(saveData);
//    }

//    private void OnLoggedIn(LoginResult result)
//    {
//        playerID = result.PlayFabId;





//        //UpdateUserData();
//    }

//    private static void OnLoginError(PlayFabError error)
//    {

//    }
//}
