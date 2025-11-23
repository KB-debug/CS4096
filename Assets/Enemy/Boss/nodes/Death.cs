using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/Death")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "Death", message: "Agent Has Died", category: "Events", id: "32a332f70c0edb09dca2e006364691d8")]
public sealed partial class Death : EventChannel { }

