Your [question](https://stackoverflow.com/q/74850647/5438626) is about creating dynamic bindings for textboxes and this is one tested way for doing that. First create some textboxes dynamically:

    public MainForm()
    {
        InitializeComponent();
        buttonRandom.Click += (sender, e) => generateRandomList();
    }
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        List<TextBox> tmp = new List<TextBox>();
        for (int column = 1; column < tableLayoutPanel.ColumnCount; column++)
        {
            for (int row = 1; row < tableLayoutPanel.RowCount; row++)
            {
                TextBox textBox = new TextBox { Anchor = (AnchorStyles)0xF };
                tableLayoutPanel.Controls.Add(textBox, column, row);
                tmp.Add(textBox);
                textBox.KeyDown += onAnyTextBoxKeyDown;
            }
        }
        _textboxes = tmp.ToArray();
        // Generate first dataset
        generateRandomList();
    }
    TextBox[] _textboxes = null;
    readonly List<VrstaSobeCena> _dynamicObjects = new List<VrstaSobeCena>();


[![table layout panel][1]][1]

***

Then, whenever a new random list is generated, clear any old text and databindings from every `TextBox` before creating a new data binding for it.

    public static Random Rando { get; } = new Random(2);
    private void generateRandomList()
    {
        // Clear ALL the data + bindings for ALL the textboxes.
        _dynamicObjects.Clear();
        foreach (var textbox in _textboxes)
        {
            textbox.Clear();
            textbox.DataBindings.Clear();
        }
        // Generate and create new bindings
        int count = Rando.Next(1, 105);
        for (int i = 0; i < count; i++)
        {
            var textbox = _textboxes[i];
            VrstaSobeCena vrstaSobeCena =
                new VrstaSobeCena{ Sobe = (Sobe)tableLayoutPanel.GetRow(textbox) };
            _dynamicObjects.Add(vrstaSobeCena);
            textbox.Tag = vrstaSobeCena;
            textbox.DataBindings.Add(
                new Binding(
                    nameof(TextBox.Text),
                    _dynamicObjects[i],
                    nameof(VrstaSobeCena.Cena),
                    formattingEnabled: true,
                    dataSourceUpdateMode: DataSourceUpdateMode.OnPropertyChanged,
                    null,
                    "F2"
                ));
        }
    }

***
For testing purposes, this code is using a [Minimal Reproducible Sample](https://stackoverflow.com/help/minimal-reproducible-example) "mock" version of the class that implements `INotifyPropertyChanged`. On issue I noticed in the code you posted is that it fails to check whether the value _has actually changed_ before firing the notification. This mock class shows one way to do that correctly.

    enum Sobe { APP4 = 1, APP5, STUDIO, SUP, APP6, STAND, STDNT, COMSTU, LUXSTU, APP4C, APP4L, APP62, APP6L }
    class VrstaSobeCena : INotifyPropertyChanged
    {
        decimal _price = 100 + (50 * (decimal)Rando.NextDouble());
        public decimal Cena
        {
            get => _price;
            set
            {
                if (!Equals(_price, value))
                {
                    _price = value;
                    OnPropertyChanged();
                }
            }
        }
        Sobe _sobe = 0;
        public Sobe Sobe
        {
            get => _sobe;
            set
            {
                if (!Equals(_sobe, value))
                {
                    _sobe = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

***
Finally, one way to test the two-way binding is to intercept the [Enter] key.

    private void onAnyTextBoxKeyDown(object sender, KeyEventArgs e)
    {
        if ((e.KeyCode == Keys.Enter) && (sender is TextBox textbox))
        {
            e.SuppressKeyPress = e.Handled = true;
            VrstaSobeCena vrstaSobeCena = (VrstaSobeCena)textbox.Tag;
            string msg = $"Price for {vrstaSobeCena.Sobe} is {vrstaSobeCena.Cena.ToString("F2")}";
            BeginInvoke((MethodInvoker)delegate {MessageBox.Show(msg); });
            SelectNextControl(textbox, forward: true, tabStopOnly: true, nested: false, wrap: true);
        }
    }

[![two-way binding][2]][2]


  [1]: https://i.stack.imgur.com/8VG10.png
  [2]: https://i.stack.imgur.com/1AdFk.png