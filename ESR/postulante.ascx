<%@ Control Language="C#" AutoEventWireup="true" Inherits="postulante" Codebehind="postulante.ascx.cs" %>
<link href="Style/StyleSheet.css" rel="stylesheet" type="text/css" />
<form method="POST" action="--WEBBOT-SELF--">
			<!--webbot bot="SaveResults" U-File="C:\Documents and Settings\mar02168\My Documents\Getronics\CEMEFI\Ingeniería\03 Construcción\_private\form_results.csv" S-Format="TEXT/CSV" S-Label-Fields="TRUE" -->
<table border="1" width="100%" id="table1">
	<tr>
		<td colspan="4" align="left">Sección 2 - Postulante</td>
	</tr>
	<tr>
		<td>Razón social</td>
		<td>
			<p><input type="text" name="T1" size="20"></p>
		</td>
		<td>Siglas</td>
		<td><input type="text" name="T2" size="20"></td>
	</tr>
	<tr>
		<td>Domicilio principal</td>
		<td><input type="text" name="T3" size="20"></td>
		<td>Colonia/Barrio</td>
		<td><input type="text" name="T4" size="20"></td>
	</tr>
	<tr>
		<td>Código Postal</td>
		<td><input type="text" name="T5" size="20"></td>
		<td>Teléfono(s)</td>
		<td><input type="text" name="T6" size="20"></td>
	</tr>
	<tr>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
		<td>Fax</td>
		<td><input type="text" name="T7" size="20"></td>
	</tr>
	<tr>
		<td>Ciudad</td>
		<td><input type="text" name="T12" size="20"></td>
		<td>Estado/Provincia</td>
		<td><input type="text" name="T10" size="20"></td>
	</tr>
	<tr>
		<td>País</td>
		<td><input type="text" name="T11" size="20"></td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
	<tr>
		<td height="27"></td>
		<td height="27"></td>
		<td height="27"></td>
		<td height="27">&nbsp;</td>
	</tr>
	<tr>
		<td>
            Página electrónica</td>
		<td>
            http://www.<input name="T9" size="20" type="text" /></td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
	<tr>
		<td>Nombre del contacto</td>
		<td><input type="text" name="T13" size="20"></td>
		<td>Presidente ejecutivo o Director General</td>
		<td><input type="text" name="T14" size="20"></td>
	</tr>
	<tr>
		<td>Cargo</td>
		<td><input type="text" name="T15" size="20"></td>
		<td>Correo Electrónico</td>
		<td><input type="text" name="T16" size="20"></td>
	</tr>
	<tr>
		<td>Sector</td>
		<td><select size="1" name="ddlSector">
		<option selected value="0">Seleccione...</option>
		<option value="1">Organización Sociedad Civil</option>
		<option value="2">Organismo multilateral</option>
		<option value="3">Organismo empresarial</option>
		<option value="4">Otro (especifique)</option>
		</select></td>
		<td><input type="text" name="T17" size="20"></td>
		<td>&nbsp;</td>
	</tr>
	<tr>
		<td height="23" colspan="2">Principales productos y/o servicios</td>
		<td height="23" colspan="2"><textarea rows="2" name="S1" style="width: 315px"></textarea></td>
	</tr>
	<tr>
		<td height="23" colspan="2">
            ¿Porqué considera a esta como una práctica ejemplar de Responsabilidad Social de la 
		empresa?</td>
		<td height="23" colspan="2"><textarea rows="2" name="S2" style="width: 315px"></textarea></td>
	</tr>
	<tr>
		<td colspan="4">
		<p align="center"><input type="submit" value="Submit" name="B1"><input type="reset" value="Reset" name="B2"></td>
	</tr>
</table>
</form>
