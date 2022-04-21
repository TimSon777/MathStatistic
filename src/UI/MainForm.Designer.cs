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
        ClientSize = new Size(Constants.ButtonSize.Width * 2 + Constants.DefaultMargin * 3, Constants.ButtonSize.Height + Constants.DefaultMargin * 2);
        Text = "Descriptive statistics";
        AutoScroll = true;
        CreateButtonToOpenDataForm(Constants.DefaultMargin);
        CreateButtonToOpenHttpForm(Constants.DefaultMargin + Constants.ButtonSize.Width);
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
    
    private void CreateButtonToOpenHttpForm(int leftMargin)
    {
        var button = new Button
        {
            Text = "Open form to get data from the Internet",
            Name = "Btn Open form to get data from the Internet",
            Location = new Point(leftMargin, Constants.DefaultMargin),
            Size = Constants.ButtonSize,
            Font = Constants.MicrosoftSansSerif()
        };

        button.Click += OpenFormToGetDataFromInternet;
        Controls.Add(button);
    }

    private void OpenFormToWriteData(object sender, EventArgs e)
    {
        new DataForm().Show();
    }
    
    private void OpenFormToGetDataFromInternet(object sender, EventArgs e)
    {
        new HttpForm().Show();
    }
}