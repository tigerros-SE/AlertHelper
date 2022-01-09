using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;
using IngameScript.TiCommons.Extensions;

namespace IngameScript.Alert {
	partial class Program {
		/// <include file='AlertHelperDocs.xml' path='//member[contains(@name, "Program.Start()")]'/>
		public void Start() {
			bool all = Switches["all"];

			IsActivated = true;

			if (Switches["sounds"] || all) {
				var soundBlocks = GridTerminalSystem.GetBlocksOfType<IMySoundBlock>();

				foreach (var soundBlock in soundBlocks) {
					OriginalSoundBlockProperties.OriginalStrings["SelectedSound"] = soundBlock.SelectedSound;
					OriginalSoundBlockProperties.OriginalFloats["LoopPeriod"] = soundBlock.LoopPeriod;
					soundBlock.SelectedSound = "Alert 1";
					soundBlock.LoopPeriod = 30 * 60;
					soundBlock.Play();
				}
			}

			if (Switches["lights"] || all) {
				var lightBlocks = GridTerminalSystem.GetBlocksOfType<IMyLightingBlock>();

				foreach (var lightBlock in lightBlocks) {
					OriginalLightPropertiers.OriginalColors["Color"] = lightBlock.Color;
					OriginalLightPropertiers.OriginalFloats["BlinkIntervalSeconds"] = lightBlock.BlinkIntervalSeconds;
					OriginalLightPropertiers.OriginalFloats["BlinkLength"] = lightBlock.BlinkLength;
					OriginalLightPropertiers.OriginalFloats["Radius"] = lightBlock.Radius;
					OriginalLightPropertiers.OriginalFloats["Intensity"] = lightBlock.Intensity;
					OriginalLightPropertiers.OriginalFloats["Falloff"] = lightBlock.Falloff;
					lightBlock.Color = Switches["red"] ? Color.Red : Color.Yellow;
					lightBlock.BlinkIntervalSeconds = 1;
					lightBlock.BlinkLength = 50;
					lightBlock.Radius = 20;
					lightBlock.Intensity = 10;
					lightBlock.Falloff = 3;
				}
			}

			if (Switches["doors"] || all) {
				var doorBlocks = GridTerminalSystem.GetBlocksOfType<IMyDoor>();

				foreach (var doorBlock in doorBlocks) {
					doorBlock.CloseDoor();
				}
			}
		}
	}
}
