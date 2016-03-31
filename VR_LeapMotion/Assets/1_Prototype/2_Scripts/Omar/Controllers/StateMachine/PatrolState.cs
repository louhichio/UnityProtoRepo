using UnityEngine;
using System.Collections;

namespace Quinq
{
	public class PatrolState : IEnemyState 
	{
		public PatrolState (StatePatternEnemy statePatternEnemy)
			:base(statePatternEnemy)	{}

		#region Properties
		private int nextWayPoint;
		#endregion

		#region Public
		override public void Init()	
		{
			enemy.anim.SetTrigger ("Walk");
			enemy.navMeshAgent.speed = enemy.speedWalk;
			enemy.meshRendererFlag.material.color = Color.green;
		}

		override public void ChooseState ()
		{
			if (isCloseEnough ())
				return;
			else if (isPlayerInSight ()) 
			{
				ChangeState (enemy.chaseState);
				return;
			}
		}

		override public void UpdateState()
		{
			Patrol ();
		}

		public override bool isCloseEnough()
		{
			if (enemy.distance < 3) 
			{
				ChangeState (enemy.fightState);
				return true;
			} 
			else if (enemy.distance < enemy.detectDist) 
			{
				ChangeState (enemy.alertState);
				return true;
			}
			return false;
		}

		public override void Reset ()
		{
			nextWayPoint = 0;	
		}
		#endregion

		#region Private
		private void Patrol ()
		{
			enemy.navMeshAgent.destination = enemy.wayPoints [nextWayPoint].position;
			enemy.navMeshAgent.Resume ();

			if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance 
				&& !enemy.navMeshAgent.pathPending) 
			{
				nextWayPoint = (nextWayPoint + 1) % enemy.wayPoints.Length;
			}
		}
		#endregion
	}
}