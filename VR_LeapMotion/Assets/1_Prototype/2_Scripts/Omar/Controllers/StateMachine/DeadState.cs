using UnityEngine;
using System.Collections;

namespace Quinq
{
	public class DeadState : IEnemyState 
	{
		public DeadState (StatePatternEnemy statePatternEnemy)
			:base(statePatternEnemy)	{}
		
		#region Properties
		private bool isDying = false;
		private float waitTime = 5.0f;
		private float timeToReach = 0;
		#endregion

		#region Public
		override public void Init()	
		{
			enemy.navMeshAgent.Stop ();
			enemy.anim.SetTrigger ("Death");			
		}

		override public void ChooseState ()		{}

		override public void UpdateState()
		{
			if (isDying)
				LetMeRestInPeace ();
			else if (isAnimationEnded ("Death")) 
			{
				isDying = true;
				timeToReach = Time.time + waitTime;
			}
		}

		public override void Reset()
		{
			timeToReach = 0;
		}
		#endregion

		#region Private
		private void LetMeRestInPeace()
		{
			if (Time.time >= timeToReach)
			{
				enemy.Reset ();
			}
		}
		#endregion
	}
}