using UnityEngine;
using System.Collections;

namespace Quinq
{
	public class AlertState : IEnemyState
	{
		public AlertState (StatePatternEnemy statePatternEnemy)
			:base(statePatternEnemy)	{}
		
		#region Properties
		private int turnDir;
		private float searchTimer;
		#endregion

		#region Public
		override public void Init()	
		{
			enemy.anim.SetTrigger ("Idle");

			enemy.navMeshAgent.Stop ();
			enemy.meshRendererFlag.material.color = Color.yellow;

			SetTurnDirection ();
		}

		override public void ChooseState ()
		{
			if (isCloseEnough ())
			{
				ChangeState (enemy.fightState);
				return;
			} 
			else if (isPlayerInSight ()) 
			{
				ChangeState (enemy.chaseState);
				return;
			}
		}

		override public void UpdateState()
		{
			Search ();
		}

		public override void ChangeState(IEnemyState state)
		{
			enemy.currentState = state;
			enemy.currentState.Init();

			searchTimer = 0f;
		}

		public override void Reset ()
		{
			searchTimer = 0f;	
		}
		#endregion

		#region Private			
		private void SetTurnDirection()
		{
			Vector3 relativePlayer = enemy.transform.InverseTransformPoint (enemy.player.position);
			turnDir = relativePlayer.x > 0 ? 1 : -1;
		}

		private void Search()
		{
			enemy.transform.Rotate (0, enemy.searchingTurnSpeed * Time.deltaTime * turnDir, 0);
			searchTimer += Time.deltaTime;

			if (searchTimer >= enemy.searchingDuration)
				ChangeState (enemy.patrolState);
		}
		#endregion
	}
}