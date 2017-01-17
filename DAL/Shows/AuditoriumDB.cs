using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBAcsses;

namespace DAL.Shows
{
  public  class AuditoriumDB :  GeneralTable
    {
        public AuditoriumDB() : base("Auditorium", "ID") { }

        public string GetKey()
        {
            return base.PrimaryKey;
        }

        public new Auditorium GetCurrentRowData()
        {
            return new Auditorium(base.GetCurrentRowData());
        }

        public void UpdateAuditorium(Auditorium A)
        {
            base.UpdateRow(A);
        }

        public void AddAuditorium(Auditorium a)
        {
            base.AddRow(a);
        }
    }
}
