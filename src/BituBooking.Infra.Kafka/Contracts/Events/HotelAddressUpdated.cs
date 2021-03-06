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
	
	public partial class HotelAddressUpdated : ISpecificRecord
	{
		public static Schema _SCHEMA = Avro.Schema.Parse(@"{""type"":""record"",""name"":""HotelAddressUpdated"",""namespace"":""BituBooking.Infra.Kafka.Contracts.Events"",""fields"":[{""name"":""HotelCode"",""type"":{""type"":""string"",""logicalType"":""uuid""}},{""name"":""Street"",""type"":""string""},{""name"":""District"",""type"":""string""},{""name"":""City"",""type"":""string""},{""name"":""Country"",""type"":""string""},{""name"":""ZipCode"",""type"":""int""}],""version"":""1""}");
		private System.Guid _HotelCode;
		private string _Street;
		private string _District;
		private string _City;
		private string _Country;
		private int _ZipCode;
		public virtual Schema Schema
		{
			get
			{
				return HotelAddressUpdated._SCHEMA;
			}
		}
		public System.Guid HotelCode
		{
			get
			{
				return this._HotelCode;
			}
			set
			{
				this._HotelCode = value;
			}
		}
		public string Street
		{
			get
			{
				return this._Street;
			}
			set
			{
				this._Street = value;
			}
		}
		public string District
		{
			get
			{
				return this._District;
			}
			set
			{
				this._District = value;
			}
		}
		public string City
		{
			get
			{
				return this._City;
			}
			set
			{
				this._City = value;
			}
		}
		public string Country
		{
			get
			{
				return this._Country;
			}
			set
			{
				this._Country = value;
			}
		}
		public int ZipCode
		{
			get
			{
				return this._ZipCode;
			}
			set
			{
				this._ZipCode = value;
			}
		}
		public virtual object Get(int fieldPos)
		{
			switch (fieldPos)
			{
			case 0: return this.HotelCode;
			case 1: return this.Street;
			case 2: return this.District;
			case 3: return this.City;
			case 4: return this.Country;
			case 5: return this.ZipCode;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Get()");
			};
		}
		public virtual void Put(int fieldPos, object fieldValue)
		{
			switch (fieldPos)
			{
			case 0: this.HotelCode = (System.Guid)fieldValue; break;
			case 1: this.Street = (System.String)fieldValue; break;
			case 2: this.District = (System.String)fieldValue; break;
			case 3: this.City = (System.String)fieldValue; break;
			case 4: this.Country = (System.String)fieldValue; break;
			case 5: this.ZipCode = (System.Int32)fieldValue; break;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Put()");
			};
		}
	}
}
