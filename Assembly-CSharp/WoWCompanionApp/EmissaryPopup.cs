using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class EmissaryPopup : MonoBehaviour
	{
		public void FactionUpdate(IEnumerable<WrapperEmissaryFaction> factions)
		{
			this.m_descriptionText.text = string.Empty;
			foreach (WrapperEmissaryFaction wrapperEmissaryFaction in factions)
			{
				Text descriptionText = this.m_descriptionText;
				string text = descriptionText.text;
				descriptionText.text = string.Concat(new object[]
				{
					text,
					"FactionID:\t",
					wrapperEmissaryFaction.FactionID,
					"\t Standing:\t",
					wrapperEmissaryFaction.FactionAmount,
					"\n"
				});
			}
		}

		public Text m_descriptionText;
	}
}
