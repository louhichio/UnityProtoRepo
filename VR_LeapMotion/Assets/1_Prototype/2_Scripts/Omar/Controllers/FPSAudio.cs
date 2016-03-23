using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

namespace Quinq
{
	public class FPSAudio : Singleton<FPSAudio> 
	{
		#region Properties
		public AudioSource audioSource;
		public AudioClip audioClip;
		#endregion

		#region Public
		public void PlayGunAudio()
		{
			audioSource.PlayOneShot (audioClip);
		}
		#endregion
	}
}
