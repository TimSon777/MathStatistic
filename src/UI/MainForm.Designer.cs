using System.Globalization;
using BL;

namespace UI;

partial class MainForm
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
        components = new System.ComponentModel.Container();
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(Constants.ButtonSize.Width + Constants.DefaultMargin * 2, Constants.ButtonSize.Height + Constants.DefaultMargin * 2);
        Text = "Descriptive statistics";
        AutoScroll = true;
        CreateButtonToOpenDataForm(Constants.DefaultMargin);
    }

    private void CreateButtonToOpenDataForm(int leftMargin)
    {
        var button = new Button
        {
            Text = "Open form to enter data",
            Name = "Btn Open form to enter data",
            Location = new Point(leftMargin, Constants.DefaultMargin),
            Size = Constants.ButtonSize,
            Font = Constants.MicrosoftSansSerif()
        };

        button.Click += OpenFormToWriteData;
        Controls.Add(button);
    }
    
    private void OpenFormToWriteData(object sender, EventArgs e)
    {
        new DataForm().Show();
    }
}