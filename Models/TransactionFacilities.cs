//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TransactionFacilities
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public int FacilitiesId { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<bool> isRenew { get; set; }
        public string EntryBy { get; set; }
        public string EntryModifiedBy { get; set; }
    
        public virtual Facilities Facilities { get; set; }
        public virtual Transactions Transactions { get; set; }
    }
}
