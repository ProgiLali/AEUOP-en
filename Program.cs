using System;
using System.ComponentModel;
using System.IO;

namespace AEUOP
{
    class AccessCountry
    {
        private string countryNameEn;
        private string countryNameHun;
        private  DateTime accessDate;

        public AccessCountry(string lineData)
        {
            string[] parts = lineData.Split(";");
            this.countryNameEn = parts[0];
            this.countryNameHun = parts[1];
            string[] lowerParts = parts[2].Split(".");
            accessDate = new DateTime(Convert.ToInt32(lowerParts[0]),
                                        Convert.ToInt32(lowerParts[1]),
                                        Convert.ToInt32(lowerParts[2]));
        }

        public string CountryNameEn { get => countryNameEn; set => countryNameEn = value; }
        public string CountryNameHun { get => countryNameHun; set => countryNameHun = value; }
        public DateTime AccessDate { get => accessDate; set => accessDate = value; }

        public override string ToString()
        {
            return string.Format("{0}: {1}.{2}.{3}",
            this.countryNameEn,
            this.accessDate.Year,
            this.accessDate.Month,
            this.accessDate.Day);
        }
    }

    class Task
    {
        List<AccessCountry> accessList;

        public Task()
        {
            uploadData("AccesEU.txt");
            ShowList();
            Task2();
            Task3();
            Task4();
            Task5();
            Task6();
        }

        private void uploadData(string name)
        {
            accessList = new List<AccessCountry>();

            StreamReader inData = new StreamReader(name);
            while(!inData.EndOfStream)
            {
                accessList.Add(new AccessCountry(inData.ReadLine()));
            }
            inData.Close();
        }

        private void ShowList()
        {
            foreach(var item in accessList)
            {
                Console.WriteLine(item);
            }
        }

        private void Task2()
        {
            int accessions = 0;
            foreach(var item in accessList)
            {
                accessions++;
            }
            Console.WriteLine("Task 2: The number of accessions: {0}",accessions);
        }

        private void Task3()
        {
            string searchCountry = "Hungary";
            DateTime searchDate = new DateTime();
            foreach(var item in accessList)
            {
                if(searchCountry == item.CountryNameEn)
                {
                    searchDate = item.AccessDate;
                }
            }
            Console.WriteLine("Task 3: The date when accession was in Hungary: {0}.{1}.{2}",
                    searchDate.Year,
                    searchDate.Month,
                    searchDate.Day);
        }

        private void Task4()
        {
            bool search = false;
            int searchYear = 2007;
            foreach(var item in accessList)
            {
                if(item.AccessDate.Year == searchYear)
                {
                    search = true;
                }
            }
            Console.WriteLine("Task 4: There was{0} accession at {1}.",search?"":"n't",searchYear);
        }

        private void Task5()
        {
            DateTime lastDate = accessList[0].AccessDate;
            foreach(var item in accessList)
            {
                if(lastDate < item.AccessDate)
                {
                    lastDate = item.AccessDate;
                }
            }
            Console.WriteLine("Task 5: The last Accession Date happened at {0}.{1}.{2}",                    
                    lastDate.Year,
                    lastDate.Month,
                    lastDate.Day);
        }

        private void Task6()
        {
            Dictionary<int,int> accessStat = new Dictionary<int, int>();
            foreach(var item in accessList)
            {
                if(!accessStat.ContainsKey(item.AccessDate.Year))
                {
                    accessStat.Add(item.AccessDate.Year,0);
                }
                accessStat[item.AccessDate.Year]++;
            }
            Console.WriteLine("Task 6: Accession Statistics:");
            foreach(var item in accessStat)
            {
                Console.WriteLine("\t-{0}: {1} accession{2}",item.Key,item.Value,item.Value<2?"":"s");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Task newTask = new Task();
        }
    }
}