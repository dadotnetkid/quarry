using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Models.Repository
{
    public partial class UnitOfWork : IDisposable
    {
        private ModelDb context = new ModelDb();


        private GenericRepository<Billings> _BillingsRepo;
        public GenericRepository<Billings> BillingsRepo
        {
            get
            {
                if (this._BillingsRepo == null)
                    this._BillingsRepo = new GenericRepository<Billings>(context);
                return _BillingsRepo;
            }
            set { _BillingsRepo = value; }
        }
        private GenericRepository<Signatories> _SignatoriesRepo;
        public GenericRepository<Signatories> SignatoriesRepo
        {
            get
            {
                if (this._SignatoriesRepo == null)
                    this._SignatoriesRepo = new GenericRepository<Signatories>(context);
                return _SignatoriesRepo;
            }
            set { _SignatoriesRepo = value; }
        }


        private GenericRepository<Hauleds> _HauledsRepo;
        public GenericRepository<Hauleds> HauledsRepo
        {
            get
            {
                if (this._HauledsRepo == null)
                    this._HauledsRepo = new GenericRepository<Hauleds>(context);
                return _HauledsRepo;
            }
            set { _HauledsRepo = value; }
        }

        private GenericRepository<QuarriesInTransactions> _QuarriesInTransactionsRepo;
        public GenericRepository<QuarriesInTransactions> QuarriesInTransactionsRepo
        {
            get
            {
                if (this._QuarriesInTransactionsRepo == null)
                    this._QuarriesInTransactionsRepo = new GenericRepository<QuarriesInTransactions>(context);
                return _QuarriesInTransactionsRepo;
            }
            set { _QuarriesInTransactionsRepo = value; }
        }

        private GenericRepository<Barangays> _BarangaysRepo;
        public GenericRepository<Barangays> BarangaysRepo
        {
            get
            {
                if (this._BarangaysRepo == null)
                    this._BarangaysRepo = new GenericRepository<Barangays>(context);
                return _BarangaysRepo;
            }
            set { _BarangaysRepo = value; }
        }

        private GenericRepository<TransactionPenalties> _TransactionPenaltiesRepo;
        public GenericRepository<TransactionPenalties> TransactionPenaltiesRepo
        {
            get
            {
                if (this._TransactionPenaltiesRepo == null)
                    this._TransactionPenaltiesRepo = new GenericRepository<TransactionPenalties>(context);
                return _TransactionPenaltiesRepo;
            }
            set { _TransactionPenaltiesRepo = value; }
        }

        private GenericRepository<Logs> _LogsRepo;
        public GenericRepository<Logs> LogsRepo
        {
            get
            {
                if (this._LogsRepo == null)
                    this._LogsRepo = new GenericRepository<Logs>(context);
                return _LogsRepo;
            }
            set { _LogsRepo = value; }
        }
        private GenericRepository<SummaryProgramOfWorks> _SummaryProgramOfWorksRepo;
        public GenericRepository<SummaryProgramOfWorks> SummaryProgramOfWorksRepo
        {
            get
            {
                if (this._SummaryProgramOfWorksRepo == null)
                    this._SummaryProgramOfWorksRepo = new GenericRepository<SummaryProgramOfWorks>(context);
                return _SummaryProgramOfWorksRepo;
            }
            set { _SummaryProgramOfWorksRepo = value; }
        }
        private GenericRepository<Particulars> _ParticularsRepo;
        public GenericRepository<Particulars> ParticularsRepo
        {
            get
            {
                if (this._ParticularsRepo == null)
                    this._ParticularsRepo = new GenericRepository<Particulars>(context);
                return _ParticularsRepo;
            }
            set { _ParticularsRepo = value; }
        }
        private GenericRepository<ProgramOfWorks> _ProgramOfWorksRepo;
        public GenericRepository<ProgramOfWorks> ProgramOfWorksRepo
        {
            get
            {
                if (this._ProgramOfWorksRepo == null)
                    this._ProgramOfWorksRepo = new GenericRepository<ProgramOfWorks>(context);
                return _ProgramOfWorksRepo;
            }
            set { _ProgramOfWorksRepo = value; }
        }
        private GenericRepository<TransactionTypes> _TransactionTypesRepo;
        public GenericRepository<TransactionTypes> TransactionTypesRepo
        {
            get
            {
                if (this._TransactionTypesRepo == null)
                    this._TransactionTypesRepo = new GenericRepository<TransactionTypes>(context);
                return _TransactionTypesRepo;
            }
            set { _TransactionTypesRepo = value; }
        }
        private GenericRepository<Provinces> _ProvincesRepo;
        public GenericRepository<Provinces> ProvincesRepo
        {
            get
            {
                if (this._ProvincesRepo == null)
                    this._ProvincesRepo = new GenericRepository<Provinces>(context);
                return _ProvincesRepo;
            }
            set { _ProvincesRepo = value; }
        }

        private GenericRepository<Towns> _TownsRepo;
        public GenericRepository<Towns> TownsRepo
        {
            get
            {
                if (this._TownsRepo == null)
                    this._TownsRepo = new GenericRepository<Towns>(context);
                return _TownsRepo;
            }
            set { _TownsRepo = value; }
        }
        private GenericRepository<Productions> _ProductionsRepo;
        public GenericRepository<Productions> ProductionsRepo
        {
            get
            {
                if (this._ProductionsRepo == null)
                    this._ProductionsRepo = new GenericRepository<Productions>(context);
                return _ProductionsRepo;
            }
            set { _ProductionsRepo = value; }
        }
        private GenericRepository<TransactionSags> _TransactionSagsRepo;
        public GenericRepository<TransactionSags> TransactionSagsRepo
        {
            get
            {
                if (this._TransactionSagsRepo == null)
                    this._TransactionSagsRepo = new GenericRepository<TransactionSags>(context);
                return _TransactionSagsRepo;
            }
            set { _TransactionSagsRepo = value; }
        }
        private GenericRepository<Sags> _SagsRepo;
        public GenericRepository<Sags> SagsRepo
        {
            get
            {
                if (this._SagsRepo == null)
                    this._SagsRepo = new GenericRepository<Sags>(context);
                return _SagsRepo;
            }
            set { _SagsRepo = value; }
        }
        private GenericRepository<Actions> _ActionsRepo;
        public GenericRepository<Actions> ActionsRepo
        {
            get
            {
                if (this._ActionsRepo == null)
                    this._ActionsRepo = new GenericRepository<Actions>(context);
                return _ActionsRepo;
            }
            set { _ActionsRepo = value; }
        }

        private GenericRepository<UserRolesInActions> _UserRolesInActionsRepo;
        public GenericRepository<UserRolesInActions> UserRolesInActionsRepo
        {
            get
            {
                if (this._UserRolesInActionsRepo == null)
                    this._UserRolesInActionsRepo = new GenericRepository<UserRolesInActions>(context);
                return _UserRolesInActionsRepo;
            }
            set { _UserRolesInActionsRepo = value; }
        }
        private GenericRepository<UnitMeasurements> _UnitMeasurementsRepo;
        public GenericRepository<UnitMeasurements> UnitMeasurementsRepo
        {
            get
            {
                if (this._UnitMeasurementsRepo == null)
                    this._UnitMeasurementsRepo = new GenericRepository<UnitMeasurements>(context);
                return _UnitMeasurementsRepo;
            }
            set { _UnitMeasurementsRepo = value; }
        }

        private GenericRepository<Categories> _CategoriesRepo;
        public GenericRepository<Categories> CategoriesRepo
        {
            get
            {
                if (this._CategoriesRepo == null)
                    this._CategoriesRepo = new GenericRepository<Categories>(context);
                return _CategoriesRepo;
            }
            set { _CategoriesRepo = value; }
        }

        private GenericRepository<TransactionFacilities> _TransactionFacilitiesRepo;
        public GenericRepository<TransactionFacilities> TransactionFacilitiesRepo
        {
            get
            {
                if (this._TransactionFacilitiesRepo == null)
                    this._TransactionFacilitiesRepo = new GenericRepository<TransactionFacilities>(context);
                return _TransactionFacilitiesRepo;
            }
            set { _TransactionFacilitiesRepo = value; }
        }
        private GenericRepository<TransactionVehicles> _TransactionVehiclesRepo;
        public GenericRepository<TransactionVehicles> TransactionVehiclesRepo
        {
            get
            {
                if (this._TransactionVehiclesRepo == null)
                    this._TransactionVehiclesRepo = new GenericRepository<TransactionVehicles>(context);
                return _TransactionVehiclesRepo;
            }
            set { _TransactionVehiclesRepo = value; }
        }
        private GenericRepository<TransactionDetails> _TransactionDetailsRepo;
        public GenericRepository<TransactionDetails> TransactionDetailsRepo
        {
            get
            {
                if (this._TransactionDetailsRepo == null)
                    this._TransactionDetailsRepo = new GenericRepository<TransactionDetails>(context);
                return _TransactionDetailsRepo;
            }
            set { _TransactionDetailsRepo = value; }
        }

        private GenericRepository<ControllersActions> _ControllersActionsRepo;
        public GenericRepository<ControllersActions> ControllersActionsRepo
        {
            get
            {
                if (this._ControllersActionsRepo == null)
                    this._ControllersActionsRepo = new GenericRepository<ControllersActions>(context);
                return _ControllersActionsRepo;
            }
            set { _ControllersActionsRepo = value; }
        }

        private GenericRepository<Facilities> _FacilitiesRepo;
        public GenericRepository<Facilities> FacilitiesRepo
        {
            get
            {
                if (this._FacilitiesRepo == null)
                    this._FacilitiesRepo = new GenericRepository<Facilities>(context);
                return _FacilitiesRepo;
            }
            set { _FacilitiesRepo = value; }
        }

        private GenericRepository<Vehicles> _VehiclesRepo;
        public GenericRepository<Vehicles> VehiclesRepo
        {
            get
            {
                if (this._VehiclesRepo == null)
                    this._VehiclesRepo = new GenericRepository<Vehicles>(context);
                return _VehiclesRepo;
            }
            set { _VehiclesRepo = value; }
        }
        private GenericRepository<VehicleTypes> _VehicleTypesRepo;
        public GenericRepository<VehicleTypes> VehicleTypesRepo
        {
            get
            {
                if (this._VehicleTypesRepo == null)
                    this._VehicleTypesRepo = new GenericRepository<VehicleTypes>(context);
                return _VehicleTypesRepo;
            }
            set { _VehicleTypesRepo = value; }
        }

        private GenericRepository<Quarries> _QuarriesRepo;
        public GenericRepository<Quarries> QuarriesRepo
        {
            get
            {
                if (this._QuarriesRepo == null)
                    this._QuarriesRepo = new GenericRepository<Quarries>(context);
                return _QuarriesRepo;
            }
            set { _QuarriesRepo = value; }
        }

        private GenericRepository<PermiteeTypes> _PermiteeTypesRepo;
        public GenericRepository<PermiteeTypes> PermiteeTypesRepo
        {
            get
            {
                if (this._PermiteeTypesRepo == null)
                    this._PermiteeTypesRepo = new GenericRepository<PermiteeTypes>(context);
                return _PermiteeTypesRepo;
            }
            set { _PermiteeTypesRepo = value; }
        }

        private GenericRepository<Items> _ItemsRepo;
        public GenericRepository<Items> ItemsRepo
        {
            get
            {
                if (this._ItemsRepo == null)
                    this._ItemsRepo = new GenericRepository<Items>(context);
                return _ItemsRepo;
            }
            set { _ItemsRepo = value; }
        }

        private GenericRepository<Transactions> _TransactionsRepo;
        public GenericRepository<Transactions> TransactionsRepo
        {
            get
            {
                if (this._TransactionsRepo == null)
                    this._TransactionsRepo = new GenericRepository<Transactions>(context);
                return _TransactionsRepo;
            }
            set { _TransactionsRepo = value; }
        }
        private GenericRepository<Permitees> _PermiteesRepo;
        public GenericRepository<Permitees> PermiteesRepo
        {
            get
            {
                if (this._PermiteesRepo == null)
                    this._PermiteesRepo = new GenericRepository<Permitees>(context);
                return _PermiteesRepo;
            }
            set { _PermiteesRepo = value; }
        }

        private GenericRepository<Users> usersRepo;
        public GenericRepository<Users> UsersRepo
        {
            get
            {
                if (this.usersRepo == null)
                    this.usersRepo = new GenericRepository<Users>(context);
                return usersRepo;
            }
            set { usersRepo = value; }
        }

        private GenericRepository<UserRoles> userRolesRepo;
        public GenericRepository<UserRoles> UserRolesRepo
        {
            get
            {
                if (this.userRolesRepo == null)
                    this.userRolesRepo = new GenericRepository<UserRoles>(context);
                return userRolesRepo;
            }
            set { userRolesRepo = value; }
        }


        private GenericRepository<DeliveryReceipts> _DeliveryReceiptsRepo;
        public GenericRepository<DeliveryReceipts> DeliveryReceiptsRepo
        {
            get
            {
                if (this._DeliveryReceiptsRepo == null)
                    this._DeliveryReceiptsRepo = new GenericRepository<DeliveryReceipts>(context);
                return _DeliveryReceiptsRepo;
            }
            set { _DeliveryReceiptsRepo = value; }
        }


        public void Save()
        {
            context.SaveChanges();

        }


        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
       
    }

}