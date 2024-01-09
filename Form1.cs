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


using System.Xml;
using System.Security.AccessControl;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Drawing.Drawing2D;
using static System.Windows.Forms.DataFormats;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace Vision_API
{

    public partial class Form1 : Form
    {
        string PersonGrpID = "";
        Globals gbl = new Globals();
        List<PersistedFace> faces = new List<PersistedFace>();
        List<PersonGroup> pglist = new List<PersonGroup>();
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
            gbl.RecognitionModel = Configuration["RecognitionModel"];

            PopulatePersonGroups(lstPeronGroups);


        }
        public static void PopulateBlobs(string ImageFolder, ListBox cnt)
        {
            cnt.Items.Clear();
            string connectionstring = "DefaultEndpointsProtocol=https;AccountName=johngalvision01;AccountKey=5lJe/2IZ1bQ8jMk5QxNwRHP6vFbgf2bhh/om9Ic3jCLy/21A9AitxZZQlo9CGgvvVznQlH4uinjl+AStwimtaQ==;EndpointSuffix=core.windows.net";
            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionstring);

            // Get the container client object
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ImageFolder);
            try
            {
                // List all blobs in the container
                foreach (BlobItem blobItem in containerClient.GetBlobs())
                {
                    cnt.Items.Add(blobItem.Name);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Unable to access configured image container: " + blobServiceClient.Uri+ ImageFolder);
            }



        }
        public async void DeletePersonGroup(string PersonGroupId, string endpoint, string apikey)
        {
            string personGroupName = Name; // 
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apikey);

            var result = await client.DeleteAsync(endpoint + "face/v1.0/persongroups/" + PersonGroupId);
            await PopulatePersonGroups(lstPeronGroups);

        }
        public async void CreatePersonGroup(string Name, string endpoint, string apikey, string Images)
        {
            string personGroupName = Name; // 

            pg_content content = new pg_content();
            content.recognitionModel = gbl.RecognitionModel;
            content.name = Name;
            content.userData = Images;
            string sbody = JsonConvert.SerializeObject(content);

            // Create a string content with default UTF-8 encoding and text/plain media type
            var content1 = new StringContent(sbody, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apikey);

            var result = await client.PutAsync(endpoint + "face/v1.0/persongroups/" + personGroupName, content1);

        }

        private void txtFace_TextChanged(object sender, EventArgs e)
        {
            cmdAddPerson.Enabled = (txtFace.TextLength > 0);
        }
        private async void CreateFaceList(string faceid, string rec_model)
        {
            pg_content body = new pg_content();
            body.name = faceid;
            body.recognitionModel = rec_model;
            body.userData = "";
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", gbl.APIKey);
            string uri = gbl.Endpoint + "face/v1.0/facelists/" + faceid;
            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            var result = await client.PutAsync(uri, content);
        }
        private void cmdAddFace_Click(object sender, EventArgs e)
        {
            DataItem f = (DataItem)lstPeople.SelectedItem;

            CreateFaceList(f.Id, gbl.RecognitionModel);
            lblStatus.Text = "Adding Face";
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", gbl.APIKey);
            string uri = gbl.Endpoint + "face/v1.0/persongroups/" + gbl.PersonGroupID + "/persons/" + f.Id + "/persistedfaces?+&detectionModel=detection_01";


            face content = new face();
            content.URL = gbl.imagepath + gbl.PictureFolder + "/" + listBox1.SelectedItem;
            string sbody = JsonConvert.SerializeObject(content);

            // Create a string content with default UTF-8 encoding and text/plain media type
            var content1 = new StringContent(sbody, Encoding.UTF8, "application/json");
            var result = client.PostAsync(uri, content1).Result;
            var pFace = result.Content.ReadAsStringAsync().Result;

            var temp = JsonConvert.DeserializeObject<PersistedFace>(pFace);
            temp.PersonName = lstPeople.Text;
            faces.Add(temp);
            TrainModel(gbl.PersonGroupID);
            lblStatus.Text = "Face Added and Trained.";
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
            txtAnalysis.Text = "";
            cmdAddFace.Enabled = (listBox1.SelectedIndex > -1 && lstPeople.SelectedIndex > -1);
            pictureBox1.ImageLocation = gbl.imagepath + gbl.PictureFolder + "/" + listBox1.SelectedItem;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Analyzing Image...";
            IdentifyPeople();
            lblStatus.Text = "Ready";
        }
        public async void IdentifyPeople()
        {
            face pic = new face();
            pic.URL = gbl.imagepath + gbl.PictureFolder + "/" + listBox1.SelectedItem;
            string sbody = JsonConvert.SerializeObject(pic);
            // Create a string content with default UTF-8 encoding and text/plain media type
            var content1 = new StringContent(sbody, Encoding.UTF8, "application/json");


            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", gbl.APIKey);

            string uri = gbl.Endpoint + "face/v1.0/detect" + "?returnFaceId=true&recognitionModel=" + gbl.RecognitionModel;
            var content = new StringContent(JsonConvert.SerializeObject(pic), Encoding.UTF8, "application/json");
            var result = client.PostAsync(uri, content).Result;
            var id = result.Content.ReadAsStringAsync().Result;

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

            using var client2 = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", gbl.APIKey);

            string uri2 = gbl.Endpoint + "face/v1.0/identify?" + "recognitionModel=" + gbl.RecognitionModel;
            var content2 = new StringContent(JsonConvert.SerializeObject(DetectContent), Encoding.UTF8, "application/json");
            var result2 = client.PostAsync(uri2, content2).Result;
            var id2 = result2.Content.ReadAsStringAsync().Result;
            if (result2.IsSuccessStatusCode)
            {
                var x = JsonConvert.DeserializeObject<List<IdentifyPerson>>(id2);
                int peoplefound = GetPeopleNames(x);
                string message = x.Count().ToString() + " People found.\r\n" + peoplefound.ToString() + " of the " + x.Count().ToString() + " Identified\r\n";
                int k = 0;
                for (k = 0; k < x.Count; k++)
                {
                    try
                    {
                        message = message + x[k].candidates[0].name + " identified with confidence " + string.Format("{0:0.00}", x[k].candidates[0].confidence * 100) + "%\r\n";
                    }
                    catch
                    {

                    }
                }
                MarkUpPicture(x, id1, pictureBox1);
                txtAnalysis.Text = message;
            }
            else
            {
                MessageBox.Show(id2);
            }

        }
        private void MarkUpPicture(List<IdentifyPerson> People, List<resp_face> rect, PictureBox pic)
        {
            string name = "";
            int i = 0;
            Image img = pic.Image;
            Graphics g = Graphics.FromImage(img);
            foreach (var r in rect) 
            {
                Pen pen = new Pen(Color.White, 4);
                pen.Alignment = PenAlignment.Inset;
                Font myfont = new Font("Arial", g.VisibleClipBounds.Width / 30); // size the font to the image size
                try { if (People[i].candidates[0].name != null) name = People[i].candidates[0].name; }
                catch { name = ""; }

                Rectangle box = new Rectangle(r.faceRectangle.left, r.faceRectangle.top, r.faceRectangle.width, r.faceRectangle.height);
                g.DrawRectangle(pen, box);
                g.DrawString(name, myfont, Brushes.White, new Point(r.faceRectangle.left, r.faceRectangle.top + r.faceRectangle.height));
                i++;
            }
            img.Save("temp.Jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            pic.ImageLocation = "temp.Jpeg";
        }

        private int GetPeopleNames(List<IdentifyPerson> People)
        {
            int i = 0;
            int k = 0;
            foreach (var person in People)
            {

                try
                {
                    person.candidates[0].name = GetNameFromPersonID(person.candidates[0].personid);
                    i++;
                }
                catch (Exception ex)
                {
                    //person.candidates[i].name = "Unidentified";
                }


            }

            return i;

        }
        public string GetNameFromPersonID(string personid)
        {
            string ret = "";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", gbl.APIKey);
            string uri = gbl.Endpoint + "face/v1.0/persongroups/" + gbl.PersonGroupID + "/persons/" + personid;
            var result = client.GetStringAsync(uri).Result;
            var person = JsonConvert.DeserializeObject<Person>(result);
            if (person != null)
            {
                ret = person.name;
                if (ret.Length == 0) ret = "";
            }
            return ret;

        }


        private async Task<List<PersonGroup>> ListPersonGroup(string ApiKey, string Endpoint)
        {
            string[] ret = new string[5];
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ApiKey);
            string uri = Endpoint + "face/v1.0/persongroups?returnRecognitionModel=true";
            var result = await client.GetStringAsync(uri);
            var pGroup = JsonConvert.DeserializeObject<List<PersonGroup>>(result);



            return pGroup;

        }
        private void lstPeronGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPersonGroup.Text = "";
            string userData = "";
            string personid = "";
            foreach (PersonGroup grp in pglist)
            {
                if (lstPeronGroups.SelectedItem == grp.name)
                {
                    personid = grp.personGroupId;
                    lblPersonGroup.Text = personid;
                    userData = grp.userData;
                    gbl.PictureFolder = grp.userData;
                    gbl.PersonGroupID = personid;
                    comboBox1.Text=grp.recognitionModel;
                    txtPersonGroup.Text=grp.name;
                    txtImages.Text = grp.userData;
                    break;
                }
            }
            if (lblPersonGroup.Text.Length > 0)
            {
                lblPersonGroup.Text = personid;
                PopulateBlobs(userData, listBox1);
                PopulatePersons(personid, lstPeople);
            }



        }

        private void txtPersonGroup_TextChanged(object sender, EventArgs e)
        {
            cmdAddPersonGroup.Enabled = (txtPersonGroup.Text.Length > 0 && txtImages.Text.Length > 0 && comboBox1.SelectedIndex >= -1);
        }

        private async void cmdAddPersonGroup_Click(object sender, EventArgs e)
        {
            CreatePersonGroup(txtPersonGroup.Text, gbl.Endpoint, gbl.APIKey, txtImages.Text);
            System.Threading.Thread.Sleep(1000);
            pglist = await ListPersonGroup(gbl.APIKey, gbl.Endpoint);
            lstPeronGroups.Items.Clear();
            //lstPeronGroups.DataSource = pglist;
            foreach (PersonGroup pg in pglist)
            {
                lstPeronGroups.Items.Add(pg.name);
            }

        }
        private async Task PopulatePersonGroups(ListBox cnt)
        {
            pglist = await ListPersonGroup(gbl.APIKey, gbl.Endpoint);
            lstPeronGroups.Items.Clear();
            //lstPeronGroups.DataSource = pglist;
            foreach (PersonGroup pg in pglist)
            {
                lstPeronGroups.Items.Add(pg.name);
            }
            //return true;
        }
        private void txtImages_TextChanged(object sender, EventArgs e)
        {
            cmdAddPersonGroup.Enabled = (txtPersonGroup.Text.Length > 0 && txtImages.Text.Length > 0 && comboBox1.SelectedIndex >= -1);
        }

        private void lstPeronGroups_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void lstPeronGroups_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46)
            {
                int i = lstPeronGroups.SelectedIndex;
                DeletePersonGroup(lblPersonGroup.Text, gbl.Endpoint, gbl.APIKey);



            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
                    }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            gbl.RecognitionModel = comboBox1.Text;
            cmdAddPersonGroup.Enabled = (txtPersonGroup.Text.Length > 0 && txtImages.Text.Length > 0 && comboBox1.SelectedIndex >= -1);
        }
    }
}


