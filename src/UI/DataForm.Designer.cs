using System.ComponentModel;
using BL;
using System.Globalization;

namespace UI;

partial class DataForm
{
    private IContainer components = null;
    
    private const string TB_WriteData = nameof(TB_WriteData);
    private const string TB_Message = nameof(TB_Message);
    private const string TB_Result = nameof(TB_Result);
    
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
        components = new System.ComponentModel.Container();
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = Constants.FormDefaultSize;
        Text = "DataForm";
        AutoScroll = true;
        CreateTextBoxToWriteSelection();
        CreateResultTextBox();
    }
    
    private void CreateTextBoxToWriteSelection()
    { 
        var location = new Point(5, 58);
        var enterDataTextBox = new TextBox
        {
            ReadOnly = true,
            Text = "Enter data with separator dot...",
            Name = TB_Message,
            Location = new Point(100, 50),
            Size = new Size(1000,100),
            Font = Constants.MicrosoftSansSerif(12),
            BorderStyle = BorderStyle.None
        };
        
        Controls.Add(enterDataTextBox);
        
        var textBox = new TextBox
        {
            Name = TB_WriteData,
            Location = new Point(100, 80 + Constants.DefaultMargin),
            Size = Constants.TextBoxDefaultSize,
            Font = Constants.MicrosoftSansSerif(),
            MaxLength = int.MaxValue,
            ScrollBars = ScrollBars.Vertical,
            Multiline = true,
        };

        Controls.Add(textBox);

        var button = new Button
        {
            Text = "Send data",
            Name = "Btn Send data",
            Location = new Point(450, 290 + Constants.DefaultMargin),
            Size = Constants.ButtonSize,
            Font = Constants.MicrosoftSansSerif()
        };

        button.Click += SendDataAndDisplayResult;
        
        Controls.Add(button);
    }

    private void SendDataAndDisplayResult(object sender, EventArgs eventArgs)
    {
        var textBox = Controls[TB_WriteData] as TextBox;
        var numbersInString = textBox.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var numbers = new List<double>();
        
        var label = Controls[TB_Result] as TextBox;

        foreach (var e in numbersInString)
        {
            var isOk = double.TryParse(e, out var number);
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
    }
    
    private void CreateResultTextBox()
    {
        var result = new TextBox
        {
            Name = TB_Result,
            Location = new Point(100, 350 + Constants.DefaultMargin),
            Size = Constants.TextBoxDefaultSize,
            BorderStyle = BorderStyle.FixedSingle,
            Font = Constants.MicrosoftSansSerif(),
            ScrollBars = ScrollBars.Vertical,
            ReadOnly = true,
            Multiline = true
        };
        
        Controls.Add(result);
    }
}