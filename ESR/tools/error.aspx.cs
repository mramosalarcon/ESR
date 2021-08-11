using System;

public partial class error : System.Web.UI.Page
{
    /// <summary>
    /// Return the value of our Content URL parameter
    /// </summary>
    /// <returns></returns>
    private string GetMessage()
    {
        if (Request.Params["message"] == null)
            return "Home";
        else
            return Request.QueryString["message"];
    }

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
    protected void Page_Load(object sender, EventArgs e)
    {
        string message = this.GetMessage();
        SetTitle("Mensaje de la aplicación Distintivo ESR®");
        lblErrorMessage.Text = message;
    }
}
