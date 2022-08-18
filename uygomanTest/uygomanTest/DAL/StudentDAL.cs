using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uygomanTest.Model;

namespace uygomanTest.DAL
{
    public class StudentDAL
    {
        SqlParameter[] param;
        SqlCommand command= new SqlCommand();
        SqlDataAdapter dataAdapter;
        public DataSet StudentCount(SqlConnection connection,string spname,string StudentId) {
            int Id = Convert.ToInt32(StudentId);
            param  = new SqlParameter[2];
            param[0] = new SqlParameter("Action", "IsStudentIdAvailable");
            param[1] = new SqlParameter("StudentId", Id);
            
            command = new SqlCommand(spname, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            //connection.Open();
            command.Parameters.Clear();
            if (param.Length > 0)
            {
                command.Parameters.AddRange(param);
            }
            DataSet ds = new DataSet();
            dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(ds);
            //connection.Close();
            return ds;
        }

        public bool InsertStudent(string StudentId,string name,string spname,SqlConnection connection)
        {
            int Id = Convert.ToInt32(StudentId);
            param = new SqlParameter[3];
            param[0] = new SqlParameter("Action", "InsertStudent");
            param[1] = new SqlParameter("StudentId", Id);
            param[2] = new SqlParameter("Name",name);

            command = new SqlCommand(spname, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            //connection.Open();
            command.Parameters.Clear();
            if (param.Length > 0)
            {
                command.Parameters.AddRange(param);
            }
            SqlDataReader reader = command.ExecuteReader();
            reader.Close();
            return true;
        
        }

        public bool InsertStudentDetail(StudentDetail studentDetail, string spname, SqlConnection connection)
        {

            param = new SqlParameter[5];
            param[0] = new SqlParameter("Action", "InsertStudentDetails");
            param[1] = new SqlParameter("StudentId", studentDetail.Id);
            param[2] = new SqlParameter("Marks", studentDetail.Marks);
            param[3] = new SqlParameter("Subject", studentDetail.Subject);
            param[4] = new SqlParameter("ExamDate", studentDetail.ExamDate);


            command = new SqlCommand(spname, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            //connection.Open();
            command.Parameters.Clear();
            if (param.Length > 0)
            {
                command.Parameters.AddRange(param);
            }
            SqlDataReader reader = command.ExecuteReader();
            reader.Close();
            return true;

        }

        //

    }
}
