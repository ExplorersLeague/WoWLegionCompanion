using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public abstract class RosterPage<PageItemType, MemberInfoType> : MonoBehaviour where PageItemType : RosterPageItem<MemberInfoType>
	{
		private void RecalculateSize()
		{
			GridLayoutGroup componentInChildren = base.GetComponentInChildren<GridLayoutGroup>();
			if (componentInChildren != null && componentInChildren.constraint == 1)
			{
				RectTransform rectTransform = base.GetComponentInParent<ScrollRect>().transform as RectTransform;
				float width = rectTransform.rect.width;
				Vector2 cellSize = componentInChildren.cellSize;
				cellSize.x = (width - componentInChildren.spacing.x - (float)componentInChildren.padding.left - (float)componentInChildren.padding.right) / (float)componentInChildren.constraintCount;
				componentInChildren.cellSize = cellSize;
			}
		}

		public void AddMemberToRoster(MemberInfoType member)
		{
			PageItemType pageItemType = this.m_contentPane.AddAsChildObject(this.m_memberButtonPrefab);
			pageItemType.PopulateMemberInfo(member);
			this.RecalculateSize();
		}

		public bool AtCapacity()
		{
			return this.m_contentPane.transform.childCount == this.m_pageCapacity;
		}

		public PageItemType m_memberButtonPrefab;

		public GameObject m_contentPane;

		public int m_pageCapacity;
	}
}
