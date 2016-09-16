using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "ObjectDebugInfo", Version = 28333852u)]
	public class ObjectDebugInfo
	{
		public ObjectDebugInfo()
		{
			this.Initialized = false;
			this.MapID = 0;
		}

		[System.Runtime.Serialization.DataMember(Name = "guid")]
		[FlexJamMember(Name = "guid", Type = FlexJamType.WowGuid)]
		public string Guid { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "rawFacing")]
		[FlexJamMember(Name = "rawFacing", Type = FlexJamType.Float)]
		public float RawFacing { get; set; }

		[FlexJamMember(Optional = true, Name = "gameObjectDebugInfo", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "gameObjectDebugInfo")]
		public GameObjectDebugInfo[] GameObjectDebugInfo { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "attributeDescriptions")]
		[FlexJamMember(ArrayDimensions = 1, Name = "attributeDescriptions", Type = FlexJamType.Struct)]
		public DebugAttributeDescription[] AttributeDescriptions { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "updateTime")]
		[FlexJamMember(Name = "updateTime", Type = FlexJamType.Int32)]
		public int UpdateTime { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "attributes")]
		[FlexJamMember(ArrayDimensions = 1, Name = "attributes", Type = FlexJamType.Struct)]
		public DebugAttribute[] Attributes { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "mapID")]
		[FlexJamMember(Name = "mapID", Type = FlexJamType.Int32)]
		public int MapID { get; set; }

		[FlexJamMember(Name = "typeID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "typeID")]
		public int TypeID { get; set; }

		[FlexJamMember(Name = "position", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "position")]
		public Vector3 Position { get; set; }

		[FlexJamMember(Name = "rawPosition", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "rawPosition")]
		public Vector3 RawPosition { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "scriptTableValueDebugInfo", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "scriptTableValueDebugInfo")]
		public ScriptTableValueDebugInfo[] ScriptTableValueDebugInfo { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "phaseInfo", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "phaseInfo")]
		public ObjectPhaseDebugInfo[] PhaseInfo { get; set; }

		[FlexJamMember(Name = "ID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "ID")]
		public int ID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "initialized")]
		[FlexJamMember(Name = "initialized", Type = FlexJamType.Bool)]
		public bool Initialized { get; set; }

		[FlexJamMember(Optional = true, Name = "playerDebugInfo", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "playerDebugInfo")]
		public PlayerDebugInfo[] PlayerDebugInfo { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "unitDebugInfo")]
		[FlexJamMember(Optional = true, Name = "unitDebugInfo", Type = FlexJamType.Struct)]
		public UnitDebugInfo[] UnitDebugInfo { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "facing")]
		[FlexJamMember(Name = "facing", Type = FlexJamType.Float)]
		public float Facing { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "name")]
		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		public string Name { get; set; }
	}
}
