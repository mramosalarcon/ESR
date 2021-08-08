using System;
using System.Web.UI;

public partial class registroDePractica : System.Web.UI.Page
{
    /// <summary>
    /// Set the title of the Content section using the Content string being loaded.
    /// </summary>
    /// <param name="title"></param>
    private void SetTitle(string content)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        foreach (char c in content)
        {
            if (Char.IsUpper(c))
                sb.Append(' ');
            sb.Append(c);
        }

        this.Title = sb.ToString();
    }

    /// <summary>
    /// Return the value of our Content URL parameter
    /// </summary>
    /// <returns></returns>
    private string GetContent()
    {
        if (Request.Params["Content"] != null)
            return Request.QueryString["Content"];
        else
            return string.Empty;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string content = GetContent();
        if (content != null && content != string.Empty)
        {
            Control contentControl = Page.LoadControl(content + ".ascx");
            this.Practica.Controls.Clear();
            this.Practica.Controls.Add(contentControl);
            SetTitle(content);

        }
    }
}
