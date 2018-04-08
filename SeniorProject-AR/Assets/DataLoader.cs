using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System;
using System.Linq;
using System.Text;
using System.IO;

public class DataLoader : MonoBehaviour
{

    public class PERSON
    {
        private int PersonID;
        private string Name;
        private string Surname;
        private string OutlookMail;
        private string Password;
        private string Title;
        private string Department;
        private string Team;
        private string Speciality;
        private string PersonalInfo;
        private string ARFotoName;

        public PERSON()
        {
        }

        public PERSON(int personID1, string name1, string surname1, string outlookMail1, string password1, string title1, string department1, string team1, string speciality1, string personalInfo1, string aRFotoName1)
        {
            PersonID1 = personID1;
            Name1 = name1;
            Surname1 = surname1;
            OutlookMail1 = outlookMail1;
            Password1 = password1;
            Title1 = title1;
            Department1 = department1;
            Team1 = team1;
            Speciality1 = speciality1;
            PersonalInfo1 = personalInfo1;
            ARFotoName1 = aRFotoName1;
        }

        public int PersonID1
        {
            get
            {
                return PersonID;
            }

            set
            {
                PersonID = value;
            }
        }

        public string Name1
        {
            get
            {
                return Name;
            }

            set
            {
                Name = value;
            }
        }

        public string Surname1
        {
            get
            {
                return Surname;
            }

            set
            {
                Surname = value;
            }
        }

        public string OutlookMail1
        {
            get
            {
                return OutlookMail;
            }

            set
            {
                OutlookMail = value;
            }
        }

        public string Password1
        {
            get
            {
                return Password;
            }

            set
            {
                Password = value;
            }
        }

        public string Title1
        {
            get
            {
                return Title;
            }

            set
            {
                Title = value;
            }
        }

        public string Department1
        {
            get
            {
                return Department;
            }

            set
            {
                Department = value;
            }
        }

        public string Team1
        {
            get
            {
                return Team;
            }

            set
            {
                Team = value;
            }
        }

        public string Speciality1
        {
            get
            {
                return Speciality;
            }

            set
            {
                Speciality = value;
            }
        }

        public string PersonalInfo1
        {
            get
            {
                return PersonalInfo;
            }

            set
            {
                PersonalInfo = value;
            }
        }

        public string ARFotoName1
        {
            get
            {
                return ARFotoName;
            }

            set
            {
                ARFotoName = value;
            }
        }
    }

    public class PERSONTASK
    {
        private int TaskID;
        private int PersonID;
        private string TaskName;
        private int ProjectID;

        public PERSONTASK()
        {
        }

        public PERSONTASK(int taskID, int personID, string taskName, int projectID)
        {
            TaskID1 = taskID;
            PersonID1 = personID;
            TaskName1 = taskName;
            ProjectID1 = projectID;
        }

        public int TaskID1
        {
            get
            {
                return TaskID;
            }

            set
            {
                TaskID = value;
            }
        }

        public int PersonID1
        {
            get
            {
                return PersonID;
            }

            set
            {
                PersonID = value;
            }
        }

        public string TaskName1
        {
            get
            {
                return TaskName;
            }

            set
            {
                TaskName = value;
            }
        }

        public int ProjectID1
        {
            get
            {
                return ProjectID;
            }

            set
            {
                ProjectID = value;
            }
        }
    }

    public class PROJECT
    {
        private int ProjectID;
        private string ProjectName;

        public PROJECT()
        {
        }

        public PROJECT(int projectID1, string projectName1)
        {
            ProjectID1 = projectID1;
            ProjectName1 = projectName1;
        }

        public int ProjectID1
        {
            get
            {
                return ProjectID;
            }

            set
            {
                ProjectID = value;
            }
        }

        public string ProjectName1
        {
            get
            {
                return ProjectName;
            }

            set
            {
                ProjectName = value;
            }
        }
    }

    public List<PERSON> myPeople = new List<PERSON>();
    public List<PERSONTASK> myPersonTaskList = new List<PERSONTASK>();
    public List<PROJECT> myProjects = new List<PROJECT>();

    public DataTable dtPERSON = new DataTable();
    public DataTable dtPERSONTASK = new DataTable();

    private string randomCode = "IFOZTuxX0sUd3/PO8nU9u6Z1zrX0TBMVsr3WKEDjCWL2r7dspa7fBH52oYHAIqItgaHoUCZDRW4rne8yQdmrR8BgdRm5JC4hGsXEenDS2V38UrJF281ZSpfANks0uBv+";
    string Sqlpassword = "5gVmXKtUCxqzZ/3te7WYvuLHKJLcwtE9e8IzjomgWA5I8VQ5bjGKBRv0h/sRXqnlw2fIfCdWrTrHUn2FZRJA7QOPBOf3wmvZfaS2NkoOnN5OfYiDg5bHcklZaYNJi16E";

    public static class Decryptor
    {
        // This constant is used to determine the keysize of the encryption algorithm in bits.
        // We divide this by 8 within the code below to get the equivalent number of bytes.
        private const int Keysize = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int DerivationIterations = 1000;

        public static string Decrypt(string DecryptText, string passCode)
        {
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var decryptTextBytesWithSaltAndIv = Convert.FromBase64String(DecryptText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = decryptTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = decryptTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var decryptTextBytes = decryptTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(decryptTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            var password = new Rfc2898DeriveBytes(passCode, saltStringBytes, DerivationIterations);

            var keyBytes = password.GetBytes(Keysize / 8);
            using (var symmetricKey = new RijndaelManaged())
            {
                symmetricKey.BlockSize = 256;
                symmetricKey.Mode = CipherMode.CBC;
                symmetricKey.Padding = PaddingMode.PKCS7;
                var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes);


                var memoryStream = new MemoryStream(decryptTextBytes);

                var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

                var plainTextBytes = new byte[decryptTextBytes.Length];
                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }

        }
    }

    public void Start()
    {

        string connectionPassword = Decryptor.Decrypt(Sqlpassword, randomCode);

        string connectionString =
             "Server=den1.mssql5.gear.host;" +
             "Database=arprojectsenior;" +
             "User ID=arprojectsenior;" +
             "Password="+connectionPassword+";";

        using (SqlConnection dbCon = new SqlConnection(connectionString))
        {
            SqlCommand cmdPerson;
            try
            {
                dbCon.Open();
                Debug.Log("Connection opened succesfully");

                string query1 = "SELECT *FROM PERSON";

                cmdPerson = new SqlCommand(query1, dbCon);
                int res = cmdPerson.ExecuteNonQuery();

                using (SqlDataReader reader = cmdPerson.ExecuteReader())
                {
                    Debug.Log("Reader works");

                    PERSON tempPerson;
                    int persID;
                    string name, surname, outlookmail, password, title, dept, team, speciality, persinfo, arfoto;

                    dtPERSON.Load(reader);

                    foreach (DataRow row in dtPERSON.Rows)
                    {
                        tempPerson = null;
                        persID = int.Parse(row["PersonID"].ToString());
                        name = row["Name"].ToString();
                        surname = row["Surname"].ToString();
                        outlookmail = row["OutlookMail"].ToString();
                        password = row["Password"].ToString();
                        title = row["Title"].ToString();
                        dept = row["Department"].ToString();
                        team = row["Team"].ToString();
                        speciality = row["Speciality"].ToString();
                        persinfo = row["PersonalInfo"].ToString();
                        arfoto = row["ARFotoName"].ToString();
                        tempPerson = new PERSON(persID, name, surname, outlookmail, password, title, dept, team, speciality, persinfo, arfoto);
                        myPeople.Add(tempPerson);
                    }
                }

            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
                throw;
            }
        }
        using (SqlConnection dbCon2 = new SqlConnection(connectionString))
        {
            SqlCommand cmdPersonTask;
            try
            {
                dbCon2.Open();
                Debug.Log("Connection opened succesfully");

                string query2 = "SELECT *FROM PERSONTASK";

                cmdPersonTask = new SqlCommand(query2, dbCon2);
                int res2 = cmdPersonTask.ExecuteNonQuery();

                using (SqlDataReader reader2 = cmdPersonTask.ExecuteReader())
                {
                    Debug.Log("Reader2 works");

                    PERSONTASK tempPersonTask;

                    int taskID, personID, projectID;
                    string taskName;

                    dtPERSONTASK.Load(reader2);

                    foreach (DataRow row in dtPERSONTASK.Rows)
                    {
                        tempPersonTask = null;
                        taskID = int.Parse(row["TaskID"].ToString());
                        personID = int.Parse(row["PersonID"].ToString());
                        taskName = row["TaskName"].ToString();
                        projectID = int.Parse(row["ProjectID"].ToString());
                        tempPersonTask = new PERSONTASK(taskID, personID, taskName, projectID);
                        myPersonTaskList.Add(tempPersonTask);
                    }
                }

            }
            catch (System.Exception e2)
            {
                Debug.Log(e2.Message);
                throw;
            }
        }
    }
}
