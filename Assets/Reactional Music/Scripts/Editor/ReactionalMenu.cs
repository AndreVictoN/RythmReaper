#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;
using Reactional.Core;
using UnityEditor.VSAttribution.Reactional;

namespace Reactional.Editor
{
    [InitializeOnLoad]
    public class ReactionalEditor : MonoBehaviour
    {
        const string dontAskAgainKey = "Reactional.DontShowAgain";
        const string firstTimeKey = "Reactional.FirstTime";
        private static bool firstTime = true;        

        static ReactionalEditor()
        {
            var reactionalAssetsPath = $"{Application.streamingAssetsPath}/Reactional";

            // check if folder exists, otherwise create it
            if (!Directory.Exists(reactionalAssetsPath))
            {
                Directory.CreateDirectory(reactionalAssetsPath);
            }
            
            EditorApplication.projectChanged += FirstTimePopup;
        }

        static void FirstTimePopup()
        {
            if (!EditorPrefs.GetBool(Application.productName + "." + "vsp_validated"))
            {
                Vector2 s_WindowSize = new Vector2(360, 408);
                var window = RegisterPlugin.GetWindow<RegisterPlugin>();

                window.titleContent = new GUIContent("Reactional - Validate");
                window.minSize = s_WindowSize;
                window.maxSize = s_WindowSize;
            } else {
                EditorApplication.projectChanged -= FirstTimePopup;
            }
        }

        static void CheckManager()
        {
            if (firstTime && EditorPrefs.GetBool(Application.productName + "." + dontAskAgainKey, true))
            {
                //EditorPrefs.SetBool(firstTimeKey, false);
                var rm = FindObjectOfType<ReactionalManager>();
                if (rm == null)
                {
                    int option = EditorUtility.DisplayDialogComplex("Welcome to Reactional",
                        "Would you like to add Reactional to the current scene?", "Yes", "No", "Don't show again");
                    switch (option)
                    {
                        case 0:
                            AddReactionalManager();
                            break;
                        case 2:
                            EditorPrefs.SetBool(Application.productName + "." + dontAskAgainKey, false);
                            break;
                    }
                }
            }
            firstTime = false;
        }

        [MenuItem("Tools/Reactional/Add Reactional Manager")]
        static void AddReactionalManager()
        {
            var rm = FindObjectOfType<ReactionalManager>();
            if (rm != null)
            {
                Debug.LogWarning("Reactional Manager already exists in the scene.");
                return;
            }

            // Create the Reactional Music GameObject and add the ReactionalManager and BasicPlayback components
            GameObject reactionalMusic = new GameObject("Reactional Music");
            reactionalMusic.AddComponent<ReactionalManager>();
            reactionalMusic.AddComponent<BasicPlayback>();

            // Create the Reactional Engine child GameObject and add the ReactionalEngine script
            GameObject reactionalEngine = new GameObject("Reactional Engine");
            reactionalEngine.AddComponent<ReactionalEngine>();
            reactionalEngine.transform.SetParent(reactionalMusic.transform);
        }

        [MenuItem("Tools/Reactional/Music Platform", false, 97)]
        static void OpenReactionalPlatform() => Application.OpenURL("https://app.reactionalmusic.com/");

        [MenuItem("Tools/Reactional/Documentation", false, 98)]
        static void OpenReactionalDocumentation() => Application.OpenURL("https://docs.reactionalmusic.com/Unity/");

        [MenuItem("Tools/Reactional/Support", false, 99)]
        static void OpenReactionalSupport() => Application.OpenURL("https://docs.reactionalmusic.com/support/");

        public class AboutReactional : EditorWindow 
        {
            static readonly Vector2 s_WindowSize = new Vector2(360, 408);

            [MenuItem("Tools/Reactional/About", false, 100)]
            public static void Initialize()
            {
                var window = GetWindow<AboutReactional>();

                window.titleContent = new GUIContent("Reactional - About");
                window.minSize = s_WindowSize;
                window.maxSize = s_WindowSize;
            }

            public void OnGUI()
            {
                const int uniformPadding = 16;
                var padding = new RectOffset(uniformPadding, uniformPadding, uniformPadding, uniformPadding);
                var area = new Rect(padding.right, padding.top, position.width - (padding.right + padding.left), position.height - (padding.top + padding.bottom));

                GUILayout.BeginArea(area);
                {
                    GUILayout.Space(16f);
                    GUILayout.Label((Texture2D)Resources.Load("ReactionalLogo"));
                    GUILayout.Space(16f);

                    GUILayout.Label("Reactional Music SDK for Unity", EditorStyles.boldLabel);
                    GUILayout.Label(Reactional.Setup.PluginVersion);

                    GUILayout.Space(16f);

                    GUILayout.Label("© 2024 Reactional Music", EditorStyles.boldLabel);
                    GUILayout.Label("All rights reserved.");

                    GUILayout.Space(16f);

                    if (GUILayout.Button("Visit Website"))
                    {
                        OpenReactionalPlatform();
                    }
                }
                GUILayout.EndArea();
            }
        }

        public class RegisterPlugin : EditorWindow
        {
            static readonly Vector2 s_WindowSize = new Vector2(360, 408);

            public string actionName = "Register";
            public string partnerName = "ReactionalMusic";
            public string customerUid;

            public string customerEmail = "";

            [MenuItem("Tools/Reactional/Validate", false, 200)]
            public static void Initialize()
            {
                var window = GetWindow<RegisterPlugin>();

                window.titleContent = new GUIContent("Reactional - Validate");
                window.minSize = s_WindowSize;
                window.maxSize = s_WindowSize;
            }

            public void OnGUI()
            {
                const int uniformPadding = 16;
                var padding = new RectOffset(uniformPadding, uniformPadding, uniformPadding, uniformPadding);
                var area = new Rect(padding.right, padding.top, position.width - (padding.right + padding.left), position.height - (padding.top + padding.bottom));

                GUILayout.BeginArea(area);
                {
                    GUILayout.Space(16f);
                    GUILayout.Label((Texture2D)Resources.Load("ReactionalLogo"));
                    GUILayout.Space(16f);

                    if (!EditorPrefs.GetBool(Application.productName + "." + "vsp_validated", false))
                    {
                        GUILayout.Label("Enter the same e-mail that was used to sign up to\nthe Reactional Platform.");
                        GUILayout.Space(8f);

                        GUILayout.Label("E-mail: ", EditorStyles.boldLabel);
                        customerEmail = GUILayout.TextField(customerEmail);

                        GUILayout.Space(16f);

                        if (GUILayout.Button("Validate"))
                        {
                            if (customerEmail == "")
                            {
                                Debug.Log($"[Reactional Music] No e-mail address entered!");
                            }
                            else
                            {
                                customerUid = customerEmail;
                                var result = ReactionalVSAttribution.SendAttributionEvent(actionName, partnerName, customerUid);
                                Debug.Log($"[Reactional Music] Validation status: {result}!");
                                if (result == UnityEngine.Analytics.AnalyticsResult.Ok)
                                {
                                    EditorPrefs.SetBool(Application.productName + "." + "vsp_validated", true);
                                    Close();
                                }
                            }
                        }
                    }
                    else
                    {
                        GUILayout.Label("You have already validated your account.");
                        GUILayout.Space(8f);
                        if (GUILayout.Button("Close"))
                        {
                            Close();
                        }
                    }
                }
                GUILayout.EndArea();
            }
        }
    }
}
#endif
