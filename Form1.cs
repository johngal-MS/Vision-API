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
using System;

using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System.Xml;
using System.Security.AccessControl;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;



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
            textBox1.Text = pg;
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

            lblStatus.Text = "Adding Face";
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", gbl.APIKey);
            string uri = gbl.Endpoint + "face/v1.0/persongroups/" + gbl.PersonGroupID + "/persons/" + f.Id + "/persistedfaces?+&detectionModel=detection_03";


            face content = new face();
            content.URL = gbl.imagepath + listBox1.SelectedItem;
            string sbody = JsonConvert.SerializeObject(content);

            // Create a string content with default UTF-8 encoding and text/plain media type
            var content1 = new StringContent(sbody, Encoding.UTF8, "application/json");


            var result = client.PostAsync(uri, content1).Result;
            
            TrainModel(gbl.PersonGroupID);
            lblStatus.Text = "Face Added and Trrained.";
        }
        public void TrainModel(string PersonGroup)
        {
            var EndPoint = Configuration["CognitiveServicesEndpoint"];
            var APIKey = Configuration["CognitiveServiceKey"];
            using var client = new HttpClient();
            string uri = "https://johngal-aiservices-01.cognitiveservices.azure.com/face/v1.0/persongroups/" + PersonGroup + "/train";
            StringContent content = new StringContent("", Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", gbl.APIKey);

            var result = client.PostAsync(uri, content).Result;

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

        

        private void button1_Click(object sender, EventArgs e)
        {
            lblStatus.Text="Analyzing Image...";
            IdentifyPeople();
            lblStatus.Text = "Ready";
        }
        public async void IdentifyPeople()
        {
            face pic = new face();
            pic.URL = gbl.imagepath + listBox1.SelectedItem;
            string sbody = JsonConvert.SerializeObject(pic);
            // Create a string content with default UTF-8 encoding and text/plain media type
            var content1 = new StringContent(sbody, Encoding.UTF8, "application/json");


            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", gbl.APIKey);

            string uri = gbl.Endpoint + "face/v1.0/detect" + "?returnFaceId=true&recognitionModel=recognition_03";
            var content = new StringContent(JsonConvert.SerializeObject(pic), Encoding.UTF8, "application/json");
            var result = client.PostAsync(uri, content).Result;
            var id=result.Content.ReadAsStringAsync().Result;

            //resp_face[] id1 = new IdentifyPerson();
            //resp_face id1 = JsonConvert.DeserializeObject<resp_face>(id);

            var id1 = JsonConvert.DeserializeObject<List<resp_face>>(id);


            uri = gbl.Endpoint + "face/v1.0/identify";
            //client.DefaultRequestHeaders.Add(content-Type)
            Detect_content DetectContent = new Detect_content();
            DetectContent.PersonGroupId = gbl.PersonGroupID;
            //DetectContent.faceids.
            DetectContent.maxNumOfCandidatesReturned = 1;
            DetectContent.confidenceThreshold = .5;


            int i = 0;
           Array.Resize(ref DetectContent.faceids, id1.Count);
            foreach (var item in id1)
            {
                DetectContent.faceids[i] = item.faceid;
                i++;
            }

            uri = gbl.Endpoint + "face/v1.0/identify";
            using var client2 = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", gbl.APIKey);

            string uri2 = gbl.Endpoint + "face/v1.0/identify";
            var content2 = new StringContent(JsonConvert.SerializeObject(DetectContent), Encoding.UTF8, "application/json");
            var result2 = client.PostAsync(uri2, content2).Result;
            var id2 = result2.Content.ReadAsStringAsync().Result;
            if (result2.IsSuccessStatusCode)
            {
                var x = JsonConvert.DeserializeObject<List<IdentifyPerson>>(id2);
                int peoplefound = GetPeopleNames(x);
                string message = x.Count().ToString() + " People found.\n" + peoplefound.ToString() + " of the " + x.Count().ToString() + " Identified\n";
                int k = 0;
                for (k = 0; k < x.Count; k++)
                {
                    try
                    {
                        message = message +  x[k].candidates[0].name +" identified with confidence "+ string.Format("{0:0.00}", x[k].candidates[0].confidence*100) + "%\n";
                    }
                    catch
                    {

                    }
                }
                MessageBox.Show(message);

            }
            else
            {
                MessageBox.Show(id2);
            }
            
        }

        private int GetPeopleNames(List<IdentifyPerson> People)
        {
            int i = 0;
            int k = 0;
            foreach(var person in People)
            {
                try
                {
                    person.candidates[0].name = GetNameFromPersonID(person.candidates[0].personid);
                    i++;
                }
                catch(Exception ex)
                {
                    //person.candidates[i].name = "Unidentified";
                }
                
                
            }

            return i;

        }
        public string GetNameFromPersonID(string personid)
        {
            string ret="";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", gbl.APIKey);
            string uri = gbl.Endpoint + "face/v1.0/persongroups/" + gbl.PersonGroupID + "/persons/"+personid;
            var result = client.GetStringAsync(uri).Result;
            var person = JsonConvert.DeserializeObject<Person>(result);
            if (person != null)
            {
                ret = person.name;
                if (ret.Length == 0) ret = "Unknown";
            }
            return ret;

        }


    }
}


