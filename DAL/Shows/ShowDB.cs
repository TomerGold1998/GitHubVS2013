using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBAcsses;
using System.Data;

namespace DAL.Shows
{
   public class ShowDB : GeneralTable
    {
       public ShowDB()
           : base("Show", "ID")
        {
            PlayDB Pdb = new PlayDB();
            DataColumn d1 = Pdb.GetPrimaryKeyColumn();
            DataColumn d2 = this.GetColumn("Play");
            DBConnector.GetInstance().AddRelation(Show.PlayRelation, d1, d2);

            AuditoriumDB Adb = new AuditoriumDB();
            DataColumn d3 = Adb.GetPrimaryKeyColumn();
            DataColumn d4 = this.GetColumn("PlayPlace");
            DBConnector.GetInstance().AddRelation(Show.AuditoriumRelation, d3, d4);       
            
        }

        public string GetKey()
        {
            return base.PrimaryKey;
        }

        public new Show GetCurrentRowData()
        {
            return new Show(base.GetCurrentRowData());
        }

        public void UpdateShow(Show s)
        {
            base.UpdateRow(s);
        }

        public void AddShow(Show s)
        {
            base.AddRow(s);
        }

        public List<Show> GetAllShows()
        {
            try
            {
                this.GoToFirst();
                Show Current = new Show();
                List<Show> list = new List<Show>();

                for (int i = 0; i < this.LengthOfTable; i++)
                {
                    Current = this.GetCurrentRowData();
                    if (Current != null)
                    {
                        list.Add(Current);
                        this.MoveNext();
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                return new List<Show>();
            }
        }

        public List<Auditorium> getAllOpenAuditorimsForShow(DateTime atDay, DateTime fromTime, DateTime toTime)
        {
            AuditoriumDB Adb = new AuditoriumDB();
            List<Auditorium> AvaialbeAuditoriums = new List<Auditorium>();
            foreach (Auditorium a in Adb.GetAllAuditoriums())
            {
                if (a.IsAviableTimeToBook(atDay, fromTime) && !IsAuditoruimISAlreadyBooked(a,atDay , fromTime, toTime))
                {
                    AvaialbeAuditoriums.Add(a);
                }
            }
            return AvaialbeAuditoriums;            
        }

        private bool IsAuditoruimISAlreadyBooked(Auditorium a, DateTime atDay, DateTime fromTime, DateTime toTime)
        {   
            List<Show> showsAtTime = new List<Show>();
            foreach (var show in this.GetAllShows())
            {
                if(show.AtDate.ToShortDateString() == atDay.ToShortDateString())
                {
                    if (show.FromTime.Hour <= fromTime.Hour & show.ToTime.Hour <= toTime.Hour)
                    {
                        if (show.PlayPlace.ID == a.ID)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
