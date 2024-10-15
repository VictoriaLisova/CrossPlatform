using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class ReadFile
    {
        private string filePath;
        public int NumberOfVillages { get; set; }
        public int StartVillageNumber { get; set; }
        public int EndVillageNumber { get; set; }
        public int NumberOfBusTrips { get; set; }
        public List<Trip> Trips { get; set; } = new List<Trip>();

        public ReadFile(string path)
        {
            filePath = path;
        }

        private void GetTripsInfo(string[] values, int tripsAmount)
        {
            int index = 0;
            for (int i = 0; i < tripsAmount; i++)
            {
                Trips.Add(new Trip(int.Parse(values[index]), int.Parse(values[index + 1]), int.Parse(values[index + 2]), int.Parse(values[index + 3])));
                index += 4;
            }
        }

        public void Read()
        {
            try
            {
                var streamReader = new StreamReader(filePath);
                string? line = streamReader.ReadLine();
                if (line != null)
                {
                    string[] values = line.Split();

                    NumberOfVillages = int.Parse(values[0]);
                    StartVillageNumber = int.Parse(values[1]);
                    EndVillageNumber = int.Parse(values[2]);
                    NumberOfBusTrips = int.Parse(values[3]);
                    GetTripsInfo(values.Skip(4).ToArray(), NumberOfBusTrips);
                }
                streamReader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
