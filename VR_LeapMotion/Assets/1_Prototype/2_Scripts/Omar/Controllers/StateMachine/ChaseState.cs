using UnityEngine;
using System.Collections;

namespace Quinq
{
	public class ChaseState : IEnemyState 
	{
		public ChaseState (StatePatternEnemy statePatternEnemy)
			:base(statePatternEnemy)	{}
		#region Properties

		#endregion

		#region Public
		override public void Init()	
		{
			enemy.anim.SetTrigger ("Run");

			enemy.navMeshAgent.speed = enemy.speedRun;
			enemy.navMeshAgent.Resume ();

			enemy.meshRendererFlag.material.color = Color.red;
		}

		override public void ChooseState ()
		{
			if (isCloseEnough ())
			{
				ChangeState (enemy.fightState);
				return;
			}
			else if (!isPlayerInSight ()) 
			{
				ChangeState(enemy.alertState);
				return;
			}
		}

		override public void UpdateState()
		{
			Chase ();
		}
		#endregion

		#region Private
		public override bool isPlayerInSight()
		{
			RaycastHit hit;
			Vector3 enemyToTarget = (enemy.player.position + enemy.offset) - enemy.eyes.transform.position;

			if (Physics.Raycast (enemy.eyes.transform.position, enemyToTarget, out hit, enemy.sightRange) 
				&& hit.collider.CompareTag ("Player")) 
			{
				return true;
			}
			return false;
		}

		private void Chase()
		{
			enemy.navMeshAgent.destination = enemy.player.position;
		}
		#endregion
	}
}