using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using ESR.Business;

public partial class Home_top : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblUsuariosConectados.Text = "Usuarios conectados: " + Application["TotalDeUsuarios"].ToString();
            lblUsuariosConectados.Visible = false;
            lblMensaje.Text = "AVISO IMPORTANTE. A todas las empresas participantes en el proceso ESR 2013 que hayan depositado a alguna de las cuentas del Cemefi, se les informa que es necesario que soliciten su recibo a la brevedad posible. La fecha límite para solicitar comprobantes con fecha del 2013 es el próximo 13 de diciembre. Después de esa fecha los comprobantes se emitirán con fecha 2014. Para mayor información les solicitamos atentamente contactar a la Srita. Janet González al teléfono 5276-85-30 extensión 154 o al correo electrónico janet.gonzalez@cemefi.org . Por su apoyo gracias.";
            //lblMensaje.Text = "AVISO IMPORTANTE. A todas las empresas participantes en el proceso ESR 2009 que hayan depositado a alguna de las cuentas del Cemefi, se les informa que es necesario que soliciten su recibo a la brevedad posible. La fecha límite para solicitar comprobantes con fecha del 2009 es el próximo 18 de diciembre. Después de esa fecha los comprobantes se emitirán con fecha 2010. Para mayor información les solicitamos atentamente contactar a la Srita. Janet González al teléfono 5276-85-30 extensión 154 o al correo electrónico janet.gonzalez@cemefi.org . Por su apoyo gracias.";
            //DateTime dtFechaDeVencimiento = Convert.ToDateTime("11/sep/2009 11:59");
            //lblTiempoRestante.Text = String.Format("{0:D} horas, {1:D} minutos", dtFechaDeVencimiento.Hour - DateTime.Now.Hour, dtFechaDeVencimiento.Minute - DateTime.Now.Minute);
	    //lblTiempoRestante.Text = "El plazo para liberar cuestionario (para obtener distintivo de 1 a 5 años) se ha extendido al Viernes 4 de diciembre de 2009 a las 23:59 horas";

            if (!IsPostBack)
            {
                MenuTop.Items[0].Text += Context.User.Identity.Name;

                int idEmpresa = 0;
                if (Request.QueryString["idEmpresa"] != null)
                    idEmpresa = Convert.ToInt32(Request.QueryString["idEmpresa"]);
                else
                    if (Session["idEmpresa"] != null)
                        idEmpresa = Convert.ToInt32(Session["idEmpresa"]);

                if (idEmpresa != 0)
                {
                    //Cargar el nombre de la Empresa
                    Empresa emp = new Empresa();
                    emp.idEmpresa = idEmpresa;
                    if (emp.Cargar())
                    {
                        lblNombreEmpresa.Text = emp.nombre + " - " + emp.nombreCorto;
                    }
                }
                PopulateMenu();
            }
        }
        catch (Exception ex)
        {
            using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
            {
                sw.AutoFlush = true;
                sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                sw.WriteLine("Empresa: " + Session["idEmpresa"].ToString());
                sw.WriteLine("Error en home_top.Page_Load(): " + ex.Message);
                sw.WriteLine("InnerException: " + ex.InnerException);
                sw.Close();
            }
        }
    }

    private void PopulateMenu()
    {
        ESR.Business.Menu menuTop = new ESR.Business.Menu();
        DataSet dst = menuTop.CargaTop();
        //int contador = 0;

        foreach (DataRow masterRow in dst.Tables["Menu"].Rows)
        {
            MenuItem masterItem = new MenuItem(masterRow["nombre"].ToString());
            masterItem.NavigateUrl = masterRow["url"].ToString();
            masterItem.Target = masterRow["target"].ToString();
            Menu1.Items.Add(masterItem);
        }
    }

    protected void MenuTop_MenuItemClick(object sender, MenuEventArgs e)
    {

    }

    protected void tmrConnections_Tick(object sender, EventArgs e)
    {

    }
}
