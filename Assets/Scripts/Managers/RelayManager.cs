using System.Threading.Tasks;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;

namespace TestMulti.Game.Managers
{
    public class RelayManager : BaseManager
    {
        public bool IsInGame => NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsClient;

        public async Task<string> CreateRelay(int maxConnections)
        {
            try
            {
                Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnections);
                string joinCode = await Relay.Instance.GetJoinCodeAsync(allocation.AllocationId);

                NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(
                    allocation.RelayServer.IpV4,
                    (ushort)allocation.RelayServer.Port,
                    allocation.AllocationIdBytes,
                    allocation.Key,
                    allocation.ConnectionData);

                NetworkManager.Singleton.StartHost();
                Debug.Log($"Created relay with join code {joinCode}");
                return joinCode;
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
                return string.Empty;
            }
        }

        public async Task JoinRelay(string joinCode)
        {
            try
            {
                JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

                NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(
                    allocation.RelayServer.IpV4,
                    (ushort)allocation.RelayServer.Port,
                    allocation.AllocationIdBytes,
                    allocation.Key,
                    allocation.ConnectionData,
                    allocation.HostConnectionData);

                NetworkManager.Singleton.StartClient();
                Debug.Log($"Joined relay with join code {joinCode}");
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }
        }
    }
}