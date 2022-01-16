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
using IngameScript.TiCommons.IGCTi;

namespace IngameScript.Alert {
	partial class Program {
		/// <include file='AlertHelperDocs.xml' path='//member[contains(@name, "Program.Start()")]'/>
		public void Start() {
			bool all = Switches["all"];

			IsActivated = true;

			if (Switches["sounds"] || all) {
				var soundBlocks = GridTerminalSystem.GetBlocksOfType<IMySoundBlock>();

				foreach (var soundBlock in soundBlocks) {
					OriginalBlockProperties[soundBlock] = new Properties();
					OriginalBlockProperties[soundBlock].Strings["SelectedSound"] = soundBlock.SelectedSound;
					OriginalBlockProperties[soundBlock].Floats["LoopPeriod"] = soundBlock.LoopPeriod;
					soundBlock.SelectedSound = "Alert 1";
					soundBlock.LoopPeriod = 30 * 60;
					soundBlock.Play();
				}
			}

			if (Switches["lights"] || all) {
				var lightBlocks = GridTerminalSystem.GetBlocksOfType<IMyLightingBlock>();

				foreach (var lightBlock in lightBlocks) {
					OriginalBlockProperties[lightBlock] = new Properties();
					OriginalBlockProperties[lightBlock].Bools["Enabled"] = lightBlock.Enabled;
					OriginalBlockProperties[lightBlock].Colors["Color"] = lightBlock.Color;
					OriginalBlockProperties[lightBlock].Floats["BlinkIntervalSeconds"] = lightBlock.BlinkIntervalSeconds;
					OriginalBlockProperties[lightBlock].Floats["BlinkLength"] = lightBlock.BlinkLength;
					OriginalBlockProperties[lightBlock].Floats["Radius"] = lightBlock.Radius;
					OriginalBlockProperties[lightBlock].Floats["Intensity"] = lightBlock.Intensity;
					OriginalBlockProperties[lightBlock].Floats["Falloff"] = lightBlock.Falloff;
					lightBlock.Color = Switches["red"] ? Color.Red : Color.Yellow;
					lightBlock.Radius = 20;
					lightBlock.Intensity = 10;
					lightBlock.Falloff = 3;

					if (!(lightBlock is IMyReflectorLight)) {
						if (Switches["rotatingOnly"]) {
							lightBlock.Enabled = false;
						} else {
							lightBlock.BlinkIntervalSeconds = 1;
							lightBlock.BlinkLength = 50;
						}
					} else {
						lightBlock.Enabled = !Switches["noRotating"];
					}
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
