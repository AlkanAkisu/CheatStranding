using UnityEngine;

class StateMachine : MonoBehaviour
{
	public EnemyState State;

	public void SetState(EnemyState state)
	{
		State?.onExitState();

		var stateStr = state.GetType().Name;
		if (State != null)
			Debug.Log($"State Changed. New State -> <b>{stateStr}</b>");

		State = state;
		State.onEnterState();
	}
}


