using UnityEngine;
using System.Collections;

namespace Quinq
{
	public abstract class IEnemyState
	{
		public readonly StatePatternEnemy enemy;

		//Constructor
		public IEnemyState (StatePatternEnemy statePatternEnemy)
		{
			enemy = statePatternEnemy;
		}

		#region Abstract
		public abstract void Init ();

		public abstract void ChooseState ();

		public abstract void UpdateState ();
		#endregion

		#region Virtual
		public virtual void ChangeState(IEnemyState state)
		{
			enemy.currentState = state;
			enemy.currentState.Init();
		}

		public virtual bool isCloseEnough()
		{
			if (enemy.distance < 3)
			{
				ChangeState (enemy.fightState);
				return true;
			}
			return false;
		}

		public virtual bool isPlayerInSight()
		{
			RaycastHit hit;

			if (Physics.Raycast (enemy.eyes.transform.position, enemy.eyes.transform.forward, out hit, enemy.sightRange) 
				&& hit.collider.CompareTag ("Player")) 
			{
				return true;
			}
			return false;
		}

		public virtual bool isAnimationEnded(string animationName)
		{
			if (enemy.anim.GetCurrentAnimatorStateInfo (0).IsName (animationName) &&
				enemy.anim.GetCurrentAnimatorStateInfo (0).normalizedTime > 1)
				return true;
			return false;
		}

		public virtual void Reset (){}
		#endregion

		#region Private

		#endregion
	}
}
