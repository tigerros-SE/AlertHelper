using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.Entities.Blocks;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
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
	/// <include file='AlertHelperDocs.xml' path='//member[contains(@name, "Alert.Program")]'/>
	partial class Program : MyGridProgram {
		// This is disgusting, but reflection is not allowed in SpaceEngineers :(
		// AND the IMyTerminalBlock.SetValue() method does not work!
		static class OriginalSoundBlockProperties {
			public static Dictionary<string, string> OriginalStrings = new Dictionary<string, string>();
			public static Dictionary<string, float> OriginalFloats = new Dictionary<string, float>();
		}

		static class OriginalLightPropertiers {
			public static Dictionary<string, string> OriginalStrings = new Dictionary<string, string>();
			public static Dictionary<string, float> OriginalFloats = new Dictionary<string, float>();
			public static Dictionary<string, Color> OriginalColors = new Dictionary<string, Color>();
		}

		IMyTextSurface Surface;
		MyCommandLine CommandLine = new MyCommandLine();
		Dictionary<string, Action> Arguments = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase);
		Dictionary<string, bool> Switches = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase) {
			["doors"] = false,
			["lights"] = false,
			["sounds"] = false,
			["all"] = false,
			["red"] = false,
			["yellow"] = false
		};

		string Description = @"Arguments:
start - Starts the lockdown
stop - Stops the lockdown
		 
Switches:
doors - Closes all doors
lights - Makes the lights blink and go red
sounds - Starts the 'Alert 1' sound";

		bool _isActivated;
		bool IsActivated {
			get { return _isActivated; }
			set {
				_isActivated = value;
				if (value) {
					Surface.Writeln($"ALERT: CODE {(Switches["red"] ? "RED" : "YELLOW")} {(value ? "ACTIVATED" : "DEACTIVATED")}");
				} else {
					Surface.Writeln($"ALERT: {(value ? "ACTIVATED" : "DEACTIVATED")}");
				}
				
				//IGC.SendBroadcastMessage("SYSTEM", $"ALERT|ACTIVATED, REASON IS UNKNOWN|{(value ? 1 : 0)}");
			}
		}

		public Program() {
			Runtime.UpdateFrequency = UpdateFrequency.Update10;

			Surface = Me.GetSurface(0);
			Surface.ContentType = ContentType.TEXT_AND_IMAGE;
			Surface.FontSize = 1;
			Surface.Alignment = TextAlignment.LEFT;
			Surface.BackgroundColor = Color.Green;
			Surface.FontColor = Color.White;
		
			Me.WritelnAndData("ALERT HELPER STATUS: ONLINE", true);

			Arguments["start"] = Start;
			Arguments["stop"] = Stop;
		}
	}
}
