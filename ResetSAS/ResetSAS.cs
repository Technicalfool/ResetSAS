using System;
using UnityEngine;

namespace ResetSAS
{
	[KSPAddon(KSPAddon.Startup.Flight, false)]
	public class ResetSAS : MonoBehaviour
	{
		static bool lastSAS = false;
		/*!
		 * I hate this bit. No event to capture SAS on/off, so have to check every damn frame!
		 */
		public void Update()
		{
			Vessel v = FlightGlobals.ActiveVessel; //Get the active vessel.
			/*
			 * Check if the autopilot is enabled.
			 */
			if (v.Autopilot.Enabled){
				/*
				 * If the autopilot WASN'T enabled, then something changed.
				 * Reset the SAS to Stability Assist mode.
				 */
				if (!lastSAS){
					lastSAS = true;
					resetSAS();
				}
			}else{
				/*
				 * If the autopilot WAS enabled, then something changed.
				 * Mark that the autopilot is now disabled.
				 */
				if (lastSAS){
					lastSAS = false;
				}
			}
		}
		static private void resetSAS()
		{
			Vessel v = FlightGlobals.ActiveVessel;
			VesselAutopilotUI aui = (VesselAutopilotUI) FindObjectOfType(typeof(VesselAutopilotUI));
			foreach(RUIToggleButton tb in aui.modeButtons)
			{
				tb.SetFalse();
			}
			v.Autopilot.SetMode(VesselAutopilot.AutopilotMode.StabilityAssist);
		}
	}

}

