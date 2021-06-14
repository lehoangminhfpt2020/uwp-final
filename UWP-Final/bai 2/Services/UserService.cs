using Dm1.Helper;
using Dm1.Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dm1.Services
{
    class UserService : IUserService
    {
        private int saltLength = 6;
        public bool Create(User user)
        {
            var bP = Encoding.ASCII.GetBytes(user.Password);
            user.Password = System.Text.Encoding.Default.GetString(bP);

            var sqlConnection = SQLiteHelper.GetInstance().SQLiteConnection;
            var sqlCommandString = "INSERT INTO users (username, password) values (?,?)";
            using (var stt = sqlConnection.Prepare(sqlCommandString))
            {
                stt.Bind(1, user.UserName);
                stt.Bind(2, user.Password);
                var result = stt.Step();
                return result == SQLiteResult.OK;
            }
        }

        public bool CheckLogin(User user)
        {
            User dbUser;

            var sqlConnection = SQLiteHelper.GetInstance().SQLiteConnection;
            var sqlCommandString = "select * from users where username = ?";
            using (var stt = sqlConnection.Prepare(sqlCommandString))
            {
                stt.Bind(1, user.UserName);
                if (SQLiteResult.ROW == stt.Step())
                {
                    var ID = (string)stt[0];
                    var UserName = (string)stt[1];
                    var Password= (string)stt[2]; ;

                    if (ComparePasswords(Encoding.ASCII.GetBytes(Password), Encoding.ASCII.GetBytes(user.Password)))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        private byte[] CreateDbPassword(byte[] unsaltedPassword)
        {
            //Create a salt value.
            byte[] saltValue = new byte[saltLength];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(saltValue);

            return CreateSaltedPassword(saltValue, unsaltedPassword);
        }

        private byte[] CreateSaltedPassword(byte[] saltValue, byte[] unsaltedPassword)
        {
            byte[] rawSalted = new byte[unsaltedPassword.Length + saltValue.Length];
            unsaltedPassword.CopyTo(rawSalted, 0);
            saltValue.CopyTo(rawSalted, unsaltedPassword.Length);
         
            SHA1 sha1 = SHA1.Create();
            byte[] saltedPassword = sha1.ComputeHash(rawSalted);

            byte[] dbPassword = new byte[saltedPassword.Length + saltValue.Length];
            saltedPassword.CopyTo(dbPassword, 0);
            saltValue.CopyTo(dbPassword, saltedPassword.Length);

            return dbPassword;
        }

        private bool ComparePasswords(byte[] storedPassword, byte[] hashedPassword)
        {
            if (storedPassword == null || hashedPassword == null || hashedPassword.Length != storedPassword.Length - saltLength)
                return false;

            // Get the saved saltValue.
            byte[] saltValue = new byte[saltLength];
            int saltOffset = storedPassword.Length - saltLength;
            for (int i = 0; i < saltLength; i++)
                saltValue[i] = storedPassword[saltOffset + i];

            byte[] saltedPassword = CreateSaltedPassword(saltValue, hashedPassword);

            return CompareByteArray(storedPassword, saltedPassword);
        }
        private bool CompareByteArray(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
                return false;

            int mismatch = 0;
            for (int i = 0; i < array1.Length; i++)
            {
                mismatch |= array1[i] ^ array2[i];
            }
            return mismatch == 0;
        }
    }
}
