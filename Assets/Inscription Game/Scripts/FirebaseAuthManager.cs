//using System.Collections;
//using UnityEngine;
//using Firebase;
//using Firebase.Auth;
//using Firebase.Extensions;
//using UnityEngine.UI;

//public class FirebaseAuthManager : MonoBehaviour
//{
//    public static bool isFirebaseInitiliazed = false;
//    [Header("Firebase")]
//    public DependencyStatus dependencyStatus;
//    public FirebaseAuth auth;
//    public FirebaseUser user;

//    [Space]
//    [Header("Login")]
//    public InputField emailLoginField;
//    public InputField passwordLoginField;

//    [Space]
//    [Header("Registration")]
//    public InputField nameRegisterField;
//    public InputField emailRegisterField;
//    public InputField passwordRegisterField;
//    public InputField confirmPasswordRegisterField;

//    public Text des_Text;
//    public GameObject loginPopup, registrationPopup, loadingScreen, loading_Panel;
//    void Start()
//    {
//        //if (PlayerPrefs.HasKey("EMAIL_ID"))
//        //{
//        //    registrationPopup.SetActive(false);
//        //    loginPopup.SetActive(false);
//        //    loadingScreen.SetActive(true);
//        //}
//        InitializeFirebase();
//    }

//    void InitializeFirebase()
//    {
//        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
//        {
//            var dependencyStatus = task.Result;
//            if (dependencyStatus == DependencyStatus.Available)
//            {
//                auth = FirebaseAuth.DefaultInstance;
//                Debug.Log("Firebase Auth initialized.");
//                isFirebaseInitiliazed = true;
//                // auth.UseEmulator("localhost", 9099);

//                // System.Environment.SetEnvironmentVariable("USE_AUTH_EMULATOR", "true");

//            }
//            else
//            {
//                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
//            }
//        });
//    }

//    public void SignInAnonymously()
//    {
//        //auth.SignInAnonymouslyAsync().ContinueWithOnMainThread(task =>
//        //{
//        //    if (task.IsCanceled || task.IsFaulted)
//        //    {
//        //        Debug.LogError("Anonymous sign-in failed.");
//        //        return;
//        //    }

//        //    user = task.Result;
//        //    Debug.Log("User signed in anonymously: " + user.UserId);
//        //});
//    }

//    public void CreateUser(string email, string password)
//    {
//        //auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
//        //{
//        //    if (task.IsCanceled || task.IsFaulted)
//        //    {
//        //        Debug.LogError("User creation failed: " + task.Exception);
//        //        return;
//        //    }

//        //    user = task.Result;
//        //    Debug.Log("User created: " + user.Email);
//        //});
//    }
//    public void Register()
//    {
//        des_Text.text = "";
//        StartCoroutine(RegisterAsync(nameRegisterField.text, emailRegisterField.text, passwordRegisterField.text, confirmPasswordRegisterField.text));
//    }
//    public IEnumerator RegisterAsync(string name, string email, string password, string confirmPswd)
//    {
//        string failedMessage = "";
//        if (name == "")
//        {
//            // Debug.Log("User name is empty");
//            failedMessage = "User name is empty";
//        }
//        else if (email == "")
//        {
//            // Debug.Log("Email field is empty");
//            failedMessage = "Email field is empty";
//        }
//        else if (passwordRegisterField.text != confirmPasswordRegisterField.text)
//        {
//            // Debug.Log("Password doesn't match");
//            failedMessage = "Password doesn't match";
//        }
//        else if (passwordRegisterField.text.Length < 6)
//        {
//            // Debug.Log("Password length must be at least 6 characters/numbers.");
//            failedMessage = "Password length must be at least 6 characters/numbers.";
//        }
//        else
//        {
//            loading_Panel.SetActive(true);
//            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);
//            yield return new WaitUntil(() => registerTask.IsCompleted);

//            if (registerTask.Exception != null)
//            {
//                //  Debug.LogError(registerTask.Exception);

//                FirebaseException firebaseException = registerTask.Exception.GetBaseException() as FirebaseException;
//                AuthError authError = (AuthError)firebaseException.ErrorCode;
//                failedMessage = "Registraion Failed! Becuase ";

//                switch (authError)
//                {
//                    case AuthError.InvalidEmail:
//                        failedMessage += "Email is Invalid";
//                        break;
//                    case AuthError.WrongPassword:
//                        failedMessage += "Wrong Password";
//                        break;
//                    case AuthError.MissingEmail:
//                        failedMessage += "Email is Missing";
//                        break;
//                    case AuthError.MissingPassword:
//                        failedMessage += "Password is Missing";
//                        break;
//                    case AuthError.EmailAlreadyInUse:
//                        failedMessage += "This user email already exist";
//                        break;
//                    default:
//                        failedMessage = "Registraion Failed";
//                        break;
//                }
//                Debug.Log(failedMessage);
//            }
//            else
//            {
//                user = registerTask.Result.User;
//                Debug.LogFormat("{0} you are successfully login", user.DisplayName);
//                UserProfile userProfile = new UserProfile { DisplayName = name };
//                var updateProfileTask = user.UpdateUserProfileAsync(userProfile);

//                yield return new WaitUntil(() => updateProfileTask.IsCompleted);

//                if (updateProfileTask.Exception != null)
//                {
//                    user.DeleteAsync();
//                    //  Debug.LogError(updateProfileTask.Exception);

//                    FirebaseException firebaseException = updateProfileTask.Exception.GetBaseException() as FirebaseException;
//                    AuthError authError = (AuthError)firebaseException.ErrorCode;
//                    failedMessage = "Profile update failed! Becuase ";

//                    switch (authError)
//                    {
//                        case AuthError.InvalidEmail:
//                            failedMessage += "Email is Invalid";
//                            break;
//                        case AuthError.WrongPassword:
//                            failedMessage += "Wrong Password";
//                            break;
//                        case AuthError.MissingEmail:
//                            failedMessage += "Email is Missing";
//                            break;
//                        case AuthError.MissingPassword:
//                            failedMessage += "Password is Missing";
//                            break;
//                        default:
//                            failedMessage = "Profile update Failed";
//                            break;
//                    }
//                    Debug.Log(failedMessage);
//                }
//                else
//                {
//                    Debug.Log("Resgistraion successfully welcome" + user.DisplayName);
//                    registrationPopup.SetActive(false);
//                    loginPopup.SetActive(true);
//                }
//                PlayerPrefs.SetInt("COINS", 1000);
//                PlayerPrefs.SetInt("SCRAB_POWERUP", 1);
//                PlayerPrefs.SetInt("HINT_POWERUP", 1);
//                PlayerPrefs.SetInt("LOTUS_POWERUP", 1);
//                FirebaseData.instance.DataSaveFun();
//            }
//            loading_Panel.SetActive(false);

//        }
//        des_Text.text = failedMessage;

//    }

//    public void SignIn()
//    {
//        des_Text.text = "";
//        StartCoroutine(SignInWithEmail(emailLoginField.text, passwordLoginField.text));
//    }
//    public IEnumerator SignInWithEmail(string email, string password)
//    {
//        loading_Panel.SetActive(true);
//        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);
//        yield return new WaitUntil(() => loginTask.IsCompleted);
//        if (loginTask.Exception != null)
//        {
//            // Debug.LogError(loginTask.Exception);

//            FirebaseException firebaseException = loginTask.Exception.GetBaseException() as FirebaseException;
//            AuthError authError = (AuthError)firebaseException.ErrorCode;
//            string failedMessage = "Login Failed! Becuase ";

//            switch (authError)
//            {
//                case AuthError.InvalidEmail:
//                    failedMessage += "Email is Invalid";
//                    break;
//                case AuthError.WrongPassword:
//                    failedMessage += "Wrong Password";
//                    break;
//                case AuthError.MissingEmail:
//                    failedMessage += "Email is Missing";
//                    break;
//                case AuthError.MissingPassword:
//                    failedMessage += "Password is Missing";
//                    break;
//                default:
//                    failedMessage = "Login Failed";
//                    break;
//            }
//            Debug.Log(failedMessage);
//            des_Text.text = failedMessage;

//        }
//        else
//        {
//            user = loginTask.Result.User;
//            PlayerPrefs.SetString("USERNAME", user.DisplayName);
//            Debug.Log("UserId" + user.UserId);
//            PlayerPrefs.SetString("USERID", user.UserId);
//            Debug.LogFormat("{0} you are successfully login", user.DisplayName);
//            registrationPopup.SetActive(false);
//            loginPopup.SetActive(false);
//            loadingScreen.SetActive(true);
//            PlayerPrefs.SetString("EMAIL_ID", "Set");
//        }

//        loading_Panel.SetActive(false);

//        //    auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
//        //{
//        //    if (task.IsCanceled || task.IsFaulted)
//        //    {
//        //        Debug.LogError("Sign-in failed: " + task.Exception);
//        //        return;
//        //    }

//        //    user = task.Result;
//        //    Debug.Log("User signed in: " + user.Email);
//        //});
//    }

//    public void AlreadySignUpAndIn(bool isTrue)
//    {
//        des_Text.text = "";
//        if (isTrue)
//        {
//            registrationPopup.SetActive(false);
//            loginPopup.SetActive(true);
//        }
//        else
//        {
//            loginPopup.SetActive(false);
//            registrationPopup.SetActive(true);
//        }
//    }

//    public void SignOut()
//    {
//        auth.SignOut();
//        Debug.Log("User signed out.");
//    }

//}
