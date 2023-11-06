using UnityEngine;
using Unity.Netcode;

namespace TestMulti.Game.Network.PlayerComponents
{
    public struct PlayerTeamSync : INetworkSerializable
    {
        private int teamIndex;
        private Color teamColor;

        public int TeamIndex => teamIndex;
        public Color TeamColor => teamColor;

        public PlayerTeamSync(int teamIndex, Color teamColor)
        {
            this.teamIndex = teamIndex;
            this.teamColor = teamColor;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref teamIndex);
            serializer.SerializeValue(ref teamColor);
        }
    }
}