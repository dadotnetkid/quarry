using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Models;
using Models.Repository;

namespace Helpers
{

    public class FilemanagementImportHelper
    {
        private string UserId => HttpContext.Current.User.Identity.GetUserId();
        private UnitOfWork unitOfWork = new UnitOfWork();
        private DataTable DataTable { get; set; }
        public FilemanagementImportHelper(DataTable dataTable)
        {
            this.DataTable = dataTable;
        }

        public string ImportVehicles(bool? validate)
        {
            string errors = "";
            int row = 1;
            foreach (DataRow i in DataTable.Rows)
            {
                try
                {
                    var plateNo = i["Plate No"]?.ToString();
                    var description = i["Description"]?.ToString();
                    var vehicleTypeNameCode = i["Vehicle Type Code"]?.ToString();
                    int? capacity = i["Capacity"].ToInt();
                    var accreditationNumber = i["Accreditation Number"]?.ToString();
                    var permittee = unitOfWork.PermiteesRepo.Find(m => m.AccreditationNumber == accreditationNumber);
                    var vehicleType = unitOfWork.VehicleTypesRepo.Find(m => m.Code == vehicleTypeNameCode);

                    if (validate == true && unitOfWork.VehiclesRepo.Fetch(m => m.PlateNo == plateNo).Any())
                    {
                        errors = plateNo + $" is already exist at row {row}";
                        break;
                    }
                    else
                    {
                        unitOfWork.VehiclesRepo.Insert(new Models.Vehicles()
                        {
                            PlateNo = plateNo,
                            Description = description,
                            VehicleTypeId = vehicleType?.Id,
                            Capacity = capacity,
                            PermiteeId = permittee?.Id,

                        });
                    }
                    unitOfWork.Save();
                    row++;
                }
                catch (DbEntityValidationException e)
                {
                    errors = $" error has been throw at {row} " + string.Join(",", e.EntityValidationErrors.Select(x => x.ValidationErrors.Select(m => m.ErrorMessage)));
                    break;
                }

            }

            return errors;
        }
        public string ImportQuarries(bool? validate)
        {
            string errors = "";
            int row = 1;
            foreach (DataRow i in DataTable.Rows)
            {
                var quarryName = i[0]?.ToString();
                if (validate == true && unitOfWork.QuarriesRepo.Fetch(m => m.QuarryName == quarryName).Any())
                {
                    errors = quarryName + $" is already exist at row {row}";
                    break;
                }

                var quarry = new Models.Quarries()
                {
                    QuarryName = i[0]?.ToString(),
                    JurisdictionName = i[1]?.ToString(),

                    EntryBy = UserId,
                    LastEditedBy = UserId
                };
                var barangayName = i[2]?.ToString();

                List<Barangays> barangays = new List<Barangays>();
                foreach (var b in barangayName.Split(','))
                {
                    barangays.Add(unitOfWork.BarangaysRepo.Find(m => m.Barangay == b) ??
                                  new Barangays() { Barangay = b });
                }

                quarry.Barangays = barangays;
                unitOfWork.QuarriesRepo.Insert(quarry);
            }
            unitOfWork.Save();
            return errors;
        }
        public string ImportPermittees(bool? validate)
        {
            string errors = "";
            int row = 1;
            foreach (DataRow i in DataTable.Rows)
            {
                var accreditationNumber = i["Accreditation Number"]?.ToString();
                var permitteeTypeName = i["Permittee Type"]?.ToString();
                var firstName = i["First Name"]?.ToString();
                var middleName = i["Middle Name"]?.ToString();
                var lastName = i["Last Name"]?.ToString();
                var companyName = i["Company Name"]?.ToString();
                var contactNumber1 = i["Contact Number 1"]?.ToString();
                var contactNumber2 = i["Contact Number 2"]?.ToString();
                var email = i["Email"]?.ToString();
                var addressLine1 = i["Address Line 1"]?.ToString();
                var addressLine2 = i["Address Line 2"]?.ToString();
                var businessRegistration = i["Business Registration"]?.ToString();
                var quarrySite = i["Quarry Sites"]?.ToString();
                var permitteeType = unitOfWork.PermiteeTypesRepo.Find(m => m.PermiteeTypeName == permitteeTypeName);
                IList<Quarries> quarries = new List<Quarries>();
                if (string.IsNullOrEmpty(quarrySite))
                {
                    errors = $"Quarry Field should not be empty at row {row} ";
                    break;
                }

                if (string.IsNullOrEmpty(accreditationNumber))
                {
                    errors = $"Accreditation Number Field should not be empty at row {row} ";
                    break;
                }

                foreach (var q in quarrySite.Split(','))
                {
                    var quarry = q.TrimStart().TrimEnd();
                    if (!unitOfWork.QuarriesRepo.Fetch(m => m.QuarryName == quarry).Any())
                    {
                        errors = $"Invalid quarry {q.TrimStart()} at row {row}";
                        break;
                    }
                    else
                    {
                        quarries.Add(unitOfWork.QuarriesRepo.Find(m => m.QuarryName == quarry));
                    }

                }

                if (validate == true && unitOfWork.PermiteesRepo.Fetch(m => m.AccreditationNumber == accreditationNumber).Any())
                {
                    errors = accreditationNumber + $" is already exist at row {row}";
                    break;
                }

                else
                    unitOfWork.PermiteesRepo.Insert(new Models.Permitees()
                    {
                        AccreditationNumber = accreditationNumber,
                        PermiteeTypeId = permitteeType?.Id,
                        FirstName = firstName,
                        MiddleName = middleName,
                        LastName = lastName,
                        CompanyName = companyName,
                        ContactNumber1 = contactNumber1,
                        ContactNumber2 = contactNumber2,
                        Email = email,
                        AddressLine1 = addressLine1,
                        AddressLine2 = addressLine2,
                        BusinessRegistration = businessRegistration,
                        Quarries = quarries,
                        EntryBy = UserId,
                        LastEditedBy = UserId
                    });

            }
            unitOfWork.Save();
            return errors;
        }

        public string ImportProduction(bool? validate, bool? automaticAddVehicle)
        {
            string errors = "";
            int row = 1;
            foreach (DataRow i in DataTable.Rows)
            {
                row++;
                var accreditationNumber = i["Accreditation Number"]?.ToString();
                var projectName = i["Project Name"]?.ToString();
                var destination = i["Destination of Delivery"]?.ToString();
                var drNo = i["No of Dr"].ToInt();
                var plateNo = i["Plate No"]?.ToString();
                var ordEarth = i["Ordinary Earth"].ToDecimal();
                var mixed = i["Mixed"].ToDecimal();
                var fine = i["Fine Aggregates"].ToDecimal();
                var coarse = i["Coarse Aggregates"].ToDecimal();
                var boulder = i["Boulder"].ToDecimal();
                var crushed = i["Crushed Rocks"].ToDecimal();
                var productionDate = i["Date of Production"];
                var receiptDate = i["Receipt Date"];
                var quarry = i["Quarry"]?.ToString() ?? "";
                var receiptNo = i["Receipt No"].ToString();
                var capacity = i["Capacity"].ToDecimal();
                IQueryable<ProgramOfWorks> programOfWorks = new List<ProgramOfWorks>().AsQueryable();
                if (!string.IsNullOrEmpty(projectName))
                    programOfWorks = unitOfWork.ProgramOfWorksRepo.Fetch(m => m.Name == projectName);
                else if (string.IsNullOrEmpty(destination))
                {
                    errors = $"Project Name Field should not be empty at row {row} ";
                    break;
                }

                //if (!unitOfWork.DeliveryReceiptsRepo.Fetch(m => m.ReceiptNumber == receiptNo).Any())
                //{
                //    errors = $"Invalid receipt number { receiptNo } at row {row} ";
                //    break;
                //}


                //if (!programOfWorks.Any())
                //{
                //    errors = $"Invalid Project { projectName} at row {row} ";
                //    break;
                //}


                //if (!string.IsNullOrEmpty(destination))
                //{
                //    if (!unitOfWork.TownsRepo.Fetch(m => m.Name == destination).Any())
                //    {
                //        errors = $"Invalid destination { destination} at row {row} ";
                //        break;
                //    }
                //}
                //else if (string.IsNullOrEmpty(projectName))
                //{
                //    errors = $"Destination of Delivery Field should not be empty at row {row} ";
                //    break;
                //}

                if (!string.IsNullOrEmpty(quarry))
                {
                    if (!unitOfWork.QuarriesRepo.Fetch(m => m.QuarryName == quarry).Any())
                    {
                        errors = $"Invalid quarry { quarry } at row {row} ";

                        break;
                    }
                }
                else
                {
                    errors = $"Quarry Field should not be empty at row {row} ";
                    break;
                }

                if (!string.IsNullOrEmpty(accreditationNumber))
                {
                    if (!unitOfWork.PermiteesRepo.Fetch(m => m.AccreditationNumber == accreditationNumber).Any())
                    {
                        errors = $"Invalid Accreditation Number { accreditationNumber } at row {row} ";
                        break;
                    }
                }
                else
                {
                    errors = $"Accreditation Number Field should not be empty at row {row} ";
                    break;
                }

                decimal? amount = 0;
                int sagId = 0;
                if (ordEarth > 0)
                {
                    amount = ordEarth;
                    sagId = 1;
                }
                else if (mixed > 0)
                {
                    amount = mixed;
                    sagId = 4;
                }
                else if (fine > 0)
                {
                    amount = fine;
                    sagId = 5;
                }
                else if (coarse > 0)
                {
                    amount = coarse;
                    sagId = 8;
                }
                else if (boulder > 0)
                {
                    amount = boulder;
                    sagId = 6;
                }
                else if (Convert.ToInt32(crushed) > 0)
                {
                    amount = Convert.ToInt32(crushed);
                    sagId = 7;
                }
                if (sagId == 0)
                {
                    {
                        errors = $"invalid SAG at row {row} ";
                        break;
                    }
                }
                var transactionid = new TransactionHelper().TransactionNumber;

                var permittee = unitOfWork.PermiteesRepo.Find(m => m.AccreditationNumber == accreditationNumber);

                if (!unitOfWork.VehiclesRepo.Fetch(m => m.PlateNo == plateNo).Any() && automaticAddVehicle==true)
                {
                    unitOfWork.VehiclesRepo.Insert(new Vehicles()
                    {
                        PermiteeId = permittee.Id,
                        PlateNo = plateNo,
                        Capacity = capacity,
                        Description = "Added by import utilities",
                        VehicleTypeId = 2
                    });
                }
                if (!unitOfWork.DeliveryReceiptsRepo.Fetch(m => m.ReceiptNumber == receiptNo).Any())
                {
                    permittee.Transactions.Add(new Transactions()
                    {
                        Id = Guid.NewGuid().ToString(),
                        TransactionNumber = transactionid,
                        TransactionTypeId = 5,
                        TransactionDate = Convert.ToDateTime(productionDate),

                        TransactionDetails = new List<TransactionDetails>() {
                            new TransactionDetails() {
                                ItemId = 15, Quantity = 1,
                                Remarks="Added by import utilities",
                            }
                        },
                        DeliveryReceipts = new List<DeliveryReceipts>() {
                            new DeliveryReceipts() {
                                ReceiptNumber = receiptNo,
                                Remarks="Added by import utilities",

                            }
                        },
                        Remarks = "Added by import utilities",
                    });
                    unitOfWork.Save();
                }
                try
                {
                    var deliveryReceipts = unitOfWork.DeliveryReceiptsRepo.Fetch(m => m.ReceiptNumber == receiptNo).Select(x => new { x.TransactionId }).FirstOrDefault()?.TransactionId;
                    var vehicleId = unitOfWork.VehiclesRepo.Fetch(m => m.PlateNo == plateNo).Select(x => new { x.Id }).FirstOrDefault()?.Id;
                    var quarriesId = unitOfWork.QuarriesRepo.Fetch(m => m.QuarryName == quarry).Select(x => new { x.Id }).FirstOrDefault()?.Id;

                    unitOfWork.ProductionsRepo.Insert(new Productions()
                    {
                        PermiteeId = permittee?.Id,
                        VehicleId = vehicleId,
                        Quantity = amount,
                        SagId = sagId,
                        QuarriesId = quarriesId,
                        ProductionDate = Convert.ToDateTime(productionDate),
                        ReceiptDate = Convert.ToDateTime(receiptDate),
                        ReceiptNo = receiptNo,
                        ProgramOfWorkId = programOfWorks.FirstOrDefault()?.Id,
                        TransactionId = deliveryReceipts,
                        Destination = destination,
                        Remarks = "Added by import utilities"

                    });
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    throw;
                }

            }

            return errors;
        }

        public string ImportBarangay(bool? validate)
        {
            string errors = "";
            int row = 1;
            foreach (DataRow i in DataTable.Rows)
            {
                var barangayName = i[0]?.ToString();
                if (validate == true && unitOfWork.BarangaysRepo.Fetch(m => m.Barangay == barangayName).Any())
                {
                    errors = barangayName + $" is already exist at row {row}";
                    break;
                }

                var townName = i[1].ToString();
                var town = unitOfWork.TownsRepo.Find(m => m.Name == townName);
                var barangay = new Models.Barangays()
                {
                    Barangay = i[0]?.ToString(),
                    Towns = town ?? new Towns() { Name = i[1].ToString() }
                };

                unitOfWork.BarangaysRepo.Insert(barangay);
                unitOfWork.Save();
                row++;
            }

            return errors;
        }
    }
}


