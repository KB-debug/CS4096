using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/StartJumpCooldown")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "StartJumpCooldown", message: "Start Countdown For Jump", category: "Events", id: "f53175a7b86caf0b643cb436f0091b12")]
public sealed partial class StartJumpCooldown : EventChannel { }

