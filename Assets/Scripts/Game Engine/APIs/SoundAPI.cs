using Atomic.Extensions;
using UnityEngine;

namespace GameEngine
{
    public sealed class SoundAPI
    {
        [Contract(typeof(AudioClip))]
        public const string AttackClip = nameof(AttackClip);
    }
}