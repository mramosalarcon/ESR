using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class menu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (DateTime.Now <= Convert.ToDateTime("10/03/2008"))
            {
                PopulateMenu();
            }
        }
    }

    private void PopulateMenu()
    {
        ESR.Business.Menu menuOpciones = new ESR.Business.Menu(Session["perfil"].ToString());
        DataSet dsMenu = menuOpciones.Carga();

        foreach (DataRow masterRow in dsMenu.Tables["Menu"].Rows)
        {
            if (masterRow["habilitado"].ToString() == "True")
            {
                TreeNode nodoPadre = new TreeNode(masterRow["nombre"].ToString());
                if (masterRow["nombre"].ToString() == "Documentos")
                    nodoPadre.NavigateUrl = masterRow["url"].ToString() + Session["idEmpresa"].ToString();
                else
                    nodoPadre.NavigateUrl = masterRow["url"].ToString();

                tvwMenu.Nodes.Add(nodoPadre);
                foreach (DataRow childRow in masterRow.GetChildRows("Children"))
                {
                    TreeNode nodoHijo = new TreeNode(childRow["nombre"].ToString());
                    nodoHijo.NavigateUrl = childRow["url"].ToString();
                    nodoPadre.ChildNodes.Add(nodoHijo);
                }
            }
        }
    }
    protected void tvwMenu_SelectedNodeChanged(object sender, EventArgs e)
    {
        TreeNode nodo = (TreeNode)sender;
        if (nodo.ChildNodes.Count > 0)
        {
            nodo.Expand();
        }
    }
}
