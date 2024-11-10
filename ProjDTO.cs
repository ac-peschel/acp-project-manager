using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acProj
{
    public class ProjDTO
    {
        public List<ProjTabDTO> tabs {  get; set; }
    }

    public class ProjTabDTO
    {
        public string title { get; set; }
        public List<ProjPaneDTO> panes { get; set; }
    }

    public class ProjPaneDTO
    {
        public string subDir { get; set; }
    }
}
