//using System.Collections;
//using System.Collections.Generic;
//using Firebase.Extensions;
//using Google;
//using System.Threading.Tasks;
//using UnityEngine;
//using TMPro;
//using Firebase;
//using Firebase.Auth;
//using UnityEngine.UI;
//using UnityEngine.Networking;

//public class LoginWithGoogle : MonoBehaviour
//{
//    public string GoogleAPI = "54199031251-rspt676dq7dfp7spol4729vg1eh3u7i2.apps.googleusercontent.com";
//    private GoogleSignInConfiguration configuration;
//    public DependencyStatus dependencyStatus;
//    FirebaseAuth auth;
//    FirebaseUser user;

//    public TextMeshProUGUI Username, UserEmail;

//    public Image UserProfilePic;
//    private string imageUrl;
//    private bool isGoogleSignInInitialized = false;
//    public GameObject loginPopup, loadingScreen;
//    public static bool isFirebaseInitiliazed = false;
//    private void Start()
//    {
//        if (PlayerPrefs.HasKey("EMAIL_ID"))
//        {
//            loginPopup.SetActive(false);        
//            loadingScreen.SetActive(true);
//        }
//        //  InitFirebase();
//        InitializeFirebase();
//    }

//    void InitFirebase()
//    {
//        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
//    }
//    void InitializeFirebase()
//    {
//        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
//        {
//            var dependencyStatus = task.Result;
//            if (dependencyStatus == DependencyStatus.Available)
//            {
//                Debug.Log("Firebase Auth initialized.");
//                auth = FirebaseAuth.DefaultInstance;
//                isFirebaseInitiliazed = true;
//                // auth.UseEmulator("localhost", 9099);

//               // System.Environment.SetEnvironmentVariable("USE_AUTH_EMULATOR", "true");
               
//            }
//            else
//            {
//                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
//            }
//        });
//    }

//    public void Login()
//    {       
//        if (!isGoogleSignInInitialized)
//        {
//            GoogleSignIn.Configuration = new GoogleSignInConfiguration
//            {
//                RequestIdToken = true,
//                WebClientId = GoogleAPI,
//                RequestEmail = true
//            };

//            isGoogleSignInInitialized = true;
//        }
//        if (GoogleSignIn.Configuration == null)
//        {
//            GoogleSignIn.Configuration = new GoogleSignInConfiguration
//            {
//                RequestIdToken = true,
//                WebClientId = GoogleAPI
//            };
//        }
//        GoogleSignIn.Configuration.RequestEmail = true;

//        Task<GoogleSignInUser> signIn = GoogleSignIn.DefaultInstance.SignIn();

//        TaskCompletionSource<FirebaseUser> signInCompleted = new TaskCompletionSource<FirebaseUser>();
//        signIn.ContinueWith(task =>
//        {
//            if (task.IsCanceled)
//            {
//                signInCompleted.SetCanceled();
//                Debug.Log("Cancelled");
//            }
//            else if (task.IsFaulted)
//            {
//                signInCompleted.SetException(task.Exception);

//                Debug.Log("Faulted " + task.Exception);
//            }
//            else
//            {
//                Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(((Task<GoogleSignInUser>)task).Result.IdToken, null);
//                auth.SignInWithCredentialAsync(credential).ContinueWith(authTask =>
//                {
//                    if (authTask.IsCanceled)
//                    {
//                        signInCompleted.SetCanceled();
//                    }
//                    else if (authTask.IsFaulted)
//                    {
//                        signInCompleted.SetException(authTask.Exception);
//                        Debug.Log("Faulted In Auth " + task.Exception);
//                    }
//                    else
//                    {
//                        signInCompleted.SetResult(((Task<FirebaseUser>)authTask).Result);
//                        Debug.Log("Success");
//                        user = auth.CurrentUser;
//                        loginPopup.SetActive(false);                       
//                        loadingScreen.SetActive(true);
//                        PlayerPrefs.SetString("USERID", user.UserId);
//                        PlayerPrefs.SetString("USERNAME", user.DisplayName);
//                        PlayerPrefs.SetString("EMAIL_ID", "Set");
//                        FirebaseData.instance.DateLoadFunc();
                        
                        
//                        // Username.text = user.DisplayName;
//                        // UserEmail.text = user.Email;

//                        // StartCoroutine(LoadImage(CheckImageUrl(user.PhotoUrl.ToString())));
//                    }
//                });
//            }
//        });       
//    }

//   public void PlayAsGuest()
//    {
//        PlayerPrefs.SetString("GUEST", "Set");
//        PlayerPrefs.SetString("EMAIL_ID", "Set");
//        PlayerPrefs.SetInt("COINS", 1000);
//        PlayerPrefs.SetInt("SCRAB_POWERUP", 1);
//        PlayerPrefs.SetInt("HINT_POWERUP", 1);
//        PlayerPrefs.SetInt("LOTUS_POWERUP", 1);
//        loginPopup.SetActive(false);
//        loadingScreen.SetActive(true);
//    }
//    private string CheckImageUrl(string url)
//    {
//        if (!string.IsNullOrEmpty(url))
//        {
//            return url;
//        }
//        return imageUrl;
//    }

//    IEnumerator LoadImage(string imageUri)
//    {
//        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUri);
//        yield return www.SendWebRequest();

//        if (www.result == UnityWebRequest.Result.Success)
//        {
//            Texture2D texture = DownloadHandlerTexture.GetContent(www);
//            // Use the loaded texture here
//            Debug.Log("Image loaded successfully");
//            UserProfilePic.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
//        }
//        else
//        {
//            Debug.Log("Error loading image: " + www.error);
//        }


//    }
//}