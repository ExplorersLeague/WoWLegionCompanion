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

		[System.Runtime.Serialization.DataMember(Name = "gameObjectDebugInfo")]
		[FlexJamMember(Optional = true, Name = "gameObjectDebugInfo", Type = FlexJamType.Struct)]
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

		[System.Runtime.Serialization.DataMember(Name = "typeID")]
		[FlexJamMember(Name = "typeID", Type = FlexJamType.Int32)]
		public int TypeID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "position")]
		[FlexJamMember(Name = "position", Type = FlexJamType.Struct)]
		public Vector3 Position { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "rawPosition")]
		[FlexJamMember(Name = "rawPosition", Type = FlexJamType.Struct)]
		public Vector3 RawPosition { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "scriptTableValueDebugInfo", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "scriptTableValueDebugInfo")]
		public ScriptTableValueDebugInfo[] ScriptTableValueDebugInfo { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "phaseInfo", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "phaseInfo")]
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

		[FlexJamMember(Optional = true, Name = "unitDebugInfo", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "unitDebugInfo")]
		public UnitDebugInfo[] UnitDebugInfo { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "facing")]
		[FlexJamMember(Name = "facing", Type = FlexJamType.Float)]
		public float Facing { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "name")]
		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		public string Name { get; set; }
	}
}
