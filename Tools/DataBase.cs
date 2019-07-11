using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class DataBase
    {
        private SqlConnection connection = null;
        public SqlConnection getConnection()
        {
            connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
            return connection;
        }

        /*
                connection.Open();
                command = new SqlCommand();
                command.Connection = connection;
                command.Parameters.Add(new SqlParameter("begin", begin));
                command.Parameters.Add(new SqlParameter("end", end));

                command.CommandText = " SELECT  * from (SELECT row_number() over(order by id desc ) as num,id,number as phone,message,case  state  when '0' then '发送' when '1' then '接收'  end as state,datetime as dateTime,isRead  from SMS ) temp where num between  @begin and @end";

                 messages = command.ExecuteReader();
          
       
                if (command != null)
                    command.Dispose();
                if (connection != null) connection.Dispose();
       
         * 
         * 
      
                //   SqlConnection connection = new SqlConnection("server= 127.0.0.1;uid=sa;pwd=esun5005;database=crmrun");
                connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
                connection.Open();
                command = new SqlCommand();
                command.Connection = connection;
                command.Parameters.Add(new SqlParameter("id", id));
              //  command.Parameters.Add(new SqlParameter("isRead", isRead));

                command.CommandText = " update SMS set isRead=0 where id=@id";

               command.ExecuteNonQuery();
          
          */


    }
}
