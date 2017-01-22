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
        public ActorInShowRole Role { get; set; }

        public ActorsInShow(DataRow dataRow)
        {
            this.ID = dataRow["ID"].ToString();
            this.Actor = new Actor(dataRow.GetParentRow(ActorsInShow.relationActor));
            this.Show = new Show(dataRow.GetParentRow(ActorsInShow.relatioShow));
            this.Role = dataRow["Role"].ToString() == ActorInShowRole.Main.ToString() ? ActorInShowRole.Main : dataRow["Role"].ToString() == ActorInShowRole.Secondary.ToString() ? ActorInShowRole.Secondary : ActorInShowRole.Side; 
        }

        public ActorsInShow()
        {
            // TODO: Complete member initialization
        }
        public void populate(DataRow dataRow)
        {
            dataRow["ID"] = this.ID;
            dataRow["Actor"] = this.Actor.ID;
            dataRow["Show"] = this.Show.ID;
            dataRow["Role"] = this.Role.ToString();
        }


        public override string ToString()
        {
            return String.Format("Actor {0} {1} in play {2} of show {3} in role {4}", this.Actor.FirstName, this.Actor.LastName, this.Show.Play.p_Name, this.Show.ID, this.Role.ToString());
        }

    }

    public enum ActorInShowRole
    {
        Main, Secondary , Side
    }
}
