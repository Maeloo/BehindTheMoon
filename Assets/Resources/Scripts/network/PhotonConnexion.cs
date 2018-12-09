using UnityEngine;
using System.Collections;

public class PhotonConnexion : MonoBehaviour {
	
	[SerializeField] GUIText	playersCount;
    [SerializeField] bool		allowSingleton		= true;
    [SerializeField] bool		allowWarningOutputs	= true;
	[SerializeField] string		RoomName;
	[SerializeField] GameObject PlayerPrefab;

	public bool DontCreateClientScript;

    // singleton instance
    public static PhotonConnexion instance;

    private static bool	_created	= false;
	private static int	_nextId		= 0;
	
	private PhotonView	PhotonView;

	private bool		_connected	= false;

    private void Awake ( )
    {
        if ( !_created && allowSingleton )
        {
            //DontDestroyOnLoad ( this );
            instance = this;
            _created = true;
        }
        else
        {
            if ( allowSingleton )
            {
                if ( PhotonConnexion.instance.allowWarningOutputs )
                {
                    Debug.LogWarning ( "Only a single instance of " + this.name + " should exists!" );
                }
                Destroy ( gameObject );
            }
            else
            {
                instance = this;
            }
        }
    }
	
	void Start()
	{
		Debug.Log ( "Connexion" );
		PhotonNetwork.ConnectUsingSettings("1.0");
	}
	
	public virtual void OnConnectedToMaster()
	{
		Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
	}
	
	public virtual void OnPhotonJoinFailed()
	{
		Debug.Log("OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one. Calling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
		PhotonNetwork.CreateRoom(null, new RoomOptions() { maxPlayers = 6 }, null);
	}
	
	public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
	{
		Debug.LogError("Cause: " + cause);
	}
	
	void OnJoinedLobby()
	{
		Debug.Log ( "Join " + RoomName );
		
		RoomOptions opt = new RoomOptions();
		PhotonNetwork.JoinOrCreateRoom ( RoomName, opt, TypedLobby.Default );
	}
	
	void OnJoinedRoom()
	{
		Debug.Log ("Joined");		
		_connected = true;

		if (!GetComponent<GameMaster> ( ).AmIMaster ( ) && PhotonNetwork.isMasterClient ) {
			PhotonNetwork.Disconnect ( );

			Debug.Log ( "Error network" );

			StartCoroutine ( resetConnexion ( ) );

			return;
		}

		if ( !GetComponent<GameMaster> ( ).AmIMaster ( ) && !DontCreateClientScript ) {
			if ( PlayerPrefab == null ) {
				PhotonNetwork.Instantiate ( RoomName + "Player", Vector3.zero, Quaternion.identity, 0 );
			}
			else {
				GameObject.Instantiate ( PlayerPrefab );
			}

			_nextId++;
		}
	}

	IEnumerator resetConnexion ( ) {
		yield return new WaitForSeconds ( 1 );
		Start ( );
	}

    public bool isConnected ( ) {
        return _connected;
    }
	
	void Update() {
		PhotonNetwork.NetworkStatisticsReset();

		if ( !GetComponent<GameMaster> ( ).AmIMaster ( ) && PhotonNetwork.isMasterClient ) {
			PhotonNetwork.Disconnect ( );

			Debug.Log ( "Error network" );

			StartCoroutine ( resetConnexion ( ) );

			return;
		}
	}

	public int getMyID ( ) {
		return _nextId++;
	}

	public int getPlayersCount ( ) { 
		PhotonNetwork.NetworkStatisticsReset();
		return PhotonNetwork.playerList.Length;
	}

	void OnDestroy ( ) {
		try {
			PhotonNetwork.Disconnect ( );
		}
		catch ( System.Exception error ) {
			Debug.Log ( error.Message );
		}
		_created = false;
	}
	
	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		
		if (PhotonNetwork.isMasterClient)
		{
			GUILayout.Label("Master Client");
		}
	}

}
