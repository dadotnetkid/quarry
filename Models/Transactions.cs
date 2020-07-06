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
    
    public partial class Transactions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Transactions()
        {
            this.DeliveryReceipts = new HashSet<DeliveryReceipts>();
            this.Productions = new HashSet<Productions>();
            this.QuarriesInTransactions = new HashSet<QuarriesInTransactions>();
            this.TransactionDetails = new HashSet<TransactionDetails>();
            this.TransactionFacilities = new HashSet<TransactionFacilities>();
            this.TransactionPenalties = new HashSet<TransactionPenalties>();
            this.TransactionSags = new HashSet<TransactionSags>();
            this.TransactionVehicles = new HashSet<TransactionVehicles>();
            this.ProgramOfWorks = new HashSet<ProgramOfWorks>();
            this.Billings = new HashSet<Billings>();
        }
    
        public string Id { get; set; }
        public string TransactionNumber { get; set; }
        public string OfficialReceipt { get; set; }
        public Nullable<int> PermiteeId { get; set; }
        public Nullable<int> TransactionTypeId { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public Nullable<decimal> TransactionTotal { get; set; }
        public string EntryBy { get; set; }
        public string LastEditedBy { get; set; }
        public Nullable<bool> IsPrinted { get; set; }
        public Nullable<System.DateTime> FilingDate { get; set; }
        public Nullable<decimal> Interest { get; set; }
        public Nullable<decimal> Surcharge { get; set; }
        public string Remarks { get; set; }
        public string Signatory { get; set; }
        public string DeliveryReceipt { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryReceipts> DeliveryReceipts { get; set; }
        public virtual Permitees Permitees { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Productions> Productions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuarriesInTransactions> QuarriesInTransactions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionDetails> TransactionDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionFacilities> TransactionFacilities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionPenalties> TransactionPenalties { get; set; }
        public virtual TransactionTypes TransactionTypes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionSags> TransactionSags { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionVehicles> TransactionVehicles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProgramOfWorks> ProgramOfWorks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Billings> Billings { get; set; }
    }
}
