using UnityEngine;
using System.Collections;

public class HabitatState : MonoBehaviour {

	public enum Status {
		Running,
		Stopped
	};
	
	public enum StatusTracking {
		NotTracking,
		Tracking
	};
	

	public static Status status = Status.Stopped;
	public static StatusTracking statusTracking = StatusTracking.NotTracking;
	public GUISkin habitatSkin;
	public Texture miniBoardIcon;
	public bool doNotUseFindTargetGUI=false;
	public static bool allowShowingTargetContent=true;
	public static bool doNotHideARContent=false;
	public Status initialStatus = Status.Stopped;
	private static float targetWidth = (Screen.width/2);
	private float targetHeight = (targetWidth/100f)*70f;
	
	
	public static HabitatState instance;
	
	public void Awake()
	{
		HabitatState.instance = this;
	}
	
	void Start(){
		HabitatState.status = initialStatus;
	}	
	
	/// <summary>
	/// Monobehaviour method used to show information find target overlay
	/// 
	/// </summary>
	void OnGUI(){
		if (doNotUseFindTargetGUI)
			return;
		
		
		GUI.skin = habitatSkin;

		
		if (status == Status.Running && statusTracking==StatusTracking.NotTracking)
		{
			DrawTargetWindow();
		}

	}
	
	/// <summary>
	/// This function is drawing find target information GUI window.
	/// </summary>
	/// 
	/// 
	private void DrawTargetWindow ()
	{

		//GUI.Box(new Rect(0, 0, LigaboUtils.screenWidth, LigaboUtils.screenHeight), "", GUI.skin.GetStyle("scoreboard") );
		GUI.Box(new Rect(Screen.width/2-targetWidth/2,Screen.height/2-targetHeight/2,targetWidth,targetHeight),"",GUI.skin.GetStyle("targetFinder"));

		var boardWidth = targetWidth*0.8f;
		var boardHeight = targetHeight*0.7f;
		GUI.DrawTexture(new Rect(Screen.width/2-boardWidth/2, Screen.height/2-boardHeight/2,boardWidth,boardHeight),miniBoardIcon);
		
		var labelRect = new Rect(Screen.width/2-boardWidth/2*0.9f, Screen.height/2-40,boardWidth*0.9f,80);
		GUI.Label(labelRect,"",GUI.skin.GetStyle("labelCenterWithBack"));
		GUI.Label(labelRect,"Richt je camera op het game board",GUI.skin.GetStyle("labelCenterWithBack"));

	}
}