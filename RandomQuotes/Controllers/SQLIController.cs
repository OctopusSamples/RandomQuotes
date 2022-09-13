using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;  


namespace RandomQuotes.Controllers
{
    public class SQLIController : Controller
    {
        // testing normal: /sqli?name=Andrew
        // testing exploit: /sqli?name=1%27%20or%20%271%27==%271
        [HttpGet("sqli")]
        public IActionResult Get(string name)
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=Chinook_Sqlite.sqlite");  
            conn.Open();  
  
            SQLiteCommand cmd = new SQLiteCommand(conn);  
            cmd.CommandText = "select * from Employee where LastName == '" + name + "';";
  
            SQLiteDataReader reader = cmd.ExecuteReader();  
  
            
            List<string> res = new List<string>();
            while (reader.Read())
            {
                Console.WriteLine(reader["FirstName"].ToString());
                res.Add(reader["FirstName"].ToString());
            }  
  
            reader.Close();  
  
            //cmd.CommandText = "delete from Customer where CustomerID = 33";  
            //cmd.ExecuteScalar(); 

                
            return Ok(res);
        }
    }
}
