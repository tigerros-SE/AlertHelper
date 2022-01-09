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
		public void Main(string argument, UpdateType updateSource) {
			if ((updateSource & (UpdateType.Update10 | UpdateType.Update100)) != 0) {
				var turrets = GridTerminalSystem.GetBlocksOfType<IMyLargeInteriorTurret>();

				foreach (var turret in turrets) {
					if (turret.IsShooting) {
						var targetType = turret.GetTargetedEntity().Type;

						if (targetType == MyDetectedEntityType.Meteor) {
							Me.TryRun("start -all -red");
							//IGC.SendBroadcastMessage($"SYSTEM", "ALERT|METEOR STORM INBOUND|1");
						} else if (targetType == MyDetectedEntityType.Missile) {
							Me.TryRun("start -sounds -lights -red");
							//IGC.SendBroadcastMessage($"SYSTEM", "ALERT|HOSTILE WEAPONS DETECTED|1");
						} else if (targetType == MyDetectedEntityType.CharacterOther) {
							Me.TryRun("start -lights -yellow");
							//IGC.SendBroadcastMessage($"SYSTEM", "ALERT|UNINDENTIFIED CHARACTER DETECTED|1");
						}
					}
				}
			} else if (CommandLine.TryParse(argument)) {
				foreach (var key in Switches.Keys.ToList()) {
					Switches[key] = false;
				}

				CommandLine.HandleSwitches(Me, Switches);
				CommandLine.HandleArguments(Me, Arguments);
			}
		}
	}
}
