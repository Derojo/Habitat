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
	private static float targetWidth = (Screen.width/1.6f);
	private float targetHeight = (targetWidth/100f)*70f;
	public AudioClip ambientSound;
	private bool _isPlaying = false;
	
	public static HabitatState instance;
	
	public void Awake()
	{
		DontDestroyOnLoad (gameObject);
		HabitatState.instance = this;
	}
	
	void Start(){
		HabitatState.status = initialStatus;
	}	
	
	/// <summary>
	/// Method to show DrawTargetWindow if we lose focus
	/// </summary>
	void OnGUI(){
		if (doNotUseFindTargetGUI)
			return;
		
		
		GUI.skin = habitatSkin;

		
		if (status == Status.Running && statusTracking == StatusTracking.NotTracking) 
		{
			DrawTargetWindow ();
			_isPlaying = false;
		} 
		else 
		{
			if(!_isPlaying) {
				audio.PlayOneShot(ambientSound);
				_isPlaying = true;
			}
		}

	}
	

	/// Draw targetfinder and information to get the focus back on the game board
	private void DrawTargetWindow ()
	{
		GUI.skin.GetStyle("labelCenterWithBack").fontSize = Screen.width / 30;
		GUI.skin.GetStyle("LabelCenterBackground").fontSize = Screen.width / 30;
		//GUI.Box(new Rect(0, 0, LigaboUtils.screenWidth, LigaboUtils.screenHeight), "", GUI.skin.GetStyle("scoreboard") );
		GUI.Box(new Rect(Screen.width/2-targetWidth/2,Screen.height/2-targetHeight/2,targetWidth,targetHeight),"",GUI.skin.GetStyle("targetFinder"));

		var boardWidth = targetWidth*0.82f;
		var boardHeight = targetHeight*0.75f;
		GUI.DrawTexture(new Rect(Screen.width/2-boardWidth/2, Screen.height/2-boardHeight/2,boardWidth,boardHeight),miniBoardIcon);
		
		var labelRect = new Rect(Screen.width/2-boardWidth/2*0.9f, Screen.height/8,boardWidth*0.9f,80);
		GUI.Label(new Rect(Screen.width/2-boardWidth/2*0.9f, Screen.height/7.9f,boardWidth*0.9f,80),"Richt je camera op het game board", GUI.skin.GetStyle("LabelCenterBackground"));
		GUI.Label(labelRect,"Richt je camera op het game board", GUI.skin.GetStyle("labelCenterWithBack"));

	}

	// Auto save all data when the player is closing the game
	void OnApplicationQuit() {
		SaveLoad.Save ();
	}
}