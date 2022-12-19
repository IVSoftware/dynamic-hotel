using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dynamic_hotel
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            buttonRandom.Click += onRandomClick;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            List<TextBox> tmp = new List<TextBox>();
            for(int column = 1; column < tableLayoutPanel.ColumnCount; column ++)
            {
                for (int row = 1; row < tableLayoutPanel.RowCount; row++)
                {
                    tableLayoutPanel.Controls.Add(
                        new TextBox
                        {
                            Anchor = (AnchorStyles)0xF,
                        }, column, row
                    );
                }
            }
        }

        Random random = new Random(1);
        private void onRandomClick(object sender, EventArgs e)
        {
            int countTotal = random.Next(1, 105);
            for (int iter8 = 0; iter8 < countTotal; iter8++)
            {
                //int col = 1 + (iter8 % 13);
                //int row = 1 + (iter8 / 13);
                //tableLayoutPanel
            }             
        }

        private void CreateControls()
        {
            var colIndex = 0;
        //    var vrsteSoba = _Presenter.VrstaSobeDto.ToArray();

        //    foreach (var bindingItem in vrsteSoba)
        //    {

        //        var lbl = new Label()
        //        {
        //            Width = LABEL_WIDTH,
        //            Height = LABEL_HEIGHT - 5,
        //            Left = 10,
        //            Top = 30 + colIndex * (EDIT_BOX_HEIGHT + SPACE_BETWEEN_CONTROL),
        //            Text = bindingItem
        //        };
        //        _dataPanel.Controls.Add(lbl);
        //        colIndex++;
        //    }

        //    int a = 1;

        //    foreach (var date in _Presenter.CeneTarifa)
        //    {
        //        int y = 0;

        //        var panel = new Panel
        //        {
        //            Height = PANEL_HEIGHT * (vrsteSoba.Length - 4),
        //            Width = EDIT_BOX_WIDTH,
        //            Left = a * (EDIT_BOX_WIDTH + SPACE_BETWEEN_CONTROL + 50),
        //            Top = 5
        //        };

        //        _dataPanel.Controls.Add(panel);

        //        var label = new Label
        //        {
        //            Height = EDIT_BOX_HEIGHT,
        //            Location = new Point(0, 10),
        //            Text = date.Datum,
        //            Margin = new Padding(0)
        //        };

        //        panel.Controls.Add(label);

        //        int index = 0;

        //        foreach (var item in date.VrstaSobeCena)
        //        {
        //            var box = new TextBox();
        //            panel.Controls.Add(box);
        //            box.Height = EDIT_BOX_HEIGHT;
        //            box.Width = EDIT_BOX_WIDTH;
        //            box.Location = new Point(0, 30 + y * (EDIT_BOX_HEIGHT + SPACE_BETWEEN_CONTROL));
        //            box.DataBindings.Add(new Binding(nameof(box.Text), date, date.Cena[index].Cena1));

        //            y++;
        //            index++;
        //        }
        //        ++a;
        //    }
        //    _dataPanel.AutoScroll = true;
        }
    }
    public class CenePoTarifi
    {
        public Dictionary<string, decimal> VrstaSobeCena { get; set; } = new Dictionary<string, decimal>();
        public string Datum { get; set; }

        private List<Cena> _Cena;


        public List<Cena> Cena
        {
            get => _Cena;
            set
            {
                _Cena = value;
               // NotifyPropertyChanged("Cena");
            }
        }
    }

    public class Cena
    {
        //private string _Cena1;
        //public string Cena1
        //{
        //    get => _Cena1;
        //    set
        //    {
        //        _Cena = value;
        //        //NotifyPropertyChanged("Cena1");
        //    }
        //}
    }
}
