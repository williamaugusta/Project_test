using Microsoft.AspNetCore.Mvc;
using Project7MAR2023.PGModels;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System.Reflection.Metadata.Ecma335;
using Project7MAR2023.Models;
using Project7MAR2023.Datawarehouse;

namespace Project7MAR2023.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            var context = new DatawarehouseContext();
            //var listuser = context.EmployeeTrainingHistories.Where(x=>x.Completeddate.Value.Month == 1 && x.Completeddate.Value.Year == 2023).Count();
            ViewBag.jan_a = context.EmployeeTrainingHistories.Where(x => x.Completeddate.Value.Month == 1 && x.Completeddate.Value.Year == 2023).Count();
            ViewBag.jan_b = context.EmployeeTrainingHistories.Where(x => x.Completeddate.Value.Month == 1 && x.Completeddate.Value.Year == 2023).Select(x=>x.Traningtype).Distinct().Count();
            ViewBag.feb_a = context.EmployeeTrainingHistories.Where(x => x.Completeddate.Value.Month == 2 && x.Completeddate.Value.Year == 2023).Count();
            ViewBag.feb_b = context.EmployeeTrainingHistories.Where(x => x.Completeddate.Value.Month == 2 && x.Completeddate.Value.Year == 2023).Select(x => x.Traningtype).Distinct().Count();
            ViewBag.mar_a = context.EmployeeTrainingHistories.Where(x => x.Completeddate.Value.Month == 3 && x.Completeddate.Value.Year == 2023).Count();
            ViewBag.mar_b = context.EmployeeTrainingHistories.Where(x => x.Completeddate.Value.Month == 3 && x.Completeddate.Value.Year == 2023).Select(x => x.Traningtype).Distinct().Count();
            ViewBag.apr_a = context.EmployeeTrainingHistories.Where(x => x.Completeddate.Value.Month == 4 && x.Completeddate.Value.Year == 2023).Count();
            ViewBag.apr_b = context.EmployeeTrainingHistories.Where(x => x.Completeddate.Value.Month == 4 && x.Completeddate.Value.Year == 2023).Select(x => x.Traningtype).Distinct().Count();
            ViewBag.may_a = context.EmployeeTrainingHistories.Where(x => x.Completeddate.Value.Month == 5 && x.Completeddate.Value.Year == 2023).Count();
            ViewBag.may_b = context.EmployeeTrainingHistories.Where(x => x.Completeddate.Value.Month == 5 && x.Completeddate.Value.Year == 2023).Select(x => x.Traningtype).Distinct().Count();
            ViewBag.jun_a = context.EmployeeTrainingHistories.Where(x => x.Completeddate.Value.Month == 6 && x.Completeddate.Value.Year == 2023).Count();
            ViewBag.jun_b = context.EmployeeTrainingHistories.Where(x => x.Completeddate.Value.Month == 6 && x.Completeddate.Value.Year == 2023).Select(x => x.Traningtype).Distinct().Count();
            ViewBag.jul_a = context.EmployeeTrainingHistories.Where(x => x.Completeddate.Value.Month == 7 && x.Completeddate.Value.Year == 2023).Count();
            ViewBag.jul_b = context.EmployeeTrainingHistories.Where(x => x.Completeddate.Value.Month == 7 && x.Completeddate.Value.Year == 2023).Select(x => x.Traningtype).Distinct().Count();
            return View();
        }

        public IActionResult Report()
        {
            
            var context = new DatawarehouseContext();
            var listuser = context.EmployeeTrainingHistories.ToList();
            return View(listuser);
        }

        public IActionResult getListUser()
        {
            var context = new PostgresContext();
            var listuser = context.Users.ToList();
            return Json(listuser);
        }

        public IActionResult insUser()
        {
            using (var context = new PostgresContext())
            {
                var std = new User()
                {
                    Ids = 10,
                    Nama = "Gates",
                    Keterangan = "Ket1"
                };
                context.Users.Add(std);
                context.SaveChanges();
            }
            return Content("sukses");
        }

        public IActionResult updUser()
        {
            var context = new PostgresContext();
            var std = context.Users.FirstOrDefault(x => x.Ids == 10);
            if (std != null)
            {
                std.Nama = "Steve -- Updated";
                context.SaveChanges();
                return Content("updated");
            }
            return Content("failed");
        }

        public IActionResult delUser()
        {
            var context = new PostgresContext();
            var std = context.Users.FirstOrDefault(x => x.Ids == 10);
            if (std != null)
            {
                context.Remove(std);
                context.SaveChanges();
                return Content("deleted");
            }
            return Content("failed");
        }

        public List<GSheet> getFromGS()
        {
            string[] Scopes = { SheetsService.Scope.Spreadsheets };
            string ApplicationName = "DotTutorials";
            string sheet = "Sheet1";
            string SpreadsheetId = "1DsBSVfVJrnLaWJt-sI8k9JWTETzDhUQDKxR2Hgyrdu8";
            SheetsService service;

            GoogleCredential credential;
            using (var stream = new FileStream("Google/southern-lane.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }

            service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });


            var myInvList = new List<GSheet>();
            var range = $"{sheet}!A:C";
            int j = 0;
            SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(SpreadsheetId, range);
            var response = request.Execute();
            IList<IList<object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    j++;
                    if (j > 0)
                    {
                        var myInv = new GSheet()
                        {
                            id = row[0].ToString(),
                            title = row[1].ToString(),
                            date = row[2].ToString()
                        };

                        myInvList.Add(myInv);
                    }
                }
            }
            else
            {
                myInvList.Clear();
            }
            return myInvList;
        }

        public List<AzureEmployee> getDataAzure()
        {
            var context = new AkasiaContext();
            List<AzureEmployee> result = new List<AzureEmployee>();
            var listemployee = context.VEmployeePositions.ToList();
            foreach (var a in listemployee)
            {
                var myInv = new AzureEmployee()
                {
                    id = a.Employeeid,
                    nama = a.Fullname,
                    position = a.Postitle
                };
                result.Add(myInv);
            }
            return result;
        }

        public IActionResult processETL()
        {
            try
            {
                var getctx = new DatawarehouseContext();
                var temp_a = getctx.EmployeeTrainingHistories.ToList();
                foreach (var b in temp_a)
                {
                    using (var ctx = new DatawarehouseContext())
                    {
                        ctx.Remove(b);
                        ctx.SaveChanges();
                    }
                }
                
                List<GSheet> dataGoogleSheet = getFromGS();
                List<AzureEmployee> dataAzure = getDataAzure();

                var datajoin = from a in dataAzure
                               join b in dataGoogleSheet
                               on a.id equals b.id
                               select new { a.id, a.nama, a.position, b.title, b.date };

                foreach (var a in datajoin)
                {
                    using (var context = new DatawarehouseContext())
                    {
                        DateOnly dt = DateOnly.Parse(a.date);
                        Random random = new Random();
                        var obj = new EmployeeTrainingHistory()
                        {
                            Id = random.Next(),
                            Employeeid = a.id,
                            Fullname = a.nama,
                            Birthdate = DateOnly.FromDateTime(DateTime.Today),
                            Address = "Address",
                            Postitle = a.position,
                            Traningtype = a.title,
                            Completeddate = dt
                        };
                        context.EmployeeTrainingHistories.Add(obj);
                        context.SaveChanges();
                    }
                }
                //return Content("Success...");
                ViewBag.message = "Succes : ETL Process has been done...";
            }
            catch (Exception ex)
            {
                //return Content("Error : " + ex.ToString);
                ViewBag.message = "Error : " + ex.ToString();
            }
            return View();
        }
    }

}
