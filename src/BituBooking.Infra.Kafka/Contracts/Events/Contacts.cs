// ------------------------------------------------------------------------------
// <auto-generated>
//    Generated by avrogen, version 1.11.0.0
//    Changes to this file may cause incorrect behavior and will be lost if code
//    is regenerated
// </auto-generated>
// ------------------------------------------------------------------------------
namespace BituBooking.Infra.Kafka.Contracts.Events
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using Avro;
	using Avro.Specific;
	
	public partial class Contacts : ISpecificRecord
	{
		public static Schema _SCHEMA = Avro.Schema.Parse("{\"type\":\"record\",\"name\":\"Contacts\",\"namespace\":\"BituBooking.Infra.Kafka.Contracts" +
				".Events\",\"fields\":[{\"name\":\"Mobile\",\"type\":\"string\"},{\"name\":\"Phone\",\"type\":\"str" +
				"ing\"},{\"name\":\"Email\",\"type\":\"string\"}]}");
		private string _Mobile;
		private string _Phone;
		private string _Email;
		public virtual Schema Schema
		{
			get
			{
				return Contacts._SCHEMA;
			}
		}
		public string Mobile
		{
			get
			{
				return this._Mobile;
			}
			set
			{
				this._Mobile = value;
			}
		}
		public string Phone
		{
			get
			{
				return this._Phone;
			}
			set
			{
				this._Phone = value;
			}
		}
		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				this._Email = value;
			}
		}
		public virtual object Get(int fieldPos)
		{
			switch (fieldPos)
			{
			case 0: return this.Mobile;
			case 1: return this.Phone;
			case 2: return this.Email;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Get()");
			};
		}
		public virtual void Put(int fieldPos, object fieldValue)
		{
			switch (fieldPos)
			{
			case 0: this.Mobile = (System.String)fieldValue; break;
			case 1: this.Phone = (System.String)fieldValue; break;
			case 2: this.Email = (System.String)fieldValue; break;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Put()");
			};
		}
	}
}
