using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SqlClient;

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

    public void Start()
    {
        string connectionString =
             "Server=den1.mssql5.gear.host;" +
             "Database=arprojectsenior;" +
             "User ID=arprojectsenior;" +
             "Password=201311040_irem;";

        using (SqlConnection dbCon = new SqlConnection(connectionString))
        {
            SqlCommand cmd;
            try
            {
                dbCon.Open();
                Debug.Log("Connection opened succesfully");

                string query1 = "SELECT *FROM PERSON";

                cmd = new SqlCommand(query1, dbCon);
                int res = cmd.ExecuteNonQuery();

                using (SqlDataReader reader = cmd.ExecuteReader())
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

    }
}
