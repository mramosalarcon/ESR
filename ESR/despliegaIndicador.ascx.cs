using System;

public partial class despliegaIndicador : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
        }
    }

    protected void rbTiposDeRespuestaInciso_CheckedChanged(object sender, EventArgs e)
    {
        //RadioButton control = (RadioButton)sender;
        //Control pan = control.Parent;

        //if (control.ID.Substring(2, 2).ToUpper() != "NO")
        //    pan.Controls[5].Visible = true;
        //else
        //    pan.Controls[5].Visible = false;
    }

    protected void rbInciso_CheckedChanged(object sender, EventArgs e)
    {
        //RadioButton control = (RadioButton)sender;
        //Control pan = control.Parent;

        //foreach (Control ctrl in pan.Controls)
        //{
        //    if (ctrl.GetType() == typeof(UpdatePanel))
        //    {
        //        ctrl.Visible = false;
        //    }
        //}

        //if (control.ID.Substring(2, 2).ToUpper() != "NO")
        //{
        //    int idTiposEvidencia = pan.Controls.IndexOf(control);
        //    idTiposEvidencia++;
        //    pan.Controls[idTiposEvidencia].Visible = true;

        //}
    }


    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {

    }
}
