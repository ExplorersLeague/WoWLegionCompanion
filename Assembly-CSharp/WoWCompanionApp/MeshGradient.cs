using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	[AddComponentMenu("UI/Effects/MeshGradient")]
	public class MeshGradient : BaseMeshEffect
	{
		public Gradient Gradient
		{
			get
			{
				return this.m_gradient;
			}
			set
			{
				this.m_gradient = value;
				Graphic component = base.GetComponent<Graphic>();
				component.SetVerticesDirty();
			}
		}

		public override void ModifyMesh(VertexHelper vh)
		{
			if (!this.IsActive())
			{
				return;
			}
			List<UIVertex> list = new List<UIVertex>();
			vh.GetUIVertexStream(list);
			this.ModifyVertices(list);
			vh.Clear();
			vh.AddUIVertexTriangleStream(list);
		}

		private void ModifyVertices(List<UIVertex> vertexList)
		{
			int count = vertexList.Count;
			if (count == 0)
			{
				return;
			}
			float num = (!this.m_horizontal) ? vertexList[0].position.y : vertexList[0].position.x;
			float num2 = num;
			float num3 = num;
			for (int i = 1; i < count; i++)
			{
				float num4 = (!this.m_horizontal) ? vertexList[i].position.y : vertexList[i].position.x;
				if (num4 > num3)
				{
					num3 = num4;
				}
				else if (num4 < num2)
				{
					num2 = num4;
				}
			}
			float num5 = num3 - num2;
			for (int j = 0; j < count; j++)
			{
				UIVertex value = vertexList[j];
				float num6 = (!this.m_horizontal) ? vertexList[j].position.y : vertexList[j].position.x;
				float num7 = (num6 - num2) / num5;
				value.color = this.m_gradient.Evaluate((!this.m_horizontal) ? (1f - num7) : num7);
				vertexList[j] = value;
			}
		}

		[SerializeField]
		private Gradient m_gradient;

		public bool m_horizontal;
	}
}
