using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace test.Pages.DriverVechile
{
    public class IndexModel : PageModel
    {
        public List<DriverVechileInfo> driverVechiles =new List<DriverVechileInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-4O5C3G4\\SQLEXPRESS;Initial Catalog=DriverVechileDatabase;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Select DriverVechile.id, Vehicle.AssetNumber, Vehicle.Registration, Vehicle.Manufacturer,\r\nDriver.FirstName, Driver.LastName, Driver.LicenceNumber, Driver.Mobile,Driver.Email\r\nFrom DriverVechile\r\nLEFT JOIN Vehicle\r\nON DriverVechile.VehicleID=Vehicle.VehicleID\r\nLEFT JOIN Driver\r\nON DriverVechile.DriverID=Driver.DriverID";
					using(SqlCommand command = new SqlCommand(sql, connection))
					{
						using(SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								DriverVechileInfo info = new DriverVechileInfo();
								info.id = "" + reader.GetInt32(0);
								info.assetNumber = reader.GetString(1);
								info.registration = reader.GetString(2);
								if (reader.IsDBNull(3))
								{
									info.manufaktured = "";
								}
								else
								{
									info.manufaktured = reader.GetString(3);
								}
								info.name = reader.GetString(4);
								info.surname = reader.GetString(5);
								info.licenceNumber = reader.GetString(6);
								if (reader.IsDBNull(7))
								{
									info.phoneNumber = "";
								}
								else
								{
									info.phoneNumber = reader.GetString(7);
								}
								if (reader.IsDBNull(8))
								{
									info.email = "";
								}
								else
								{
									info.email = reader.GetString(8);
								}
								driverVechiles.Add(info);
							}
						}
					}
				}
            }
            catch(Exception ex)
            {

            }
        }
    }

	public class DriverVechileInfo
	{
		public String id;
		public String assetNumber;
		public String registration;
		public String manufaktured;
		public String name;
		public String surname;
		public String licenceNumber;
		public String phoneNumber;
		public String email;
	}
}
