using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Configuration;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Collections.Generic;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Policy;

namespace Vision_API
{

    public partial class Form1 : Form
    {
        string PersonGrpID = "";
        Globals gbl = new Globals();

        public Form1()
        {
            InitializeComponent();
        }

        public static IConfiguration Configuration { get; set; }
        private async void Form1_Load(object sender, EventArgs e)
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();

            // Accessing values from appsettings.json
            var EndPoint = Configuration["CognitiveServicesEndpoint"];
            var APIKey = Configuration["CognitiveServiceKey"];
            var ImageURL = Configuration["ImageURL"];
            gbl.imagepath = ImageURL;
            gbl.Endpoint = EndPoint;
            gbl.APIKey = APIKey;

            var pg = await CreatePersonGroupAsync(EndPoint, APIKey);
            lblPersonGroup.Text = pg;
            gbl.PersonGroupID = pg;
            PopulateBlobs(listBox1);
        }
        public static void PopulateBlobs(ListBox cnt)
        {
            string connectionstring = "DefaultEndpointsProtocol=https;AccountName=johngalvision01;AccountKey=5lJe/2IZ1bQ8jMk5QxNwRHP6vFbgf2bhh/om9Ic3jCLy/21A9AitxZZQlo9CGgvvVznQlH4uinjl+AStwimtaQ==;EndpointSuffix=core.windows.net";
            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionstring);

            // Get the container client object
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("pictures");

            // List all blobs in the container
            foreach (BlobItem blobItem in containerClient.GetBlobs())
            {
                cnt.Items.Add(blobItem.Name);

            }
        }
        public static async Task<string> CreatePersonGroupAsync(string endpoint, string apikey)
        {
            string personGroupId = Guid.NewGuid().ToString();
            pg_content content = new pg_content();
            string sbody = JsonConvert.SerializeObject(content);

            // Create a string content with default UTF-8 encoding and text/plain media type
            var content1 = new StringContent(sbody, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apikey);

            var result = await client.PutAsync(endpoint + "face/v1.0/persongroups/" + personGroupId, content1);
            return personGroupId;
        }

        private void txtFace_TextChanged(object sender, EventArgs e)
        {
            cmdAddPerson.Enabled = (txtFace.TextLength > 0);
        }

        private void cmdAddFace_Click(object sender, EventArgs e)
        {
            DataItem f = (DataItem)lstPeople.SelectedItem;


            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", gbl.APIKey);
            string uri = gbl.Endpoint + "face/v1.0/persongroups/" + gbl.PersonGroupID + "/persons/" + f.Id + "/persistedfaces?url=";


            face content = new face();
            content.URL = gbl.imagepath + listBox1.SelectedItem;
            string sbody = JsonConvert.SerializeObject(content);

            // Create a string content with default UTF-8 encoding and text/plain media type
            var content1 = new StringContent(sbody, Encoding.UTF8, "application/json");


            var result = client.PostAsync(uri, content1).Result;

            if (result.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show(f.Name + " Sucessfully trained.");
            }

        }
        public async void AddPerson(string pg, string s_person)
        {
            var EndPoint = Configuration["CognitiveServicesEndpoint"];
            var APIKey = Configuration["CognitiveServiceKey"];

            ///face/v1.0/persongroups/abc/persons
            ///
            person_content content = new person_content();
            content.name = s_person;
            string sbody = JsonConvert.SerializeObject(content);

            // Create a string content with default UTF-8 encoding and text/plain media type
            var content1 = new StringContent(sbody, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIKey);
            string uri = EndPoint + "face/v1.0/persongroups/";
            uri = uri + pg + "/persons";
            // + "/persons/";
            var result = await client.PostAsync(uri, content1);

            PopulatePersons(pg, lstPeople);

        }
        public void PopulatePersons(string pg, ListBox cnt)
        {
            var EndPoint = Configuration["CognitiveServicesEndpoint"];
            var APIKey = Configuration["CognitiveServiceKey"];

            // GET {Endpoint}/face/v1.0/persongroups/{personGroupId}/persons
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIKey);
            string uri = EndPoint + "face/v1.0/persongroups/" + pg + "/persons";

            var result = client.GetStringAsync(uri).Result;
            var people = JsonConvert.DeserializeObject<List<person_content>>(result);
            cnt.Items.Clear();

            foreach (var person in people)
            {

                cnt.Items.Add(new DataItem { Id = person.personid, Name = person.name });
            }

            cnt.DisplayMember = "Name";
            cnt.ValueMember = "Id";
            cnt.Refresh();

        }
        private void cmdAddPerson_Click(object sender, EventArgs e)
        {
            AddPerson(lblPersonGroup.Text, txtFace.Text);
        }

        private void lstPeople_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmdAddFace.Enabled = (listBox1.SelectedIndex > -1 && lstPeople.SelectedIndex > -1);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmdAddFace.Enabled = (listBox1.SelectedIndex > -1 && lstPeople.SelectedIndex > -1);
            pictureBox1.ImageLocation = gbl.imagepath + listBox1.SelectedItem;
        }

        private void cmdAnalyze_Click(object sender, EventArgs e)
        {
            AnalyzeImage();
        }

        public void AnalyzeImage()
        {
            face pic = new face();
            pic.URL = gbl.imagepath + listBox1.SelectedItem;
            string sbody = JsonConvert.SerializeObject(pic);
            // Create a string content with default UTF-8 encoding and text/plain media type
            var content1 = new StringContent(sbody, Encoding.UTF8, "application/json");


            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", gbl.APIKey);
            string uri = gbl.Endpoint + "face/v1.0/persongroups/" + gbl.PersonGroupID + "/persons";

            var result = client.GetStringAsync(uri).Result;
            var KnownFaces = JsonConvert.DeserializeObject<List<Person>>(result);

            //  face/v1.0/detect[?returnFaceId]

            using var client1 = new HttpClient();
            client1.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", gbl.APIKey);
            
            
            string uri1 = gbl.Endpoint + "face/v1.0/identify";
            Detect_content content = new Detect_content();
            content.PersonGroup = gbl.PersonGroupID;
            Array.Resize(ref content.faceids,KnownFaces.Count);
            int i=0;
            foreach(var f in KnownFaces)
            {

                content.faceids[i] = f.personId;
                i++;

            }

            var content2 = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            var result1 = client1.PostAsync(uri1, content2).Result;
           // var KnownFaces = JsonConvert.DeserializeObject<List<person_content>>(result);


        }
    }
}
