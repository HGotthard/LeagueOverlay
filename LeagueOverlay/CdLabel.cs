using System.Windows.Forms;

namespace LeagueOverlay
{
    internal class CdLabel
    {
        Label label;
        int cd;

        public CdLabel(Label label, int cd)
        {
            this.label = label;
            this.cd = cd;
        }

        public Label GetLabel
        {
            get
            {
                return label;
            }
        }

        public int GetCd
        {
            get
            {
                return cd;
            }
        }
    }
}