using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBAcsses
{
    public interface IEntity   
    {       
      void populate(DataRow dataRow);
    }
}
