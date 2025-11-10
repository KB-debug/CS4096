using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/Stagger")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "Stagger", message: "Agent Has Been Staggered", category: "Events", id: "3be03b2e732f5ab29f8d0273e105e53d")]
public sealed partial class Stagger : EventChannel { }

