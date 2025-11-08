using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/StartShootingCooldown")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "StartShootingCooldown", message: "Agent has Shot", category: "Events", id: "5b0158075b2b842eac53bf89c88ab9a6")]
public sealed partial class StartShootingCooldown : EventChannel { }

