using System.Data;
using EmployeeCURDCore.Models;
using Microsoft.Data.SqlClient;

namespace EmployeeCURDCore.DAL
{
    public class Employee_DAL
    {
        SqlConnection connection = null;
        SqlCommand command = null;
        public static IConfiguration Configuration { get; set; }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            return Configuration.GetConnectionString("DefaultConncetion");
        }
        /// <summary>
        /// Get all employees record as list
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetAll()
        {
            List<Employee> employeeList = new List<Employee>();
            using (connection = new SqlConnection(GetConnectionString()))
            {
                command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPR_Employee";
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    Employee employee = new Employee();
                    employee.ID = Convert.ToInt32(dr["ID"]);
                    employee.FirstName = dr["FirstName"].ToString();
                    employee.LastName = dr["LastName"].ToString();
                    employee.DOB = Convert.ToDateTime(dr["DOB"]);
                    employee.Email = dr["Email"].ToString();
                    employee.Salary = Convert.ToDouble(dr["Salary"]);
                    employeeList.Add(employee);
                }
                connection.Close();
            }
            return employeeList;
        }
        /// <summary>
        /// Insert data into database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Insert(Employee model)
        {
            int id = 0;
            using (connection = new SqlConnection(GetConnectionString()))
            {
                command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPI_Employee";
                command.Parameters.AddWithValue("FirstName", model.FirstName);
                command.Parameters.AddWithValue("@LastName", model.LastName);
                command.Parameters.AddWithValue("@DOB", model.DOB);
                command.Parameters.AddWithValue("@Email", model.Email);
                command.Parameters.AddWithValue("@Salary", model.Salary);
                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
            }
            return id > 0 ? true : false; ;
        }
        /// <summary>
        /// Get employee details by id
        /// </summary>
        /// <returns></returns>
        public Employee GetById(int ID)
        {
            Employee employeeID = null;
            using (connection = new SqlConnection(GetConnectionString()))
            {
                command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPI_EmployeeByID";
                command.Parameters.AddWithValue("@ID", ID);
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();
                if (dr.Read()) 
                {
                    employeeID = new Employee
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        DOB = Convert.ToDateTime(dr["DOB"]),
                        Email = dr["Email"].ToString(),
                        Salary = Convert.ToDouble(dr["Salary"])
                    };
                }
                connection.Close();
            }
            return employeeID; 
        }

        /// <summary>
        /// Update Employee record
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(Employee model)
        {
            int id = 0;
            using (connection = new SqlConnection(GetConnectionString()))
            {
                command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPU_Employee";
                command.Parameters.AddWithValue("ID", model.ID);
                command.Parameters.AddWithValue("FirstName", model.FirstName);
                command.Parameters.AddWithValue("@LastName", model.LastName);
                command.Parameters.AddWithValue("@DOB", model.DOB);
                command.Parameters.AddWithValue("@Email", model.Email);
                command.Parameters.AddWithValue("@Salary", model.Salary);
                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
            }
            return id > 0 ? true : false; 
        }
        /// <summary>
        /// Delete Employee record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            int i = 0;
            using (connection = new SqlConnection(GetConnectionString()))
            {
                command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPD_Employee";
                command.Parameters.AddWithValue("@ID",id);
               
                connection.Open();
                i = command.ExecuteNonQuery();
                connection.Close();
            }
            return i > 0 ? true : false; 
        }
    }
}
