using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace test.Pages.Vehicle
{
    public class VehiclePageModel : PageModel
    {
		public List<Vehicle> vehicles = new List<Vehicle>();
		public void OnGet()
		{
			try
			{
				String connectionString = "Data Source=DESKTOP-4O5C3G4\\SQLEXPRESS;Initial Catalog=DriverVechileDatabase;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "Select * from Vehicle";
					using (SqlCommand commandD = new SqlCommand(sql, connection))
					{
						using (SqlDataReader readerD = commandD.ExecuteReader())
						{
							while (readerD.Read())
							{
								Vehicle v = new Vehicle();
								v.id = "" + readerD["VehicleID"];
								v.assetNumber = "" + readerD["AssetNumber"];
								v.registration = "" + readerD["Registration"];
								if (readerD["Manufactured"] == DBNull.Value)
								{
									v.manufactured = "";
								}
								else
								{
									v.manufactured = "" + readerD["Manufactured"];
								}

								Console.WriteLine(v.id);
								vehicles.Add(v);
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

	public class Vehicle
	{
		public String id;
		public String assetNumber;
		public String registration;
		public String manufactured;

		public Vehicle()
		{
			id = null;
			assetNumber = null;
			registration = null;
			manufactured = null;

		}
		public void Clear()
		{
			id = null;
			assetNumber = null;
			registration = null;
			manufactured = null;

		}
	}
}
