using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ScintillaNET;
namespace ZXCassetteDeck
{
    class Class1:Scintilla
    {
        const int LINENUMBER_MARGIN = 0;
        const int BOOKMARK_MARGIN = 1; // Conventionally the symbol margin

        public Class1():base()
        {
            StyleResetDefault();
            Styles[Style.Default].Font = "Consolas";
            Styles[Style.Default].Size = 8;
            Styles[Style.Default].ForeColor = Color.Lime;
            Styles[Style.Default].BackColor = SystemColors.WindowFrame;
            Styles[Style.LineNumber].Visible = false;

            Margin margin = Margins[LINENUMBER_MARGIN];
            margin.Width = 0;

            margin = Margins[BOOKMARK_MARGIN];
            margin.Width = 0;

            StyleClearAll();
            Invalidate();
        }


    }
}
