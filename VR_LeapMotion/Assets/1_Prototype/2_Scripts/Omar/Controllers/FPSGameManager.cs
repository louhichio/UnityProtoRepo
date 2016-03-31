using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Quinq
{
	public class FPSGameManager : Singleton<FPSGameManager> 
	{
		FPSPlayer player;
		public List<StatePatternEnemy> listEnemy;

		void Start()
		{
			player = FPSPlayer.Instance;

			foreach(var zombie in GameObject.FindGameObjectsWithTag("Zombie"))
			{
				listEnemy.Add (zombie.GetComponent<StatePatternEnemy> ());
			}
		}

		public void GameOver()
		{
			player.Reset ();

			foreach (var zombie in listEnemy) 
			{
				zombie.Reset ();
			}
		}

	}
}
