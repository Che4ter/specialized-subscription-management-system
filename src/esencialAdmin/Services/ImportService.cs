using esencialAdmin.Data.Models;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ExcelDataReader;
using System.Data;
using System.Collections.Generic;

namespace esencialAdmin.Services
{
    public class ImportService : IImportService
    {
        protected readonly esencialAdminContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ImportService(esencialAdminContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public int importCustomer(IFormFile formFile)
        {
            try
            {
                if (formFile.Length > 0)
                {
                    using (var memStream = new MemoryStream())
                    {
                        formFile.CopyTo(memStream);
                        memStream.Position = 0;

                        // Auto-detect format, supports:
                        //  - Binary Excel files (2.0-2003 format; *.xls)
                        //  - OpenXml Excel files (2007 format; *.xlsx)
                        using (var reader = ExcelReaderFactory.CreateReader(memStream))
                        {
                            var result = reader.AsDataSet();
                            HashSet<Customers> customerList = new HashSet<Customers>();
                            foreach (DataTable table in result.Tables)
                            {
                                for (int i = 1; i < table.Rows.Count; i++)
                                {
                                    var cust = new Customers
                                    {
                                        LastName = table.Rows[i].ItemArray[0]?.ToString(),
                                        FirstName = table.Rows[i].ItemArray[1]?.ToString(),
                                        Street = table.Rows[i].ItemArray[2]?.ToString(),
                                        Zip = table.Rows[i].ItemArray[3]?.ToString(),
                                        City = table.Rows[i].ItemArray[4]?.ToString(),
                                        Phone = table.Rows[i].ItemArray[5]?.ToString(),
                                        Email = table.Rows[i].ItemArray[6]?.ToString(),
                                        GeneralRemarks = table.Rows[i].ItemArray[7]?.ToString(),
                                        Company = table.Rows[i].ItemArray[8]?.ToString(),
                                    };
                                    if (cust.Email == "")
                                    {
                                        cust.Email = null;
                                    }
                                    if (cust.GeneralRemarks == "")
                                    {
                                        cust.Email = null;
                                    }
                                    if (cust.Company == "")
                                    {
                                        cust.Company = null;
                                    }
                                    if (cust.Phone != "")
                                    {
                                        cust.Phone = cust.Phone.Replace(" ", "");
                                    }
                                    customerList.Add(cust);
                                }
                            }
                            reader.Close();
                            this._context.Customers.AddRange(customerList);
                            this._context.SaveChanges();
                            return customerList.Count;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var a = ex.Message;
            }

            return -1;
        }
    }
}