using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;

namespace WoWCompanionApp
{
	public class MapInfo : MonoBehaviour
	{
		public static void RegisterMapInfo(MapInfo mapInfo)
		{
			if (!MapInfo.s_mapInfoDictionary.ContainsKey(mapInfo.m_zone))
			{
				MapInfo.s_mapInfoDictionary.Add(mapInfo.m_zone, mapInfo);
			}
			else
			{
				MapInfo.s_mapInfoDictionary[mapInfo.m_zone] = mapInfo;
			}
		}

		public static MapInfo GetMapInfo(AdventureMapPanel.eZone zone)
		{
			return (!MapInfo.s_mapInfoDictionary.ContainsKey(zone)) ? null : MapInfo.s_mapInfoDictionary[zone];
		}

		public static List<MapInfo> GetAllMapInfos()
		{
			return (from info in MapInfo.s_mapInfoDictionary.Values
			orderby (int)(((GarrisonStatus.Faction() != PVP_FACTION.HORDE || info.m_zone != AdventureMapPanel.eZone.Zandalar) && (GarrisonStatus.Faction() != PVP_FACTION.ALLIANCE || info.m_zone != AdventureMapPanel.eZone.Kultiras)) ? info.m_zone : ((AdventureMapPanel.eZone)(-2147483648)))
			select info).ToList<MapInfo>();
		}

		private void Awake()
		{
			this.Init();
		}

		public void SetMaxZoom(float val)
		{
			this.m_maxZoomFactor = val;
		}

		public Vector2 GetFillViewSize()
		{
			if (!this.m_initialized)
			{
				this.Init();
				AdventureMapPanel.instance.m_pinchZoomContentManager.SetZoom(1f, false);
			}
			return this.m_fillViewSize;
		}

		public void CalculateFillScale()
		{
			float mapW = this.m_mapW;
			float mapH = this.m_mapH;
			float num = mapW / mapH;
			float num2 = AdventureMapPanel.instance.m_mapViewRT.rect.width / AdventureMapPanel.instance.m_mapViewRT.rect.height;
			this.m_viewRelativeScale = 1f;
			if (num < num2)
			{
				this.m_viewRelativeScale = AdventureMapPanel.instance.m_mapViewRT.rect.width / mapW;
			}
			else
			{
				this.m_viewRelativeScale = AdventureMapPanel.instance.m_mapViewRT.rect.height / mapH;
			}
			this.m_fillViewSize.x = mapW * this.m_viewRelativeScale;
			this.m_fillViewSize.y = mapH * this.m_viewRelativeScale;
		}

		public float GetViewRelativeScale()
		{
			return this.m_viewRelativeScale;
		}

		private void Init()
		{
			if (!this.m_initialized)
			{
				this.CalculateFillScale();
				this.m_initialized = true;
			}
		}

		private void Update()
		{
			if (this.m_initialized)
			{
				this.CalculateFillScale();
				AdventureMapPanel.instance.m_pinchZoomContentManager.SetZoom(AdventureMapPanel.instance.m_pinchZoomContentManager.m_zoomFactor, false);
			}
		}

		public GameObject GetWorldQuestArea()
		{
			return base.gameObject.GetComponentInChildren<ZoneButtonMissionArea>().gameObject;
		}

		public AdventureMapPanel.eZone m_zone;

		public float m_minZoomFactor;

		public float m_maxZoomFactor;

		public GameObject m_scaleRoot;

		public Image m_mapImage;

		public float m_mapW;

		public float m_mapH;

		public float m_sizeDeltaY;

		public Vector2 m_anchoredPos;

		public Sprite m_navButtonSprite;

		public Sprite m_mapSwapButtonSprite;

		public string m_zoneNameCapsKey;

		public string m_zoneNameKey;

		public float m_worldQuestScale;

		public Vector2 m_worldQuestOffset;

		private bool m_initialized;

		private Vector2 m_fillViewSize;

		private float m_viewRelativeScale;

		private static Dictionary<AdventureMapPanel.eZone, MapInfo> s_mapInfoDictionary = new Dictionary<AdventureMapPanel.eZone, MapInfo>();
	}
}
