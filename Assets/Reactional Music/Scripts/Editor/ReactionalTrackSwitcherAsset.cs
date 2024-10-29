using UnityEngine;
using UnityEngine.Playables;

namespace Reactional.Editor
{
	public class ReactionalTrackSwitcherAsset : PlayableAsset
	{
		public string trackName;
		public bool stopMusicOnStop = false;
		public float fadeoutInBeats = 2f;

		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			var playable = ScriptPlayable<ReactionalTrackSwitcher>.Create(graph);

			var behaviour = playable.GetBehaviour();
			behaviour.trackName = trackName;
			behaviour.stopMusicOnStop = stopMusicOnStop;
			behaviour.fadeoutInBeats = fadeoutInBeats;

			return playable;
		}
	}
}