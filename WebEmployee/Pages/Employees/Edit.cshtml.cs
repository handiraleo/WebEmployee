using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebEmployee.Pages.Employees
{
    public class EditModel : PageModel
    {
        public EmployeeInfo employeeInfo = new EmployeeInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            String Id = Request.Query["Id"];
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=webemployee;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM employees WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id",Id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                employeeInfo.Id = "" + reader.GetInt32(0);
                                employeeInfo.name = reader.GetString(1);
                                employeeInfo.email = reader.GetString(2);
                                employeeInfo.phone = reader.GetString(3);
                                employeeInfo.address = reader.GetString(4);
                                employeeInfo.positions = reader.GetString(5);
                            }
                        }
                    }
                }


            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            employeeInfo.Id = Request.Form["Id"];
            employeeInfo.name = Request.Form["name"];
            employeeInfo.email = Request.Form["email"];
            employeeInfo.phone = Request.Form["phone"];
            employeeInfo.address = Request.Form["address"];
            employeeInfo.positions = Request.Form["positions"];

            if (employeeInfo.Id.Length == 0 || employeeInfo.name.Length == 0 ||
                employeeInfo.email.Length == 0 || employeeInfo.phone.Length == 0 ||
                employeeInfo.address.Length == 0 || employeeInfo.positions.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            //save the new client into the database
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=webemployee;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE employees " +
                                 "SET name=@name, email=@email, phone=@phone, address=@address, positions=@positions " +
                                 "WHERE Id=@Id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", employeeInfo.name);
                        command.Parameters.AddWithValue("@email", employeeInfo.email);
                        command.Parameters.AddWithValue("@phone", employeeInfo.phone);
                        command.Parameters.AddWithValue("@address", employeeInfo.address);
                        command.Parameters.AddWithValue("@positions", employeeInfo.positions);
                        command.Parameters.AddWithValue("@Id", employeeInfo.Id);

                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("Index");
        }
    }
}