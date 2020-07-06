using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{

    public partial class Transactions
    {

        public decimal? DetailSubTotal => this.TransactionDetails.Sum(m => m.TotalCost);
        public decimal? VehicleSubTotal => this.TransactionVehicles.Sum(m => m.Cost);
        public decimal? FacilitiesSubTotal => this.TransactionFacilities.Sum(m => m.Cost);
        public decimal? SagSubTotal => this.TransactionSags.Sum(m => m.TotalCost);

        public decimal? PenaltiesSubTotal => this.TransactionPenalties.Sum(m => m.Amount);

        public decimal? SagTotalQuantity => this.TransactionSags.Sum(m => m.Quantity);
        public decimal? Penalty;

        public decimal? PenaltyCost => this.TransactionPenalties.Sum(m => m.Amount);

        public string Penalties => string.Join(Environment.NewLine, TransactionPenalties.Select(x => new { Penalties = x.Penalty + " " + x.Amount?.ToString("0.0 Php") }).Select(x => x.Penalties));

        public bool IsPaid => !string.IsNullOrEmpty(this.OfficialReceipt);


        public string POWQuarries =>
            string.Join(",", this.QuarriesInTransactions.Select(x => x.QuarrySitesDistribution));

        public string Projects => string.Join(Environment.NewLine, this.ProgramOfWorks.Select(x => x.Name));
        //  public decimal? Interest => SurCharge * 0.02M;
        //  public decimal? SurCharge => (DetailSubTotal + VehicleSubTotal + FacilitiesSubTotal) * 0.25M;



    }



    //public partial class Transactions
    //{
    //    public int Sticker { get; set; }

    //    public void ComputeSticker()
    //    {
    //        Sticker = ((PayloaderNew ?? 0) + (PayloaderOld ?? 0)) + ((BackHoeNew ?? 0) + (BackHoeOld ?? 0)) +
    //                  ((HaulingTrucksFourteenNew ?? 0) + (HaulingTrucksFourteenOld ?? 0)) +
    //                  ((HaulingTrucksTenNew ?? 0) + (HaulingTrucksTenOld ?? 0)) +
    //                  ((HaulingTrucksSixNew ?? 0) + (HaulingTrucksSixnOld ?? 0)) +
    //                  ((HaulingTrucksLessSixNew ?? 0) + (HaulingTrucksLessSixnOld ?? 0)) +
    //                  ((PlantsNew ?? 0) + (PlantsOld ?? 0)) +
    //                  ((PotteriesandCementNew ?? 0) + (PotteriesandCementOld ?? 0));
    //    }
    //    public void RenewTransaction()
    //    {

    //        PayloaderOld = (PayloaderNew ?? 0) + (PayloaderOld ?? 0);
    //        PayloaderNew = 0;
    //        BackHoeOld = (BackHoeNew ?? 0) + (BackHoeOld ?? 0);
    //        BackHoeNew = 0;

    //        HaulingTrucksFourteenOld = (HaulingTrucksFourteenNew ?? 0) + (HaulingTrucksFourteenOld ?? 0);
    //        HaulingTrucksFourteenNew = 0;
    //        HaulingTrucksTenOld = (HaulingTrucksTenNew ?? 0) + (HaulingTrucksTenOld ?? 0);
    //        HaulingTrucksTenNew = 0;
    //        HaulingTrucksSixnOld = (HaulingTrucksSixNew ?? 0) + (HaulingTrucksSixnOld ?? 0);
    //        HaulingTrucksSixNew = 0;
    //        HaulingTrucksLessSixnOld = (HaulingTrucksLessSixNew ?? 0) + (HaulingTrucksLessSixnOld ?? 0);
    //        HaulingTrucksLessSixNew = 0;
    //        PlantsOld = (PlantsNew ?? 0) + (PlantsOld ?? 0);
    //        PlantsNew = 0;
    //        PotteriesandCementOld = (PotteriesandCementNew ?? 0) + (PotteriesandCementOld ?? 0);
    //        PotteriesandCementNew = 0;
    //        ComputeSticker();
    //    }
    //}

    //public partial class Permittees
    //{


    //    public void RenewTransaction()
    //    {
    //        this.Transactions.OrderByDescending(m => m.Id).FirstOrDefault()?.RenewTransaction();
    //    }

    //}
}
