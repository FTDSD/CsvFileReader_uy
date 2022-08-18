using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using uygomanTest.DAL;
using uygomanTest.Model;

namespace uygomanTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            sourcepath.Text = CommonValues.DefaultRootFolder;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StudentDAL studentDAL = new StudentDAL();
            Validation validation = new Validation();
            string datetime = DateTime.Now.ToString("yyyyMMddHHmmss");
            bool r = validation.ValidateFilePath(sourcepath.Text);
            if (r == false)
            {
                MessageBox.Show("Please Enter Valid Path");
            }
            else {
                bool isExist=Directory.Exists(sourcepath.Text);
                if (isExist)
                {
                    try
                    {
                        string[] fileEntries = Directory.GetFiles(sourcepath.Text, "*" + CommonValues.FileExtention, SearchOption.AllDirectories);
                        SqlConnection connection = new SqlConnection(ConnectionString.UYConnectionString);
                        connection.Open();
                        foreach (var files in fileEntries)
                        {
                            StudentDetail studentDetail = new StudentDetail();
                            int counter = 0;
                            string line;
                            string ColumnList = "";
                            StreamReader sourcefile = new StreamReader(files);
                            while ((line = sourcefile.ReadLine()) != null)
                            {

                                if (counter == 0)
                                {
                                    ColumnList = "[" + line.Replace(CommonValues.FileDelimeter, "],[") + "]";
                                }
                                else
                                {
                                    string[] result = line.Split(",");
                                    string Id = result[0].Trim();
                                    string name = result[1].Trim();
                                    string marks = result[2];
                                    string subject = result[3];
                                    string examDate = result[4];

                                    studentDetail.Id = Convert.ToInt32(Id);
                                    studentDetail.Marks = Convert.ToInt32(marks);
                                    studentDetail.Subject = subject;
                                    studentDetail.ExamDate = Convert.ToDateTime(examDate);

                                    DataSet StudentCount = studentDAL.StudentCount(connection, CommonValues.StoredProcedure, Id);
                                    DataTable customerCategoryDataTable = StudentCount.Tables[0];
                                    int SCount = customerCategoryDataTable.Rows[0].Field<int>(0);

                                    if (SCount == 0)
                                    {

                                        bool IsInsertStudent = studentDAL.InsertStudent(Id, name, CommonValues.StoredProcedure, connection);
                                        bool IsInsertDetails = studentDAL.InsertStudentDetail(studentDetail, CommonValues.StoredProcedure, connection);

                                    }
                                    else
                                    {
                                        bool IsInsertDetails = studentDAL.InsertStudentDetail(studentDetail, CommonValues.StoredProcedure, connection);
                                    }

                                }
                                counter++;



                            }
                            connection.Close();
                            sourcefile.Close();
                            connection.Close();
                            bool isMovePathExist = Directory.Exists(CommonValues.MovePath);
                            if (isMovePathExist)
                            {
                                File.Move(files, CommonValues.MovePath + "\\" + datetime + "." + CommonValues.FileExtention);
                            }
                            else {
                                Directory.CreateDirectory(CommonValues.MovePath);
                                MessageBox.Show("CSV File Moving Folder is Created");
                            }
                        }
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    catch (DirectoryNotFoundException ex)
                    {
 
                        MessageBox.Show(ex.Message);
                    }
                    catch (FileNotFoundException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    catch (NullReferenceException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        Directory.CreateDirectory(sourcepath.Text);
                    }
                    catch (IOException ex) { MessageBox.Show(ex.Message); }
                    catch (UnauthorizedAccessException ex) { MessageBox.Show(ex.Message); }

                     
                }

            }
        }
    }
}
