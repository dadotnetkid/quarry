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
    
    public partial class DeliveryReceipts
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public string ReceiptNumber { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
    
        public virtual Transactions Transactions { get; set; }
    }
}
