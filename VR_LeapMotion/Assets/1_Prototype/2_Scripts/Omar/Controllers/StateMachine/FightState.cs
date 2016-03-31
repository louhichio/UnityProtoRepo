using UnityEngine;
using System.Collections;

namespace Quinq
{
	public class FightState : IEnemyState 
	{
		public FightState (StatePatternEnemy statePatternEnemy)
			:base(statePatternEnemy)	{}
		
		#region Properties
		#endregion

		#region Public
		override public void Init()	
		{
			enemy.anim.SetTrigger ("Attack");
			enemy.navMeshAgent.Stop ();

			enemy.meshRendererFlag.material.color = Color.gray;

			FPSPlayer.Instance.IsHIt ();
		}

		override public void ChooseState ()
		{
			if (isAnimationEnded ("Attack")) 
			{
				ChangeState (enemy.chaseState);
			}
		}

		override public void UpdateState()
		{
			LookAtPlayer ();
		}
		#endregion

		#region Private
		private void LookAtPlayer()
		{
			Vector3 targetPos = enemy.player.position;
			targetPos.y = enemy.transform.position.y;
			enemy.transform.LookAt (targetPos);
		}
		#endregion
	}
}