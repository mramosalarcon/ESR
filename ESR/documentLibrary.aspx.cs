using System;
using System.Configuration;
using System.IO;
using ESR.Business;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using System.Collections.Generic;

namespace ESR
{
    public partial class documentLibrary : System.Web.UI.Page
    {
        protected int GetIdEmpresa()
        {
            try
            {
                if (Request.Params["idEmpresa"] == null)
                    return Convert.ToInt32(Session["idEmpresa"].ToString());
                else
                    return Convert.ToInt32(Request.Params["idEmpresa"].ToString());
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
                {
                    sw.AutoFlush = true;
                    sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                    sw.WriteLine("Empresa: " + Session["idEmpresa"].ToString());
                    sw.WriteLine("Error en visorDelCuestionario.GetIdEmpresa(): " + ex.Message);
                    sw.WriteLine("InnerException: " + ex.InnerException);
                    sw.Close();
                    return -1;
                }
            }
        }

        protected string GetIdUsuario()
        {
            if (Request.Params["idUsuario"] == null)
                return Session["idUsuario"].ToString();
            else
                return Request.Params["idUsuario"].ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void tvwEvidencias_TreeNodePopulate(object sender, System.Web.UI.WebControls.TreeNodeEventArgs e)
        {
            if (e.Node.ChildNodes.Count == 0)
            {
                switch (e.Node.Depth)
                {
                    case 0:
                        PopulateCategories(e.Node);
                        break;
                    case 1:
                        PopulateEvidences(e.Node);
                        break;
                }
            }

        }

        void PopulateCategories(TreeNode node)
        {
            SPFolder myLibrary;
            
            using (SPSite oSite = new SPSite("http://esr.cemefi.org/"))
            {
                using (SPWeb oWeb = oSite.OpenWeb(GetIdEmpresa().ToString()))
                {
                    myLibrary = oWeb.GetFolder("Documentos compartidos");
                    SPFolderCollection myFolders = myLibrary.SubFolders;
                }
            }
            if (myLibrary.SubFolders.Count > 0)
            {
                foreach (SPFolder spfFolder in myLibrary.SubFolders)
                {
                    if (spfFolder.Name != "Forms")
                    {
                        TreeNode NewNode = new TreeNode(spfFolder.Name,
                            spfFolder.Name);
                        NewNode.PopulateOnDemand = true;
                        NewNode.SelectAction = TreeNodeSelectAction.Expand;
                        NewNode.Expanded = false;
                        node.ChildNodes.Add(NewNode);
                    }
                }
            }
            else
            {
                TreeNode treeNode2 = new TreeNode("\\", "\\");
                treeNode2.PopulateOnDemand = true;
                treeNode2.SelectAction = TreeNodeSelectAction.Expand;
                treeNode2.Expanded = false;
                node.ChildNodes.Add(treeNode2);
            }
            
        }

        void PopulateEvidences(TreeNode node)
        {
            SPFolder myLibrary;
            SPFileCollection myFiles; 

            using (SPSite oSite = new SPSite("http://esr.cemefi.org/"))
            {
                using (SPWeb oWeb = oSite.OpenWeb(GetIdEmpresa().ToString()))
                {
                    //Checar si el folder ya existe,
                    SPFolderCollection myFolders = oWeb.Folders;
                    myLibrary = oWeb.Folders["Documentos compartidos"];
                    myFiles = myLibrary.SubFolders[node.Value].Files;
                    List<SPFile> spFiles = new List<SPFile>();
                    foreach (SPFile myFile in myFiles)
                    {
                        spFiles.Add(myFile);
                    }
                    spFiles.Sort(delegate(SPFile p1, SPFile p2) { return p1.Name.CompareTo(p2.Name); });
                    foreach (SPFile spfFile in spFiles)
                    {
                        TreeNode NewNode = new TreeNode(spfFile.Name, spfFile.Url);
                        NewNode.PopulateOnDemand = false;
                        NewNode.SelectAction = TreeNodeSelectAction.None;
                        NewNode.ShowCheckBox = true;
                        node.ChildNodes.Add(NewNode);
                    }
                }
            }
        }

        protected void btnAsignarDocumentos_Click(object sender, EventArgs e)
        {
            bool result = false;
            StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString());
            sw.AutoFlush = true;
            TreeNode trnFoldersPadres = tvwEvidencias.Nodes[0];
            //sw.WriteLine("Nodos padres: " + trnFoldersPadres.ChildNodes.Count);
            foreach (TreeNode FolderNode in trnFoldersPadres.ChildNodes)
            {
                //sw.WriteLine("Nodo padre: " + FolderNode.Text);
                foreach (TreeNode childNode in FolderNode.ChildNodes)
                {
                    //sw.WriteLine("Nodo hijo: " + childNode.Text);
                    if (childNode.Checked)
                    {
                        //sw.WriteLine("Seleccionado");
                        try
                        {
                            Indicador respuestaIndicador = new Indicador();
                            respuestaIndicador.idIndicador = Convert.ToInt32(Request.QueryString["idIndicador"]);
                            respuestaIndicador.idTema = Convert.ToInt32(Request.QueryString["idTema"]);
                            respuestaIndicador.idTipoEvidencia = Convert.ToInt32(Request.QueryString["idTipoEvidencia"]);

                            // MRA 27/10/2010
                            // Esta linea esta rara
                            // ¿Que pasa si sube un documento una empresa que no es la que se esta consultando?

                            respuestaIndicador.idEmpresa = Convert.ToInt32(Session["idEmpresa"].ToString());
                            respuestaIndicador.idCuestionario = Convert.ToInt32(Request.QueryString["idCuestionario"]);
                            //sw.WriteLine("fileName: " + childNode.Text);
                            respuestaIndicador.fileName = childNode.Text;
                            //sw.WriteLine("Url: " + childNode.Value);
                            respuestaIndicador.Url = childNode.Value;
                            respuestaIndicador.idUsuario = Session["idUsuario"].ToString();
                            respuestaIndicador.idTipoRespuesta = Convert.ToInt32(Request.QueryString["idTipoRespuesta"]);
                            //sw.WriteLine("Guardar evidencia antes: " + result);
                            result = respuestaIndicador.GuardarEvidencia();
                            //sw.WriteLine("Guardar evidencia despues: " + result);
                        }
                        catch (Exception ex)
                        {
                            sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                            sw.WriteLine("Empresa: " + Session["idEmpresa"].ToString());
                            sw.WriteLine("idIndicador: " + Request.QueryString["idIndicador"]);
                            sw.WriteLine("idTema: " + Request.QueryString["idTema"]);
                            sw.WriteLine("idCuestionario: " + Request.QueryString["idCuestionario"]);
                            sw.WriteLine("Error en visorDelCuestionario.Page_Load(): " + ex.Message);
                            sw.WriteLine("InnerException: " + ex.InnerException);
                        }
                    }
                }
            }
            if (result)
            {
                lblMensaje.Text = "Las referencias fueron almacenadas correctamente. <input type=\"button\" onclick=\"opener.location.reload(true);self.close();\" value=\"Cerrar\" name=\"btnCerrar\">";
                //imgWait.Visible = false;
            }
            else
            {
                lblMensaje.Text = "Hubo un error al subir las referencias, intente de nuevo. <input type=\"button\" onclick=\"opener.location.reload(true);self.close();\" value=\"Cerrar\" name=\"btnCerrar\">";
                //imgWait.Visible = false;
            }
            sw.Close();
        }
    }
}