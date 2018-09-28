using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MercadoPago;
using MercadoPago.Resources;
using MercadoPago.DataStructures.Preference;

namespace PruebaMP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // https://api.mercadolibre.com/sites/MLA/shipping_options?zip_code_from=5900&zip_code_to=2550&dimensions=10x10x20,500
            MercadoPago.SDK.ClientId = "8668524491166741";
            MercadoPago.SDK.ClientSecret = "4PaR49TQj2gBLMygCWZ5hhK3ITcUGASb";
            Preference preference = new Preference();
            preference.Items.Add(
              new Item()
              {
                  Id = "1234",
                  Title = "Mediocre Paper Knife",
                  Quantity = 3,
                  CurrencyId = MercadoPago.Common.CurrencyId.ARS,
                  UnitPrice = (float)4
              }
            );
            preference.Items.Add(
             new Item()
             {
                 Id = "1235",
                 Title = "Mayonesa",
                 Quantity = 1,
                 CurrencyId = MercadoPago.Common.CurrencyId.ARS,
                 UnitPrice = (float)1
             }
           );
            // Setting a payer object as value for Payer property
            //preference.Payer = new Payer()
            //{
            //    Email = "cary@yahoo.com"
            //};



            preference.Payer = new Payer()
            {
                Name = "Lucas",
                Surname = "r",
                Email = "charles@yahoo.com",
                Phone = new Phone()
                {
                    AreaCode = "",
                    Number = "941159745"
                },
                Identification = new Identification()
                {
                    Type = "DNI",
                    Number = "12345678"
                },
                Address = new Address()
                {
                    StreetName = "San Martin",
                    StreetNumber = 523,
                    ZipCode = "5903"
                }
            };


            List<int> lst = new List<int>();
            lst.Add(73328);


            Shipment shipment = new Shipment()
            {
                Cost = 520,
                ReceiverAddress = new ReceiverAddress()
                {
                    ZipCode = "5900",
                    StreetName = "Corrientes",
                    StreetNumber = 1515,
                    Floor = "12",
                    Apartment = "C"
                },
                Mode = MercadoPago.Common.ShipmentMode.Me2,
                Dimensions = "30x30x30,500",
                DefaultShippingMethod = 73328,
                FreeMethods = lst,
                FreeShipping = true
            };
            
            
            preference.Shipment = shipment;
            
            BackUrls backUrls = new BackUrls();
            backUrls.Failure = "";
            backUrls.Success = "";
            backUrls.Pending = "";


            // Save and posting preference
            preference.Save();

            ViewBag.btnPagar = preference.InitPoint;


            return View();

        }
        public ActionResult PagoEnProceso()
        {
            return View();
        }

        public ActionResult PagoFinalizado()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}