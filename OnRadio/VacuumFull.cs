using System;
using System.Data;
using System.Data.SQLite;

namespace OnRadio
{
   
    public class VacuumFull
    {
      ////  private string baseName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\OnRadio\\OnRadio.db";
      //  public static void VacuumDB()
      //  {
            
      //      try
      //      {       // сжать базу данных
      //          using (SQLiteConnection con = new SQLiteConnection("Data Source=" + baseName))
      //          {
      //              var cmd = new SQLiteCommand();
      //              if (con.State != ConnectionState.Open)
      //                  con.Open();
      //              cmd.Parameters.Clear();
      //              cmd.Connection = con;
      //              cmd.CommandText = "VACUUM;";
      //              cmd.CommandType = CommandType.Text;
      //              cmd.CommandTimeout = 30;
      //              cmd.ExecuteNonQuery();
      //              con.Close();
      //          }
      //      }
      //      catch //(Exception ex)
      //      {
      //          // MessageBox.Show(ex.Message);
      //      }
      //  }
    }
}
