using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace test.Pages.Driver
{
    public class DriverPageModel : PageModel
    {
		public List<DriverInfo> Drivers = new List<DriverInfo>();
		public void OnGet()
		{
			try
			{
				String connectionString = "Data Source=DESKTOP-4O5C3G4\\SQLEXPRESS;Initial Catalog=DriverVechileDatabase;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "Select * from Driver";
					using (SqlCommand commandD = new SqlCommand(sql, connection))
					{
						using (SqlDataReader readerD = commandD.ExecuteReader())
						{
							while (readerD.Read())
							{
								DriverInfo driverInfo = new DriverInfo();
								driverInfo.id = "" + readerD["DriverID"];
								driverInfo.name = "" + readerD["FirstName"];
								driverInfo.surname = "" + readerD["LastName"];
								driverInfo.licenceNumber = "" + readerD["LicenceNumber"];
								if (readerD["Mobile"] == DBNull.Value)
								{
									driverInfo.phoneNumber = "";
								}
								else
								{
									driverInfo.phoneNumber = "" + readerD["Mobile"];
								}
								if (readerD["Email"] == DBNull.Value)
								{
									driverInfo.email = "";
								}
								else
								{
									driverInfo.email = "" + readerD["Email"];
								}

								Drivers.Add(driverInfo);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{

			}
		}
	}

	public class DriverInfo
	{
		public String id;
		public String name;
		public String surname;
		public String licenceNumber;
		public String phoneNumber;
		public String email;

		public DriverInfo()
		{
			id = null;
			name = null;
			surname = null;
			licenceNumber = null;
			phoneNumber = null;
			email = null;

		}
		public void Clear()
		{
			id = null;
			name = null;
			surname = null;
			licenceNumber = null;
			phoneNumber = null;
			email = null;

		}
	}
}

