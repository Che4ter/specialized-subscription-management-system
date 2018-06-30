using esencialAdmin.Data.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using esencialAdmin.Models.GoodiesViewModels;
using esencialAdmin.Extensions;
using esencialAdmin.Models.SubscriptionViewModels;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

namespace esencialAdmin.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        protected readonly esencialAdminContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public SubscriptionService(esencialAdminContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public int createNewSubscription(SubscriptionCreateViewModel newSubscription)
        {
            using (var dbContextTransaction = this._context.Database.BeginTransaction())
            {
                try
                {
                    var plan = this._context.Plans.Where(x => x.Id == newSubscription.PlanID).FirstOrDefault();
                    if (plan == null)
                    {
                        return 0;
                    }

                    var s = new Subscription()
                    {
                        FkCustomerId = newSubscription.CustomerID,
                        FkPlanId = newSubscription.PlanID,
                        PlantNumber = newSubscription.PlantNumber,
                        FkSubscriptionStatus = 3 //Rechnung noch nicht bezahlt
                    };

                    if (!String.IsNullOrEmpty(newSubscription.SubscriptionRemarks))
                    {
                        s.SubscriptionRemarks = newSubscription.SubscriptionRemarks;
                    }

                    this._context.Subscription.Add(s);
                    this._context.SaveChanges();

                    var currentDeadline = new DateTime(newSubscription.StartDate.Year, plan.Deadline.Month, plan.Deadline.Day);
                    var periodeEnd = new DateTime(newSubscription.StartDate.Year, 12, 31);
                    if (newSubscription.StartDate > currentDeadline)
                    {
                        periodeEnd = periodeEnd.AddYears((plan.Duration));
                    }
                    else
                    {
                        periodeEnd = periodeEnd.AddYears(plan.Duration - 1);
                    }
                    var p = new Periodes()
                    {
                        FkSubscriptionId = s.Id,
                        StartDate = newSubscription.StartDate,
                        EndDate = periodeEnd,
                        Price = plan.Price
                    };

                    if (newSubscription.Payed)
                    {
                        p.Payed = true;
                        p.PayedDate = DateTime.UtcNow;
                        s.FkSubscriptionStatus = 1; //Da Rechnung bereits bezahlt, Status Aktiv
                    }
                    p.FkPayedMethodId = newSubscription.PaymentMethodID;

                    if (newSubscription.GiverCustomerId != 0)
                    {
                        p.FkGiftedById = newSubscription.GiverCustomerId;
                    }

                    int startYear = (periodeEnd.Year - plan.Duration) + 1;

                    for (int i = 0; i < plan.Duration; i++)
                    {
                        PeriodesGoodies newGoodie = new PeriodesGoodies
                        {
                            FkPlanGoodiesId = plan.FkGoodyId,
                            SubPeriodeYear = startYear
                        };
                        p.PeriodesGoodies.Add(newGoodie);
                        startYear++;

                    }
                    this._context.Periodes.Add(p);

                    this._context.SaveChanges();
                    dbContextTransaction.Commit();
                    return s.Id;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                }
            }
            return 0;
        }

        public bool deleteSubscription(int id)
        {
            try
            {
                var subscriptionToDelete = this._context.Subscription
                    .Where(x => x.Id == id)
                    .Include(x => x.SubscriptionPhotos)
                    .FirstOrDefault();
                if (subscriptionToDelete == null)
                {
                    return false;
                }

                string subPath = _hostingEnvironment.WebRootPath + "\\Data\\Userdata\\" + subscriptionToDelete.FkCustomerId + "\\" + id + "\\";
                try
                {
                    Directory.Delete(subPath, true);

                }
                catch (Exception ex)
                {

                }
                List<int> fileList = subscriptionToDelete.SubscriptionPhotos.Select(x => x.FkFileId).ToList();
                this._context.SubscriptionPhotos.RemoveRange(subscriptionToDelete.SubscriptionPhotos);
                foreach (int i in fileList)
                {
                    this._context.Files.Remove(this._context.Files.Where(x => x.Id == i).FirstOrDefault());
                }

                foreach (var p in this._context.Periodes.Where(x => x.FkSubscriptionId == id).Include(x => x.PeriodesGoodies))
                {
                    this._context.PeriodesGoodies.RemoveRange(p.PeriodesGoodies);
                }

                this._context.Periodes.RemoveRange(this._context.Periodes.Where(x => x.FkSubscriptionId == id));

                this._context.Subscription.Remove(this._context.Subscription.Where(x => x.Id == id).FirstOrDefault());

                this._context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<GoodiesViewModel> getAvailableGoodies()
        {
            List<GoodiesViewModel> goodiesList = new List<GoodiesViewModel>();

            foreach (PlanGoodies goody in _context.PlanGoodies)
            {
                goodiesList.Add(new GoodiesViewModel
                {
                    Id = goody.Id,
                    Name = goody.Name
                });
            }

            return goodiesList;
        }

        public List<PaymentMethodsViewModel> getAvailablePaymentMethods()
        {
            List<PaymentMethodsViewModel> paymentList = new List<PaymentMethodsViewModel>();

            foreach (PaymentMethods method in _context.PaymentMethods)
            {
                paymentList.Add(new PaymentMethodsViewModel
                {
                    Id = method.Id,
                    Name = method.Name
                });
            }

            return paymentList;
        }

        public List<SubscriptionSelectPlanViewModel> getAvailableSelectPlanMethods()
        {
            List<SubscriptionSelectPlanViewModel> planList = new List<SubscriptionSelectPlanViewModel>();

            foreach (Plans plan in _context.Plans)
            {
                planList.Add(new SubscriptionSelectPlanViewModel
                {
                    Id = plan.Id,
                    Name = plan.Name
                });
            }

            return planList;
        }

        public List<SubscriptionSelectStatusViewModel> getAvailableSelectStatusMethods()
        {
            List<SubscriptionSelectStatusViewModel> statusList = new List<SubscriptionSelectStatusViewModel>();

            foreach (SubscriptionStatus status in _context.SubscriptionStatus)
            {
                statusList.Add(new SubscriptionSelectStatusViewModel
                {
                    Id = status.Id,
                    Name = status.Label
                });
            }

            return statusList;
        }

        public SubscriptionPlanFilterViewModel getAvailablePlans()
        {
            SubscriptionPlanFilterViewModel plans = new SubscriptionPlanFilterViewModel
            {
                Plans = new Dictionary<int, string>()
            };
            foreach (Plans p in _context.Plans.Where(x => x.Subscription.Any()))
            {
                plans.Plans.Add(p.Id, p.Name);
            }

            return plans;
        }

        public JsonResult getSelect2Customers(string searchTerm, int pageSize, int pageNum)
        {
            try
            {
                // Getting all Customer data  
                var customerData = (from tmpcustomer in _context.Customers
                                    select new { Id = tmpcustomer.Id, DisplayString = ((tmpcustomer.FirstName ?? "") + " " + (tmpcustomer.LastName ?? "") + " " + (tmpcustomer.Zip ?? "")).Replace("  ", " ").Trim() });

                //Sorting  
                customerData = customerData.OrderBy(x => x.DisplayString);

                //Search  
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    customerData = customerData.Where(m => m.DisplayString.Contains(searchTerm));
                }

                //total number of rows count   
                var recordsTotal = customerData.Count();
                var skip = pageNum * pageSize;
                //Paging   
                var data = customerData.Skip(skip).Take(pageSize).ToList();

                var resultList = new List<Select2Result>();
                foreach (var item in data)
                {
                    resultList.Add(new Select2Result()
                    {
                        text = item.DisplayString.Replace("  ", " "),
                        id = item.Id.ToString()
                    });
                }

                //Returning Json Data  
                return new JsonResult(new { items = resultList, total = recordsTotal });
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JsonResult getSelect2Plans(string searchTerm, int pageSize, int pageNum)
        {
            try
            {
                // Getting all Plan data  
                var planData = (from tmpplan in _context.Plans
                                select new { Id = tmpplan.Id, DisplayString = ((tmpplan.Name ?? "") + " - Laufzeit: " + (tmpplan.Duration.ToString() ?? "") + " Jahre - Preis " + (tmpplan.Price.ToString("N2") ?? "")).Replace("  ", " ").Trim() });

                //Sorting  
                planData = planData.OrderBy(x => x.DisplayString);

                //Search  
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    planData = planData.Where(m => m.DisplayString.Contains(searchTerm));
                }

                //total number of rows count   
                var recordsTotal = planData.Count();
                var skip = pageNum * pageSize;
                //Paging   
                var data = planData.Skip(skip).Take(pageSize).ToList();

                var resultList = new List<Select2Result>();
                foreach (var item in data)
                {
                    resultList.Add(new Select2Result()
                    {
                        text = item.DisplayString.Replace("  ", " "),
                        id = item.Id.ToString()
                    });
                }

                //Returning Json Data  
                return new JsonResult(new { items = resultList, total = recordsTotal });
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JsonResult loadDefaultSubscriptionDataTable(HttpRequest Request)
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                // Skiping number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();

                int planId = int.Parse(Request.Form["planId"].FirstOrDefault());
                int statusId = int.Parse(Request.Form["statusId"].FirstOrDefault());
                //bool notGoodyReceived = !bool.Parse(Request.Form["goody"].FirstOrDefault());

                // Paging Length 10,20  
                var length = Request.Form["length"].FirstOrDefault();
                // Sort Column Name                  
                var columnIndex = Request.Form["order[0][column]"].ToString();

                // var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                string sortColumn = Request.Form[$"columns[{columnIndex}][data]"].ToString();

                var sortDirection = Request.Form["order[0][dir]"].ToString();
                // Sort Column Direction ( asc ,desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                bool filterPlan = false;
                if (planId == 0)
                {
                    filterPlan = true;
                }
                bool filterStatus = false;
                if (statusId == 0)
                {
                    filterStatus = true;
                }
                int currentYear = DateTime.UtcNow.Year;
                // Getting all Customer data  
                var planData = (from tempplan in _context.Subscription
                                where (tempplan.FkPlanId == planId || filterPlan) &&
                                (tempplan.FkSubscriptionStatus == statusId || filterStatus)
                                select new
                                {
                                    Id = tempplan.Id,
                                    PlantNr = tempplan.PlantNumber,
                                    Customer = tempplan.FkCustomer.FirstName + " " + tempplan.FkCustomer.LastName,
                                    Plan = tempplan.FkPlan.Name,
                                    Periode = tempplan.Periodes.OrderByDescending(x => x.EndDate).FirstOrDefault().StartDate.ToString("dd.MM.yyyy") + " -<br>" + tempplan.Periodes.OrderByDescending(x => x.EndDate).FirstOrDefault().EndDate.ToString("dd.MM.yyyy"),
                                    StartDate = tempplan.Periodes.OrderByDescending(x => x.EndDate).FirstOrDefault().StartDate,
                                    Payed = tempplan.Periodes.OrderByDescending(x => x.EndDate).FirstOrDefault().Payed ? "Ja" : "Nein",
                                    Status = tempplan.FkSubscriptionStatusNavigation.Label,
                                    EndDate = tempplan.Periodes.OrderByDescending(x => x.EndDate).FirstOrDefault().EndDate

                                });
                //select new {Id = tmpcustomer.Id, DisplayString = ((tmpcustomer.Company ?? "") + " " + (tmpcustomer.FirstName ?? "") + " " + (tmpcustomer.LastName ?? "") + " " + (tmpcustomer.Street ?? "") + " " + (tmpcustomer.Zip ?? "") + " " + (tmpcustomer.City ?? "")).Replace("  "," ").Trim() });

                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    sortColumn = sortColumn.Substring(0, 1).ToUpper() + sortColumn.Remove(0, 1);
                    if (sortColumn == "Periode")
                    {
                        sortColumn = "StartDate";
                    }
                    planData = planData.OrderBy(sortColumn + ' ' + sortColumnDirection);
                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    searchValue = searchValue.ToLower();
                    int plantNr = 0;
                    if (int.TryParse(searchValue, out plantNr))
                    {
                        planData = planData.Where(m => m.PlantNr == plantNr);

                    }
                    else if (searchValue == "ja")
                    {
                        planData = planData.Where(m => m.Payed == "Ja");
                    }
                    else if (searchValue == "nein")
                    {
                        planData = planData.Where(m => m.Payed == "Nein");
                    }
                    else if (("aktiv").StartsWith(searchValue))
                    {
                        planData = planData.Where(m => m.Status == "Aktiv");

                    }
                    else if (("läuft aus").StartsWith(searchValue))
                    {
                        planData = planData.Where(m => m.Status == "Läuft aus");

                    }
                    else if (("rechnung noch nicht bezahlt").StartsWith(searchValue))
                    {
                        planData = planData.Where(m => m.Status == "Rechnung noch nicht bezahlt");

                    }
                    else if (("ausgelaufen").StartsWith(searchValue))
                    {
                        planData = planData.Where(m => m.Status == "Ausgelaufen");

                    }
                    else if (searchValue.Length > 7 && searchValue.Contains("."))
                    {
                        DateTime tmp = new DateTime();
                        if (DateTime.TryParse(searchValue, out tmp))
                        {
                            planData = planData.Where(x => x.StartDate == tmp || x.EndDate == tmp);
                        }
                    }
                    else
                    {
                        planData = planData.Where(m => m.Customer.ToLower().Contains(searchValue) || m.Plan.ToLower().StartsWith(searchValue));
                    }
                }

                //total number of rows count   
                recordsTotal = planData.Count();
                //Paging   
                var data = planData.Skip(skip).Take(pageSize).ToList();

                //Returning Json Data  
                return new JsonResult(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JsonResult loadGoodiesSubscriptionDataTable(HttpRequest Request)
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                // Skiping number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();
                // Paging Length 10,20  
                var length = Request.Form["length"].FirstOrDefault();
                // Sort Column Name                  
                var columnIndex = Request.Form["order[0][column]"].ToString();

                // var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                string sortColumn = Request.Form[$"columns[{columnIndex}][data]"].ToString();

                var sortDirection = Request.Form["order[0][dir]"].ToString();
                // Sort Column Direction ( asc ,desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                int currentYear = DateTime.UtcNow.Year;
                // Getting all Customer data  
                var planData = (from tempplan in _context.Subscription
                                where (tempplan.FkSubscriptionStatus == 1 || tempplan.FkSubscriptionStatus == 2) && tempplan.Periodes.Where(x => x.PeriodesGoodies.Where(y => y.Received == false && y.SubPeriodeYear <= currentYear).Any()).Any()
                                select new
                                {
                                    Id = tempplan.Id,
                                    PlantNr = tempplan.PlantNumber,
                                    Customer = tempplan.FkCustomer.FirstName + " " + tempplan.FkCustomer.LastName,
                                    Plan = tempplan.FkPlan.Name,
                                    Goodies = tempplan.Periodes.OrderByDescending(x => x.EndDate).FirstOrDefault().PeriodesGoodies.Where(y => y.Received == false && y.SubPeriodeYear <= currentYear),
                                    Periode = tempplan.Periodes.OrderByDescending(x => x.EndDate).FirstOrDefault().StartDate.ToString("dd.MM.yyyy") + " -<br>" + tempplan.Periodes.OrderByDescending(x => x.EndDate).FirstOrDefault().EndDate.ToString("dd.MM.yyyy"),
                                    Status = tempplan.FkSubscriptionStatusNavigation.Label,
                                });

                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    if (sortColumn == "Periode")
                    {
                        sortColumn = "StartDate";
                    }
                    sortColumn = sortColumn.Substring(0, 1).ToUpper() + sortColumn.Remove(0, 1);
                    planData = planData.OrderBy(sortColumn + ' ' + sortColumnDirection);
                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    planData = planData.Where(m => m.Customer.ToLower().Contains(searchValue) || m.PlantNr.ToString() == searchValue);
                }

                //total number of rows count   
                recordsTotal = planData.Count();
                //Paging   
                var data = planData.Skip(skip).Take(pageSize).ToList();
                List<SubscriptionGoodiesDataTableJSONModel> jsonResultList = new List<SubscriptionGoodiesDataTableJSONModel>();
                foreach (var item in data)
                {
                    SubscriptionGoodiesDataTableJSONModel tmpModel = new SubscriptionGoodiesDataTableJSONModel
                    {
                        Id = item.Id,
                        PlantNr = item.PlantNr ?? 0,
                        Customer = item.Customer,
                        Plan = item.Plan,
                        Periode = item.Periode,
                        Status = item.Status,
                        Goodies = ""
                    };
                    foreach (var goodie in item.Goodies)
                    {
                        tmpModel.Goodies += "<label>" + this._context.PlanGoodies.Where(x => x.Id == goodie.FkPlanGoodiesId).FirstOrDefault().Name + " (" + goodie.SubPeriodeYear + ")" + "</label> <input type = 'checkbox' class='datatableGoodieCheckbox' id='goodie_" + goodie.Id + "' value='" + goodie.Id + "' ><br>";

                    }

                    if (tmpModel.Goodies == "")
                    {
                        tmpModel.Goodies = "Alle Ernteanteile erhalten";
                    }

                    jsonResultList.Add(tmpModel);
                }
                //Returning Json Data  
                return new JsonResult(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = jsonResultList });
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void checkSubscriptionStatus(int id)
        {
            var subscriptionToCheck = this._context.Subscription
                  .Include(c => c.Periodes)
                  .Include(c => c.FkPlan)
                  .Where(c => c.Id == id)
                  .FirstOrDefault();

            if (subscriptionToCheck == null)
            {
                return;
            }

            var LastPeriode = subscriptionToCheck.Periodes.OrderByDescending(x => x.Id).FirstOrDefault();
            if (LastPeriode == null)
            {
                return;
            }

            //Ausgelaufene Patenschaft
            if (subscriptionToCheck.FkSubscriptionStatus == 4 && !LastPeriode.Payed)
            {
                return;
            }

            //Patenschaft läuft nicht dieses Jahr aus
            if (LastPeriode.EndDate.Year > DateTime.UtcNow.Year)
            {
                //Patenschaft bezahlt und läuft nicht dieses Jahr aus
                if (subscriptionToCheck.FkSubscriptionStatus == 1 && LastPeriode.Payed)
                {
                    return;
                }
                //Patenschaft wurde im letzten Jahr erstellt oder ist eine verlängerung und wurde nicht bezahlt, daher als Ausgelaufen markieren
                else if (LastPeriode.StartDate.Year < DateTime.UtcNow.Year && !LastPeriode.Payed)
                {
                    //subscriptionToCheck.FkSubscriptionStatus = 4;
                    //this._context.SaveChanges();
                    return;
                }
                else if (LastPeriode.Payed)
                {
                    subscriptionToCheck.FkSubscriptionStatus = 1;
                    this._context.SaveChanges();
                }
                //else if (!LastPeriode.Payed && LastPeriode.StartDate.Year == DateTime.UtcNow.Year)
                //{
                //    subscriptionToCheck.FkSubscriptionStatus = 3;
                //    this._context.SaveChanges();
                //}
                else if (!LastPeriode.Payed)
                {
                    subscriptionToCheck.FkSubscriptionStatus = 3;
                    this._context.SaveChanges();
                }
            }
            //Patenschaft läuft dieses Jahr aus, als Läuft aus markieren
            else if (LastPeriode.EndDate.Year == DateTime.UtcNow.Year && LastPeriode.Payed)
            {
                subscriptionToCheck.FkSubscriptionStatus = 2;
                this._context.SaveChanges();
                return;
            }
            //Patenschaft ist ausgelaufen und wurde nicht verlängert, daher als Ausgelaufen markieren
            else
            {
                subscriptionToCheck.FkSubscriptionStatus = 4;
                this._context.SaveChanges();
                return;
            }
        }

        public bool expireSubscription(int subId)
        {
            using (var dbContextTransaction = this._context.Database.BeginTransaction())
            {
                try
                {
                    var subscription = this._context.Subscription.Where(x => x.Id == subId).FirstOrDefault();
                    if (subscription == null)
                    {
                        return false;
                    }
                    subscription.FkSubscriptionStatus = 4;

                    this._context.SaveChanges();
                    dbContextTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                }
            }
            return false;
        }
        public bool renewSubscription(int subId)
        {
            using (var dbContextTransaction = this._context.Database.BeginTransaction())
            {
                try
                {
                    var subscription = this._context.Subscription.Where(x => x.Id == subId).FirstOrDefault();
                    if (subscription == null)
                    {
                        return false;
                    }

                    var plan = this._context.Plans.Where(x => x.Id == subscription.FkPlanId).FirstOrDefault();
                    if (plan == null)
                    {
                        return false;
                    }

                    var lastperiode = this._context.Periodes.Where(x => x.FkSubscriptionId == subId).OrderByDescending(x => x.EndDate).FirstOrDefault();

                    var newStartDate = DateTime.Now;

                    if (lastperiode.EndDate > newStartDate && lastperiode.Payed)
                    {
                        newStartDate = lastperiode.EndDate;
                        newStartDate = newStartDate.AddDays(1);
                    }

                    var currentDeadline = new DateTime(newStartDate.Year, plan.Deadline.Month, plan.Deadline.Day);
                    var periodeEnd = new DateTime(newStartDate.Year, 12, 31);
                    if (newStartDate > currentDeadline)
                    {
                        periodeEnd = periodeEnd.AddYears((plan.Duration));
                    }
                    else
                    {
                        periodeEnd = periodeEnd.AddYears(plan.Duration - 1);
                    }

                    var p = new Periodes()
                    {
                        FkSubscriptionId = subId,
                        StartDate = newStartDate,
                        EndDate = periodeEnd,
                        Price = plan.Price
                    };

                    subscription.FkSubscriptionStatus = 3;

                    int startYear = (p.EndDate.Year - plan.Duration) + 1;

                    for (int i = 0; i < plan.Duration; i++)
                    {
                        PeriodesGoodies newGoodie = new PeriodesGoodies
                        {
                            FkPlanGoodiesId = plan.FkGoodyId,
                            SubPeriodeYear = startYear
                        };
                        p.PeriodesGoodies.Add(newGoodie);
                        startYear++;

                    }
                    this._context.Periodes.Add(p);

                    this._context.SaveChanges();
                    dbContextTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                }
            }
            return false;
        }

        public SubscriptionEditViewModel loadSubscriptionInputModel(int id)
        {
            checkSubscriptionStatus(id);

            var subscriptionToLoad = this._context.Subscription
                  .Include(c => c.FkCustomer)
                  .Include(c => c.FkPlan).ThenInclude(x => x.FkGoody)
                  .Include(c => c.FkSubscriptionStatusNavigation)
                  .Where(c => c.Id == id)
                  .FirstOrDefault();

            if (subscriptionToLoad == null)
            {
                return null;
            }

            var subscriptionToEdit = new SubscriptionEditViewModel
            {
                ID = subscriptionToLoad.Id,
                PlantNumber = subscriptionToLoad.PlantNumber ?? 0,
                Customer = SubscriptionCustomerViewModel.CreateFromCustomer(subscriptionToLoad.FkCustomer),
                Plan = SubscriptionPlanViewModel.CreateFromPlan(subscriptionToLoad.FkPlan),
                StatusID = subscriptionToLoad.FkSubscriptionStatus,
                StatusLabel = subscriptionToLoad.FkSubscriptionStatusNavigation.Label,
                TemplateID = subscriptionToLoad.FkPlan.FkGoody.FkTemplateLabel.Value,
                SubscriptionRemarks = subscriptionToLoad.SubscriptionRemarks
            };
            var periodesToLoad = this._context.Periodes
                .Include(c => c.FkGiftedBy)
                .Include(c => c.PeriodesGoodies)
                .Where(c => c.FkSubscriptionId == subscriptionToLoad.Id);

            subscriptionToEdit.Periodes = new List<SubscriptionPeriodeViewModel>();

            var currentDeadline = new DateTime(DateTime.Now.Year, subscriptionToLoad.FkPlan.Deadline.Month, subscriptionToLoad.FkPlan.Deadline.Day);

            bool hasCurrent = false;
            foreach (Periodes p in periodesToLoad)
            {
                SubscriptionPeriodeViewModel pModel = SubscriptionPeriodeViewModel.CreateFromPeriode(p);
                if (p.StartDate.ToUniversalTime() < DateTime.UtcNow && p.EndDate > currentDeadline && p.Payed)
                {
                    pModel.CurrentPeriode = true;
                    hasCurrent = true;
                }
                //else if (p.StartDate > DateTime.UtcNow && !hasCurrent)
                //{
                //    pModel.CurrentPeriode = true;
                //    hasCurrent = true;
                //}
                foreach (PeriodesGoodies pg in p.PeriodesGoodies)
                {
                    pModel.Goodies.Add(SubscriptionPeriodesGoodiesViewModel.CreateFromGoodie(pg));
                }

                pModel.PaymentMethods = getAvailablePaymentMethods();
                pModel.GoodiesLabel = subscriptionToLoad.FkPlan.FkGoody.Name;
                subscriptionToEdit.Periodes.Add(pModel);
            }

            if (!hasCurrent)
            {
                subscriptionToEdit.Periodes.LastOrDefault().CurrentPeriode = true;
            }
            subscriptionToEdit.Photos = new Dictionary<int, String>();
            foreach (var photo in _context.SubscriptionPhotos.Where(x => x.FkSubscriptionId == id).Select(x => new { x.FkFileId, x.FkFile.OriginalName }))
            {
                subscriptionToEdit.Photos.Add(photo.FkFileId, "/file/img/" + id + '/' + photo.OriginalName.Insert(photo.OriginalName.LastIndexOf("."), "_thumb"));
            }

            if (subscriptionToLoad.DateCreated != null)
            {
                subscriptionToEdit.DateCreated = subscriptionToLoad?.DateCreated.Value.ToLocalTime().ToString("dd.MM.yyyy");
            }
            if (subscriptionToLoad.DateModified != null)
            {
                subscriptionToEdit.DateModified = subscriptionToLoad?.DateModified.Value.ToLocalTime().ToString("dd.MM.yyyy");
            }

            subscriptionToEdit.UserCreated = subscriptionToLoad?.UserCreated;
            subscriptionToEdit.UserModified = subscriptionToLoad?.UserModified;

            return subscriptionToEdit;
        }

        public async Task<bool> addSubscriptionPhoto(IFormFile formFile, int subscriptionID)
        {
            try
            {
                var subscription = this._context.Subscription
                             .Where(c => c.Id == subscriptionID)
                             .FirstOrDefault();
                if (subscription == null)
                {
                    return false;
                }

                string webRootPath = _hostingEnvironment.WebRootPath;

                string customerPath = webRootPath + "\\Data\\Userdata\\" + subscription.FkCustomerId;
                if (!Directory.Exists(customerPath))
                {
                    Directory.CreateDirectory(customerPath);
                }
                string subscriptionPath = customerPath + "\\" + subscription.Id;
                if (!Directory.Exists(subscriptionPath))
                {
                    Directory.CreateDirectory(subscriptionPath);
                }

                String newFileName = Guid.NewGuid().ToString();

                while (File.Exists(subscriptionPath + "\\" + newFileName))
                {
                    newFileName = Guid.NewGuid().ToString();
                }

                using (var fileStream = new FileStream(subscriptionPath + "\\" + newFileName, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                String originalFileName = formFile.FileName;
                int count = 1;
                while (_context.SubscriptionPhotos.Where(x => x.FkSubscriptionId == subscriptionID && x.FkFile.OriginalName == originalFileName).Any())
                {
                    originalFileName = formFile.FileName;
                    originalFileName = originalFileName.Insert(originalFileName.LastIndexOf("."), "_" + count);
                    count++;
                }

                var newFile = new Files
                {
                    OriginalName = originalFileName,
                    Path = "\\Data\\Userdata\\" + subscription.FkCustomerId + "\\" + subscription.Id + "\\",
                    FileName = newFileName,
                    ContentType = formFile.ContentType
                };
                this._context.Files.Add(newFile);

                var newSubscriptionPhotos = new SubscriptionPhotos
                {
                    FkFile = newFile,
                    FkSubscription = subscription
                };

                this._context.SubscriptionPhotos.Add(newSubscriptionPhotos);

                this._context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

            }

            return false;
        }

        public bool updatePaymentStatus(int periodeID, bool isPayed)
        {
            try
            {
                var periodeToEdit = this._context.Periodes
                  .Where(c => c.Id == periodeID)
                  .Include(x => x.FkSubscription)
                  .FirstOrDefault();
                if (periodeToEdit == null)
                {
                    return false;
                }
                periodeToEdit.Payed = isPayed;

                if (isPayed)
                {
                    if (periodeToEdit.FkSubscription.FkSubscriptionStatus == 3)
                    {
                        periodeToEdit.FkSubscription.FkSubscriptionStatus = 1;
                    }
                    periodeToEdit.PayedDate = DateTime.UtcNow;
                }
                else
                {
                    periodeToEdit.PayedDate = null;
                    periodeToEdit.FkSubscription.FkSubscriptionStatus = 3;
                }
                this._context.SaveChanges();
                checkSubscriptionStatus(periodeToEdit.FkSubscriptionId);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool updatePaymentReminderSent(int periodeID, bool isReminderSent)
        {
            try
            {
                var periodeToEdit = this._context.Periodes
                  .Where(c => c.Id == periodeID)
                  .FirstOrDefault();
                if (periodeToEdit == null)
                {
                    return false;
                }
                periodeToEdit.PaymentReminderSent = isReminderSent;

                if (isReminderSent)
                {
                    periodeToEdit.PaymentReminderSentDate = DateTime.UtcNow;
                }
                else
                {
                    periodeToEdit.PayedDate = null;
                }

                this._context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool updateSubscriptionRemarks(int subscriptionId, string subscriptionRemarks)
        {
            try
            {
                var subscriptionToEdit = this._context.Subscription
                  .Where(c => c.Id == subscriptionId)
                  .FirstOrDefault();
                if (subscriptionToEdit == null)
                {
                    return false;
                }
                subscriptionToEdit.SubscriptionRemarks = subscriptionRemarks;

                this._context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool updateReceivedGoody(int goodyID, bool hasReceived)
        {
            try
            {
                var goodyToEdit = this._context.PeriodesGoodies
                  .Where(c => c.Id == goodyID)
                  .FirstOrDefault();
                if (goodyToEdit == null)
                {
                    return false;
                }
                goodyToEdit.Received = hasReceived;

                if (hasReceived)
                {
                    goodyToEdit.ReceivedAt = DateTime.UtcNow;
                }
                else
                {
                    goodyToEdit.ReceivedAt = null;
                }

                this._context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool updatePeriodeGiver(int periodeID, int giverId)
        {
            try
            {
                var periodeToEdit = this._context.Periodes
                  .Where(c => c.Id == periodeID)
                  .FirstOrDefault();
                if (periodeToEdit == null)
                {
                    return false;
                }

                if (giverId > -1)
                {
                    if (this._context.Customers.Any(x => x.Id == giverId))
                    {
                        periodeToEdit.FkGiftedById = giverId;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    periodeToEdit.FkGiftedById = null;
                }

                this._context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool updatePaymentMethod(int periodeID, int paymentID)
        {
            try
            {
                var periodeToEdit = this._context.Periodes
                  .Where(c => c.Id == periodeID)
                  .FirstOrDefault();
                if (periodeToEdit == null)
                {
                    return false;
                }

                if (this._context.PaymentMethods.Any(x => x.Id == paymentID))
                {
                    periodeToEdit.FkPayedMethodId = paymentID;
                }
                else
                {
                    return false;
                }

                this._context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task updateSubscriptionStatusAsync()
        {
            await this._context.Subscription.Select(x => x.Id).ForEachAsync(x => checkSubscriptionStatus(x));
        }

        public bool checkIfNrExists(int planId, int nr)
        {
            return this._context.Subscription.Where(x => x.FkPlanId == planId && x.PlantNumber == nr).Any();
        }

        public String getCustomerSelect2Text(int customerID)
        {
            var customerData = (from tmpcustomer in _context.Customers
                                where tmpcustomer.Id == customerID
                                select new { DisplayString = ((tmpcustomer.FirstName ?? "") + " " + (tmpcustomer.LastName ?? "") + " " + (tmpcustomer.Zip ?? "")).Replace("  ", " ").Trim() }).FirstOrDefault();
            return customerData.DisplayString;
        }

        public String getPlanSelect2Text(int planID)
        {
            var planData = (from tmpplan in _context.Plans
                            where tmpplan.Id == planID
                            select new { DisplayString = ((tmpplan.Name ?? "") + " - Laufzeit: " + (tmpplan.Duration.ToString() ?? "") + " Jahre - Preis " + (tmpplan.Price.ToString("N2") ?? "")).Replace("  ", " ").Trim() }).FirstOrDefault();
            return planData.DisplayString;
        }

        public int getNextPlantNr(int planID)
        {
            try
            {
                int last = _context.Subscription.Where(x => x.FkPlanId == planID).Max(x => x.PlantNumber).Value + 1;

                return last;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }
        public bool updatePeriodeDates(int periodID, string periodStartDate, string periodEndDate)
        {
            try
            {
                var periodeToEdit = this._context.Periodes
                  .Where(c => c.Id == periodID)
                  .Include(c => c.PeriodesGoodies)
                  .Include(c => c.FkSubscription).ThenInclude(c => c.FkPlan)
                  .FirstOrDefault();
                if (periodeToEdit == null)
                {
                    return false;
                }

                periodeToEdit.StartDate = DateTime.Parse(periodStartDate).ToUniversalTime();
                periodeToEdit.EndDate = DateTime.Parse(periodEndDate).ToUniversalTime();
                var goodies = periodeToEdit.PeriodesGoodies;
                this._context.PeriodesGoodies.RemoveRange(goodies);

                int planDuration = periodeToEdit.FkSubscription.FkPlan.Duration;
                int goodyId = periodeToEdit.FkSubscription.FkPlan.FkGoodyId;
                int startYear = (periodeToEdit.EndDate.Year - planDuration) + 1;

                for (int i = 0; i < planDuration; i++)
                {
                    PeriodesGoodies newGoodie = new PeriodesGoodies
                    {
                        FkPlanGoodiesId = goodyId,
                        SubPeriodeYear = startYear
                    };
                    periodeToEdit.PeriodesGoodies.Add(newGoodie);
                    startYear++;
                }

                if (periodeToEdit.EndDate > periodeToEdit.StartDate)
                {
                    this._context.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}