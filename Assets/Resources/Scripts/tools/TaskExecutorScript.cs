using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public delegate void Task ( );

// Permet de créer une queue de tâche à effectuer
public class TaskExecutorScript : MonoBehaviour {

	private Queue<Task> TaskQueue	= new Queue<Task> ( );
	private object		_queueLock	= new object ( );

	// singleton instance
	public static TaskExecutorScript instance;

	private static bool	_created = false;

	private void Awake ( ) {
		if ( !_created ) {
			instance = this;

			_created = true;
		}
		else {
			Destroy ( gameObject );
		}
	}

	void Update ( ) {
		lock ( _queueLock ) {
			if ( TaskQueue.Count > 0 )
				TaskQueue.Dequeue ( ) ( );
		}
	}

	public void ScheduleTask ( Task newTask ) {
		lock ( _queueLock ) {
			if ( TaskQueue.Count < 100 )
				TaskQueue.Enqueue ( newTask );
		}
	}
}