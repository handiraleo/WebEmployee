using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebEmployee.Pages.Employees
{
	public class IndexModel : PageModel
	{
		public List<EmployeeInfo> listemployees = new List<EmployeeInfo>();
		public void OnGet()
		{
			try
			{
				string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=webemployee;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM employees";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								EmployeeInfo employeeInfo = new EmployeeInfo();
								employeeInfo.Id = "" + reader.GetInt32(0);
								employeeInfo.name = reader.GetString(1);
								employeeInfo.email = reader.GetString(2);
								employeeInfo.phone = reader.GetString(3);
								employeeInfo.address = reader.GetString(4);
								employeeInfo.positions = reader.GetString(5);

								listemployees.Add(employeeInfo);
							}
						}
					}
				}

			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.ToString());
			}

		}
	}

	public class EmployeeInfo
	{
		public String Id;
		public String name;
		public String email;
		public String phone;
		public String address;
		public String positions;

	}
}

