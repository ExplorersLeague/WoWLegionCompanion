using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "ObjectDebugInfo", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
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

		[FlexJamMember(Name = "updateTime", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "updateTime")]
		public int UpdateTime { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "attributes", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "attributes")]
		public DebugAttribute[] Attributes { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "mapID")]
		[FlexJamMember(Name = "mapID", Type = FlexJamType.Int32)]
		public int MapID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "typeID")]
		[FlexJamMember(Name = "typeID", Type = FlexJamType.Int32)]
		public int TypeID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "position")]
		[FlexJamMember(Name = "position", Type = FlexJamType.Struct)]
		public Vector3 Position { get; set; }

		[FlexJamMember(Name = "rawPosition", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "rawPosition")]
		public Vector3 RawPosition { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "scriptTableValueDebugInfo")]
		[FlexJamMember(ArrayDimensions = 1, Name = "scriptTableValueDebugInfo", Type = FlexJamType.Struct)]
		public ScriptTableValueDebugInfo[] ScriptTableValueDebugInfo { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "phaseInfo")]
		[FlexJamMember(ArrayDimensions = 1, Name = "phaseInfo", Type = FlexJamType.Struct)]
		public ObjectPhaseDebugInfo[] PhaseInfo { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "ID")]
		[FlexJamMember(Name = "ID", Type = FlexJamType.Int32)]
		public int ID { get; set; }

		[FlexJamMember(Name = "initialized", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "initialized")]
		public bool Initialized { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "playerDebugInfo")]
		[FlexJamMember(Optional = true, Name = "playerDebugInfo", Type = FlexJamType.Struct)]
		public PlayerDebugInfo[] PlayerDebugInfo { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "unitDebugInfo")]
		[FlexJamMember(Optional = true, Name = "unitDebugInfo", Type = FlexJamType.Struct)]
		public UnitDebugInfo[] UnitDebugInfo { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "facing")]
		[FlexJamMember(Name = "facing", Type = FlexJamType.Float)]
		public float Facing { get; set; }

		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "name")]
		public string Name { get; set; }
	}
}
