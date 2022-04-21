using System.Globalization;
using BL;

namespace UI;

partial class HomeForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(1200, 1000);
        this.Text = "Descriptive statistics";
        AutoScroll = true;
        CreateButtonToOpenTextBox();
        CreateResultTextBox();
    }

    private void CreateButtonToOpenTextBox()
    {
        var button = new Button
        {
            Text = "Enter data",
            Name = "Btn Enter data",
            Location = new Point(3, 3),
            Size = Constants.ButtonSize,
            Font = Constants.MicrosoftSansSerif()
        };

        button.Click += CreateTextBoxToWriteSelection;
        Controls.Add(button);
    }

    private const string TextBox_WriteData = nameof(TextBox_WriteData);
    private const string TB_Result = nameof(TB_Result);
    
    private void CreateTextBoxToWriteSelection(object sender, EventArgs e)
    {
        if (Controls.ContainsKey(TextBox_WriteData))
        {
            return;
        }

        var location = new Point(5, 58);
        var textBox = new TextBox
        {
            Name = TextBox_WriteData,
            Location = new Point(100, 61),
            Size = new Size(1000, 400),
            Font = Constants.MicrosoftSansSerif(),
            MaxLength = int.MaxValue,
            ScrollBars = ScrollBars.Vertical,
            Multiline = true
        };

        textBox.KeyPress += KeyPressWithDigitsAndComma;
        Controls.Add(textBox);

        var button = new Button
        {
            Text = "Send data",
            Name = "Btn Send data",
            Location = new Point(450, 467),
            Size = Constants.ButtonSize,
            Font = Constants.MicrosoftSansSerif()
        };

        button.Click += SendDataAndDisplayResult;
        
        Controls.Add(button);
    }

    private void SendDataAndDisplayResult(object sender, EventArgs eventArgs)
    {
        var textBox = Controls[TextBox_WriteData] as TextBox;
        var numbersInString = textBox.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var numbers = new List<double>();
        
        var label = Controls[TB_Result] as TextBox;

        foreach (var e in numbersInString)
        {
            var isOk = double.TryParse(e, NumberStyles.Any, CultureInfo.InvariantCulture, out var number);
            if (!isOk)
            {
                label.Text = $"Error with number: {e}";
                return;
            }
            else
            {
                numbers.Add(number);
            }
        }

        var statistic = Statistic.WithAllParams(numbers);
        label.Text = statistic.WriteAsText();
        CreatePictureBox(statistic);
    }
    
    
    private void CreateResultTextBox()
    {
        var result = new TextBox
        {
            Name = TB_Result,
            Location = new Point(300, 540),
            Size = new Size(600, 300),
            BorderStyle = BorderStyle.FixedSingle,
            Font = Constants.MicrosoftSansSerif(),
            ScrollBars = ScrollBars.Vertical,
            ReadOnly = true,
            Multiline = true
        };
        
        Controls.Add(result);
    }

    private void KeyPressWithDigitsAndComma(object sender, KeyPressEventArgs args)
    {
        args.Handled = !Char.IsDigit(args.KeyChar) && args.KeyChar != ',' && args.KeyChar != ' ' && args.KeyChar != '.';
    }
    
    
    private void CreatePictureBox(Statistic statistic)
    {
        var pictureBox = new PictureBox()
        {
            Name = "1",
            Location = new Point(300, 1000),
            Size = new Size(600, 600),
            BorderStyle = BorderStyle.FixedSingle
        };

        pictureBox.Paint += (sender, args) =>
        {
            var graphics = pictureBox.CreateGraphics();
            var pen = new Pen(Color.Black, 10F);

            foreach (var interval in statistic.Intervals)
            {
                var divider = statistic.Max - statistic.Min + 0.01;
                var height = (float) interval.GetRelativeFrequency(statistic.ElementsCount) * 1000;
                var leftDown = new PointF((float) (interval.Left / divider) * 1000, 0);
                var leftUp = new PointF((float) (interval.Left / divider) * 1000, height);
                var rightUp = new PointF((float) (interval.Right / divider) * 1000, height);
                var rightDown = new PointF((float) (interval.Right / divider) * 1000, 0);
                graphics.DrawLine(pen, leftDown, leftUp);
                graphics.DrawLine(pen, leftUp, rightUp);
                graphics.DrawLine(pen, rightUp, rightDown);
            }
        };
        
        Controls.Add(pictureBox);
    }
}