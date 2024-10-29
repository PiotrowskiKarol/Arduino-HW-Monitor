using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormHWPApp.HWM
{
    public class PCComponent
    {
        public string name;
        public List<ComponentData> components = new List<ComponentData>();

        public Dictionary<string, List<ComponentData>> getComponentsByType()
        {
            Dictionary<string, List<ComponentData>> com = new Dictionary<string, List<ComponentData>>();
            foreach (ComponentData data in components)
            {
                if(!com.ContainsKey(data.type))
                {
                    com.Add(data.type, new List<ComponentData>());
                }
                com[data.type].Add(data);
            }
            return com;
        }
    }
}
