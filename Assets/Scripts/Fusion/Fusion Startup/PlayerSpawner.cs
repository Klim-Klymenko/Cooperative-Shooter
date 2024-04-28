using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Fusion.Startup
{
    internal sealed class PlayerSpawner : SimulationBehaviour, IPlayerJoined, IPlayerLeft
    {
        [SerializeField]
        private PlayerFactory _playerFactory;
        
        private readonly Dictionary<PlayerRef, NetworkObject> _players = new();

        void IPlayerJoined.PlayerJoined(PlayerRef player)
        {
            if (!Runner.IsServer) return;

            NetworkObject spawnedPlayer = _playerFactory.Create(Runner, player);

            if (spawnedPlayer.TryGetComponent(out IAtomicObject atomicObject))
            {
                IAtomicObservable deathObservable = atomicObject.GetObservable(LiveableAPI.DeathObservable);
                deathObservable.Subscribe(() => Despawn(spawnedPlayer, player));
            }
            
            _players.Add(player, spawnedPlayer);
        }

        void IPlayerLeft.PlayerLeft(PlayerRef player)
        {
            if (!_players.TryGetValue(player, out NetworkObject spawnedPlayer)) return;
            
            Despawn(spawnedPlayer, player);
        }

        private void Despawn(NetworkObject spawnedPlayer, PlayerRef player)
        {
            Runner.Despawn(spawnedPlayer);
            _players.Remove(player);
        }
    }
}