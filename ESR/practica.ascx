<%@ Control Language="C#" AutoEventWireup="true" Inherits="practica" Codebehind="practica.ascx.cs" %>
<link href="Style/StyleSheet.css" rel="stylesheet" type="text/css" />
<form method="POST" action="--WEBBOT-SELF--" enctype="multipart/form-data">
			<!--webbot bot="FileUpload" U-File="C:\Documents and Settings\mar02168\My Documents\Getronics\CEMEFI\Ingeniería\03 Construcción\_private\form_results.csv" S-Format="TEXT/CSV" S-Label-Fields="TRUE" -->
<table border="0" width="100%" id="table1">
    <tr>
        <td align="left" colspan="4">
            &nbsp;</td>
    </tr>
	<tr>
		<td colspan="4" align="left">Sección 3 - Postulación de Práctica</td>
	</tr>
	<tr>
		<td colspan="2">Favor de indicar las categorías a las cuales se está 
		postulando</td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
	<tr>
		<td>&nbsp;</td>
		<td><input type="checkbox" name="C1" value="ON">Calidad de vida en la 
		empresa</td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
	<tr>
		<td>&nbsp;</td>
		<td><input type="checkbox" name="C2" value="ON">Vinculación con la 
		comunidad (ciudadanía corporativa)</td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
	<tr>
		<td>&nbsp;</td>
		<td><input type="checkbox" name="C3" value="ON">Cuidado y preservación 
		del medio ambiente</td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
	<tr>
		<td>&nbsp;</td>
		<td><input type="checkbox" name="C4" value="ON">Aplicación de los 
		principios del pacto mundial</td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
    <tr>
        <td>
        </td>
        <td>
            <input type="checkbox" name="C4" value="ON">Negocios en la base de la pirámide</td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <input type="checkbox" name="C4" value="ON">Ética empresarial</td>
        <td>
        </td>
        <td>
        </td>
    </tr>
	<tr>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
	<tr>
        <td colspan="2" height="27">
            Nombre de la práctica</td>
		<td height="27">&nbsp;<input type="text" name="T1" size="20"></td>
		<td height="27">&nbsp;</td>
	</tr>
	<tr>
        <td colspan="2">
            Breve descripción de la práctica&nbsp;</td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
	<tr>
		<td>&nbsp;</td>
		<td><textarea rows="4" name="S1" cols="41"></textarea></td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
	<tr>
		<td>Beneficiarios</td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
	<tr>
        <td colspan="2">
            Número total de personas que se benefician</td>
		<td>&nbsp;<input type="text" name="T2" size="20"></td>
		<td>&nbsp;</td>
	</tr>
	<tr>
        <td colspan="2">
            Población beneficiada&nbsp;</td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
	<tr>
		<td>A) Edad</td>
		<td><select size="1" name="ddlBeneficiadaEdad">
		<option value="0" selected>Seleccione...</option>
		<option value="1">Jóvenes (17-29 años)</option>
		<option value="2">Adultos (30-64 años)</option>
		<option value="3">Adultos mayores (65 años o +)</option>
		<option value="4">Población en general</option>
		</select></td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
	<tr>
		<td>B) Problemática</td>
		<td><input type="checkbox" name="C5" value="ON">Adicciones</td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
	<tr>
		<td>&nbsp;</td>
		<td><input type="checkbox" name="C6" value="ON">Discapacidad intelectual</td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
	<tr>
		<td height="23">&nbsp;</td>
		<td height="23"><input type="checkbox" name="C7" value="ON">Enfermos 
		terminales</td>
		<td height="23">&nbsp;</td>
		<td height="23">&nbsp;</td>
	</tr>
	<tr>
		<td height="23">&nbsp;</td>
		<td height="23"><input type="checkbox" name="C8" value="ON">Convictos y 
		Ex Convictos</td>
		<td height="23">&nbsp;</td>
		<td height="23">&nbsp;</td>
	</tr>
	<tr>
		<td height="23">&nbsp;</td>
		<td height="23"><input type="checkbox" name="C9" value="ON">En situación 
		de calle</td>
		<td height="23">&nbsp;</td>
		<td height="23">&nbsp;</td>
	</tr>
	<tr>
		<td height="23">&nbsp;</td>
		<td height="23"><input type="checkbox" name="C10" value="ON">Escasos 
		recursos</td>
		<td height="23">&nbsp;</td>
		<td height="23">&nbsp;</td>
	</tr>
	<tr>
		<td height="23">&nbsp;</td>
		<td height="23"><input type="checkbox" name="C11" value="ON">Medio 
		ambiente y animales</td>
		<td height="23">&nbsp;</td>
		<td height="23">&nbsp;</td>
	</tr>
	<tr>
		<td height="25">&nbsp;</td>
		<td height="25"><input type="checkbox" name="C12" value="ON">Madres 
		solteras</td>
		<td height="25">&nbsp;</td>
		<td height="25">&nbsp;</td>
	</tr>
	<tr>
		<td height="24">&nbsp;</td>
		<td height="24"><input type="checkbox" name="C13" value="ON">Discapacidad 
		física</td>
		<td height="24">&nbsp;</td>
		<td height="24">&nbsp;</td>
	</tr>
	<tr>
		<td height="24">&nbsp;</td>
		<td height="24"><input type="checkbox" name="C14" value="ON">Enfermos</td>
		<td height="24">&nbsp;</td>
		<td height="24">&nbsp;</td>
	</tr>
	<tr>
		<td height="24">&nbsp;</td>
		<td height="24"><input type="checkbox" name="C15" value="ON">Familiar</td>
		<td height="24">&nbsp;</td>
		<td height="24">&nbsp;</td>
	</tr>
	<tr>
		<td height="24">&nbsp;</td>
		<td height="24"><input type="checkbox" name="C16" value="ON">Maltrato/Violencia</td>
		<td height="24">&nbsp;</td>
		<td height="24">&nbsp;</td>
	</tr>
	<tr>
		<td height="24">&nbsp;</td>
		<td height="24"><input type="checkbox" name="C17" value="ON">Sin hogar / 
		Abandono</td>
		<td height="24">&nbsp;</td>
		<td height="24">&nbsp;</td>
	</tr>
	<tr>
		<td height="24">&nbsp;</td>
		<td height="24"><input type="checkbox" name="C18" value="ON">VIH / SIDA</td>
		<td height="24">&nbsp;</td>
		<td height="24">&nbsp;</td>
	</tr>
	<tr>
		<td height="24">&nbsp;</td>
		<td height="24"><input type="checkbox" name="C19" value="ON">Otra:
		<input type="text" name="T3" size="20"></td>
		<td height="24">&nbsp;</td>
		<td height="24">&nbsp;</td>
	</tr>
	<tr>
		<td height="24">C) Género</td>
		<td height="24"><select size="1" name="ddlGenero">
		<option selected value="0">Seleccione...</option>
		<option value="1">Hombres</option>
		<option value="2">Mujeres</option>
		<option value="3">Ambos</option>
		</select></td>
		<td height="24">&nbsp;</td>
		<td height="24">&nbsp;</td>
	</tr>
	<tr>
		<td height="24">D) Población</td>
		<td height="24"><select size="1" name="ddlPoblacion">
		<option selected value="0">Seleccione...</option>
		<option value="1">Urbana</option>
		<option value="2">Rural</option>
		<option value="3">Mixta</option>
		</select></td>
		<td height="24">&nbsp;</td>
		<td height="24">&nbsp;</td>
	</tr>
	<tr>
        <td colspan="2" height="24">
            Implementada desde</td>
		<td height="24">&nbsp;<input type="text" name="T4" size="20"></td>
		<td height="24">&nbsp;</td>
	</tr>
	<tr>
        <td colspan="2" height="24">
            Área / Departamento de la Empresa en que se aplica</td>
		<td height="24">&nbsp;<input type="text" name="T5" size="20"></td>
		<td height="24">&nbsp;</td>
	</tr>
    <tr>
        <td colspan="2" height="24">
            Área de acción en la que se desarrolla</td>
        <td height="24">
        </td>
        <td height="24">
        </td>
    </tr>
	<tr>
		<td height="24"></td>
		<td height="24"><input type="checkbox" name="C20" value="ON">Arte y 
		cultura</td>
		<td height="24"><input type="checkbox" name="C23" value="ON">Bienestar 
		social</td>
		<td height="24"><input type="checkbox" name="C26" value="ON">Ciencia y 
		tecnología</td>
	</tr>
	<tr>
		<td height="24">&nbsp;</td>
		<td height="24"><input type="checkbox" name="C21" value="ON">Derechos 
		humanos</td>
		<td height="24"><input type="checkbox" name="C24" value="ON">Desarrollo</td>
		<td height="24"><input type="checkbox" name="C27" value="ON">Educación</td>
	</tr>
	<tr>
		<td height="24">&nbsp;</td>
		<td height="24"><input type="checkbox" name="C22" value="ON">Medio 
		Ambiente</td>
		<td height="24"><input type="checkbox" name="C25" value="ON">Salud</td>
		<td height="24"><input type="checkbox" name="C28" value="ON">Otra:
		<input type="text" name="T6" size="20"></td>
	</tr>
	<tr>
        <td colspan="2" height="24">
            Objetivo de la práctica</td>
		<td height="24">&nbsp;<textarea rows="4" name="S2" cols="41"></textarea></td>
		<td height="24">&nbsp;</td>
	</tr>
	<tr>
		<td height="23">Status</td>
		<td height="23"><input type="text" name="T7" size="20"></td>
		<td height="23">Nueva iniciativa (1 año de implementación)</td>
		<td height="23"><input type="text" name="T8" size="20"></td>
	</tr>
	<tr>
		<td height="25">&nbsp;</td>
		<td height="25">&nbsp;</td>
		<td height="25">Iniciativa revisada (más de un año de operación y con 
		mejoras)</td>
		<td height="25"><input type="text" name="T9" size="20"></td>
	</tr>
	<tr>
		<td height="24">&nbsp;</td>
		<td height="24">&nbsp;</td>
		<td height="24">Iniciativa en proceso de revisión</td>
		<td height="24"><input type="text" name="T10" size="20"></td>
	</tr>
    <tr>
        <td colspan="2" height="24">
            Evalúa periódicamente la práctica</td>
        <td height="24">
        </td>
        <td height="24">
        </td>
    </tr>
	<tr>
		<td height="24"></td>
		<td height="24"><input type="radio" value="1" checked name="optEvalua">Si
		<input type="radio" name="optEvalua" value="0">No</td>
		<td height="24">Reporta sobre ella en su informe anual</td>
		<td height="24"><input type="radio" name="optReporta" value="1" checked>Si
		<input type="radio" name="optReporta" value="0">No</td>
	</tr>
	<tr>
		<td height="24">Participantes</td>
		<td height="24"><input type="checkbox" name="C29" value="ON">Accionistas</td>
		<td height="24"><input type="checkbox" name="C33" value="ON">Directivos</td>
		<td height="24"><input type="checkbox" name="C37" value="ON">Gerentes</td>
	</tr>
	<tr>
		<td height="24">&nbsp;</td>
		<td height="24"><input type="checkbox" name="C30" value="ON">Personal 
		operativo y/o producción</td>
		<td height="24"><input type="checkbox" name="C34" value="ON">Consumidores 
		finales</td>
		<td height="24"><input type="checkbox" name="C38" value="ON">Proveedores</td>
	</tr>
	<tr>
		<td height="24">&nbsp;</td>
		<td height="24"><input type="checkbox" name="C31" value="ON">Organizaciones 
		sociales</td>
		<td height="24"><input type="checkbox" name="C35" value="ON">Sindicato</td>
		<td height="24"><input type="checkbox" name="C39" value="ON">Organismos 
		gubernamentales</td>
	</tr>
	<tr>
		<td height="24">&nbsp;</td>
		<td height="24"><input type="checkbox" name="C32" value="ON">Familiares 
		del personal</td>
		<td height="24"><input type="checkbox" name="C36" value="ON">Otros</td>
		<td height="24">&nbsp;</td>
	</tr>
	<tr>
        <td colspan="2" height="24">
            Principio o política de la Empresa a la que responde con 
		la práctica</td>
		<td height="24">&nbsp;<textarea rows="4" name="S3" cols="41"></textarea></td>
		<td height="24">&nbsp;</td>
	</tr>
	<tr>
        <td colspan="2" height="24">
            Repercusión en el personal de la empresa</td>
		<td height="24">&nbsp;<textarea rows="4" name="S4" cols="41"></textarea></td>
		<td height="24">&nbsp;</td>
	</tr>
	<tr>
        <td colspan="2" height="24">
            Impacto de la empresa</td>
		<td height="24">&nbsp;<textarea rows="4" name="S5" cols="41"></textarea></td>
		<td height="24">&nbsp;</td>
	</tr>
	<tr>
        <td colspan="2" height="24">
            Impacto en la comunidad</td>
		<td height="24">&nbsp;<textarea rows="4" name="S6" cols="41"></textarea></td>
		<td height="24">&nbsp;</td>
	</tr>
    <tr>
        <td colspan="2" height="24">
            Tipo de recursos invertidos</td>
        <td height="24">
            Aportación de terceros:</td>
        <td height="24">
        </td>
    </tr>
	<tr>
		<td height="24"></td>
		<td height="24"><input type="checkbox" name="C40" value="ON">Tiempo</td>
		<td height="24"></td>
		<td height="24"><input type="checkbox" name="C44" value="ON">Tiempo 
		voluntario</td>
	</tr>
	<tr>
		<td height="24">&nbsp;</td>
		<td height="24"><input type="checkbox" name="C41" value="ON">Talento 
		empresarial</td>
		<td height="24">&nbsp;</td>
		<td height="24"><input type="checkbox" name="C45" value="ON">Talento 
		empresarial</td>
	</tr>
	<tr>
		<td height="24">&nbsp;</td>
		<td height="24"><input type="checkbox" name="C42" value="ON">Recursos 
		financieros</td>
		<td height="24">&nbsp;</td>
		<td height="24"><input type="checkbox" name="C46" value="ON">Recursos 
		financieros</td>
	</tr>
	<tr>
		<td height="24">&nbsp;</td>
		<td height="24"><input type="checkbox" name="C43" value="ON">Recursos 
		técnicos o materiales</td>
		<td height="24">&nbsp;</td>
		<td height="24"><input type="checkbox" name="C47" value="ON">Recursos 
		técnicos o materiales</td>
	</tr>
	<tr>
        <td colspan="2" height="24">
            Presupuesto (totales)</td>
		<td height="24">&nbsp;<textarea rows="4" name="S7" cols="41"></textarea></td>
		<td height="24">&nbsp;</td>
	</tr>
	<tr>
        <td colspan="2" height="24">
            Porcentaje de la inversión con respecto a las utilidades 
		brutas</td>
		<td height="24">&nbsp;<textarea rows="4" name="S8" cols="41"></textarea></td>
		<td height="24">&nbsp;</td>
	</tr>
	<tr>
		<td height="24">&nbsp;</td>
		<td height="24"></td>
		<td height="24">&nbsp;<input type="checkbox" name="C48" value="ON">Personal</td>
		<td height="24">&nbsp;</td>
	</tr>
	<tr>
		<td height="24">&nbsp;</td>
		<td height="24"></td>
		<td height="24">&nbsp;<input type="checkbox" name="C49" value="ON">Comunidad</td>
		<td height="24">&nbsp;</td>
	</tr>
	<tr>
		<td height="24">&nbsp;</td>
		<td height="24"></td>
		<td height="24">&nbsp;<input type="checkbox" name="C50" value="ON">Gobierno</td>
		<td height="24">&nbsp;</td>
	</tr>
	<tr>
		<td height="24">&nbsp;</td>
		<td height="24"></td>
		<td height="24">&nbsp;<input type="checkbox" name="C51" value="ON">Otras 
		organizaciones</td>
		<td height="24">&nbsp;</td>
	</tr>
	<tr>
		<td height="24">&nbsp;</td>
		<td height="24"></td>
		<td height="24">&nbsp;<input type="checkbox" name="C52" value="ON">Otras 
		empresas</td>
		<td height="24">&nbsp;</td>
	</tr>
	<tr>
		<td height="24">&nbsp;</td>
		<td height="24"></td>
		<td height="24">&nbsp;<input type="checkbox" name="C53" value="ON">Otros 
		(¿cual?) <input type="text" name="T11" size="20"></td>
		<td height="24">&nbsp;</td>
	</tr>
	<tr>
        <td colspan="3" height="24">
            Listado de documentos y/o materiales que se anexan para 
		soportar la aplicación de la práctica &nbsp;</td>
		<td height="24">&nbsp;</td>
	</tr>
	<tr>
		<td height="24">&nbsp;</td>
		<td height="24"><input type="file" name="F1" size="20"></td>
		<td height="24"><input type="button" value="Ok" name="cmdOk"><input type="button" value="Ok y agregar otro archivo" name="cmdOkYOtro"></td>
		<td height="24">&nbsp;</td>
	</tr>
	<tr>
		<td height="24">&nbsp;</td>
		<td height="24">&nbsp;</td>
		<td height="24">&nbsp;</td>
		<td height="24">&nbsp;</td>
	</tr>
	<tr>
		<td colspan="4">
		<p align="center"><input type="submit" value="Submit" name="B1"><input type="reset" value="Reset" name="B2"></td>
	</tr>
</table>
</form>