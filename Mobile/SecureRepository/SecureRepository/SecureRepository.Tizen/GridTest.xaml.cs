using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.Security.SecureRepository;
using Tizen;

namespace SecureRepository.Tizen
{
    /// <summary>
    /// Class responsible for UI 
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private double width;
        private double height;
        private Data data;
        private Keys keys;
        private Certificates certs;
        public List<PageTypeGroup> Groups;
        public MainPage()
        {
            data = new Data();
            keys = new Keys();
            certs = new Certificates();
            InitializeComponent();

            listView.ItemsSource = new string[] { };
            var template = new DataTemplate(typeof(TextCell));
            template.SetBinding(TextCell.TextProperty, "Alias");
            listView.ItemTemplate = template;
            ListItems();
        }
        /// <summary>
        /// Veryfies certificates and adds them to CertificateManager
        /// </summary>
        void AddCertificates()
        {
            //Certificates used in this example are from tizen.org. They are valid from 18 May 2017 till 18 July 2018 
            // So when running this app change your date to fit between this dates, otherwise You will get error that certificate is not valid 
            string chain_path = Tizen.Program.Current.DirectoryInfo.SharedResource + "tizen_pem_chain.crt";
            string path = Tizen.Program.Current.DirectoryInfo.SharedResource + "tizen_pem.crt";
            byte[] b = Encoding.UTF8.GetBytes("TESRASDAS");

            if (Data.CheckPath(path) && Data.CheckPath(chain_path))
            {
                IEnumerable<Certificate> list = null;
                list = certs.GetCertificates(chain_path);

                if (list != null)
                {
                    //Saves first certificate
                    if (certs.IsTrusted(list.First(), list)) //Checks if Certificate chain is correct  
                        certs.Save("tizen.org_cert", path); //Saves single certificate 
                    else
                    {
                        appInfo.Text += "Certificate is NOT TRUSTED so app will not save it inside CertificateManager"; //Displays info that Certificate chain is incorrect
                    }

                }
            }
        }
        /// <summary>
        /// Adds data (user defined strings) to DataManager
        /// </summary>
        void AddData()
        {
            string testData = "TEST12321312dsajdas";
            string anotherData = "new_data_with_utf8_string_ĄĘŹŻŁć_서울"; //Another data to show that you can use UTF8 encoded data
            //data.RemoveAll(); //Use it to manually remove all data from DataManager
            data.Add(Encoding.UTF8.GetBytes(testData), "my_data", null);
            data.Add(Encoding.UTF8.GetBytes(anotherData), "my_data", null); //Will not succeed, because data is already added to this alias
            data.Add(Encoding.UTF8.GetBytes(anotherData), "my_data_2", null); // this will succeed, because there is no data under "my_data_2" alias
        }
        /// <summary>
        /// Adds Aes / Rsa Keys to KeyManager
        /// </summary>
        void AddKeys()
        {

            //keys.RemoveAll(); // KeyManager has previously keys even after reboot / recompile etc.
            //In normal app we don`t use this Method 
            keys.AddAesKey("my_aes");
            keys.AddPrivateRsaKey("my_private_rsa");
            keys.AddPublicRsaKey("my_public_rsa");
            keys.CreateRsaKeyPair("private_auto_rsa", "public_auto_rsa");
        }
        /// <summary>
        /// Gets content of selected item from listView
        /// </summary>
        void GetItemContent()
        {

            string type = null, prefix = null;
            PageModel selectedItem = null;
            try //Gets selected item 
            {
                if (listView.SelectedItem != null)
                    selectedItem = listView.SelectedItem as PageModel;
                else
                    return;
            }
            catch //Error trying to get selected item from listVieW e.g. no item selected 
            {
                return;
            }
            if (selectedItem == null) //Exits if no item is selected
                return;
            try
            {//Gets item Type and it`s content prefix
                switch (selectedItem.Type)
                {
                    case AliasType.Certificate:
                        type = certs.GetType(selectedItem.Alias);
                        prefix = certs.GetContentPrefix(selectedItem.Alias);
                        break;
                    case AliasType.Data:
                        type = data.GetType(); //data type is always byte[], so there is no need to pass any arguments to it
                        prefix = data.GetContentPrefix(selectedItem.Alias);
                        break;
                    case AliasType.Key:
                        type = keys.GetType(selectedItem.Alias);
                        prefix = keys.GetContentPrefix(selectedItem.Alias);
                        break;
                }
            }
            catch (Exception ex) //Unable to get Data,Certificate,Key related to that alias
            {
                Log.Error("SecureRepository_UI", "Unable to get item data or prefix");
                Log.Error("SecureRepository_UI", ex.Message);
            }
            appInfo.Text = "Item type: " + type;
            contentPrefix.Text = "Item prefix: " + prefix;
        }
        /// <summary>
        /// Adjusts margins of labels and buttons
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        private void GridBtnSizeChanged(object sender, EventArgs e) //Changes buttons size to fit current UI
        {
            double margin, marginLeftRight;
            if (gridBtn.Children.Count > 0) //Horizontal View
            {
                margin = gridBtn.Height * 0.02;
                marginLeftRight = gridBtn.Width * 0.15;
            }
            else //Vertical Views
            {
                margin = optionsGrid.Height * 0.02;
                marginLeftRight = optionsGrid.Height * 0.15;
            }
            //Adjusts buttons margins
            btnAdd.Margin = new Thickness(marginLeftRight, margin, marginLeftRight, margin);
            btnRemove.Margin = new Thickness(marginLeftRight, margin, marginLeftRight, margin);
            btnEncrypt.Margin = new Thickness(marginLeftRight, margin, marginLeftRight, margin);

            margin = appInfo.Height * 0.02;
            marginLeftRight = appInfo.Width * 0.02;
            //Adjusts labels margins
            appInfo.Margin = new Thickness(marginLeftRight, margin, marginLeftRight, margin);
            contentPrefix.Margin = new Thickness(marginLeftRight, margin, marginLeftRight, margin);
        }
        /// <summary>
        /// Adds avaliable aliases from all Managers to listView, Pkcs12 not included (it`s not part of this tutorial)
        /// </summary>
        void ListItems()
        {
            IEnumerable<string> dataAliases, keyAliases, certificateAliases;
            keyAliases = keys.GetAliasesShort(); //Gets aliases from KeyManager without App.Name prefix
            dataAliases = data.GetAliasesShort(); //Gets aliases from DataManager without App.Name prefix
            certificateAliases = certs.GetAliasesShort(); //Gets aliases from CertificateManager without App.Name prefix
            List<PageTypeGroup> groups = new List<PageTypeGroup>();
            int GroupCount = 0;
            if (dataAliases != null)
            {
                groups.Add(new PageTypeGroup(AliasType.Data));
                foreach (string alias in dataAliases) //Adds aliases to the list 
                {
                    groups[GroupCount].Add(new PageModel(alias, AliasType.Data));
                }
                GroupCount++;
            }
            if (keyAliases != null)
            {
                groups.Add(new PageTypeGroup(AliasType.Key));
                foreach (string alias in keyAliases) //Adds aliases to the list 
                {
                    groups[GroupCount].Add(new PageModel(alias, AliasType.Key));
                }
                GroupCount++;
            }
            if (certificateAliases != null)
            {
                groups.Add(new PageTypeGroup(AliasType.Certificate));
                foreach (string alias in certificateAliases) //Adds aliases to the list 
                {
                    groups[GroupCount].Add(new PageModel(alias, AliasType.Certificate));
                }
                GroupCount++;
            }
            Groups = groups;
            listView.ItemsSource = Groups;
            //listView.ItemsSource = list; //Adds aliases list to the listView

        }
        void OnAddKeyClicked(object sender, EventArgs e) //Adds items to *Manager
        {
            AddCertificates(); //Adds certificates to CertificateManager
            AddKeys(); //Adds Keys to KeyManager
            AddData(); //Adds Data to DataManager
            ListItems(); //Shows changes to User -- adds items to listView
        }
        /// <summary>
        /// Encrypyt, Decrypt data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnEncKeyClicked(object sender, EventArgs e)
        {
            string textToEncrypt = "string1_ĄĘŹŻŁć_서울", encText;
            Cryptography c = new Cryptography();

            byte[] b = c.Encrypt(System.Text.Encoding.UTF8.GetBytes(textToEncrypt)); //Encrypys string

            if (b != null)
            {
                encText = "Encrypted:" + Cryptography.ByteArrayToHexViaLookup32(b); //Converts byte[] to hexadecimal string for reading only, in real app just use byte[]
                string decText;
                byte[] decBin = c.Decrypt(b);
                if (decBin != null)
                    decText = "Orginal text: " + textToEncrypt + "\nDecrypted:    " + System.Text.Encoding.UTF8.GetString(decBin); //Displays decrypted text
                else
                    decText = "Unable To decrypt text";
                contentPrefix.Text = decText;
            }
            else
                encText = "Unable To encrypt text";
            appInfo.Text = encText;
            ListItems();
        }
        /// <summary>
        /// Removes all items from Managers and cleans up UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnRemoveKeyClicked(object sender, EventArgs e)
        {
            keys.RemoveAll(); //Removes all keys from KeyManager
            data.RemoveAll(); //Removes all data from DataManager
            certs.RemoveAll(); //Removes all certyficates from CertificateManager
            appInfo.Text = "";
            contentPrefix.Text = "";
            ListItems(); //Updates list (it will be empty)
        }
        /// <summary>
        /// Handles display orientation change (horizontal, vertical)
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width != this.width || height != this.height) // width or height has changed
            {
                this.width = width;
                this.height = height;

                MainGrid.RowDefinitions.Clear();
                MainGrid.ColumnDefinitions.Clear();
                MainGrid.Children.Clear();

                optionsGrid.RowDefinitions.Clear();
                optionsGrid.ColumnDefinitions.Clear();
                optionsGrid.Children.Clear();

                if (width > height) //Realigns UI for vertical mode
                { 

                    MainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.6, GridUnitType.Star) });
                    MainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                    optionsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    optionsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    optionsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    optionsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    optionsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

                    optionsGrid.Children.Add(appInfo, 0, 0);
                    optionsGrid.Children.Add(contentPrefix, 0, 1);
                    optionsGrid.Children.Add(btnAdd, 0, 2);
                    optionsGrid.Children.Add(btnRemove, 0, 3);
                    optionsGrid.Children.Add(btnEncrypt, 0, 4);

                    MainGrid.Children.Add(listGrid, 0, 0);
                    MainGrid.Children.Add(optionsGrid, 1, 0);
                }
                else //Realigns UI for horizontal mode
                {
                    labelGrid.RowDefinitions.Clear();
                    labelGrid.ColumnDefinitions.Clear();
                    labelGrid.Children.Clear();

                    gridBtn.RowDefinitions.Clear();
                    gridBtn.ColumnDefinitions.Clear();
                    gridBtn.Children.Clear();

                    MainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1.6, GridUnitType.Star) });
                    MainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

                    optionsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.6, GridUnitType.Star) });
                    optionsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                    labelGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    labelGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

                    gridBtn.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    gridBtn.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    gridBtn.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

                    labelGrid.Children.Add(appInfo, 0, 0);
                    labelGrid.Children.Add(contentPrefix, 0, 1);

                    gridBtn.Children.Add(btnAdd, 0, 0);
                    gridBtn.Children.Add(btnRemove, 0, 1);
                    gridBtn.Children.Add(btnEncrypt, 0, 2);

                    optionsGrid.Children.Add(labelGrid, 0, 0);
                    optionsGrid.Children.Add(gridBtn, 1, 0);

                    MainGrid.Children.Add(listGrid, 0, 0);
                    MainGrid.Children.Add(optionsGrid, 0, 1);
                }
                GridBtnSizeChanged(null, null); //Changes Buttons and Labels size to fit in new UI
            }
        }
    }
}