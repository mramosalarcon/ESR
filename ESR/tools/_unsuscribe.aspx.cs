using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ESR.Business;

public class _unsubscribe : Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		try
		{
			Usuario usuario = new Usuario();
			if (base.Request.QueryString["token"] != null)
			{
				usuario.idUsuario = base.Request["token"].ToString();
				if (!usuario.unsuscribe())
				{
					lblMensaje.Text = "No pudimos eliminar tu correo de la lista de distribución, por favor comunicate al área de RSE del Cemefi - tel. (55) 10541479, para ser removido manualmente.";
				}
			}
			else
			{
				lblMensaje.Text = "No se recibió información para remover.";
			}
		}
		catch (Exception ex)
		{
			lblMensaje.Text = ex.Message;
		}
	}
}
