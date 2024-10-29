using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

namespace Reactional.Core
{
    [DefaultExecutionOrder(-100)]
    public class ReactionalManager : MonoBehaviour
    {
        public static ReactionalManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<ReactionalManager>();
                }

                return _instance;
            }
        }
        private static ReactionalManager _instance;

        [Tooltip("The default section to load when the project starts. String.")]
        public string defaultSection = "";

        [SerializeField] public List<Bundle> bundles = new List<Bundle>();
        public List<TrackInfo> _loadedThemes = new List<TrackInfo>();
        public List<TrackInfo> _loadedTracks = new List<TrackInfo>();

        [HideInInspector] public int lookahead = 0;
        [HideInInspector] public int selectedTheme = 0;
        [HideInInspector] public int selectedTrack = 0;
        [HideInInspector] public bool _reactionalLog = false;

        public enum PlaylistMode { Random, Sequential, Repeat, Single }

        [SerializeField]
        private PlaylistMode _playlistMode = PlaylistMode.Sequential;

        [SerializeField]
        private Setup.LoadType _loadType = Setup.LoadType.LoadInBackground;

        public Reactional.Setup.LoadType loadType
        {
            get
            {
                return _loadType;
            }
            set
            {
                _loadType = value;
            }
        }

        private float m_themeGain = 1;
        private float m_trackGain = 1;

        [HideInInspector] public bool isDucked;        

        public Setup.LoadType LoadType
        {
            get
            {
                return _loadType;
            }
            set
            {
                _loadType = value;
            }
        }

        public float themeGain
        {
            get
            {
                return m_themeGain;
            }
            set
            {
                if (Reactional.Playback.MusicSystem.GetEngine() != null)
                    Reactional.Playback.Theme.Volume = value;
                m_themeGain = value;
            }
        }
        
        public float trackGain
        {
            get
            {
                return m_trackGain;
            }
            set
            {
                if (Reactional.Playback.MusicSystem.GetEngine() != null)
                    Reactional.Playback.Playlist.Volume = value;
                m_trackGain = value;
            }
        }

        public UnityEngine.Audio.AudioMixerGroup mainOut;

        private void Start()
        {
            if (Application.isPlaying)
            {            
                ReactionalEngine.Instance.output.outputAudioMixerGroup = mainOut;
                Reactional.Playback.Theme.Volume = m_themeGain;
                Reactional.Playback.Playlist.Volume = m_trackGain;
                ReactionalEngine.Instance.onAudioEnd += AudioEnd;                
            }
        }

        void OnEnable()
        {
            if (!Application.isPlaying)
            {
                UpdateBundles();
            }
        }

        void OnDisable()
        {
            if (Application.isPlaying)
                ReactionalEngine.Instance.onAudioEnd -= AudioEnd;
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void UpdateBundles()
        {

            if (!Directory.Exists(Application.persistentDataPath + "/Reactional"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/Reactional");
            }

            string[] folders = null;

            try
            {
                var persistentFolders = new string[0];
                var streamingFolders = new string[0];

                if (Directory.Exists(Application.persistentDataPath + "/Reactional"))
                {
                    persistentFolders = Directory.GetDirectories(Application.persistentDataPath + "/Reactional");
                    if (Application.platform == RuntimePlatform.Android)
                    {
                        for (int i = 0; i < persistentFolders.Length; i++)
                        {                            
                            persistentFolders[i] = "file://" + persistentFolders[i];
                        }
                    }
                }

                if (Directory.Exists(Application.streamingAssetsPath + "/Reactional"))
                {
                    streamingFolders = Directory.GetDirectories(Application.streamingAssetsPath + "/Reactional");
                }

                folders = persistentFolders.Concat(streamingFolders).ToArray();

                if (folders.Length == 0)
                {
                    Debug.LogWarning("Reactional: No bundles found in " + Application.persistentDataPath + "/Reactional or " + Application.streamingAssetsPath + "/Reactional");
                    return;
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error accessing directories: " + ex.Message);
                return;
            }

            
            if(Application.platform != RuntimePlatform.Android)
                bundles.Clear();
            _loadedTracks.Clear();
            _loadedThemes.Clear();

            foreach (string folder in folders)
            {
                Debug.Log("BundlePath: " + folder);
                
                Bundle bundle = new Bundle();
                var contents = AssetHelper.ParseBundle(folder);
                bundle.name = Path.GetFileName(folder);
                bundle.path = folder;
                
                bundle.sections = contents;                
                bundles.Add(bundle);
            }
        }
        private void AudioEnd()
        {
            switch (_playlistMode)
            {
                case PlaylistMode.Random:
                    Reactional.Playback.Playlist.Random();
                    break;
                case PlaylistMode.Sequential:
                    Reactional.Playback.Playlist.Next();
                    break;
                case PlaylistMode.Repeat:
                    Reactional.Playback.Playlist.Play();
                    break;
                case PlaylistMode.Single:
                    break;
            }
        }
    }
}
