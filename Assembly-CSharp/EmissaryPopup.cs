using System;
using UnityEngine;
using UnityEngine.UI;
using WowJamMessages.MobileClientJSON;

public class EmissaryPopup : MonoBehaviour
{
	public void FactionUpdate(MobileClientEmissaryFactionUpdate msg)
	{
		this.m_descriptionText.text = string.Empty;
		foreach (MobileEmissaryFaction mobileEmissaryFaction in msg.Faction)
		{
			Text descriptionText = this.m_descriptionText;
			string text = descriptionText.text;
			descriptionText.text = string.Concat(new object[]
			{
				text,
				"FactionID:\t",
				mobileEmissaryFaction.FactionID,
				"\t Standing:\t",
				mobileEmissaryFaction.FactionAmount,
				"\n"
			});
		}
	}

	public Text m_descriptionText;
}
