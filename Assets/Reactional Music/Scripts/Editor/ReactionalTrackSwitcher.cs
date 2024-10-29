using UnityEngine;
using UnityEngine.Playables;

namespace Reactional.Editor
{
	public class ReactionalTrackSwitcher : PlayableBehaviour
	{
		public string trackName;
		public bool stopMusicOnStop = false;
		public float fadeoutInBeats = 2f;

		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{

		}

		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
			if (trackName != null && Application.isPlaying)
			{
				Reactional.Playback.Playlist.PlayTrack(trackName);
			}
		}

		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			if (stopMusicOnStop && Application.isPlaying)
				Reactional.Playback.Playlist.Stop(fadeoutInBeats);
		}
	}
}