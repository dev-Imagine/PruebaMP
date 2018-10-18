using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using mercadopago;
using Newtonsoft.Json;

namespace PruebaMP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // https://api.mercadolibre.com/sites/MLA/shipping_options?zip_code_from=5900&zip_code_to=2550&dimensions=10x10x20,500
            //MercadoPago.SDK.ClientId = "8668524491166741";
            //MercadoPago.SDK.ClientSecret = "4PaR49TQj2gBLMygCWZ5hhK3ITcUGASb";


            /*mercadopago.SDK.ClientId = "3825884689807039";
            mercadopago.SDK.ClientSecret = "2aLAWWtUxSs4ZbXjSXQRVilQCG1RdSlz";

            
            //MercadoPago.SDK.AccessToken = "TEST-3825884689807039-100215-16400b9b5d9943961c4c3c1242690a8f-357350759";
            Preference preference = new Preference();
            
            preference.Items.Add(
              new Item()
              {
                  Id = "1234",
                  Title = "Mediocre Paper Knife",
                  Quantity = 1,
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
            backUrls.Success = "http://localhost:64615/Home/PagoFinalizado";
            backUrls.Pending = "http://localhost:64615/Home/PagoEnProceso";


            // Save and posting preference
            preference.Save();*/

            MP mp = new MP("3825884689807039", "2aLAWWtUxSs4ZbXjSXQRVilQCG1RdSlz");
            
            String preferenceData = 
                "{\"items\":" +
                    "[{" +
                    "\"title\":\"Multicolor kite\"," +
                    "\"quantity\":1," +
                    "\"currency_id\":\"ARS\"," +
                    "\"unit_price\":11" +
                    "}"+
                    "]," +
                "\"shipments\":{" +
                    "\"mode\":\"me2\"," +
                    "\"dimensions\":\"30x30x30,500\"," +
                    "\"local_pickup\":true," +
                    "\"free_methods\":[" +
                        "{\"id\":73328}" +
                    "]," +
                "\"default_shipping_method\":73328," +
                "\"zip_code\":\"5700\"" +
                "}," +
                "\"back_urls\":{" +
                        "\"success\": \"https://www.dev-imagine.com\"" +
                "}," +
                "\"auto_return\":\"approved\"," +


                "}";

            Hashtable preference = mp.createPreference(preferenceData);

            // link de pago
            ViewBag.btnPagar = ((Hashtable)preference["response"])["init_point"].ToString();
            // collection id
            // ViewBag.Collection_id = ((Hashtable)preference["response"])["collection_id"].ToString();

            //                                          collection_id
            // https://api.mercadopago.com/v1/payments/4205733345?access_token=APP_USR-3825884689807039-100215-028a54b7ff4b3017ab7830f35784a693-357350759
            //con envio
            // https://www.dev-imagine.com/?collection_id=4205580523&collection_status=approved&preference_id=357350759-b26bbfb3-7857-496c-a2f9-814a2e4aa084&external_reference=null&payment_type=account_money&merchant_order_id=857112399
            //con envio gratis
            // https://www.dev-imagine.com/?collection_id=4207772795&collection_status=approved&preference_id=357350759-8aa31d01-1c36-481f-9ae2-f521327671f1&external_reference=null&payment_type=account_money&merchant_order_id=858026235


            // costso de envio
            // https://api.mercadolibre.com/sites/MLA/shipping_options?zip_code_from=5000&zip_code_to=5152&dimensions=10x10x20,500

            // Al recibir la notificacion
            // Obtener topic=payment&id=123456789
            // filtrar por topic=payment
            // Obtener informacion de pago con el id  collection_id
            // https://api.mercadopago.com/v1/payments/4205580523?access_token=APP_USR-3825884689807039-100215-028a54b7ff4b3017ab7830f35784a693-357350759
            // del resultado de lo anterior
            //                                             order.id
            // https://api.mercadopago.com/merchant_orders/857112399?access_token=APP_USR-3825884689807039-100215-028a54b7ff4b3017ab7830f35784a693-357350759
            // y obtenemos todos los datos
            // Obtener etiqueta de envio
            // https://api.mercadolibre.com/shipment_labels?shipment_ids=27709894001&response_type=zpl2&access_token=APP_USR-3825884689807039-100215-028a54b7ff4b3017ab7830f35784a693-357350759

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