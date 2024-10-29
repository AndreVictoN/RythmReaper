using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Reactional.Core;
using System;

namespace Reactional
{
    [DefaultExecutionOrder(550)]
    public static class Setup
    {

        public static string PluginVersion
        {
            get
            {
                return "1.0rc12";
            }
        }

        /// <summary>
        /// Get or Set the sample rate of the engine
        /// </summary>
        public static int Samplerate
        {
            get
            {
                var engine = ReactionalEngine.Instance.engine;
                var pindex = engine.FindParameter(-1, "sample_rate");
                return (int)engine.GetParameterInt(-1, pindex);
            }
            set
            {
                var engine = ReactionalEngine.Instance.engine;
                var pindex = engine.FindParameter(-1, "sample_rate");
                engine.SetParameterInt(-1, pindex, value);
            }
        }

        /// <summary>
        /// Get or Set the block size of the engine.
        /// Adjusting the block size may affect the engine's processing capabilities.
        /// </summary>
        /// <example>
        /// // Setting the BlockSize
        /// Reactional.Setup.BlockSize = 512;
        /// 
        /// // Getting the BlockSize
        /// int blockSize = Reactional.Setup.BlockSize;
        /// </example>
        public static int BlockSize
        {
            get
            {
                var engine = ReactionalEngine.Instance.engine;
                var pindex = engine.FindParameter(-1, "block_size");
                return (int)engine.GetParameterInt(-1, pindex);
            }
            set
            {
                var engine = ReactionalEngine.Instance.engine;
                var pindex = engine.FindParameter(-1, "block_size");
                engine.SetParameterInt(-1, pindex, value);
            }
        }

        /// <summary>
        /// Get or Set the lookahead parameter of the engine.
        /// Adjusting the lookahead value can influence the audio processing latency.
        /// </summary>
        /// <example>
        /// // Setting the Lookahead
        /// Reactional.Setup.Lookahead = 1;
        /// 
        /// // Getting the Lookahead
        /// int lookahead = Reactional.Setup.Lookahead;
        /// </example>
        public static int Lookahead
        {
            get
            {
                var engine = ReactionalEngine.Instance.engine;
                var pindex = engine.FindParameter(-1, "lookahead");
                return (int)engine.GetParameterInt(-1, pindex);
            }
            set
            {
                var engine = ReactionalEngine.Instance.engine;
                var pindex = engine.FindParameter(-1, "lookahead");
                engine.SetParameterInt(-1, pindex, value);
            }
        }

        /// <summary>
        /// Initialize audio settings with specified buffer size and sample rate.
        /// </summary>
        /// <param name="bufferSize">The buffer size to set, defaults to -1 for auto detection.</param>
        /// <param name="sampleRate">The sample rate to set, defaults to -1 for auto detection.</param>
        /// <example>
        /// Reactional.Setup.InitAudio();
        /// Reactional.Setup.InitAudio(512, 44100);
        /// </example>
        public static void InitAudio(int bufferSize = -1, int sampleRate = -1)
        {
            if (bufferSize == -1)
            {
                AudioSettings.GetDSPBufferSize(out int bufsize, out int numbufs);
                bufferSize = bufsize;
            }
            if (sampleRate == -1)
            {
                sampleRate = AudioSettings.outputSampleRate;
            }

            Reactional.Setup.BlockSize = bufferSize;
            Reactional.Setup.Samplerate = sampleRate;
            Reactional.Setup.Lookahead = ReactionalManager.Instance.lookahead;
        }

        /// <summary>
        /// Checks the validity of the ReactionalManager instance in the scene.
        /// Returns true if a valid instance exists; otherwise, logs an error and returns false.
        /// </summary>
        /// <example>
        /// bool valid = Reactional.Setup.IsValid;
        /// </example>
        public static bool IsValid
        {
            get
            {
                var rM = ReactionalManager.Instance;
                if (rM == null)
                {
                    Debug.LogWarning("Reactional: No ReactionalManager found in scene");
                    return false;
                }
                bool isValid = false;
                if (rM.bundles.Count > 0) isValid = true;
                return isValid;
            }
        }

        /// <summary>
        /// Log a message with a specified severity level.
        /// </summary>
        /// <param name="msg">The message to log.</param>
        /// <param name="level">The severity level (0=Log, 1=Warning, 2=Error).</param>
        /// <example>
        /// Reactional.Setup.ReactionalLog("Initialization complete.", 0);
        /// Reactional.Setup.ReactionalLog("Warning: Potential issue detected.", 1);
        /// </example>
        public static void ReactionalLog(string msg, int level = 0)
        {
            if (ReactionalManager.Instance._reactionalLog == false && level == 0) return;

            switch (level)
            {
                case 0:
                    Debug.Log("Reactional: " + msg);
                    break;
                case 1:
                    Debug.LogWarning("Reactional: " + msg);
                    break;
                case 2:
                    Debug.LogError("Reactional: " + msg);
                    break;
            }
        }

        /// <summary>
        /// Updates the bundles in the ReactionalManager.
        /// This method will check the StreamingAssets/Reactional folder for content.
        /// </summary>
        /// <example>
        /// Reactional.Setup.UpdateBundles();
        /// </example>
        public static void UpdateBundles()
        {
            ReactionalManager rM = ReactionalManager.Instance;
            rM.UpdateBundles();
        }

        /// <summary>
        /// Load all bundles asynchronously.
        /// </summary>
        /// <example>
        /// await Reactional.Setup.LoadBundles();
        /// </example>
        public static async Task LoadBundles()
        {
            var rM = ReactionalManager.Instance;

            foreach (var bundle in rM.bundles)
            {
                await LoadBundle(bundle.name);
            }
        }

        /// <summary>
        /// Load a specific bundle asynchronously by its name.
        /// </summary>
        /// <param name="bundleName">The name of the bundle to load.</param>
        /// <example>
        /// await Reactional.Setup.LoadBundle("MyMusicProject_12030");
        /// </example>
        public static async Task LoadBundle(string bundleName)
        {
            var rM = ReactionalManager.Instance;

            var bundle = rM.bundles.Find(x => x.name.Equals(bundleName));
            if (bundle == null)
            {
                ReactionalLog("Bundle not found: " + bundleName, 1);
                return;
            }

            foreach (var section in bundle.sections)
            {
                await LoadSection(bundleName, section.name);

            }
        }

        /// <summary>
        /// Load a specific section of a bundle asynchronously.
        /// </summary>
        /// <param name="bundleName">The name of the bundle containing the section.</param>
        /// <param name="sectionName">The name of the section to load.</param>
        /// <example>
        /// await Reactional.Setup.LoadSection("MyMusicProject_12030", "Level2");
        /// </example>
        public static async Task LoadSection(string bundleName, string sectionName)
        {
            var rM = ReactionalManager.Instance;
            var bundle = rM.bundles.Find(x => x.name.Equals(bundleName));
            if (bundle == null)
            {
                ReactionalLog("Bundle not found: " + bundleName, 1);
                return;
            }
            var section = bundle.sections.Find(x => x.name.Equals(sectionName));
            if (section == null)
            {
                ReactionalLog("Section not found: " + sectionName, 1);
                return;
            }

            await LoadTheme(bundleName, sectionName, section.themes[0].trackName);

            foreach (var playlist in section.playlists)
            {
                await LoadPlaylist(bundleName, sectionName, playlist.name);
            }
        }

        /// <summary>
        /// Load the default section asynchronously.
        /// </summary>
        /// <example>
        /// await Reactional.Setup.LoadSection();
        /// </example>
        public static async Task LoadSection()
        {
            var rM = ReactionalManager.Instance;
            string defaultSection = rM.defaultSection;
            if (defaultSection == "")
            {
                defaultSection = rM.bundles[0].sections[0].name;
            }

            // find section in bundles
            foreach (var bundle in rM.bundles)
            {
                foreach (var section in bundle.sections)
                {
                    if (section.name == defaultSection)
                    {
                        await LoadSection(bundle.name, section.name);
                        return;
                    }
                }
            }

            await Setup.LoadTheme();
            await LoadPlaylist();
        }

        /// <summary>
        /// Load a specific playlist asynchronously.
        /// </summary>
        /// <param name="bundleName">The name of the bundle containing the section.</param>
        /// <param name="sectionName">The name of the section containing the playlist.</param>
        /// <param name="playlistName">The name of the playlist to load.</param>
        /// <example>
        /// await Reactional.Setup.LoadPlaylist("MyMusicProject_12030", "Level2", "DefaultPlaylist");
        /// </example>
        public static async Task LoadPlaylist(string bundleName, string sectionName, string playlistName)
        {
            var rM = ReactionalManager.Instance;
            var bundle = rM.bundles.Find(x => x.name.Equals(bundleName));
            var section = bundle.sections.Find(x => x.name.Equals(sectionName));
            var playlist = section.playlists.Find(x => x.name.Equals(playlistName));
            if (playlist == null)
            {
                ReactionalLog("Playlist not found: " + playlistName, 1);
                return;
            }

            foreach (var track in playlist.tracks)
            {
                await LoadTrack(bundleName, track.trackName);
            }
        }

        /// <summary>
        /// Load a playlist asynchronously by its name.
        /// </summary>
        /// <param name="bundleName">The name of the bundle containing the playlist.</param>
        /// <param name="playlistName">The name of the playlist to load.</param>
        /// <example>
        /// await Reactional.Setup.LoadPlaylist("MyMusicProject_12030", "DefaultPlaylist");
        /// </example>
        public static async Task LoadPlaylist(string bundleName, string playlistName)
        {
            bool foundPlaylist = false;

            ReactionalManager rM = ReactionalManager.Instance;
            Reactional.Core.Engine engine = ReactionalEngine.Instance.engine;

            var bundle = rM.bundles.Find(x => x.name.Equals(bundleName));
            foreach (var section in bundle.sections)
            {
                var playlist = section.playlists.Find(y => y.name.Equals(playlistName));
                if (playlist != null)
                {
                    foundPlaylist = true;
                    ReactionalLog("Found Playlist \"" + playlistName + "\". Loading.");
                    foreach (var track in playlist.tracks)
                    {
                        await LoadTrack(bundleName, track.trackName);
                    }
                    ReactionalLog("Load finished.");
                }
            }
            if (!foundPlaylist)
                ReactionalLog("No playlist found.", 1);
        }

        /// <summary>
        /// Loads a playlist by its index in the loaded bundles list.
        /// </summary>
        /// <param name="bundleIndex">The index of the bundle containing the playlist.</param>
        /// <param name="playlistName">The name of the playlist to load.</param>
        public static async Task LoadPlaylist(int bundleIndex, string playlistName)
        {
            ReactionalManager rM = ReactionalManager.Instance;
            var bundles = rM.bundles;
            if (bundleIndex >= bundles.Count)
            {
                Debug.LogWarning("Reactional: Bundle index out of range");
                return;
            }
            var bundle = bundles[bundleIndex];
            await LoadPlaylist(bundle.name, playlistName);
        }

        /// <summary>
        /// Loads a playlist by its name, optionally using the default section if no name is provided.
        /// </summary>
        /// <param name="playlistName">The name of the playlist to load, defaults to an empty string to use the default section.</param>
        public static async Task LoadPlaylist(string playlistName = "")
        {
            ReactionalManager rM = ReactionalManager.Instance;
            var bundles = rM.bundles;
            string bundleName = "";
            if (playlistName == "" && rM.defaultSection == "")
            {
                playlistName = bundles?[0]?.sections?[0]?.playlists?[0]?.name ?? "";
                bundleName = bundles[0].name;
            }
            else if (playlistName == "")
            {
                foreach (var bundle in bundles)
                {
                    foreach (var section in bundle.sections)
                    {
                        if (section.name == rM.defaultSection)
                        {
                            playlistName = section.playlists?.Count > 0 ? section.playlists[0]?.name : "";
                            bundleName = bundle.name;
                        }
                    }
                }
            }

            if (bundleName == "")
            {
                Debug.LogWarning("Reactional: Default Section not found, falling back to first bundle");
                bundleName = bundles[0].name;
                playlistName = bundles[0].sections[0].playlists[0].name;
            }

            await LoadPlaylist(bundleName, playlistName);
        }

        private static readonly object trackLock = new object();

        /// <summary>
        /// Loads a specific track by its name from a given bundle.
        /// </summary>
        /// <param name="bundleName">The name of the bundle containing the track.</param>
        /// <param name="trackName">The name of the track to load.</param>
        public static async Task LoadTrack(string bundleName, string trackName)
        {
            ReactionalManager rM = ReactionalManager.Instance;
            Reactional.Core.Engine engine = ReactionalEngine.Instance.engine;

            var bundle = rM.bundles.Find(x => x.name.Equals(bundleName));

            TrackInfo track = null;
            foreach (var section in bundle.sections)
            {
                foreach (var playlist in section.playlists)
                {
                    track = playlist.tracks.Find(y => y.trackName.Equals(trackName));
                    if (track != null)
                    {
                        ReactionalLog("Found Track \"" + trackName + "\". Loading.");

                        // Check if track exists in loaded tracks
                        if (rM._loadedTracks.Find(x => x.trackName == trackName) != null)
                        {
                            ReactionalLog("Track already loaded: " + trackName, 0);
                            return;
                        }
                        string path;
                        if (bundle.path.Contains("StreamingAssets"))
                            path = Application.streamingAssetsPath + "/Reactional/" + track.bundleID;
                        else
                            path = bundle.path;
                        string trackData = AssetHelper.GetTrackAsString(System.IO.Path.Combine(path, track.trackHash));
                        track.trackID = engine.AddTrackFromString(trackData);
                        if (track.trackID >= 0)
                        {
                            await AssetHelper.LoadTrackAssets(engine, track.trackID, Application.streamingAssetsPath + "/Reactional/" + track.bundleID, loadAsync: rM.loadType == Setup.LoadType.LoadInBackground ? true : false);
                        }
                        lock (trackLock)
                        {
                            rM._loadedTracks.Add(track);
                        }
                        ReactionalLog("Load finished.");
                        return;
                    }
                }
            }
            if (track == null)
                ReactionalLog("No track found:" + trackName, 1);
        }

        /// <summary>
        /// Loads a specific theme by its name from a given section and bundle.
        /// </summary>
        /// <param name="bundleName">The name of the bundle containing the theme.</param>
        /// <param name="sectionName">The name of the section containing the theme.</param>
        /// <param name="themeName">The name of the theme to load.</param>
        public static async Task LoadTheme(string bundleName, string sectionName, string themeName)
        {
            ReactionalManager rM = ReactionalManager.Instance;
            Reactional.Core.Engine engine = ReactionalEngine.Instance.engine;

            var bundle = rM.bundles.Find(x => x.name.Equals(bundleName));
            var section = bundle.sections.Find(x => x.name.Equals(sectionName));
            var theme = section.themes.Find(x => x.trackName.Equals(themeName));

            if (theme == null)
            {
                ReactionalLog("Theme not found: " + themeName, 1);
                return;
            }

            // check if theme exists in loaded themes
            if (rM._loadedThemes.Find(x => x.trackName == themeName) != null)
            {
                ReactionalLog("Theme already loaded: " + themeName, 0);
                return;
            }

            string path;
            if (bundle.path.Contains("StreamingAssets"))
                path = Application.streamingAssetsPath + "/Reactional/" + bundleName;
            else
                path = bundle.path;
            string trackData = AssetHelper.GetTrackAsString(System.IO.Path.Combine(path, theme.trackHash));
            theme.trackID = engine.AddTrackFromString(trackData);
            await AssetHelper.LoadTrackAssets(engine, theme.trackID, path, loadAsync: rM.loadType == Setup.LoadType.LoadInBackground ? true : false);
            if (Playback.Theme.GetState() == Playback.MusicSystem.PlaybackState.Stopped)
                engine.SetTheme(theme.trackID);
            rM._loadedThemes.Add(theme);
        }

        /// <summary>
        /// Loads a theme by its name, optionally using the default section if no name is provided.
        /// </summary>
        /// <param name="themeName">The name of the theme to load, defaults to an empty string to use the default section.</param>
        public static async Task LoadTheme(string themeName = "")
        {
            if (themeName == "" && ReactionalManager.Instance.defaultSection == "")
            {
                themeName = ReactionalManager.Instance.bundles[0].sections[0].themes[0].trackName;
            }

            foreach (var bundle in ReactionalManager.Instance.bundles)
            {
                foreach (var section in bundle.sections)
                {
                    if (themeName == "" && section.name == ReactionalManager.Instance.defaultSection)
                        themeName = section.themes[0].trackName;

                    foreach (var theme in section.themes)
                    {
                        if (theme.trackName == themeName)
                        {
                            await LoadTheme(bundle.name, section.name, themeName);
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the permission to play.
        /// </summary>
        public static bool AllowPlay
        {
            get
            {
                if (ReactionalEngine.Instance == null)
                    return false;
                else
                    return ReactionalEngine.Instance._allowPlay;
            }
            set
            {
                ReactionalEngine.Instance._allowPlay = value;
            }
        }

        public enum LoadType
        {
            LoadInBackground = 0,
            Synchronous = 1,
        }

        public enum UpdateMode
        {
            Main = 0,
            Threaded = 1,
            AudioThread = 2
        }

        public enum AudioOutputMode
        {
            Unity = 0,
            Custom = 1
        }

        public class ParameterRecorder
        {
            [System.Serializable]
            public class ParameterRecord
            {
                public float time;
                public float value;
            }

            [System.Serializable]
            public class ParameterRecordParent
            {
                public string name;
                public int id;
                public List<ParameterRecord> records = new List<ParameterRecord>();
            }

            [System.Serializable]
            public class ParameterRecordList
            {
                public List<ParameterRecordParent> records = new List<ParameterRecordParent>();
            }


            public ParameterRecordList records = new ParameterRecordList();

            public void Record(string name, int id, float time, float value)
            {
                if (records.records.Find(x => x.name == name) == null)
                {
                    records.records.Add(new ParameterRecordParent() { name = name });
                }
                var parent = records.records.Find(x => x.name == name);
                parent.records.Add(new ParameterRecord() { time = time, value = value });
            }

            public void Clear()
            {
                records.records.Clear();
            }

            public void Save(string path)
            {
                var json = JsonUtility.ToJson(records);
                System.IO.File.WriteAllText(path, json);
            }
        }
    }

    namespace Playback
    {
        [DefaultExecutionOrder(200)]
        public static class Theme
        {

            /// <summary>
            /// Gets the ID of the current theme.
            /// </summary>
            /// <returns>The theme ID, or -1 if no theme is loaded.</returns>
            public static int GetThemeID()
            {
                if (ReactionalEngine.Instance == null) return -1;
                var theme = ReactionalEngine.Instance.engine.GetTheme();
                return theme;
            }

            /// <summary>
            /// Checks if a theme is loaded.
            /// </summary>
            /// <returns>True if a theme is loaded, false otherwise.</returns>
            public static bool IsLoaded()
            {
                var theme = ReactionalEngine.Instance.engine.GetTheme();
                bool loaded = theme < 0 ? false : true;
                return loaded;
            }

            /// <summary>
            /// Plays the current theme.
            /// </summary>
            public static void Play()
            {
                Engine engine = ReactionalEngine.Instance.engine;

                int trackID = Reactional.Playback.Theme.GetThemeID();

                if (Theme.GetState() == MusicSystem.PlaybackState.Playing)
                {
                    if (CurrentBeat() < 0f || Theme.GetCurrentPart() == -1 || Theme.GetPartName(Theme.GetCurrentPart()) == "part: silence")
                    {
                        Theme.SetControl(GetPartName(0), 1f);
                    }
                    return;
                }

                if (trackID < 0)
                {
                    Debug.Log("Reactional: No theme loaded");
                    return;
                }
                int trk = Reactional.Playback.Playlist.GetTrackID();
                if (trk >= 0)
                {
                    Reactional.Playback.Playlist.CopyPrerollToTheme(trk);
                }

                int pindex = engine.FindParameter(trackID, "status");
                engine.SetParameterInt(trackID, pindex, 1);
            }

            /// <summary>
            /// Resets the current theme.
            /// </summary>
            public static void Reset()
            {
                int trackID = Reactional.Playback.Theme.GetThemeID();
                if (trackID < 0) return;
                ReactionalEngine.Instance.engine.ResetTrack(trackID);
            }

            /// <summary>
            /// Stops the current theme.
            /// </summary>
            public static void Stop(bool forceStop = false)
            {
                if (!Playlist.IsPlaying || forceStop)
                {
                    var trackID = Reactional.Playback.Theme.GetThemeID();
                    var engine = ReactionalEngine.Instance.engine;
                    var pindex = engine.FindParameter(trackID, "status");
                    engine.SetParameterInt(trackID, pindex, 0);
                }
                else
                {
                    Theme.SetControl("part: silence", 0f);
                }
            }

            /// <summary>
            /// Gets the state of the current theme.
            /// </summary>
            /// <returns>The state of the theme.</returns>
            private static int State
            {
                get
                {
                    if (ReactionalEngine.Instance == null) return 0;
                    var trackID = Reactional.Playback.Theme.GetThemeID();
                    var engine = ReactionalEngine.Instance.engine;
                    var pindex = engine.FindParameter(trackID, "status");
                    if (pindex < 0)
                        return 0;
                    else
                        return (int)engine.GetParameterInt(trackID, pindex);
                }
            }

            /// <summary>
            /// Gets the state of the current theme.
            /// </summary>
            /// <returns>The state of the theme.</returns>
            public static MusicSystem.PlaybackState GetState()
            {
                return (MusicSystem.PlaybackState)State;
            }

            /// <summary>
            /// Overrides the instrument settings for the theme.
            /// </summary>
            /// <param name="instrumentName">Name of the instrument.</param>
            /// <param name="pulseRate">Pulse rate for the instrument.</param>
            /// <param name="pitch">Pitch of the instrument.</param>
            /// <param name="velocity">Velocity of the instrument.</param>
            /// <param name="active">Whether the instrument is active.</param>
            /// <param name="legato">Legato value for the instrument.</param>
            public static void InstrumentOverride(string instrumentName, float pulseRate, float pitch, float velocity, bool active, float legato = 1f)
            {
                string name = "Playable Performer: " + instrumentName;

                var engine = ReactionalEngine.Instance.engine;
                int trackID = engine.GetTheme();
                int control = engine.FindControl(trackID, name);

                if (control < 0)
                {
                    Debug.Log("Reactional Control not found: " + name);
                    return;
                }

                double[] values = new double[5];
                values[0] = active ? 1 : 0;
                values[1] = pulseRate;
                values[2] = pitch;
                values[3] = velocity;
                values[4] = legato;

                engine.SetControlValue(trackID, control, values);
            }

            /// <summary>
            /// Gets a list of overridable instruments in the theme.
            /// </summary>
            /// <returns>A list of overridable instruments.</returns>
            public static List<string> GetOverridableInstruments()
            {
                var engine = ReactionalEngine.Instance.engine;
                int trackID = engine.GetTheme();
                List<string> ctrls = new List<string>();
                for (int i = 0; i < engine.GetNumControls(trackID); i++)
                {
                    string c = engine.GetControlName(trackID, i);
                    if (c.StartsWith("Playable Performer: "))
                    {
                        c = c.Substring(20);
                        ctrls.Add(c);
                    }
                }
                return ctrls;
            }

            /// <summary>
            /// Triggers a stinger in the theme.
            /// </summary>
            /// <param name="stingerName">The name of the stinger.</param>
            /// <param name="quant">The quantization value.</param>
            public static void TriggerStinger(string stingerName, MusicSystem.Quant quant = MusicSystem.Quant.Half)
            {
                SetControl(stingerName, (1f / (int)quant * 4f));
            }

            /// <summary>
            /// Triggers a stinger in the theme with a specific quantization value.
            /// </summary>
            /// <param name="stingerName">The name of the stinger.</param>
            /// <param name="value">The value to trigger.</param>
            public static void TriggerStinger(string stingerName, float value)
            {
                SetControl(stingerName, value);
            }

            /// <summary>
            /// Triggers a state change in the theme.
            /// </summary>
            /// <param name="stateName">The name of the state.</param>
            public static void TriggerState(string stateName)
            {
                SetControl(stateName, 1f);
            }

            /// <summary>
            /// Triggers a part change in the theme with a specific quantization value.
            /// </summary>
            /// <param name="partName">The name of the part.</param>
            public static void TriggerPart(string partName, float value = 1f)
            {
                SetControl("part: " + partName, value);
            }

            /// <summary>
            /// Gets a dictionary of controls for the theme or track.
            /// </summary>
            /// <param name="istheme">Whether to get controls for the theme or track.</param>
            /// <param name="engine">The engine instance.</param>
            /// <returns>A dictionary of controls with their values and types.</returns>
            public static Dictionary<string, (float controlValue, string controlType)> GetControls(bool istheme = true, Engine engine = null)
            {
                Dictionary<string, (float controlValue, string controlType)> ctrls = new Dictionary<string, (float controlValue, string controlType)>();

                if (engine == null)
                    engine = ReactionalEngine.Instance.engine;
                var trackID = istheme ? engine.GetTheme() : engine.GetTrack();
                for (int i = 0; i < engine.GetNumControls(trackID); i++)
                {
                    var level = engine.GetControlLevel(trackID, i);
                    if (level != "custom") continue;
                    string controlName = engine.GetControlName(trackID, i);
                    float controlValue = (float)engine.GetControlValue(trackID, i);
                    string controlType = engine.GetControlType(trackID, i);
                    ctrls.Add(controlName, (controlValue, controlType));
                }
                return ctrls;
            }

            /// <summary>
            /// Gets the current beat of the theme.
            /// </summary>
            /// <returns>The current beat.</returns>
            public static float CurrentBeat()
            {
                return ReactionalEngine.Instance.CurrentBeat;
            }

            /// <summary>
            /// Gets the current tempo of the theme in BPM.
            /// </summary>
            /// <returns>The current tempo in BPM.</returns>
            public static float TempoBpm()
            {
                return ReactionalEngine.Instance.TempoBpm;
            }

            /// <summary>
            /// Sets a control value in the theme or track.
            /// </summary>
            /// <param name="controlName">The name of the control.</param>
            /// <param name="value">The value to set.</param>
            /// <param name="istheme">Whether to set the control for the theme or track.</param>
            public static void SetControl(string controlName, float value = 1f, bool istheme = true)
            {
                var engine = ReactionalEngine.Instance.engine;

                int t;
                if (istheme)
                    t = engine.GetTheme();
                else
                    return;

                int control = engine.FindControl(t, controlName);
                if (control < 0)
                {
                    Debug.Log("Reactional Control not found: " + controlName);
                    return;
                }

                value = Mathf.Clamp(value, 0f, 1f);
                engine.SetControlValue(t, control, value);
            }

            /// <summary>
            /// Gets a control value from the theme or track.
            /// </summary>
            /// <param name="controlName">The name of the control.</param>
            /// <param name="istheme">Whether to get the control from the theme or track.</param>
            /// <returns>The value of the control.</returns>
            public static double GetControl(string controlName, bool istheme = true)
            {
                var engine = ReactionalEngine.Instance.engine;

                int t;
                if (istheme)
                    t = engine.GetTheme();
                else
                    t = engine.GetTrack();

                int control = engine.FindControl(t, controlName);
                if (control < 0)
                {                    
                    return -1;
                }

                return engine.GetControlValue(t, control);
            }

            /// <summary>
            /// Gets or sets the volume of the theme.
            /// </summary>
            public static float Volume
            {
                get
                {
                    var eng = MusicSystem.GetEngine();
                    return (float)eng.GetParameterFloat(-1, eng.FindParameter(-1, "theme_gain"));
                }
                set
                {
                    var eng = MusicSystem.GetEngine();
                    int gain = eng.FindParameter(-1, "theme_gain");
                    if (gain < 0) return;
                    eng.SetParameterFloat(-1, gain, value);
                }
            }

            /// <summary>
            /// Gets the number of parts in the theme.
            /// </summary>
            /// <returns>The number of parts.</returns>
            public static int GetNumParts()
            {
                var engine = ReactionalEngine.Instance.engine;
                int trackID = engine.GetTheme();
                return engine.GetNumParts(trackID);
            }

            /// <summary>
            /// Gets the current part index in the theme.
            /// </summary>
            /// <returns>The current part index.</returns>
            public static int GetCurrentPart()
            {
                var engine = ReactionalEngine.Instance.engine;
                int trackID = engine.GetTheme();
                return engine.GetCurrentPart(trackID);
            }


            /// <summary>
            /// Gets the name of a specific part of the theme.
            /// </summary>
            /// <param name="part">The part index.</param>
            /// <returns>The name of the part.</returns>
            public static string GetPartName(int part)
            {
                var engine = ReactionalEngine.Instance.engine;
                int trackID = engine.GetTheme();
                return engine.GetPartName(trackID, part);
            }

            /// <summary>
            /// Gets the offset of a specific part of the theme in seconds.
            /// </summary>
            /// <param name="part">The part index.</param>
            /// <returns>The offset of the part in seconds.</returns>
            public static float GetPartOffset(int part)
            {
                var engine = ReactionalEngine.Instance.engine;
                int trackID = engine.GetTheme();
                return (float)(engine.GetPartOffset(trackID, part) / 1000000f);
            }

            /// <summary>
            /// Gets the duration of a specific part of the theme in seconds.
            /// </summary>
            /// <param name="part">The part index.</param>
            /// <returns>The duration of the part in seconds.</returns>
            public static float GetPartDuration(int part)
            {
                var engine = ReactionalEngine.Instance.engine;
                int trackID = engine.GetTheme();
                return (float)(engine.GetPartDuration(trackID, part) / 1000000f);
            }

            /// <summary>
            /// Gets the number of bars in the theme.
            /// </summary>
            /// <returns>The number of bars.</returns>
            public static int GetNumBars()
            {
                var engine = ReactionalEngine.Instance.engine;
                int trackID = engine.GetTheme();
                return engine.GetNumBars(trackID);
            }

            /// <summary>
            /// Gets the current bar index in the theme.
            /// </summary>
            /// <returns>The current bar index.</returns>
            public static int GetCurrentBar()
            {
                var engine = ReactionalEngine.Instance.engine;
                int trackID = engine.GetTheme();
                return engine.GetCurrentBar(trackID);
            }

            /// <summary>
            /// Gets the offset of a specific bar in the theme in seconds.
            /// </summary>
            /// <param name="bar">The bar index.</param>
            /// <returns>The offset of the bar in seconds.</returns>
            public static float GetBarOffset(int bar)
            {
                var engine = ReactionalEngine.Instance.engine;
                int trackID = engine.GetTheme();
                return (float)(engine.GetBarOffset(trackID, bar) / 1000000f);
            }

            /// <summary>
            /// Gets the duration of a specific bar in the theme in seconds.
            /// </summary>
            /// <param name="bar">The bar index.</param>
            /// <returns>The duration of the bar in seconds.</returns>
            public static float GetBarDuration(int bar)
            {
                var engine = ReactionalEngine.Instance.engine;
                int trackID = engine.GetTheme();
                return (float)(engine.GetBarDuration(trackID, bar) / 1000000f);
            }
        }
        [DefaultExecutionOrder(550)]
        public static class Playlist
        {
            /// <summary>
            /// Gets the current beat of the playlist.
            /// </summary>
            /// <returns>The current beat.</returns>
            public static float CurrentBeat()
            {
                int trackID = Playlist.GetTrackID();
                var engine = ReactionalEngine.Instance.engine;
                var pindex = engine.FindParameter(trackID, "current_beat");
                return (float)engine.GetParameterFloat(trackID, pindex) / 1000000f;
            }

            /// <summary>
            /// Gets the tempo of the playlist in BPM.
            /// </summary>
            /// <returns>The tempo in BPM.</returns>
            public static float TempoBpm()
            {
                int trackID = Playlist.GetTrackID();
                var engine = ReactionalEngine.Instance.engine;
                var pindex = engine.FindParameter(trackID, "bpm");
                return (float)engine.GetParameterFloat(trackID, pindex);
            }

            /// <summary>
            /// Gets the ID of the current track.
            /// </summary>
            /// <returns>The track ID.</returns>
            public static int GetTrackID()
            {
                var track = ReactionalEngine.Instance.engine.GetTrack();
                return track;
            }

            /// <summary>
            /// Gets the metadata of the current track.
            /// </summary>
            /// <returns>The metadata of the track as a string.</returns>
            public static string GetCurrentTrackMetadata()
            {
                var track = ReactionalEngine.Instance.engine.GetTrack();
                var engine = ReactionalEngine.Instance.engine;
                var pindex = engine.FindParameter(track, "metadata");
                return (string)engine.GetParameterValue(track, pindex);
            }

            /// <summary>
            /// Gets the information of the current track.
            /// </summary>
            /// <returns>The TrackInfo object of the current track.</returns>
            public static TrackInfo GetCurrentTrackInfo()
            {
                var track = ReactionalEngine.Instance.engine.GetTrack();
                var ti = ReactionalManager.Instance._loadedTracks.Find(x => x.trackID == track);
                return ti;
            }

            /// <summary>
            /// Gets the playlist index of the track currently selected/playing.
            /// </summary>
            /// <returns>The index of the selected track.</returns>
            public static int GetSelectedTrackIndex()
            {
                var track = ReactionalManager.Instance.selectedTrack;
                return track;
            }

            /// <summary>
            /// Plays a specified track.
            /// </summary>
            /// <param name="ti">The TrackInfo object of the track to play.</param>
            /// <param name="fadeintime">The fade-in time in seconds.</param>
            /// <param name="fadeouttime">The fade-out time in seconds.</param>
            public static void PlayTrack(TrackInfo ti, float fadeintime = 0, float fadeouttime = 0.1f)
            {
                var trackID = ti.trackID;

                // ReactionalManager.Instance.selectedTrack = ReactionalManager.Instance._loadedTracks.IndexOf(ti);
                PlayTrackByID(trackID, fadeintime: fadeintime, fadeouttime: fadeouttime);
            }

            /// <summary>
            /// Plays a track by its index in the loaded tracks list.
            /// </summary>
            /// <param name="loadedTrackIndex">The index of the track to play.</param>
            /// <param name="fadeintime">The fade-in time in seconds.</param>
            /// <param name="fadeouttime">The fade-out time in seconds.</param>
            public static void PlayTrack(int loadedTrackIndex, float fadeintime = 0, float fadeouttime = 0.1f)
            {
                // ReactionalManager.Instance.selectedTrack = loadedTrackIndex;
                var trackID = ReactionalManager.Instance._loadedTracks[loadedTrackIndex].trackID;
                PlayTrackByID(trackID, fadeintime: fadeintime, fadeouttime: fadeouttime);
            }

            /// <summary>
            /// Plays a track by its name.
            /// </summary>
            /// <param name="trackName">The name of the track to play.</param>
            /// <param name="fadeintime">The fade-in time in seconds.</param>
            /// <param name="fadeouttime">The fade-out time in seconds.</param>
            public static void PlayTrack(string trackName, float fadeintime = 0, float fadeouttime = 0.1f)
            {
                Reactional.Core.TrackInfo ti = ReactionalManager.Instance._loadedTracks.Find(x => x.trackName.Equals(trackName));
                if (ti == null)
                {
                    Debug.LogWarning("Reactional: Track not found: " + trackName);
                    return;
                }
                var trackID = ti.trackID;

                // ReactionalManager.Instance.selectedTrack = ReactionalManager.Instance._loadedTracks.IndexOf(ti);
                PlayTrackByID(trackID, fadeintime: fadeintime, fadeouttime: fadeouttime);
            }

            /// <summary>
            /// Plays a track by its ID asynchronously.
            /// </summary>
            /// <param name="trackID">The ID of the track to play.</param>
            /// <param name="fadeintime">The fade-in time in seconds.</param>
            /// <param name="fadeouttime">The fade-out time in seconds.</param>
            public static async void PlayTrackByID(int trackID, float fadeintime = 0, float fadeouttime = 0.1f)
            {
                if (Playlist.State == 1)
                {
                    await FadeOut(fadeouttime);
                }
                else
                {
                    await Task.Delay((int)(0.01f * 1000)); // safeguard if theme.play was called after track.play
                }
                bool themePaused = Theme.GetState() != MusicSystem.PlaybackState.Playing || Theme.GetPartName(Theme.GetCurrentPart()) == "part: silence";
                Theme.Stop(true);

                Engine engine = ReactionalEngine.Instance.engine;

                Reactional.Setup.AllowPlay = false;
                engine.ResetTrack(trackID);
                engine.SetTrack(trackID);
                ReactionalManager.Instance.selectedTrack = ReactionalManager.Instance._loadedTracks.FindIndex(x => x.trackID == trackID);

                engine.ResetTrack(Theme.GetThemeID());
                engine.SetTheme(Theme.GetThemeID());
                CopyPrerollToTheme(trackID);
                Playlist.Start();

                if (themePaused)
                    Theme.SetControl("part: silence", 0f);
                Theme.Play();

                Reactional.Setup.AllowPlay = true;
                if (fadeintime > 0)
                    await FadeIn(fadeintime);

                engine.SetParameterInt(trackID, engine.FindParameter(trackID, "resample_quality"), 2);
            }

            /// <summary>
            /// Copies the preroll data to the theme.
            /// </summary>
            /// <param name="trackID">The ID of the track.</param>
            public static void CopyPrerollToTheme(int trackID)
            {
                Engine engine = ReactionalEngine.Instance.engine;
                int preroll = engine.FindParameter(trackID, "pre_roll");
                long prerolldata = engine.GetParameterInt(trackID, preroll);
                engine.SetParameterInt(Reactional.Playback.Theme.GetThemeID(), preroll, prerolldata);
            }

            /// <summary>
            /// Loads a streamed track asynchronously.
            /// </summary>
            /// <param name="ti">The track information.</param>
            private static async Task LoadStreamedTrack(TrackInfo ti)
            {
                Engine engine = ReactionalEngine.Instance.engine;
                int trackID = engine.AddTrackFromPath(Application.streamingAssetsPath + "/Reactional/" + ti.bundleID + "/" + ti.trackHash, null);
                engine.SetParameterBool(trackID, engine.FindParameter(trackID, "disk_streaming"), true);
                engine.SetParameterValue(trackID, engine.FindParameter(trackID, "asset_base_path"), Application.streamingAssetsPath + "/Reactional/" + ti.bundleID + "/");
                await AssetHelper.LoadTrackAssets(engine, trackID, Application.streamingAssetsPath + "/Reactional/" + ti.bundleID, streaming: true);
                engine.SetTrack(trackID);
            }

            /// <summary>
            /// Checks if a track is loaded.
            /// </summary>
            /// <returns>True if a track is loaded, false otherwise.</returns>
            public static bool IsLoaded()
            {
                if (ReactionalEngine.Instance == null)
                    return false;
                var track = ReactionalEngine.Instance.engine.GetTrack();
                bool loaded = track < 0 ? false : true;
                return loaded;
            }

            /// <summary>
            /// Plays the next track in the playlist.
            /// </summary>
            /// <param name="fadeouttime">The fade-out time in seconds.</param>
            /// <param name="fadeintime">The fade-in time in seconds.</param>
            public static void Next(float fadeouttime = 0.1f, float fadeintime = 0f)
            {
                if (ReactionalEngine.Instance == null)
                    return;
                var rM = ReactionalManager.Instance;
                rM.selectedTrack = (rM.selectedTrack + 1) % (rM._loadedTracks.Count);
                int trackNum = rM.selectedTrack;
                Playback.Playlist.PlayTrack(trackNum, fadeouttime: fadeouttime, fadeintime: fadeintime);
            }

            /// <summary>
            /// Plays the previous track in the playlist.
            /// </summary>
            /// <param name="fadeouttime">The fade-out time in seconds.</param>
            /// <param name="fadeintime">The fade-in time in seconds.</param>
            public static void Prev(float fadeouttime = 0.1f, float fadeintime = 0f)
            {
                if (ReactionalEngine.Instance == null)
                    return;
                var rM = ReactionalManager.Instance;
                rM.selectedTrack = (rM.selectedTrack - 1) < 0 ? rM._loadedTracks.Count - 1 : rM.selectedTrack - 1;
                int trackNum = rM.selectedTrack;
                Playback.Playlist.PlayTrack(trackNum, fadeouttime: fadeouttime, fadeintime: fadeintime);
            }

            /// <summary>
            /// Plays a random track from the playlist.
            /// </summary>
            /// <param name="fadeouttime">The fade-out time in seconds.</param>
            /// <param name="fadeintime">The fade-in time in seconds.</param>
            public static void Random(float fadeouttime = 0.1f, float fadeintime = 0f)
            {
                if (ReactionalEngine.Instance == null)
                    return;
                var rM = ReactionalManager.Instance;
                rM.selectedTrack = UnityEngine.Random.Range(0, rM._loadedTracks.Count);
                int trackNum = rM.selectedTrack;
                Playback.Playlist.PlayTrack(trackNum, fadeouttime: fadeouttime, fadeintime: fadeintime);
            }

            /// <summary>
            /// Gets the state of the current track.
            /// </summary>
            /// <returns>The state of the track.</returns>
            private static int State
            {
                get
                {
                    if (!IsLoaded()) return 0;
                    var trackID = Reactional.Playback.Playlist.GetTrackID();
                    var engine = ReactionalEngine.Instance.engine;
                    var pindex = engine.FindParameter(trackID, "status");
                    return (int)engine.GetParameterInt(trackID, pindex);
                }
            }

            /// <summary>
            /// Gets the playback state of the current track.
            /// </summary>
            /// <returns>The playback state of the track.</returns>
            public static MusicSystem.PlaybackState GetState()
            {
                if (!IsLoaded()) return MusicSystem.PlaybackState.Stopped;
                var trackID = Reactional.Playback.Playlist.GetTrackID();
                var engine = ReactionalEngine.Instance.engine;
                var pindex = engine.FindParameter(trackID, "status");
                return (MusicSystem.PlaybackState)engine.GetParameterInt(trackID, pindex);
            }

            /// <summary>
            /// Checks if a track is playing.
            /// </summary>
            /// <returns>True if a track is playing, false otherwise.</returns>
            public static bool IsPlaying
            {
                get
                {
                    if (!IsLoaded()) return false;
                    var trackID = Reactional.Playback.Playlist.GetTrackID();
                    var engine = ReactionalEngine.Instance.engine;
                    var pindex = engine.FindParameter(trackID, "status");
                    if ((int)engine.GetParameterInt(trackID, pindex) == 1)
                        return true;
                    else
                        return false;
                }
            }

            /// <summary>
            /// Stops the current track with an optional fade-out time.
            /// </summary>
            /// <param name="fadeout">The fade-out time in seconds.</param>
            public static void Stop(float fadeout = 0)
            {
                if (fadeout > 0)
                {
                    _ = FadeOut(fadeout);
                }
                else
                {
                    var trackID = Reactional.Playback.Playlist.GetTrackID();
                    var engine = ReactionalEngine.Instance.engine;
                    var pindex = engine.FindParameter(trackID, "status");
                    engine.SetParameterInt(trackID, pindex, 0);
                }
            }

            /// <summary>
            /// Fades out the current track.
            /// </summary>
            /// <param name="fadeout">The fade-out time in seconds.</param>
            private static async Task FadeOut(float fadeout)
            {
                if (ReactionalEngine.Instance == null)
                    return;
                ReactionalEngine.Instance.engine.TrackFade(0f, 0, (long)(fadeout * 1000000), true);

                while (Playback.Playlist.State == 1) await Task.Delay(100);
            }

            /// <summary>
            /// Fades in the current track.
            /// </summary>
            /// <param name="fadein">The fade-in time in seconds.</param>
            private static async Task FadeIn(float fadein)
            {
                if (ReactionalEngine.Instance == null)
                    return;
                float amp = (float)ReactionalEngine.Instance.engine.GetTrackChannelAmp(0);
                ReactionalEngine.Instance.engine.TrackFade(0f, 0, 1);
                await Task.Delay(100);
                ReactionalEngine.Instance.engine.TrackFade(amp, 1, (long)(fadein * 1000000));
            }

            /// <summary>
            /// Starts playing the current track.
            /// </summary>
            public static void Start()
            {
                Engine engine = ReactionalEngine.Instance.engine;
                int trackID = Reactional.Playback.Playlist.GetTrackID();
                if (trackID < 0)
                {
                    trackID = ReactionalManager.Instance._loadedTracks[0].trackID;
                    engine.SetTrack(trackID);
                }

                Setup.ReactionalLog("Playing track: " + trackID);

                if (trackID >= 0)
                {
                    int pindex = engine.FindParameter(trackID, "status");
                    engine.SetParameterInt(trackID, pindex, 1);
                }
            }

            /// <summary>
            /// Plays the current track.
            /// </summary>
            public static void Play()
            {
                Engine engine = ReactionalEngine.Instance.engine;
                int trackID = Reactional.Playback.Playlist.GetTrackID();
                if (trackID < 0 && ReactionalManager.Instance._loadedTracks.Count > 0)
                {
                    trackID = ReactionalManager.Instance._loadedTracks[0].trackID;
                }

                if (trackID < 0)
                {
                    Setup.ReactionalLog("No track loaded", 1);
                    return;
                }

                if (Reactional.Playback.Playlist.State == 1)
                {
                    Reactional.Playback.Playlist.Stop();
                    PlayTrackByID(trackID, 0f, 0.5f);
                }
                else
                {
                    PlayTrackByID(trackID, 0f, 0f);
                }
            }

            /// <summary>
            /// Gets or sets the volume of the playlist.
            /// </summary>
            public static float Volume
            {
                get
                {
                    var eng = MusicSystem.GetEngine();
                    return (float)eng.GetParameterFloat(-1, eng.FindParameter(-1, "track_gain"));
                }
                set
                {
                    var eng = MusicSystem.GetEngine();
                    int gain = eng.FindParameter(-1, "track_gain");
                    if (gain < 0) return;
                    eng.SetParameterFloat(-1, gain, value);
                }
            }

            /// <summary>
            /// Gets the number of parts in the current track.
            /// </summary>
            /// <returns>The number of parts.</returns>
            private static int GetNumParts()
            {
                var engine = ReactionalEngine.Instance.engine;
                int trackID = engine.GetTrack();
                return engine.GetNumParts(trackID);
            }

            /// <summary>
            /// Gets the current part index in the current track.
            /// </summary>
            /// <returns>The current part index.</returns>
            private static int GetCurrentPart()
            {
                var engine = ReactionalEngine.Instance.engine;
                int trackID = engine.GetTrack();
                return engine.GetCurrentPart(trackID);
            }

            /// <summary>
            /// Gets the name of a specific part of the current track.
            /// </summary>
            /// <param name="part">The part index.</param>
            /// <returns>The name of the part.</returns>
            private static string GetPartName(int part)
            {
                var engine = ReactionalEngine.Instance.engine;
                int trackID = engine.GetTrack();
                return engine.GetPartName(trackID, part);
            }

            /// <summary>
            /// Gets the offset of a specific part of the current track in seconds.
            /// </summary>
            /// <param name="part">The part index.</param>
            /// <returns>The offset of the part in seconds.</returns>
            private static float GetPartOffset(int part)
            {
                var engine = ReactionalEngine.Instance.engine;
                int trackID = engine.GetTrack();
                return (float)(engine.GetPartOffset(trackID, part) / 1000000f);
            }

            /// <summary>
            /// Gets the duration of a specific part of the current track in seconds.
            /// </summary>
            /// <param name="part">The part index.</param>
            /// <returns>The duration of the part in seconds.</returns>
            private static float GetPartDuration(int part)
            {
                var engine = ReactionalEngine.Instance.engine;
                int trackID = engine.GetTrack();
                return (float)(engine.GetPartDuration(trackID, part) / 1000000f);
            }

            /// <summary>
            /// Gets the number of bars in the current track.
            /// </summary>
            /// <returns>The number of bars.</returns>
            public static int GetNumBars()
            {
                var engine = ReactionalEngine.Instance.engine;
                int trackID = engine.GetTrack();
                return engine.GetNumBars(trackID);
            }

            /// <summary>
            /// Gets the current bar index in the current track.
            /// </summary>
            /// <returns>The current bar index.</returns>
            public static int GetCurrentBar()
            {
                var engine = ReactionalEngine.Instance.engine;
                int trackID = engine.GetTrack();
                return engine.GetCurrentBar(trackID);
            }

            /// <summary>
            /// Gets the offset of a specific bar in the current track in seconds.
            /// </summary>
            /// <param name="bar">The bar index.</param>
            /// <returns>The offset of the bar in seconds.</returns>
            public static float GetBarOffset(int bar)
            {
                var engine = ReactionalEngine.Instance.engine;
                int trackID = engine.GetTrack();
                return (float)(engine.GetBarOffset(trackID, bar) / 1000000f);
            }

            /// <summary>
            /// Gets the duration of a specific bar in the current track in seconds.
            /// </summary>
            /// <param name="bar">The bar index.</param>
            /// <returns>The duration of the bar in seconds.</returns>
            public static float GetBarDuration(int bar)
            {
                var engine = ReactionalEngine.Instance.engine;
                int trackID = engine.GetTrack();
                return (float)(engine.GetBarDuration(trackID, bar) / 1000000f);
            }
        }

        public static class MusicSystem
        {

            /// <summary>
            /// Schedules audio playback at a specified quantized time.
            /// </summary>
            /// <param name="audioSource">The audio source to play.</param>
            /// <param name="quant">The quantization value.</param>
            /// <param name="timeOffset">The time offset in seconds.</param>
            public static void ScheduleAudio(AudioSource audioSource, float quant = 1, float timeOffset = 0)
            {
                var time = GetRealTimeToBeat(quant);
                audioSource.PlayScheduled(AudioSettings.dspTime + time + timeOffset);
            }

            /// <summary>
            /// Quantization values for musical timing.
            /// </summary>
            public enum Quant
            {
                Whole = 1,
                Half = 2,
                Quarter = 4,
                Eighth = 8,
                Sixteenth = 16,
            }

            /// <summary>
            /// Playback states for the music system.
            /// </summary>
            public enum PlaybackState
            {
                Stopped = 0,
                Playing = 1,
                Paused = 2
            }

            /// <summary>
            /// Gets or sets the playback permission.
            /// </summary>
            public static bool PlaybackAllowed
            {
                get
                {
                    return Reactional.Setup.AllowPlay;
                }
                set
                {
                    Reactional.Setup.AllowPlay = value;
                }
            }

            /// <summary>
            /// Gets the engine instance.
            /// </summary>
            /// <returns>The engine instance.</returns>
            public static Engine GetEngine()
            {
                return ReactionalEngine.Instance.engine;
            }

            /// <summary>
            /// Get the ammount of beats left until the next quantized beat.
            /// Same as doing GetNextBeat(quant) - GetCurrentBeat()
            /// </summary>
            /// <param name="quant">The next beat multiple to find.</param>
            /// <param name="offset">A shift for when you don't want rounded beats.</param>
            /// <param name="theme"></param>
            /// <returns>The number of beats left until next quant</returns>
            public static float GetTimeToBeat(float quant, float offset = 0, bool theme = true)
            {
                int trackID = theme ? Playback.Theme.GetThemeID() : Playback.Playlist.GetTrackID();

                long nextQuant = GetEngine().GetNextQuantBeat(trackID, (long)(quant * 1000000), (long)(offset * 1000000));
                double currBeat = GetCurrentMicroBeat();

                return (float)(nextQuant - currBeat) / 1000000f;
            }

            /// <summary>
            /// Gets the real-time to the next quantized beat.
            /// </summary>
            /// <param name="quant">The quantization value.</param>
            /// <returns>The time in seconds to the next beat.</returns>
            public static float GetRealTimeToBeat(float quant)
            {
                return BeatsToSeconds(GetTimeToBeat(quant));
            }
            
            /// <summary>
            /// Converts seconds to beats based on the current tempo.
            /// </summary>
            /// <param name="seconds"></param>
            /// <returns></returns>
            public static float SecondsToBeats(float seconds)
            {
                return (GetTempoBpm() / 60f) * seconds;
            }

            /// <summary>
            /// Get the beat position of the next quantized beat.
            /// i.e. if current beat is 5.32, and quant=4 is supplied, it will return beat 8.
            /// </summary>
            /// <param name="quant">The next beat multiple to find.</param>
            /// <param name="offset">A shift for when you don't want rounded beats.</param>
            /// <param name="theme"></param>
            /// <returns>A beat value in the future.</returns>
            public static float GetNextBeat(float quant, float offset = 0, bool theme = true)
            {
                int trackID = theme ? Playback.Theme.GetThemeID() : Playback.Playlist.GetTrackID();

                long nextQuant = GetEngine().GetNextQuantBeat(trackID, (long)(quant * 1000000), (long)(offset * 1000000));

                return nextQuant / 1000000f;
            }

            // <summary>
            /// Get the beat position of the next quantized beat in microseconds. <summary>
            /// Get the beat position of the next quantized beat in microbeats, i.e. 1/1000000th of a beat.
            /// </summary>
            /// <param name="quant"></param>
            /// <param name="offset"></param>
            /// <param name="theme"></param>
            /// <returns>A microbeat value in the future.</returns>
            public static long GetNextBeatAbsolute(float quant, float offset = 0, bool theme = true)
            {
                int trackID = theme ? Playback.Theme.GetThemeID() : Playback.Playlist.GetTrackID();

                long nextQuant = GetEngine().GetNextQuantBeat(trackID, (long)(quant * 1000000), (long)(offset * 1000000));
                return nextQuant;
            }

            /// <summary>
            /// Gets the current beat position.
            /// </summary>
            /// <returns>The current beat position.</returns>
            public static float GetCurrentBeat()
            {
                if (ReactionalEngine.Instance == null) return 0f;

                return ReactionalEngine.Instance.CurrentBeat;
            }

            /// <summary>
            /// Gets the current beat position in microbeats.
            /// </summary>
            /// <returns>The current beat position in microbeats.</returns>
            public static double GetCurrentMicroBeat()
            {
                if (PlaybackAllowed == false) return 0f;
                int trackID = Playback.Theme.GetThemeID();
                var engine = ReactionalEngine.Instance.engine;
                var pindex = engine.FindParameter(trackID, "current_beat");
                return engine.GetParameterFloat(trackID, pindex);
            }

            /// <summary>
            /// Gets the tempo in beats per minute (BPM).
            /// </summary>
            /// <returns>The tempo in BPM.</returns>
            public static float GetTempoBpm()
            {
                if (PlaybackAllowed == false) return 0f;
                return ReactionalEngine.Instance.TempoBpm;
            }

            /// <summary>
            /// Converts beats to seconds based on the current tempo.
            /// </summary>
            /// <param name="beats">The number of beats to convert.</param>
            /// <returns>The equivalent time in seconds.</returns>
            public static float BeatsToSeconds(float beats)
            {
                return (60f / GetTempoBpm()) * beats;
            }

            //public static void DuckMusic(float amp)
            //{
            //    //var engine = ReactionalEngine.Instance.engine;
            //    //int control = engine.FindControl(Reactional.Playback.Theme.GetThemeID(), "duck");

            //    //if (control < 0) return;

            //    //engine.SetControlValue(Theme.GetThemeID(), control, amp);


            //}


            /// <summary>
            /// Coroutine to duck the music volume.
            /// </summary>
            /// <param name="amp">The target amplitude for ducking.</param>
            /// <returns>An IEnumerator for the coroutine.</returns>
            public static IEnumerator DuckMusic(float amp)
            {
                float startVolume = ReactionalManager.Instance.trackGain;
                var FadeTime = 0.75f;

                if (amp < 1 && !ReactionalManager.Instance.isDucked)
                {
                    ReactionalManager.Instance.isDucked = true;
                    while (Reactional.Playback.Playlist.Volume > amp && ReactionalManager.Instance.isDucked)
                    {
                        Reactional.Playback.Playlist.Volume -= startVolume * Time.deltaTime / FadeTime;

                        yield return null;
                    }
                }
                else if (amp == 1 && ReactionalManager.Instance.isDucked)
                {
                    ReactionalManager.Instance.isDucked = false;
                    while (Reactional.Playback.Playlist.Volume < startVolume && !ReactionalManager.Instance.isDucked)
                    {
                        Reactional.Playback.Playlist.Volume += startVolume * Time.deltaTime / FadeTime;

                        yield return null;
                    }
                }
            }

            /// <summary>
            /// Custom yield instruction that waits for the next beat.
            /// </summary>
            public class WaitForNextBeat : CustomYieldInstruction
            {
                float beat;
                float quantBeat;
                float offset;

                /// <summary>
                /// Determines whether to keep waiting.
                /// </summary>
                public override bool keepWaiting
                {
                    get
                    {
                        double mb = Reactional.Playback.MusicSystem.GetCurrentMicroBeat();
                        if ((quantBeat - mb) > (beat + 1)) quantBeat = Reactional.Playback.MusicSystem.GetNextBeatAbsolute(beat) + (offset * 1000000f);
                        bool state = (mb < quantBeat) ? true : false;
                        return state;
                    }
                }

                /// <summary>
                /// Initializes a new instance of the <see cref="WaitForNextBeat"/> class.
                /// </summary>
                /// <param name="beat">The beat duration to wait for.</param>
                /// <param name="offset">The offset in beats.</param>
                public WaitForNextBeat(float beat, float offset = 0f)
                {
                    this.beat = beat;
                    this.offset = offset;
                    this.quantBeat = Reactional.Playback.MusicSystem.GetNextBeatAbsolute(beat) + (offset * 1000000f);
                }
            }
        }



    }
}
