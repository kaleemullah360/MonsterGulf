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
		// well this is my main function I'll call it on Execute button press
		private void monster_gulf_grabber_function(int totalPages_Int, String default_Url)
		{
			int pageNumber = 1;
			/* initializing chrome driver */
			var chrome_Driver = ChromeDriverService.CreateDefaultService();
			//no need to show disturbing command window so set it hidden
			chrome_Driver.HideCommandPromptWindow = true;
			try
			{

				var job_title = "title";
				var job_company = "cmp";
				var job_desc = "desc";

				var job_location = "";
				var job_nationality = "";
				var job_experience = "";
				var job_keyskils = "";
				var job_function = "";
				var job_role = "";
				var job_industry = "";
				var job_date = "";
				//var job_ref_code = "";


				/* create a csv file for output*/
				try
				{
					File.WriteAllText(Path.Combine(path_Desktop, "Output_page_no_" + pageNumber + ".csv"), "Job Title, Company, Description, Location, Nationality, Experience,  Keyskills, Function, Roll, Industry, Date\r\n");
				}
				catch (Exception ex)
				{
					//incase of file is open or readonly or no permission show message
					result_textBox.AppendText(ex.StackTrace);
					//MessageBox.Show("Cannot write to file !");
				}

				//initialize chrome driver 
				var main_page_chrome_Driver_Obj = new ChromeDriver(chrome_Driver, new ChromeOptions());
				// goto main page (its the first page listing jobs)
				//http://jobsearch.monstergulf.com/searchresult.html?rfr=uae-jobs.html;loc=192;jbc=22;day=60;srt=rel;ref=http:%2F%2Fjobsearch.monstergulf.com%2Flocation%2Fuae-jobs.html;show_omit=1;res_cnt=40;n=11
				main_page_chrome_Driver_Obj.Navigate().GoToUrl(default_Url + "/searchresult.html?rfr=;loc=" + region_Selection + ";jbc=" + job_type_Selection + ";day=60;srt=pst;ref=http:%2F%2Fjobsearch%2Emonstergulf%2Ecom%2Fsearch%2Ehtml;show_omit=1;res_cnt=" + resultsPerPage_Int + ";n=" + totalPages_Int);
				//select result part of DOM
				var main_page_dom = main_page_chrome_Driver_Obj.FindElementsByXPath("//*[@class='ns_sresultmain']/div[2]");
				//find main pages count.
				var main_pages_count = main_page_dom[0].FindElements(By.XPath("./div[1]/div/div[2]/div[3]/div/div/div/div/div[@class='liDiv']"));
				//this loop open main page one by one. main page contains jobs lisitng subpages
				for (int main_page_number = 1; main_page_number <= main_pages_count.Count() + 1; main_page_number++)
				{
					totalPages_Int++;
					main_page_chrome_Driver_Obj.Navigate().GoToUrl(default_Url + "/searchresult.html?rfr=;loc=" + region_Selection + ";jbc=" + job_type_Selection + ";day=60;srt=pst;ref=http:%2F%2Fjobsearch%2Emonstergulf%2Ecom%2Fsearch%2Ehtml;show_omit=1;res_cnt=" + resultsPerPage_Int + ";n=" + totalPages_Int);
					//find jobs URLs in main page, it contains 40 jobs urls .
					//var main_page_urls = main_page_dom[0].FindElement(By.XPath(".//div[2]/div[2]/a")).GetAttribute("href");   //it gives me 237 count leave it
					var main_page_urls = main_page_chrome_Driver_Obj.FindElementsByXPath("//a[@class='ns_joblink']");   //it gives me 40 count i.e 40 jobs in single main page
					//loop for each job sub page
					for (int sub_page_url = 2; sub_page_url <= main_page_urls.Count() + 2; sub_page_url++)
					{
						var job_page_url = main_page_urls[sub_page_url].FindElement(By.XPath("//div[" + sub_page_url + "]/div[2]/a[@class='ns_joblink']")).GetAttribute("href");
						var job_details_page_chrome_Driver_Obj = new ChromeDriver(chrome_Driver, new ChromeOptions());
						job_details_page_chrome_Driver_Obj.Navigate().GoToUrl(job_page_url);
						var job_details_page = job_details_page_chrome_Driver_Obj.FindElementsByXPath("//div[@class='ns_sresultmain']");

						for (int details_page_index = 0; details_page_index < job_details_page.Count(); details_page_index++)
						{

							job_title = job_details_page[details_page_index].FindElement(By.XPath(".//div[2]/div[1]/h1/strong")).Text;
							job_company =  //job_details_page[details_page_index].FindElement(By.XPath(".//div[2]/div[2]/span")).Text;
								job_desc = job_details_page[details_page_index].FindElement(By.XPath(".//div[2]/div[4]/div[2]")).Text;

							job_location = job_details_page[details_page_index].FindElement(By.XPath(".//div[1]/div[5]")).Text;
							job_nationality = job_details_page[details_page_index].FindElement(By.XPath(".//div[1]/div[7]")).Text;
							job_experience = job_details_page[details_page_index].FindElement(By.XPath(".//div[1]/div[9]")).Text;
							job_keyskils = job_details_page[details_page_index].FindElement(By.XPath(".//div[1]/div[1]/div[12]")).Text;
							job_function = job_details_page[details_page_index].FindElement(By.XPath(".//div[1]/div[1]/div[14]")).Text;
							job_role = job_details_page[details_page_index].FindElement(By.XPath(".//div[1]/div[1]/div[16]")).Text;
							job_industry = job_details_page[details_page_index].FindElement(By.XPath(".//div[1]/div[1]/div[18]")).Text;
							job_date = job_details_page[details_page_index].FindElement(By.XPath(".//div[1]/div[1]/div[20]")).Text;
							//job_ref_code = job_details_page[details_page_index].FindElement(By.XPath(".//div[1]/div[1]/div[22]")).Text;

							/* to output the result in textbox*/
							result_textBox.AppendText(job_title.ToString().Trim() + "   " + job_company.ToString().Trim() + " " + job_desc.ToString().Trim() + "  " + job_location.ToString().Trim() + "   " + job_nationality.ToString().Trim() + "  " + job_experience.ToString().Trim() + "  " + job_keyskils.ToString().Trim() + "  " + job_function.ToString().Trim() + "  " + job_role.ToString().Trim() + "  " + job_industry.ToString().Trim() + "  " + job_date.ToString().Trim() + "\r\n");
							/* add data to above 4 created csv file */
							File.AppendAllText(Path.Combine(path_Desktop, "Output_page_no_" + pageNumber + ".csv"), job_title.ToString().Trim().Replace(",", "") + "," + job_company.ToString().Trim().Replace(",", "") + "," + job_desc.ToString().Trim().Replace(",", "") + "," + job_location.ToString().Trim().Replace(",", "") + "," + job_nationality.ToString().Trim().Replace(",", "") + "," + job_experience.ToString().Trim().Replace(",", "") + "," + job_keyskils.ToString().Trim().Replace(",", "") + "," + job_function.ToString().Trim().Replace(",", "") + "," + job_role.ToString().Trim().Replace(",", "") + "," + job_industry.ToString().Trim().Replace(",", "") + "," + job_date.ToString().Trim().Replace(",", "") + "\r\n");


							job_details_page_chrome_Driver_Obj.Quit();
						}
					}
				}

			}
			catch (Exception ex)
			{

				result_textBox.AppendText(ex.StackTrace);
				MessageBox.Show("Google Chrome not found! Click OK to install it");
				//System.Diagnostics.Process.Start("https://www.google.com/chrome/#eula");
				//https://www.google.com/chrome/browser/thankyou.html?standalone=1&system=true&platform=win&installdataindex=defaultbrowser
				this.Close();
			}
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
				if (web_url_textBox.Text.Equals(""))
				{
					totalPages_Int = Int32.Parse(pages_num_textBox.Text);
					monster_gulf_grabber_function(totalPages_Int, default_Url);
				}
				else
				{
					default_Url = web_url_textBox.Text;
					totalPages_Int = Int32.Parse(pages_num_textBox.Text);
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
				break;
			case 1:
				//Customer Service/ BPO/ KPO  3
				job_type_Selection = 3;
				break;
			case 2:
				//Finance & Accounts  7
				job_type_Selection = 7;
				break;
			case 3:
				//HR  11
				job_type_Selection = 11;
				break;
			case 4:
				//IT  22
				job_type_Selection = 22;
				break;
			case 5:
				//Legal   13
				job_type_Selection = 13;
				break;
			case 6:
				//Marketing   14
				job_type_Selection = 14;
				break;
			case 7:
				//Purchase & Supply Chain 18
				job_type_Selection = 18;
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
				break;
			case 1:
				//Saudi Arabia    182
				region_Selection = 182;
				break;
			case 2:
				//Qatar   180
				region_Selection = 180;
				break;
			case 3:
				//Bahrain 158
				region_Selection = 158;
				break;
			case 4:
				//Oman    178
				region_Selection = 178;
				break;
			case 5:
				//Kuwait  170
				region_Selection = 170;
				break;
			case 6:
				//Egypt   250
				region_Selection = 250;
				break;
			case 7:
				//Iraq    486
				region_Selection = 486;
				break;
			case 8:
				//Jordan  238
				region_Selection = 238;
				break;
			case 9:
				//Lebanon 237
				region_Selection = 237;
				break;
			}//end switch
			//result_textBox.AppendText(region_Selection.ToString());
		}

		private void monstergulf_frm_Load(object sender, EventArgs e)
		{

		}


	}
}