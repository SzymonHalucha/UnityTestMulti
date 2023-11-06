using UnityEngine;
using Unity.Netcode;

namespace TestMulti.Game.Network.PlayerComponents
{
    public struct PlayerArtSync : INetworkSerializable
    {
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            
        }
    }
}