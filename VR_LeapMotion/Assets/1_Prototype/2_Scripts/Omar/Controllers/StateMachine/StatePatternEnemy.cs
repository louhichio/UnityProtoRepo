using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Quinq
{
	public class StatePatternEnemy : MonoBehaviour 
	{
		#region Properties
		public float searchingTurnSpeed = 120f;
		public float searchingDuration = 4f;
		public float sightRange = 20f;
		public Transform[] wayPoints;
		public Transform eyes;
		public Vector3 offset = new Vector3 (0,.5f,0);
		public MeshRenderer meshRendererFlag;


		[HideInInspector] public Transform player;

		[HideInInspector] public IEnemyState currentState;
		[HideInInspector] public ChaseState chaseState;
		[HideInInspector] public AlertState alertState;
		[HideInInspector] public PatrolState patrolState;
		[HideInInspector] public FightState fightState;
		[HideInInspector] public DeadState deadState;

		[HideInInspector] public NavMeshAgent navMeshAgent;
		[HideInInspector] public Animator anim;

		[HideInInspector] public float speedWalk = 0.3f;
		[HideInInspector] public float speedRun = 3f;
		[HideInInspector] public float distance;
		[HideInInspector] public float detectDist = 5.0f;
		[HideInInspector] public int lifePoints = 10;

		private List<IEnemyState> listStates = new List<IEnemyState>();

		private Vector3 initPos;
		private Vector3 initRot;

		public bool isHit = false;
		#endregion

		#region Unity
		private void Awake()
		{
			SetStates ();

			navMeshAgent = GetComponent<NavMeshAgent> ();
			anim = GetComponent<Animator> ();
		}

		void Start () 
		{
			player = FPSPlayer.Instance.transform;

			initPos = transform.position;
			initRot = transform.eulerAngles;

			currentState = patrolState;
			currentState.Init ();

			anim.SetTrigger ("Walk");
			navMeshAgent.speed = speedWalk;
		}

		void Update () 
		{
			distance = Vector3.Distance (transform.position, player.position);

			CheckStates ();

			currentState.UpdateState ();
		}
		#endregion

		#region Private
		private void SetStates()
		{
			chaseState = new ChaseState(this);
			alertState = new AlertState (this);
			patrolState = new PatrolState (this);
			fightState = new FightState (this);
			deadState = new DeadState (this);

			listStates.Add (chaseState);
			listStates.Add (alertState);
			listStates.Add (patrolState);
			listStates.Add (fightState);
			listStates.Add (deadState);
		}

		private void CheckStates ()
		{
			currentState.ChooseState ();

			if (isHit) 
				ApplyHit ();
		}

		private void ResetStates()
		{
			foreach (var state in listStates) 
				state.Reset ();			
		}

		private bool ApplyHit()
		{
			lifePoints--;

			if (currentState != deadState && lifePoints < 0) 
			{
				currentState.ChangeState (deadState);
				return true;
			}
			return false;
		}
		#endregion

		#region Public
		public void Reset()
		{
			ResetStates ();

			transform.position = initPos;
			transform.eulerAngles = initRot;

			currentState = patrolState;
			currentState.Init ();

			anim.SetTrigger ("Walk");
			navMeshAgent.speed = speedWalk;
			navMeshAgent.Resume ();

			lifePoints = 10;
			isHit = false;
		}
		#endregion
	}
}



//		private void OnTriggerEnter(Collider other)
//		{
//			currentState.OnTriggerEnter (other);
//		}

//	void OnAnimatorMove ()
//	{
//		print ((anim.deltaPosition.ToString("F4"))+ "  " + Vector3.Equals(anim.deltaPosition, Vector3.zero));
//		transform.position += anim.deltaPosition / Time.deltaTime * speed;
//		transform.position += anim.deltaPosition * 5;
//		navMeshAgent.speed = anim.deltaPosition.magnitude;
//		navMeshAgent.speed = anim.GetCurrentAnimatorClipInfo(0).
//		Vector3 position =  anim.rootPosition;
//		position.y = navMeshAgent.nextPosition.y;
//		transform.position = position;
//		transform.rotation = Quaternion.LookRotation(navMeshAgent.desiredVelocity);
//	}