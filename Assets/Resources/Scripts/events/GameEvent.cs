using UnityEngine;
using System.Collections;

public class GameEvent : CustomEvent {

	// event types
   	public static string GRID_INIT = "Grid Init Complete";

	public GameEvent(string eventType = "") {
  		type = eventType;
	}
}
