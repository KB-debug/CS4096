using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/Start Defend Cooldown")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "Start Defend Cooldown", message: "Start Countdown For Defend", category: "Events", id: "b25e9a2020a4fdf6170598a2cfa28c23")]
public sealed partial class StartDefendCooldown : EventChannel { }

