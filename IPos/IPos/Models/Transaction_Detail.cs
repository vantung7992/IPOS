//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IPos.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transaction_Detail
    {
        public string Transaction_Code { get; set; }
        public int Product_Unit_ID { get; set; }
        public Nullable<int> Number { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Total { get; set; }
    }
}
