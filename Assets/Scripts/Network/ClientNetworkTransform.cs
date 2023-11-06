using UnityEngine;
using Unity.Netcode.Components;

namespace TestMulti.Game.Network
{
    [DisallowMultipleComponent]
    public class ClientNetworkTransform : NetworkTransform
    {
        protected override bool OnIsServerAuthoritative() => false;
    }
}