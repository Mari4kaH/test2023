using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace test.Pages.Driver
{
    public class AddDriverModel : PageModel
    {
        public DriverInfo driver = new DriverInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
		public void OnPost()
		{
			Regex validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$");

			driver.name = Request.Form["name"];
            driver.surname = Request.Form["surname"];
            driver.licenceNumber = Request.Form["licenceNumber"];
            driver.phoneNumber = Request.Form["mobile"];
            driver.email = Request.Form["email"];

            if (driver.name.Length == 0 || driver.surname.Length == 0 || driver.licenceNumber.Length == 0)
            {
                errorMessage = "First Name, Last Name and Licence Number can not be emty";
                return;
            } else if (driver.licenceNumber.Length != 10)
            {
                errorMessage = "The licence Number must have 10 sign";
                return;
            } else if (driver.email.Length != 0) 
            {
                if (!validateEmailRegex.IsMatch(driver.email))
                {
                    errorMessage = "Invalid e-mail";
                    return;
                }
            }

            try
            {
				String connectionString = "Data Source=DESKTOP-4O5C3G4\\SQLEXPRESS;Initial Catalog=DriverVechileDatabase;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Insert into Driver\n"+
                                "Values(@name, @surname, @licenceNumber, @mobile, @email);";
                    using(SqlCommand command=new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", driver.name);
                        command.Parameters.AddWithValue ("@surname", driver.surname);
                        command.Parameters.AddWithValue ("@licenceNumber", driver.licenceNumber);
                        command.Parameters.AddWithValue ("@mobile", driver.phoneNumber);
                        command.Parameters.AddWithValue ("@email", driver.email);
                        command.ExecuteNonQuery();
                    }
                }

			}
			catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            driver.Clear();

            successMessage = "New driver added corectly";

            Response.Redirect("/Driver/DriverPage");
		}
	}
}
