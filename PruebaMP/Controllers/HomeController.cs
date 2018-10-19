using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using mercadopago;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PruebaMP.Models;
using PruebaMP.Services;

namespace PruebaMP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            //Nico
            //MercadoPago.SDK.ClientId = "8668524491166741";
            //MercadoPago.SDK.ClientSecret = "4PaR49TQj2gBLMygCWZ5hhK3ITcUGASb";

            //dev
            //mercadopago.SDK.ClientId = "3825884689807039";
            //mercadopago.SDK.ClientSecret = "2aLAWWtUxSs4ZbXjSXQRVilQCG1RdSlz";

            
            //MercadoPago.SDK.AccessToken = "TEST-3825884689807039-100215-16400b9b5d9943961c4c3c1242690a8f-357350759";
           

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

            // envio
            // https://api.mercadolibre.com/sites/MLA/shipping_options?zip_code_from=5900&zip_code_to=2550&dimensions=10x10x20,500

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
            // https://www.dev-imagine.com/?collection_id=4234545208&collection_status=approved&preference_id=357350759-ab6ae775-9a8f-49ee-86ad-0a523c8533c0&external_reference=null&payment_type=account_money&merchant_order_id=870753252
            // https://www.dev-imagine.com/?collection_id=4234281281&collection_status=approved&preference_id=357350759-9ead90b3-1c9a-4709-adf7-a2544638cdec&external_reference=null&payment_type=account_money&merchant_order_id=870755027

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
        [HttpGet]
        public ActionResult NotificacionPago(string topic, string id)
        {
            // URL Notificacion
            // http://localhost:64615/Home/NotificacionPago/?topic=payment&id=4205580523

            srvTest sTest = new srvTest();

            test oTest = new test();
            oTest.result = "topic:" + topic + " - id:" + id;
            sTest.AddTest(oTest);
            try
            {

                //id = "4205580523";
                if (topic == "payment")
                {
                    MP mp = new MP("3825884689807039", "2aLAWWtUxSs4ZbXjSXQRVilQCG1RdSlz");
                    Hashtable Payment = mp.getPaymentInfo(id);
                    Payment = ((Hashtable)Payment["response"]);
                    string orderID = ((Hashtable)Payment["collection"])["order_id"].ToString();
                    //if (((Hashtable)Payment["collection"])["status"].ToString() == "status correcto")
                    //{



                    oTest = new test();
                    oTest.result = "order_id:" + orderID+" - status:" + ((Hashtable)Payment["collection"])["status"].ToString();
                    sTest.AddTest(oTest);



                    string baseUrl = "https://api.mercadopago.com";
                    string url = "merchant_orders/" + orderID + "?access_token=APP_USR-3825884689807039-100215-028a54b7ff4b3017ab7830f35784a693-357350759";
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUrl);
                        var responseTask = client.GetAsync(url);
                        responseTask.Wait();
                        var result = responseTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsStringAsync();
                            readTask.Wait();

                            JObject order = JObject.Parse(readTask.Result);
                            // verificar status de la orden
                            //if(order.GetValue("status").ToString() == "status correcto")
                            //{
                            Newtonsoft.Json.Linq.JToken items = order.GetValue("items");
                            
                            oTest = new test();
                            oTest.result = "items:" + items + " - status:" + order.GetValue("status").ToString() + " - total_amount:" + order.GetValue("total_amount").ToString();
                            sTest.AddTest(oTest);

                            //}

                        }
                    }

                    //}
                }
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}