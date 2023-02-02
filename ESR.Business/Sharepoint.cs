using System;
using System.Configuration;
using System.IO;
using Microsoft.SharePoint;

namespace ESR.Business
{
    public class SharePoint
    {
        public object GetDocuments(string idEmpresa)
        {
            using (SPSite site = new SPSite("https://esrv1.cemefi.org/" + idEmpresa))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists["Documentos Compartidos"];
                    return list.Items;
                }
            }
        }
        //sp.CreateSite("https://esrv1.cemefi.org", emp.idEmpresa.ToString(), emp.nombre, emp.nombreCorto
        public int CreateSite(string parentSiteURL, string siteURLRequested, string siteTitle, string siteDescription)
        {
            //const Int32 LOCALE_ID_ENGLISH = 1033;
            const Int32 LOCALE_ID_SPANISH = 3082;

            //StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logSharepoint"].ToString());
            //sw.AutoFlush = true;
            int returnValue = -1;

            //sw.WriteLine("CreateSite(): Usuario: " + System.Security.Principal.WindowsIdentity.GetCurrent().Name + ", " + DateTime.Now.ToString());
            //sw.Flush();

            try
            {
                //sw.WriteLine("Agregar sitio a la colección: " + parentSiteURL);

                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite oSiteCollection = new SPSite(parentSiteURL))
                    {
                        if (!oSiteCollection.AllWebs[siteURLRequested].Exists)
                        {
                            SPWeb parentWeb = oSiteCollection.OpenWeb();
                            //sw.WriteLine("Abrir sitio: " + parentSiteURL);
                            parentWeb.AllowUnsafeUpdates = true;
                            //sw.WriteLine("AllowUnsafeUpdates");
                            parentWeb.Update();
                            //sw.WriteLine("Update");

                            SPWebTemplateCollection Templates =
                                oSiteCollection.GetWebTemplates(Convert.ToUInt32(LOCALE_ID_SPANISH));
                            //sw.WriteLine("GetWebTemplates");
                            //foreach (SPWebTemplate spwt in Templates)
                            //{
                            //    sw.WriteLine(spwt.ID + "-" + spwt.Description +"-" + spwt.Name + "-" + spwt.Title);
                            //}
                            SPWebTemplate siteTemplate = Templates["STS#0"];
                            //sw.WriteLine("siteTemplate");
                            //if (parentWeb.Webs[siteURLRequested].Exists)
                            //{
                            //    parentWeb.Webs.Delete(siteURLRequested);
                            //    sw.WriteLine("Se eliminó el sitio: " + siteURLRequested);
                            //}
                            //STS#0 - Team Site
                            //STS#1 - Blank Site
                            //STS#2 - Document Workspace
                            //MPS#0 - Basic Meeting Workspace
                            //MPS#1 - Blank Meeting Workspace
                            //MPS#2 - Decision Meeting Workspace
                            //MPS#3 - Social Meeting Workspace
                            //MPS#4 - Multipage Meeting Workspace
                            //WIKI#0 - Wiki
                            //BLOG#0 - Blog

                            parentWeb.Webs.Add(
                                siteURLRequested,
                                siteTitle,
                                siteDescription,
                                parentWeb.Language,
                                "STS#0",
                                true, false);

                            ////Asignar permisos de diseñador al responsable de empresa
                            //SPRoleAssignment spRoleAssignment = new SPRoleAssignment(idUsuario, sNombre, sNombre, "añadido desde el programa");
                            ////Using Contribute, might need high access

                            //SPRoleDefinition spSPRoleDefinition =
                            //    oWebSite.RoleDefinitions["Diseño"];

                            //spRoleAssignment.RoleDefinitionBindings.Add(spSPRoleDefinition);
                            //oWebSite.RoleAssignments.Add(spRoleAssignment);
                            
                            parentWeb.Update();

                            //sw.WriteLine("Se agregó el sitio: " + siteURLRequested);
                            //sw.Close();
                        }
                        returnValue = 0;
                    }
                });
                return returnValue;
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
                {
                    sw.AutoFlush = true;
                    sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                    sw.WriteLine("Error en ESR.Business.Sharepoint.CreateSite(" + siteURLRequested + " ): " + ex.Message);
                    sw.Close();
                }
                throw new Exception("Error en misCuestionarios.PopulateMenu(" + siteURLRequested + " ): " + ex.Message);
            }
        }

        public int DeleteSite(string parentSiteURL, string siteURLToDelete)
        {
            int returnValue = -1;
            StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logSharepoint"].ToString());

            sw.WriteLine("Usuario de ejecución: " + System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            sw.Flush();
            try
            {
                sw.WriteLine("Eliminar sitio: " + siteURLToDelete + " de la colección: " + parentSiteURL);
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite oSiteCollection = new SPSite(parentSiteURL))
                    {
                        SPWeb parentWeb = oSiteCollection.OpenWeb();
                        sw.WriteLine("Abrir sitio: " + parentSiteURL);
                        parentWeb.AllowUnsafeUpdates = true;

                        Microsoft.SharePoint.Administration.SPWebApplication webApp = parentWeb.Site.WebApplication;
                        webApp.FormDigestSettings.Enabled = false;
                        if (parentWeb.Webs[siteURLToDelete].Exists)
                        {
                            parentWeb.Webs.Delete(siteURLToDelete);
                            sw.WriteLine("Se eliminó el sitio: " + siteURLToDelete);
                            parentWeb.Update();
                        }
                        webApp.FormDigestSettings.Enabled = true;
                        returnValue = 0;
                    }
                });
                return returnValue;
            }
            catch (Exception ex)
            {
                sw.Flush();
                sw.WriteLine("Ocurrió el siguiente error en ESR.Sharepoint.DeleteSite(): " + ex.Message + " Inner Exception: " + ex.InnerException);
                sw.Flush();
                sw.Close();
                return returnValue;
            }
        }
    }
}
