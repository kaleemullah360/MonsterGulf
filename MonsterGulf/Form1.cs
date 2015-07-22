using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;


/*
Require dependencies
Name:	ChromeDriver
ID:	WebDriver.ChromeDriver.win32
URL:	https://www.nuget.org/packages/WebDriver.ChromeDriver.win32/2.14.0

Name:	Selenium
ID:	Selenium.WebDriver
URL:	https://www.nuget.org/packages/Selenium.WebDriver/2.45.0
*/

/*well I am including over here */
using OpenQA.Selenium;
/* Chrome will do Perfect Job so adding it. :) */
using OpenQA.Selenium.Chrome;
/* Some File Writing */
using System.IO;
/* Xpath Lib */
using System.Xml.XPath;

/*
Project Home URL: http://jobsearch.monstergulf.com
Project Prececc URL: http://jobsearch.monstergulf.com/searchresult.html?loc=192&cat=22
*/
namespace MonsterGulf
{

    public partial class monstergulf_frm : Form
    {
        //user desktop path to store display files
        string path_Desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        //how many Pages ? n=1
        public Int32 totalPages_Int = 1;
        //how many results per page ? res_cnt=40
        public Int32 resultsPerPage_Int = 40;
        public String default_Url = "http://jobsearch.monstergulf.com";
        public Int32 region_Selection = 192;
        public Int32 job_type_Selection = 22;
        public string job_region_selected = "United Arab Emirates";
        public string job_type_selected = "IT";

        // well this is my main function I'll call it on Execute button press
        private void monster_gulf_grabber_function(int totalPages_Int, String default_Url)
        {

            /* initializing chrome driver */
            var chrome_Driver = ChromeDriverService.CreateDefaultService();
            //no need to show disturbing command window so set it hidden
            chrome_Driver.HideCommandPromptWindow = true;
            //try
            //{


            var job_region = job_region_selected;
            var job_industry = job_type_selected;

            //var job_ref_code = "";

            var totalPages = 0;
            /* create a csv file for output*/
            try
            {
                File.WriteAllText(Path.Combine(path_Desktop, "MonsterGulf_Scrapped_DataSet_" + job_region_selected + "_" + job_type_selected + ".csv"), "Job Title, Company, Description, Location, Experience,  Keyskills, Region, Industry, Date\r\n");
            }
            catch (Exception ex)
            {
                //incase of file is open or readonly or no permission show message
                //result_textBox.AppendText(ex.StackTrace);
                MessageBox.Show("Cannot write to file !");
            }

            //initialize chrome driver 
            var main_page_chrome_Driver_Obj = new ChromeDriver(chrome_Driver, new ChromeOptions());

            do
            {
                var job_title = "";
                var job_company = "";
                var job_desc = "";

                var job_location = "";

                var job_experience = "";
                var job_keyskils = "";
                var job_date = "";
                main_page_chrome_Driver_Obj.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
                main_page_chrome_Driver_Obj.Navigate().GoToUrl(default_Url + "/searchresult.html?rfr=;loc=" + region_Selection + ";jbc=" + job_type_Selection + ";day=60;srt=pst;ref=http:%2F%2Fjobsearch%2Emonstergulf%2Ecom%2Fsearch%2Ehtml;show_omit=1;res_cnt=" + resultsPerPage_Int + ";n=" + totalPages_Int);
                //main_page_chrome_Driver_Obj.Navigate().GoToUrl("http://jobsearch.monstergulf.com/searchresult.html?rfr=refine;day=60;srt=pst;ref=http:%2F%2Fjobsearch.monstergulf.com%2Fsearch.html;show_omit=1;res_cnt=40;n=" + totalPages_Int);
                //http://jobsearch.monstergulf.com/searchresult.html?rfr=refine;day=60;srt=pst;ref=http:%2F%2Fjobsearch.monstergulf.com%2Fsearch.html;show_omit=1;res_cnt=40;n=2
                //find main pages count.
                //select result part of DOM
                var main_page_dom = main_page_chrome_Driver_Obj.FindElementsByXPath("//*[@class='ns_sresultmain']/div[2]");
                //find main pages count.

                if (totalPages_Int > 1) { }
                else
                {
                    var main_pages_count = main_page_dom[0].FindElements(By.XPath("./div[1]/div/div[2]/div[3]/div/div/div/div/div[@class='liDiv']"));
                    totalPages = main_pages_count.Count();
                }

                //find jobs URLs in main page, it contains 40 jobs urls .
                var main_page_jobs = main_page_chrome_Driver_Obj.FindElementsByXPath("//*[@class='ns_job_wrapper']");   //it gives me 40 count i.e 40 jobs in single main page
                //
                //pages_num_textBox.AppendText(" " + main_page_jobs.Count().ToString());
                if (main_page_jobs.Count() == 40) { }
                else
                {
                    DateTime dt = DateTime.Now + TimeSpan.FromSeconds(7);
                    do
                    {
                        main_page_jobs = main_page_chrome_Driver_Obj.FindElementsByXPath("//*[@class='ns_job_wrapper']");   //it gives me 40 count i.e 40 jobs in single main page
                        //MessageBox.Show(main_page_jobs.Count().ToString());
                    } while (DateTime.Now < dt);

                }
                for (int mainPage_jobIndex = 0; mainPage_jobIndex < main_page_jobs.Count(); mainPage_jobIndex++)
                {
                    job_title = main_page_jobs[mainPage_jobIndex].FindElement(By.XPath("./div[2]/a/div[@class='ns_jobtitle ns_lt']")).Text;
                    job_company = main_page_jobs[mainPage_jobIndex].FindElement(By.XPath("./div[2]/a/div[@class='ns_cmpname ns_lt']/h2/strong")).Text;
                    job_desc = main_page_jobs[mainPage_jobIndex].FindElement(By.XPath("./div[2]/a/div[@class='ns_jobdesc ns_lt']")).Text;
                    job_desc = job_desc.Replace(System.Environment.NewLine, " ");
                    string job_location_and_experience = main_page_jobs[mainPage_jobIndex].FindElement(By.XPath("./div[2]/a/div[@class='ns_joblocation ns_lt']")).Text;
                    string s = job_location_and_experience;
                    string[] values = s.Split(',');
                    job_location = "";
                    for (int array_item = 0; array_item < values.Length - 1; array_item++)
                    {
                        job_location += values[array_item].Trim();
                    }
                    job_experience = values.Last().Trim();// the only last index of array is experience remaining index are locations

                    if (main_page_jobs[mainPage_jobIndex].FindElements(By.XPath("./div[2]/a/div[@class='ns_jobkeyskills ns_lt']")).Count() > 0)
                    {
                        job_keyskils = main_page_jobs[mainPage_jobIndex].FindElement(By.XPath("./div[2]/a/div[@class='ns_jobkeyskills ns_lt']")).GetAttribute("title");
                    }

                    //from 16th Apr 2015 => 16/4/2015
                    var job_date_before_conversion = main_page_jobs[mainPage_jobIndex].FindElement(By.XPath("./div[2]/a/div[@class='ns_jobdate ns_rt']")).Text;
                    job_date = timeStampCnversion(job_date_before_conversion);

                    /* to output the result in textbox*/
                    //Job Title, Company, Description, Location, Experience,  Keyskills, Region, Industry, Date\r\n
                    result_textBox.AppendText("\nJob Title: " + job_title.ToString().Trim() + "\nCompany: " + job_company.ToString().Trim() + "\nDescription: " + job_desc.ToString().Trim() + "\nLocation: " + job_location.ToString().Trim() + "\nExperience: " + job_experience.ToString().Trim() + "\nKeyskills: " + job_keyskils.ToString().Trim() + "\nRegion: " + job_region.ToString().Trim() + "\nIndustry: " + job_industry.ToString().Trim() + "\nDate: " + job_date.ToString().Trim() + "\r.....................................................................................................\n");
                    /* add data to above created csv file */
                    File.AppendAllText(Path.Combine(path_Desktop, "MonsterGulf_Scrapped_DataSet_" + job_region_selected + "_" + job_type_selected + ".csv"), job_title.ToString().Trim().Replace(",", "") + "," + job_company.ToString().Trim().Replace(",", "") + "," + job_desc.ToString().Trim().Replace(",", "") + "," + job_location.ToString().Trim().Replace(",", "") + "," + job_experience.ToString().Trim().Replace(",", "") + "," + job_keyskils.ToString().Trim().Replace(",", "") + "," + job_region.ToString().Trim().Replace(",", "") + "," + job_industry.ToString().Trim().Replace(",", "") + "," + job_date.ToString().Trim().Replace(",", "") + "\r\n");

                }
                totalPages_Int++;


            } while (totalPages_Int <= totalPages + 1);
            //close chrome driver/ chrome browser window
            main_page_chrome_Driver_Obj.Close();
            //relase all recources associated with selenium chrome driver

            //}
            //catch (Exception ex)
            //{
            //    result_textBox.AppendText(ex.StackTrace);
            //    MessageBox.Show("Google Chrome not found! Click OK to install it");
            //    System.Diagnostics.Process.Start("https://www.google.com/chrome/#eula");
            //    //https://www.google.com/chrome/browser/thankyou.html?standalone=1&system=true&platform=win&installdataindex=defaultbrowser
            //    //this.Close();
            //}
        }
        public monstergulf_frm()
        {
            InitializeComponent();
        }

        private void exit_app_btn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Really Quit?", "Confirm quit", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void open_folder_btn_Click(object sender, EventArgs e)
        {
            Process.Start(path_Desktop);
        }

        private void execute_btn_Click(object sender, EventArgs e)
        {
            if (pages_num_textBox.Text.Equals(""))
            {
                MessageBox.Show("Pages field cannot be empty!");
            }
            else
            {
                //url: http://jobsearch.monstergulf.com/searchresult.html?rfr=;loc=192;jbc=22;day=60;srt=pst;ref=http:%2F%2Fjobsearch%2Emonstergulf%2Ecom%2Fsearch%2Ehtml;show_omit=1;res_cnt=40;n=2
                job_type_comboBox.SelectedIndex.Equals("1");

                if (pages_num_textBox.Text.Equals("0"))
                {
                    totalPages_Int = 1;
                }
                else
                {
                    totalPages_Int = Int32.Parse(pages_num_textBox.Text);
                }
                if (web_url_textBox.Text.Equals(""))
                {

                    monster_gulf_grabber_function(totalPages_Int, default_Url);
                }
                else
                {
                    default_Url = web_url_textBox.Text;

                    monster_gulf_grabber_function(totalPages_Int, default_Url);
                }
            }
        }

        private void pages_num_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void web_url_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void result_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void job_type_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            ComboBox cmb = (ComboBox)sender;
            int selectedIndex = cmb.SelectedIndex;
            switch (selectedIndex)
            {
                /*Job Types Selectors with values*/
                case 0:
                    //Admin/Secretarial   907
                    job_type_Selection = 907;
                    job_type_selected = "Admin Secretarial";
                    break;
                case 1:
                    //Customer Service/ BPO/ KPO  3
                    job_type_Selection = 3;
                    job_type_selected = "Customer Service BPO KPO";
                    break;
                case 2:
                    //Finance & Accounts  7
                    job_type_Selection = 7;
                    job_type_selected = "Finance & Accounts";
                    break;
                case 3:
                    //HR  11
                    job_type_Selection = 11;
                    job_type_selected = "HR";
                    break;
                case 4:
                    //IT  22
                    job_type_Selection = 22;
                    job_type_selected = "IT";
                    break;
                case 5:
                    //Legal   13
                    job_type_Selection = 13;
                    job_type_selected = "Legal";
                    break;
                case 6:
                    //Marketing   14
                    job_type_Selection = 14;
                    job_type_selected = "Marketing";
                    break;
                case 7:
                    //Purchase & Supply Chain 18
                    job_type_Selection = 18;
                    job_type_selected = "Purchase & Supply Chain";
                    break;
            }//end switch

            //Console.WriteLine(selectedIndex.ToString());
            //result_textBox.AppendText(job_type_Selection.ToString());
        }

        private void region_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {


            ComboBox cmb = (ComboBox)sender;
            int selectedIndex = cmb.SelectedIndex;

            /*Location Selection (region selection) with values*/
            switch (selectedIndex)
            {
                case 0:
                    //United Arab Emirates    192
                    region_Selection = 192;
                    job_region_selected = "United Arab Emirates";
                    break;
                case 1:
                    //Saudi Arabia    182
                    region_Selection = 182;
                    job_region_selected = "Saudi Arabia";
                    break;
                case 2:
                    //Qatar   180
                    region_Selection = 180;
                    job_region_selected = "Qatar";
                    break;
                case 3:
                    //Bahrain 158
                    region_Selection = 158;
                    job_region_selected = "Bahrain";
                    break;
                case 4:
                    //Oman    178
                    region_Selection = 178;
                    job_region_selected = "Oman";
                    break;
                case 5:
                    //Kuwait  170
                    region_Selection = 170;
                    job_region_selected = "Kuwait";
                    break;
                case 6:
                    //Egypt   250
                    region_Selection = 250;
                    job_region_selected = "Egypt";
                    break;
                case 7:
                    //Iraq    486
                    region_Selection = 486;
                    job_region_selected = "Iraq";
                    break;
                case 8:
                    //Jordan  238
                    region_Selection = 238;
                    job_region_selected = "Jordan";
                    break;
                case 9:
                    //Lebanon 237
                    region_Selection = 237;
                    job_region_selected = "Jordan";
                    break;
            }//end switch
            //result_textBox.AppendText(region_Selection.ToString()); 
        }

        private void monstergulf_frm_Load(object sender, EventArgs e)
        {

        }
        public string RemoveSpecialChars(string input)
        {
            return Regex.Replace(input, @"[^0-9a-zA-Z\._]", string.Empty);
        }


        public string timeStampCnversion(string string_date)
        {
            //standard date timeStampCnversion format is 6/14/2015
            // current website date format is 16th Apr 2015
            string s = string_date;
            string[] values = s.Split(' ');
            string str_day = values[0].Trim();
            string str_month = values[1].Trim();
            string str_year = values[2].Trim();

            int int_day = 0;
            int int_month = 0;
            int int_year = int.Parse(str_year);

            string[] numbers = Regex.Split(str_day, @"\D+");
            foreach (string value in numbers)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    int_day = int.Parse(value);
                }
            }

            switch (str_month)
            {
                case "Jan":
                    int_month = 1;
                    break;
                case "Feb":
                    int_month = 2;
                    break;
                case "Mar":
                    int_month = 3;
                    break;
                case "Apr":
                    int_month = 4;
                    break;
                case "May":
                    int_month = 5;
                    break;
                case "Jun":
                    int_month = 6;
                    break;
                case "Jul":
                    int_month = 7;
                    break;
                case "Aug":
                    int_month = 8;
                    break;
                case "Sep":
                    int_month = 9;
                    break;
                case "Oct":
                    int_month = 10;
                    break;
                case "Nov":
                    int_month = 11;
                    break;
                case "Dec":
                    int_month = 12;
                    break;

            }//end switch

            DateTime date = new DateTime(int_year, int_month, int_day, 12, 0, 0, 0);
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan span = (date - epoch);
            double unixTime = span.TotalSeconds;
            return unixTime.ToString();
        }

    }
}