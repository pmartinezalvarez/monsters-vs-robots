using UnityEngine;
using System.Collections;
using System;

public class AudioManager : MonoBehaviour
{
		public enum AudioClipType
		{
				GODZILLA_DAMAGE,
				MAZINGER_DAMAGE
		}

		private AudioSource _audioSource;
		private AudioClip _godzillaDamageAudioclip;
		private AudioClip _mazingerDamageudioclip;

		void Start ()
		{
				_audioSource = gameObject.AddComponent <AudioSource>();

				_godzillaDamageAudioclip = _doLoadAudioClipByName (MainConstants.AUDIO_GODZILLA_DAMAGE);
				_mazingerDamageudioclip = _doLoadAudioClipByName (MainConstants.AUDIO_MAZINGER_DAMAGE);
		}

		public void DoPlaySound (AudioClipType aClipName)
		{
				switch (aClipName) {
				case AudioClipType.GODZILLA_DAMAGE:
						_audioSource.PlayOneShot (_godzillaDamageAudioclip);
						break;
				case AudioClipType.MAZINGER_DAMAGE:
						_audioSource.PlayOneShot (_mazingerDamageudioclip);
						break;
				default:
						Debug.LogError ("Invalid clip type");
						break;
				}
		}

		private AudioClip _doLoadAudioClipByName (string aAudioClipName_string)
		{
				AudioClip audioClip = Resources.Load (aAudioClipName_string) as AudioClip;

				if (audioClip == null) {
						throw new Exception ("AudioClip '" + aAudioClipName_string + "' Cannot Be Null. Choose new path name");
				}

				return audioClip;

		}
}
