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
		/// <include file='AlertHelperDocs.xml' path='//member[contains(@name, "Program.Stop()")]'/>
		public void Stop() {
			bool all = Switches["all"];

			IsActivated = false;

			if (Switches["sounds"] || all) {
				var soundBlocks = GridTerminalSystem.GetBlocksOfType<IMySoundBlock>();

				foreach (var soundBlock in soundBlocks) {
					soundBlock.SelectedSound = OriginalSoundBlockProperties.OriginalStrings["SelectedSound"];
					soundBlock.LoopPeriod = OriginalSoundBlockProperties.OriginalFloats["LoopPeriod"];
					soundBlock.Stop();
				}
			}

			if (Switches["lights"] || all) {
				var lightBlocks = GridTerminalSystem.GetBlocksOfType<IMyLightingBlock>();

				foreach (var lightBlock in lightBlocks) {
					lightBlock.Color = OriginalLightPropertiers.OriginalColors["Color"];
					lightBlock.BlinkIntervalSeconds = OriginalLightPropertiers.OriginalFloats["BlinkIntervalSeconds"];
					lightBlock.BlinkLength = OriginalLightPropertiers.OriginalFloats["BlinkLength"];
					lightBlock.Radius = OriginalLightPropertiers.OriginalFloats["Radius"];
					lightBlock.Intensity = OriginalLightPropertiers.OriginalFloats["Intensity"];
					lightBlock.Falloff = OriginalLightPropertiers.OriginalFloats["Falloff"];
				}
			}

			if (Switches["doors"] || all) {
				var doorBlocks = GridTerminalSystem.GetBlocksOfType<IMyDoor>();

				foreach (var doorBlock in doorBlocks) {
					doorBlock.Enabled = true;
					doorBlock.OpenDoor();
				}
			}
		}
	}
}
