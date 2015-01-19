using UnityEngine;
using System.Collections;

public class QuestManager {
	
	public static bool DisplayQuestlog { get { return _displayQuestLog; } set { _displayQuestLog = value; } }
	public static bool ActiveQuest { get { return _activeQuest; } set { _activeQuest = value; } }
	public static bool CompleteQuest;
	
	[HideInInspector]
	private static bool _displayQuestLog = false;
	[HideInInspector]
	private static bool _activeQuest = false;
	// Use this for initialization

}
