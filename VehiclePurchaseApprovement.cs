//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace consumer_kafka_tam
{
    using System;
    using System.Collections.Generic;
    
    public partial class VehiclePurchaseApprovement
    {
        public long Id { get; set; }
        public string Topic { get; set; }
        public Nullable<long> KafkaKey { get; set; }
        public string Value { get; set; }
        public System.DateTime DtmCrt { get; set; }
        public System.DateTime DtmUpd { get; set; }
    }
}
