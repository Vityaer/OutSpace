using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class NetworkManagerScript : NetworkLobbyManager{
    public void CreateEnemy(GameObject prefabEnemy){
    		GameObject Enemy = Instantiate(prefabEnemy);
    		NetworkServer.Spawn(Enemy);
	}
	private GameObject CreatePlayer(int CurrentPlayer){
		return Instantiate(gamePlayerPrefab, startPositions[CurrentPlayer].position, Quaternion.identity);
	}
	public override void OnServerConnect(NetworkConnection conn) {
        Debug.Log("OnServerConnect: " + conn);
    }
    private int createdPlayer = 0;
    public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short PlayerControllerId){
		Debug.Log("создаю игрока");
        GameObject Player = CreatePlayer(createdPlayer++);
        Debug.Log(createdPlayer.ToString() + " from " + NetworkServer.connections.Count.ToString());
		// if(createdPlayer == NetworkServer.connections.Count)
			// EnemiesCotrollerScript.Instance.StartLevel();
        return Player;        		
	}

    public override void OnLobbyServerDisconnect(NetworkConnection conn) {
        NetworkServer.DestroyPlayersForConnection(conn);
        if (conn.lastError != NetworkError.Ok) {
            if (LogFilter.logError) { Debug.LogError("ServerDisconnected due to error: " + conn.lastError); }
        }
        Debug.Log("A client disconnected from the server: " + conn);
    }
    
    public override void OnServerError(NetworkConnection conn, int errorCode) {
        Debug.Log("Server network error occurred: " + (NetworkError)errorCode);
    }

    public override void OnLobbyStartHost() {
        Debug.Log("OnStartHost");
    }

    public override void OnLobbyStartServer() {
        Debug.Log("OnStartServer");
    }

    public override void OnLobbyStopHost() {
        ClearData();
        Debug.Log("OnStopHost");
    }

    // Client callbacks
    [Header("ControllerID")]
    public short ControllerID = 0;
    public override void OnLobbyClientConnect(NetworkConnection conn){
        ClearData();
        Debug.Log("Client Side : Client " + conn.connectionId + " Connected!");
        base.OnLobbyClientConnect(conn);
    }
    public override void OnLobbyClientDisconnect(NetworkConnection conn) {
        StopClient();
        ClearData();
        if (conn.lastError != NetworkError.Ok)
        {
            if (LogFilter.logError) { Debug.LogError("ClientDisconnected due to error: " + conn.lastError); }
        }
        Debug.Log("Client disconnected from server: " + conn);
    }

    public override void OnLobbyStartClient(NetworkClient client) {
        Debug.Log("OnStartClient");
    }

    public override void OnLobbyStopClient() {
        Debug.Log("OnStopClient");
    }

    public override void OnLobbyClientSceneChanged(NetworkConnection conn) {
        Debug.Log("OnClientSceneChanged");
        base.OnLobbyClientSceneChanged(conn);
    }
    private void ClearData(){
        createdPlayer = 0;
    }
}