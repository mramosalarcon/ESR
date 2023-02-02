using System;
using System.Data;
using System.Web.UI;
using ESR.Business;

namespace ESR
{
    public partial class ESRMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblUsuario.Text += Context.User.Identity.Name;
                Empresa empresas = new Empresa();
                empresas.idUsuario = Session["idUsuario"].ToString();
                DataSet dsEmpresa = empresas.CargaEmpresas();
                ddlEmpresas.DataSource = dsEmpresa;
                ddlEmpresas.DataTextField = "nombre";
                ddlEmpresas.DataValueField = "idEmpresa";
                ddlEmpresas.DataBind();

                ddlEmpresas.SelectedValue = Session["idEmpresa"].ToString();
                DataRow[] drSector = dsEmpresa.Tables[0].Select("idEmpresa = " + Session["idEmpresa"].ToString());
                if (drSector.GetLength(0) > 0)
                {
                    lblSector.Text += drSector[0]["Sector"].ToString();
                    lblSubsector.Text += drSector[0]["Subsector"].ToString();
                }
                //lblEmpresa.Text = "";


                //    if (Session["perfil"].ToString().IndexOf('2') > -1 || Session["perfil"].ToString().IndexOf('3') > -1 ||
                //        Session["perfil"].ToString().IndexOf('4') > -1 || Session["perfil"].ToString().IndexOf('5') > -1)
                //    {
                //        //PopulateMenuCEMEFI();
                //    }
                //    else
                //    {
                //        //PopulateMenuEmpresa();
                //    }
            }
        }

        protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["idEmpresa"] = ddlEmpresas.SelectedValue;
            Response.Redirect("main.aspx", false);
        }

        protected void imbSitio_Click(object sender, ImageClickEventArgs e)
        {
            imbSitio.ImageUrl = "images/ESRbotonMenuInteriorOver_Sitio.jpg";
            imbSitio.Attributes["onmouseout"] = "'images/ESRbotonMenuInteriorOver_Sitio.jpg'";
            Response.Write("<script>window.open('https://esrv1.cemefi.org/" + Session["idEmpresa"].ToString() + "','newwin');</script>");
        }

        //private void PopulateMenuCEMEFI()
        //{
        //    ESR.Business.Menu menuOpciones = new ESR.Business.Menu(Session["perfil"].ToString());
        //    DataSet dsMenu = menuOpciones.Carga();

        //    foreach (DataRow masterRow in dsMenu.Tables["Menu"].Rows)
        //    {
        //        if (masterRow["habilitado"].ToString() == "True")
        //        {
        //            MenuItem nodoPadre = new MenuItem(masterRow["nombre"].ToString());
        //            if (masterRow["nombre"].ToString() == "Documentos")
        //                nodoPadre.NavigateUrl = masterRow["url"].ToString() + Session["idEmpresa"].ToString();
        //            else
        //                nodoPadre.NavigateUrl = masterRow["url"].ToString();

        //            //tvwMenu.Nodes.Add(nodoPadre);
        //            tvwMenu.Items.Add(nodoPadre);
        //            foreach (DataRow childRow in masterRow.GetChildRows("Children"))
        //            {
        //                MenuItem nodoHijo = new MenuItem(childRow["nombre"].ToString());
        //                nodoHijo.NavigateUrl = childRow["url"].ToString();
        //                nodoPadre.ChildItems.Add(nodoHijo);
        //            }
        //        }
        //    }
        //}
    }
}
