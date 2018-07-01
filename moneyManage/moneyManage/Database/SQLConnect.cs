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
        const string MyConnectionString = "server=qtserver.dynu.net;port=33306;uid=dang;" +
                                          "pwd=passtest;database=test;";

        /// <summary>
        /// Create new user in the database
        /// </summary>
        /// <param name="username">the username</param>
        /// <param name="password">the password</param>
        /// <returns>
        /// 1 - Duplicate username
        /// </returns>
        public int CreateNewUser(string username, string password)
        {
            using (var myConnection = new MySqlConnection {ConnectionString = MyConnectionString})
            {
                myConnection.Open();
                using (var myCommand = myConnection.CreateCommand())
                {
//                    
                    using (var myTrans = myConnection.BeginTransaction())
                    {
                        try
                        {
                            myCommand.Connection = myConnection;
                            myCommand.Transaction = myTrans;

                            password = SecurePasswordHasher.Hash(password);

                            myCommand.CommandText =
                                $"INSERT INTO User (username,password) VALUES ('{username}','{password}');";
                            myCommand.ExecuteNonQuery();
                            myTrans.Commit();
                        }
                        catch (MySqlException e)
                        {
                            var message = e.Message;
                            if (message.Contains("Duplicate"))
                                return 1;
//                            Console.WriteLine(e.Message);
                        }
                    }


//                    try
//                    {
//                        myCommand.CommandText =
//                            $"INSERT INTO User (username,password) VALUES ('{username}','{password}');";
//                        myCommand.ExecuteNonQuery();
//                        myTrans.Commit();
//
//                        Console.WriteLine(@"Both records are written to database.");
//                    }
//                    catch (Exception e)
//                    {
//                        try
//                        {
//                            myTrans.Rollback();
//                        }
//                        catch (SqlException ex)
//                        {
//                            if (myTrans.Connection != null)
//                            {
//                                Console.WriteLine(
//                                    $@"An exception of type {ex.GetType()} was encountered while attempting to roll back the transaction.");
//                            }
//                        }
//
//
//                        Console.WriteLine(
//                            $@"An exception of type {e.GetType()} was encountered while inserting the data.");
//                        Console.WriteLine(@"Neither record was written to database.");
//                    }
                }
            }

            return 0;
        }


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

        // TODO Use for sign in and not done yet
        public bool VerifyUser(string username, string password)
        {
            using (var myConnection = new MySqlConnection {ConnectionString = MyConnectionString})
            {
                myConnection.Open();
                using (var myCommand = myConnection.CreateCommand())
                {
                    var myTrans = myConnection.BeginTransaction();

                    myCommand.Connection = myConnection;
                    myCommand.Transaction = myTrans;

                    password = SecurePasswordHasher.Hash(password);

                    try
                    {
                        myCommand.CommandText = $"SELECT password FROM User WHERE username = '{username}';";
                        var reader = myCommand.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine(reader.GetString("password"));
                        }


                        Console.WriteLine(@"Read record from the database.");
                        return true;
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
                                Console.WriteLine(
                                    $@"An exception of type {ex.GetType()} was encountered while attempting to roll back the transaction.");
                            }
                        }


                        Console.WriteLine(
                            $@"An exception of type {e.GetType()} was encountered while reading the data.");
                    }
                }
            }

            return false;
        }
    }
}