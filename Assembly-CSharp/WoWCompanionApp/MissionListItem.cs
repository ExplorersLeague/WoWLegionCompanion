using System;
using UnityEngine;
using UnityEngine.UI;
using WowStaticData;

namespace WoWCompanionApp
{
	public class MissionListItem : MonoBehaviour
	{
		private void Start()
		{
		}

		private void Update()
		{
			if (!this.missionTimeRemainingText.gameObject.activeSelf)
			{
				return;
			}
			TimeSpan t = GarrisonStatus.CurrentTime() - this.missionStartedTime;
			TimeSpan timeSpan = this.missionDurationInSeconds - t;
			timeSpan = ((timeSpan.TotalSeconds <= 0.0) ? TimeSpan.Zero : timeSpan);
			this.missionTimeRemainingText.text = timeSpan.GetDurationString(false, TimeUnit.Second) + " <color=#ff0000ff>(In Progress)</color>";
		}

		public void OnTap()
		{
			if (this.isResultsItem)
			{
				return;
			}
		}

		public void Init(int missionRecID)
		{
			this.garrMissionID = missionRecID;
			GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(this.garrMissionID);
			if (record == null)
			{
				return;
			}
			if (!PersistentMissionData.missionDictionary.ContainsKey(this.garrMissionID))
			{
				return;
			}
			this.missionDurationInSeconds = TimeSpan.FromSeconds((double)record.MissionDuration);
			WrapperGarrisonMission wrapperGarrisonMission = PersistentMissionData.missionDictionary[this.garrMissionID];
			this.missionStartedTime = wrapperGarrisonMission.StartTime;
			Duration duration = new Duration(record.MissionDuration, false);
			string str = (duration.Hours < 2) ? "<color=#ffffffff>" : "<color=#ff8600ff>";
			TimeSpan t = GarrisonStatus.CurrentTime() - this.missionStartedTime;
			TimeSpan timeSpan = this.missionDurationInSeconds - t;
			timeSpan = ((timeSpan.TotalSeconds <= 0.0) ? TimeSpan.Zero : timeSpan);
			bool flag = wrapperGarrisonMission.MissionState == 1 && timeSpan.TotalSeconds > 0.0;
			this.missionNameText.text = record.Name + ((!flag) ? (" (" + str + duration.DurationString + "</color>)") : string.Empty);
			this.missionLevelText.text = string.Empty + record.TargetLevel;
			this.inProgressDarkener.SetActive(flag);
			this.missionTimeRemainingText.gameObject.SetActive(flag);
			this.missionDurationInSeconds = TimeSpan.FromSeconds((double)record.MissionDuration);
			this.missionResultsText.gameObject.SetActive(false);
			this.isResultsItem = false;
			MissionRewardDisplay.InitMissionRewards(this.missionRewardDisplayPrefab, this.missionRewardGroup.transform, wrapperGarrisonMission.Rewards);
			for (int i = 0; i < this.locationImages.Length; i++)
			{
				if (this.locationImages[i] != null)
				{
					this.locationImages[i].gameObject.SetActive(false);
				}
			}
			Image image = null;
			ushort uiTextureKitID = record.UiTextureKitID;
			switch (uiTextureKitID)
			{
			case 101:
				image = this.locationImages[1];
				this.locationIndex = 1;
				break;
			case 102:
				image = this.locationImages[10];
				this.locationIndex = 10;
				break;
			case 103:
				image = this.locationImages[3];
				this.locationIndex = 3;
				break;
			case 104:
				image = this.locationImages[4];
				this.locationIndex = 4;
				break;
			case 105:
				image = this.locationImages[9];
				this.locationIndex = 9;
				break;
			case 106:
				image = this.locationImages[7];
				this.locationIndex = 7;
				break;
			case 107:
				image = this.locationImages[8];
				this.locationIndex = 8;
				break;
			default:
				switch (uiTextureKitID)
				{
				case 203:
					image = this.locationImages[2];
					this.locationIndex = 2;
					break;
				case 204:
					image = this.locationImages[6];
					this.locationIndex = 6;
					break;
				case 205:
					image = this.locationImages[5];
					this.locationIndex = 5;
					break;
				default:
					if (uiTextureKitID != 164)
					{
						if (uiTextureKitID != 165)
						{
							this.locationIndex = 0;
						}
						else
						{
							image = this.locationImages[11];
							this.locationIndex = 11;
						}
					}
					else
					{
						image = this.locationImages[0];
						this.locationIndex = 0;
					}
					break;
				}
				break;
			}
			if (image != null)
			{
				image.gameObject.SetActive(true);
			}
			GarrMissionTypeRec record2 = StaticDB.garrMissionTypeDB.GetRecord((int)record.GarrMissionTypeID);
			this.m_missionTypeImage.overrideSprite = TextureAtlas.instance.GetAtlasSprite((int)record2.UiTextureAtlasMemberID);
			if ((record.Flags & 1u) != 0u)
			{
				this.rareMissionText.gameObject.SetActive(true);
				Color color = this.levelBG.color;
				color.r = 0f;
				color.g = 0.211f;
				color.b = 0.506f;
				this.levelBG.color = color;
			}
			else
			{
				this.rareMissionText.gameObject.SetActive(false);
			}
		}

		public Text missionNameText;

		public Text missionLevelText;

		public Text missionTimeRemainingText;

		public Text missionResultsText;

		public Text rareMissionText;

		public Image levelBG;

		public GameObject inProgressDarkener;

		public Image[] locationImages;

		public Image m_missionTypeImage;

		public int garrMissionID;

		public int locationIndex;

		public GameObject missionRewardGroup;

		public GameObject missionRewardDisplayPrefab;

		public bool isResultsItem;

		private TimeSpan missionDurationInSeconds;

		private DateTime missionStartedTime;
	}
}
