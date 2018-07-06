using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using moneyManage.Tool;
using MySql.Data.MySqlClient;
using System.Diagnostics;

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
        /// 2 - username or password is empty or whitespace
        /// 3 - password is too short (at least 8)
        /// </returns>
        public int CreateNewUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return 2;
            if (password.Length < 7)
                return 3;
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
                        }
                    }
                }

                myConnection.Close();
            }

            return 0;
        }

        public UserInfo VerifyUser(string username, string password)
        {
            var hashPassword = "";
            var user = new UserInfo();
            using (var myConnection = new MySqlConnection {ConnectionString = MyConnectionString})
            {
                myConnection.Open();
                using (var myCommand = myConnection.CreateCommand())
                {
                    using (var myTrans = myConnection.BeginTransaction())
                    {
                        myCommand.Connection = myConnection;
                        myCommand.Transaction = myTrans;

                        try
                        {
                            myCommand.CommandText = $"SELECT id, password FROM User WHERE username = '{username}';";
                            using (var reader = myCommand.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    hashPassword = reader.GetString("password");
                                    user.Id = reader.GetInt32("id");
                                    Console.WriteLine(hashPassword);
                                }

                                reader.Close();

                                if (string.IsNullOrWhiteSpace(hashPassword))
                                {
                                    user.Valid = false;
                                    return user;
                                }


                                Console.WriteLine(@"Read record from the database.");
                            }

//                        var reader = myCommand.ExecuteReader();
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
            }

            user.Valid = SecurePasswordHasher.Verify(password, hashPassword);
            return user;
        }

        public void InsertMoneyExpense(int userid, string category, decimal money)
        {
            if (userid < 1)
                throw new Exception("UserID is positive number");
            if (string.IsNullOrWhiteSpace(category))
                throw new Exception("Category can't be empty");
            if (money < 0)
                throw new Exception("Money is positive number");
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


                            myCommand.CommandText =
                                $"INSERT INTO Expense (Uid,Category,$) VALUES ('{userid}','{category}','{money}');";

                            myCommand.ExecuteNonQuery();
                            myTrans.Commit();
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
                        }
                    }
                }
            }
        }

        public void InsertMoneyTotal(int userid, decimal money)
        {
            if (userid < 1)
                throw new Exception("UserID is positive number");
            if (money < 0)
                throw new Exception("Money is positive number");
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


                            myCommand.CommandText =
                                $"INSERT INTO Total (Uid,$) VALUES ('{userid}','{money}');";

                            myCommand.ExecuteNonQuery();
                            myTrans.Commit();
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
                        }
                    }
                }
            }
        }

        public List<Bank> PullExpenses(int userid)
        {
            var result = new List<Bank>();

            using (var myConnection = new MySqlConnection {ConnectionString = MyConnectionString})
            {
                myConnection.Open();
                using (var myCommand = myConnection.CreateCommand())
                {
                    var myTrans = myConnection.BeginTransaction();

                    myCommand.Connection = myConnection;
                    myCommand.Transaction = myTrans;

                    try
                    {
                        myCommand.CommandText = $"SELECT * FROM Expense WHERE Uid = {userid};";
                        using (var reader = myCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var expense = new Bank(reader.GetDecimal("$"),
                                    reader.GetDateTime("Timestamp"), reader.GetString("Category"));
//                                result.Add();
                                result.Add(expense);
                            }
                        }


                        Console.WriteLine(@"Read record from the database.");
                    }
                    catch (Exception e)
                    {
                        try
                        {
                            Console.WriteLine(e.Message);
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

            return result;
        }

        public List<Bank> PullTotal(int userid)
        {
            var result = new List<Bank>();

            using (var myConnection = new MySqlConnection {ConnectionString = MyConnectionString})
            {
                myConnection.Open();
                using (var myCommand = myConnection.CreateCommand())
                {
                    var myTrans = myConnection.BeginTransaction();

                    myCommand.Connection = myConnection;
                    myCommand.Transaction = myTrans;

                    try
                    {
                        myCommand.CommandText = $"SELECT * FROM Expense WHERE Uid = {userid};";
                        using (var reader = myCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var total = new Bank(reader.GetDecimal("$"), reader.GetDateTime("Timestamp"));
                                result.Add(total);
                                Debug.WriteLine("Inserting");
                            }

                            Debug.WriteLine("Running");
                            //Debug.WriteLine(result.Current.money);
                        }

                        Console.WriteLine(@"Read record from the database.");
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

            return result;
        }

        internal class UserInfo
        {
            public int Id { get; set; }
            public bool Valid { get; set; }
        }

        public class Bank
        {
            public decimal Money { get; set; }
            public DateTime Timestamp { get; set; }
            public string Category { get; set; }

            public Bank(decimal money, DateTime timestamp, string category = null)
            {
                Money = money;
                Timestamp = timestamp;
                Category = category;
            }
        }
    }
}