using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foxlynx
{
    public class Component
    {

        public Entity Parent { get; set; }

        public virtual void Initialize()
        {

        }

    }
}
