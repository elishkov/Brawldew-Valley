using Cinemachine;
using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Assets.scripts.Utils;

public class BasicSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] private NetworkPrefabRef player1Prefab;
    [SerializeField] private NetworkPrefabRef player2Prefab;
    private bool player1Prefab_spawned = false;

    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        // Create a unique position for the player
        if (runner.IsServer)
        {            
            NetworkObject networkPlayerObject = null;
            Vector3 spawnPosition;
            print("OnPlayerJoined - running as Server, spawning player");
            spawnPosition = new Vector3(
                (player.RawEncoded % runner.Config.Simulation.DefaultPlayers) * 3,
                -0.237660408f, -2f);

            NetworkPrefabRef prefab_to_spawn = GetPrefabToSpawn();

            networkPlayerObject = runner.Spawn(prefab_to_spawn, spawnPosition, Quaternion.identity, player);
            print($"networkPlayerObject created: {networkPlayerObject}");
            _spawnedCharacters.Add(player, networkPlayerObject);
            PrintObj(
                "isActiveAndEnabled",
                networkPlayerObject.GetComponent<NetworkObject>().isActiveAndEnabled
                );

        }
        else
        {
            print("OnPlayerJoined - running as client, ignore");
        }

        NetworkPrefabRef GetPrefabToSpawn()
        {
            NetworkPrefabRef prefab_to_spawn;
            if (!this.player1Prefab_spawned)
            {
                prefab_to_spawn = player1Prefab;
                this.player1Prefab_spawned = true;
            }
            else
            {
                prefab_to_spawn = player2Prefab;
            }

            return prefab_to_spawn;
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        // Find and remove the players avatar
        if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        {
            runner.Despawn(networkObject);
            _spawnedCharacters.Remove(player);
        }
    }
    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        var data = new NetworkInputData();

        if (Input.GetKey(KeyCode.W))
            data.direction += new Vector3(0,1);

        if (Input.GetKey(KeyCode.S))
            data.direction += new Vector3(0, -1);

        if (Input.GetKey(KeyCode.A))
            data.direction += Vector3.left;

        if (Input.GetKey(KeyCode.D))
            data.direction += Vector3.right;

        if (Input.GetKey(KeyCode.Space))
            data.attack = true;

        input.Set(data);
    }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }

    private NetworkRunner _runner;

    private void OnGUI()
    {
        if (_runner == null)
        {
            if (GUI.Button(new Rect(0, 0, 200, 40), "Host"))
            {
                StartGame(GameMode.Host);
            }
            if (GUI.Button(new Rect(0, 40, 200, 40), "Join"))
            {
                StartGame(GameMode.Client);
            }
        }
    }

    async void StartGame(GameMode mode)
    {
        // Create the Fusion runner and let it know that we will be providing user input
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;

        // Start or join (depends on gamemode) a session with a specific name
        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = "Brawldew Session",
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }
}
