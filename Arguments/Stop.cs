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
using SpaceEngineers.Game.Entities.Blocks;

namespace IngameScript.Alert {
	partial class Program {
		/// <include file='AlertHelperDocs.xml' path='//member[contains(@name, "Program.Stop()")]'/>
		public void Stop() {
			bool all = Switches["all"];

			IsActivated = false;

			if (Switches["sounds"] || all) {
				var soundBlocks =
					OriginalBlockProperties.Where(pair => pair.Key is IMySoundBlock)
					.Select(pair => pair.Key as IMySoundBlock);

				foreach (var soundBlock in soundBlocks.ToList()) {
					soundBlock.SelectedSound = OriginalBlockProperties[soundBlock].Strings["SelectedSound"];
					soundBlock.LoopPeriod = OriginalBlockProperties[soundBlock].Floats["LoopPeriod"];
					OriginalBlockProperties.Remove(soundBlock);
					soundBlock.Stop();
				}
			}

			if (Switches["lights"] || all) {
				var lightBlocks =
					OriginalBlockProperties.Where(pair => pair.Key is IMyLightingBlock)
					.Select(pair => pair.Key as IMyLightingBlock);

				foreach (var lightBlock in lightBlocks.ToList()) {
					lightBlock.Enabled = OriginalBlockProperties[lightBlock].Bools["Enabled"];
					lightBlock.Color = OriginalBlockProperties[lightBlock].Colors["Color"];
					lightBlock.BlinkIntervalSeconds = OriginalBlockProperties[lightBlock].Floats["BlinkIntervalSeconds"];
					lightBlock.BlinkLength = OriginalBlockProperties[lightBlock].Floats["BlinkLength"];
					lightBlock.Radius = OriginalBlockProperties[lightBlock].Floats["Radius"];
					lightBlock.Intensity = OriginalBlockProperties[lightBlock].Floats["Intensity"];
					lightBlock.Falloff = OriginalBlockProperties[lightBlock].Floats["Falloff"];
					OriginalBlockProperties.Remove(lightBlock);
				}
			}

			if (Switches["doors"] || all) {
				var doorBlocks =
					OriginalBlockProperties.Where(pair => pair.Key is IMyDoor)
					.Select(pair => pair.Key as IMyDoor);

				foreach (var doorBlock in doorBlocks.ToList()) {
					doorBlock.Enabled = true;
					doorBlock.OpenDoor();
				}
			}
		}
	}
}
