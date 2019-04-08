using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace dev.scan_back.Tools
{
    public  class ExcelReader
    {
        private static OleDbConnection connectionXLS = new OleDbConnection();
        private static OleDbCommand commandXLS = new OleDbCommand();
        private static OleDbDataReader readerRequete;

        /// <summary>
        /// Fonction qui lit un tableau dans un fichier xls et retourne une Datatable
        /// </summary>
        /// <param name="xlsFile">Chemin du fichier XLS</param>
        /// <param name="xlsSheet">Nom de la feuille</param>
        /// <returns>Retourne une DataTable</returns>
        public static DataTable excelToTable(String xlsFile, String xlsSheet)
        {
            DataTable tableXls = new DataTable();
            DataRow lineXls = null;

            connectionXLS.ConnectionString = "Data Source=" + xlsFile + "; Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties=Excel 8.0;";
            commandXLS.Connection = connectionXLS;
            commandXLS.CommandType = System.Data.CommandType.Text;
            commandXLS.CommandText = "select * from [" + xlsSheet + "$];";

            connectionXLS.Open();
            readerRequete = commandXLS.ExecuteReader();

            //On récupére le schema du datareader
            DataTable schemaXls = readerRequete.GetSchemaTable();

            //Création des colonnes de la datatable de retour via le schema du Datareader
            for (int i = 0; i < schemaXls.Rows.Count; i++)
            {
                DataRow line = schemaXls.Rows[i];
                String columnName = line["ColumnName"].ToString();

                DataColumn column = new DataColumn(columnName);
                tableXls.Columns.Add(column);
            }

            //On parcourt les lignes du datareader qu'on ajoute à la datatable 
            while (readerRequete.Read())
            {
                lineXls = tableXls.NewRow();
                for (int i = 0; i < readerRequete.FieldCount; i++)
                {
                    lineXls[i] = readerRequete[i];
                }

                tableXls.Rows.Add(lineXls);
            }

            readerRequete.Close();
            connectionXLS.Close();
            return tableXls;
        }
    }
}