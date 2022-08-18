﻿using System;
namespace APi_MyCar_POS.Models
{
    public class Expens
    {
        //ТОПЛИВО + ВСЕ 
        public int ID { get; set; }
        public float Sum  { get; set; }
        public int? Mileage { get; set; }
        public float? Volume { get; set; }
        public DateTime Date { get; set; }
        public float? Cost { get; set; }
        public int AutoID { get; set; }

        //ЗАПЧАСТИ, МОЙКИ, ПАРКОВКИ, ШИНОМОНТАЖ, СТРАХОВКА (ПОЯСНЕНИЕ В ВК ПРО КАТЕГОРИИ + ДОБАВИТЬ ГРАФИКИ КАК В ПРИЛОЖЕНИИ И ИНФА В ВК )
        public string? Text { get; set; }
        
        public void Update(Expens newExpens)
        {
            Sum = newExpens.Sum;
            Mileage = newExpens.Mileage;
            Volume = newExpens.Volume;
            Date = newExpens.Date;
            Cost = newExpens.Cost;
            AutoID = newExpens.AutoID;
            Text = newExpens.Text;
        }
    }
}
