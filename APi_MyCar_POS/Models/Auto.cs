using System.Collections.Generic;

namespace APi_MyCar_POS.Models
{
    public class Auto : AutoForApp
    {
        public int ID { get; set; }

        public string VIN { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }

        public int Year { get; set; }
        public float EngineCapacity { get; set; }
        public int Power { get; set; }
        public int Mileage { get; set; }

        public List<AutoUser> AutoUsers { get; set; } = new();
        public List<User> Users { get; set; } = new();
        public List<T_O> T_Os { get; set; } = new();
        public List<Expens> Expenses { get; set; } = new();
        public List<Record> Records { get; set; } = new();

        public AutoForApp CovertToBase()
        {
            return new AutoForApp
            {
                ID = ID,
                VIN = VIN,
                Mark = Mark,
                Model = Model,
                Year = Year,
                EngineCapacity = EngineCapacity,
                Power = Power,
                Mileage = Mileage,
                Expenses = Expenses,
                Records = Records,
                T_Os = T_Os
            };
        }
        
        public void Update(AutoForApp autoBase)
        {
            VIN = autoBase.VIN;
            Mark = autoBase.Mark;
            Model = autoBase.Model;
            Year = autoBase.Year;
            EngineCapacity = autoBase.EngineCapacity;
            Power = autoBase.Power;
            Mileage = autoBase.Mileage;
            Expenses = autoBase.Expenses;
            Records = autoBase.Records;
            T_Os = autoBase.T_Os;
        }
    }
}
