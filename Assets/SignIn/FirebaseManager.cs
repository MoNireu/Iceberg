using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;
using Google;

namespace FirebaseSignIn
{
    public class FirebaseManager : MonoBehaviour
    {
        FirebaseAuth auth;

        public Text statusText;

        public string webClientId = "<your client id here>";

        private GoogleSignInConfiguration configuration;
        public Button SignInBtn;

        // Defer the configuration creation until Awake so the web Client ID
        // Can be set via the property inspector in the Editor.
        void Awake()
        {
            auth = FirebaseAuth.DefaultInstance;
            configuration = new GoogleSignInConfiguration
            {
                WebClientId = webClientId,
                RequestIdToken = true
            };

            statusText = gameObject.GetComponentInChildren<Text>();
            SignInBtn = gameObject.GetComponentInChildren<Button>();            
        }

        private void Start()
        {
            SignInBtn.onClick.AddListener(OnSignIn);
        }

        public void OnSignIn()
        {
            AddStatusText("Btn Clicked");
            GoogleSignIn.Configuration = configuration;
            GoogleSignIn.Configuration.UseGameSignIn = false;
            GoogleSignIn.Configuration.RequestIdToken = true;
            AddStatusText("Calling SignIn");

            GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
              OnAuthenticationFinished);
        }

        public void OnSignOut()
        {
            AddStatusText("Calling SignOut");
            GoogleSignIn.DefaultInstance.SignOut();
        }

        public void OnDisconnect()
        {
            AddStatusText("Calling Disconnect");
            GoogleSignIn.DefaultInstance.Disconnect();
        }

        internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
        {
            if (task.IsFaulted)
            {
                using (IEnumerator<System.Exception> enumerator =
                        task.Exception.InnerExceptions.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        GoogleSignIn.SignInException error =
                                (GoogleSignIn.SignInException)enumerator.Current;
                        AddStatusText("Got Error: " + error.Status + " " + error.Message);
                    }
                    else
                    {
                        AddStatusText("Got Unexpected Exception?!?" + task.Exception);
                    }
                }
            }
            else if (task.IsCanceled)
            {
                AddStatusText("Canceled");
            }
            else
            {
                AddStatusText("Welcome: " + task.Result.DisplayName + "!");
                SignInWithGoogleOnFirebase(task.Result.IdToken);
            }
        }

        private void SignInWithGoogleOnFirebase(string idToken)
        {
            Credential credential = GoogleAuthProvider.GetCredential(idToken, null);

            auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
            {
                AggregateException ex = task.Exception;
                if (ex != null)
                {
                    if (ex.InnerExceptions[0] is FirebaseException inner && (inner.ErrorCode != 0))
                        AddStatusText("\nError code = " + inner.ErrorCode + " Message = " + inner.Message);
                }
                else
                {
                    AddStatusText("Sign In Successful.");
                    SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
                }
            });
        }

        public void OnSignInSilently()
        {
            GoogleSignIn.Configuration = configuration;
            GoogleSignIn.Configuration.UseGameSignIn = false;
            GoogleSignIn.Configuration.RequestIdToken = true;
            AddStatusText("Calling SignIn Silently");

            GoogleSignIn.DefaultInstance.SignInSilently()
                  .ContinueWith(OnAuthenticationFinished);
        }


        public void OnGamesSignIn()
        {
            GoogleSignIn.Configuration = configuration;
            GoogleSignIn.Configuration.UseGameSignIn = true;
            GoogleSignIn.Configuration.RequestIdToken = false;

            AddStatusText("Calling Games SignIn");

            GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
              OnAuthenticationFinished);
        }

        private List<string> messages = new List<string>();
        void AddStatusText(string text)
        {
            if (messages.Count == 5)
            {
                messages.RemoveAt(0);
            }
            messages.Add(text);
            string txt = "";
            foreach (string s in messages)
            {
                txt += "\n" + s;
            }
            statusText.text = txt;
        }
    }
}
