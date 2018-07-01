using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using moneyManage.Tool;
using MySql.Data.MySqlClient;

namespace moneyManage.Database
{
    class SqlConnect
    {
        private readonly MySqlConnection _myConnection;

        public SqlConnect()
        {
            var myConnectionString = "server=qtserver.dynu.net;port=33306;uid=dang;" +
                                     "pwd=passtest;database=test;";
            _myConnection = new MySqlConnection {ConnectionString = myConnectionString};
            
        }

        public void CreateNewUser()
        {
            _myConnection.Open();
            MySqlCommand myCommand = _myConnection.CreateCommand();
            var myTrans = _myConnection.BeginTransaction();

            myCommand.Connection = _myConnection;
            myCommand.Transaction = myTrans;
            var username = "Test1";
            var password = SecurePasswordHasher.Hash("passtest");

            try
            {
                myCommand.CommandText = $"INSERT INTO User (username,password) VALUES (\"{username}\",\"{password}\");";
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
//                myCommand.CommandText = @"SELECT COUNT(*) FROM information_schema.tables
//                                            WHERE table_schema = 'test'
//                                            AND table_name = 'Tsest'
//                                            LIMIT 1; ";
//                MySqlDataReader Reader = myCommand.ExecuteReader();
//
//                while (Reader.Read())
//                {
//                    string row = "";
//                    for (int i = 0; i < Reader.FieldCount; i++)
//                    {
//                        Console.WriteLine(i);
//                        row += Reader.GetValue(i) + ", ";
//                    }
//
////                    if (row)
//                    Console.WriteLine(row);
//                }

//                myTrans.Commit();
                Console.WriteLine("Both records are written to database.");
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        Console.WriteLine("An exception of type " + ex.GetType() +
                                          " was encountered while attempting to roll back the transaction.");
                    }
                }


                Console.WriteLine("An exception of type " + e.GetType() +
                                  " was encountered while inserting the data.");
                Console.WriteLine("Neither record was written to database.");
            }
            finally
            {
                _myConnection.Close();
            }

//            Console.ReadLine();
        }
    }
}