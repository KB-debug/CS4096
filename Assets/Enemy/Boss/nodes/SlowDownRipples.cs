using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/Slow Down Ripples")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "Slow Down Ripples", message: "Equake has occured", category: "Events", id: "29b7778c238017f7704d31671cdc89b2")]
public sealed partial class SlowDownRipples : EventChannel { }

