using System;
using System.Web.UI;
using Microsoft.SharePoint;

public partial class administradorDeIndicadores : System.Web.UI.Page
{
    /// <summary>
    /// Return the value of our Content URL parameter
    /// </summary>
    /// <returns></returns>
    private string GetContent()
    {
        if (Request.Params["Content"] == null)
            return "Home";
        else
            return Request.QueryString["Content"];
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
        string content = this.GetContent();
        Control contentControl = Page.LoadControl(content + ".ascx");
        if (contentControl != null)
        {
            this.indicador.Controls.Clear();
            this.indicador.Controls.Add(contentControl);
            SetTitle(content);
        }
    }
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        SPWeb Web = SPContext.Current.Web;
        string strUrl =
            Web.ServerRelativeUrl + "/_catalogs/masterpage/seattle.master";

        this.MasterPageFile = strUrl;
    }
}
