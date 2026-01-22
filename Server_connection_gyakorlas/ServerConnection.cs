using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Server_connection_gyakorlas
{
    public class ServerConnection
    {
        private HttpClient _client = new HttpClient();
        string baseUrl = "";
        public ServerConnection(string url)
        {
            if (!url.StartsWith("http://")) throw new ArgumentException("Hibás url -> http://");
            baseUrl = url;
            _client.BaseAddress = new Uri(baseUrl);
        }

        public async Task<List<Pilots>> ListAllPilots()
        {
            List<Pilots> PilotList = new List<Pilots>();
            string url = baseUrl + "/listPilots";
            try
            {
                HttpResponseMessage response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                PilotList = JsonSerializer.Deserialize<List<Pilots>>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return PilotList;
        }

        public async Task<List<Planes>> ListAllPlanes()
        {
            List<Planes> PlaneList = new List<Planes>();
            string url = baseUrl + "/listPlanes";
            try
            {
                HttpResponseMessage response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                PlaneList = JsonSerializer.Deserialize<List<Planes>>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return PlaneList;
        }

        public async Task<Message> CreatePilot(int LicenseID, string nev, DateOnly SzuletesiDatum, int RepuloOrak)
        {
            Message msg = new Message();
            string url = baseUrl + "/createPilot";
            try
            {
                var JsonData = new
                {
                    LicenseID = LicenseID,
                    nev = nev,
                    SzuletesiDatum = SzuletesiDatum,
                    RepuloOrak = RepuloOrak
                };
                string JsonString = JsonSerializer.Serialize(JsonData);
                HttpContent content = new StringContent(JsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                msg = JsonSerializer.Deserialize<Message>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return msg;
        }

        public async Task<Message> CreatePlane(string Tipus, int RepuloOrak, DateOnly GyartasiDatum, int SzallithaoSuly)
        {
            Message msg = new Message();
            string url = baseUrl + "/createPlane";
            try
            {
                var JsonData = new
                {
                    Tipus = Tipus,
                    RepuloOrak = RepuloOrak,
                    GyartasiDatum = GyartasiDatum,
                    SzallithaoSuly = SzallithaoSuly
                };
                string JsonString = JsonSerializer.Serialize(JsonData);
                HttpContent content = new StringContent(JsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                msg = JsonSerializer.Deserialize<Message>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return msg;
        }

        public async Task<Message> DeletePilot()
        {
            Message msg = new Message();
            string url = baseUrl + "/deletePilot";
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
                msg = JsonSerializer.Deserialize<Message>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return msg;
        }

        public async Task<Message> DeletePlane()
        {
            Message msg = new Message();
            string url = baseUrl + "/deletePlane";
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
                msg = JsonSerializer.Deserialize<Message>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return msg;
        }

        public async Task<Message> FlyCalc(int weight, int hour, int planeID, int pilotID)
        {
            Message msg = new Message();
            string url = baseUrl + "/repules";
            try
            {
                var JsonData = new
                {
                    weight = weight,
                    hour = hour,
                    planeID = planeID,
                    pilotID = pilotID
                };
                string JsonString = JsonSerializer.Serialize(JsonData);
                HttpContent content = new StringContent(JsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync(url, content);
                response.EnsureSuccessStatusCode();
                msg = JsonSerializer.Deserialize<Message>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return msg;
        }

        public async Task<Planes>FindPlane(string typeValue, int hourFrom, int hourTo, int weightFrom, int weightTo, DateOnly dateFrom, DateOnly dateTo)
        {
            Planes Plane = new Planes();
            string url = baseUrl + "/planeFind";
            try
            {
                var JsonData = new
                {
                    typeValue = typeValue,
                    hourFrom = hourFrom,
                    hourTo = hourTo,
                    weightFrom = weightFrom,
                    weightTo = weightTo,
                    dateFrom = dateFrom,
                    dateTo = dateTo
                };
                string JsonString = JsonSerializer.Serialize(JsonData);
                HttpContent content = new StringContent(JsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                Plane = JsonSerializer.Deserialize<Planes>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Plane;
        }
    }
}
