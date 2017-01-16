using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBAcsses;
using System.Data;

namespace DAL.Shows
{
    public class ActorsInShow :IEntity
    {
        public static string relationActor = "ActorInShowToActorRelation";
        public static string relatioShow = "ActorInShowToShowRelation";


        public string ID { get; set; }
        public Actor Actor { get; set; }
        public Show Show { get; set; }

        public ActorsInShow(DataRow dataRow)
        {
            this.ID = dataRow["ID"].ToString();
            this.Actor = new Actor(dataRow.GetParentRow(ActorsInShow.relationActor));
            this.Show = new Show(dataRow.GetParentRow(ActorsInShow.relatioShow));
        }
        public void populate(DataRow dataRow)
        {
            dataRow["ID"] = this.ID;
            dataRow["Actor"] = this.Actor.ID;
            dataRow["Show"] = this.Show.ID;
        }


    }
}
