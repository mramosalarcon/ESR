USE [ESR_2022]
GO
/****** Object:  StoredProcedure [dbo].[CESR_rpt_evidencias_cuestionario]    Script Date: 10/5/2021 6:45:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[CESR_rpt_evidencias_cuestionario]
	(
	@idCuestionario int,
	@idEmpresa int
	)
AS

declare @idCuestionarioAnterior int
declare @idCuestionarioPreAnterior int

	if (@idCuestionario = 89 and (@idEmpresa in (select idEmpresa from Cuestionario_Empresa where idCuestionario = 76))) -- Excepción por prorroga y cierre de proceso, pero hay que quitar este tramo
		set @idCuestionarioAnterior = 76
	else
	Begin
		select @idCuestionarioAnterior = anterior from Cuestionario where idCuestionario = @idCuestionario
		select @idCuestionarioPreAnterior = anterior from Cuestionario where idCuestionario = @idCuestionarioAnterior
	end

	SELECT  distinct  idTema, ordinal, CONVERT(varchar(3), ordinal) + '. ' + REPLACE(REPLACE(descripcion, '<b>', ''), '</b>', '') AS Indicador, Tema,
						  idTipoEvidencia, Evidencia, Tipo_Evidencia, Tipo_Respuesta
	From (Select distinct CI.idTema, CI.ordinal, I.descripcion, T.descripcion AS Tema,
						  EI.idTipoEvidencia, EI.descripcion AS Evidencia, TE.descripcion AS Tipo_Evidencia, TR.descripcion Tipo_Respuesta
	FROM         Cuestionario_Indicador AS CI Inner join
				 Indicador AS I ON CI.idTema = I.idTema AND CI.idIndicador = I.idIndicador Inner join
				 Tema as T On CI.idTema = T.idTema Inner join
				 Cuestionario_Empresa as CE On CI.idCuestionario = CE.idCuestionario inner join
				 Respuesta_Indicador RI On RI.idCuestionario = CI.idCuestionario and RI.idEmpresa = CE.idEmpresa and RI.idTema = CI.idTema and RI.idIndicador = CI.idIndicador Inner join
				 Tipo_Respuesta TR On RI.idTipoRespuesta = TR.idTipoRespuesta left join
				 Evidencia_indicador EI on CI.idIndicador = EI.idIndicador and CI.idTema = EI.idTema and CI.idCuestionario = EI.idCuestionario and CE.idEmpresa = EI.idEmpresa and EI.descripcion not in (select descripcion  COLLATE DATABASE_DEFAULT AS descripcion from Tipo_Evidencia) and EI.copia = 1 inner join
				 Tipo_Evidencia TE on EI.idTipoEvidencia = TE.idTipoEvidencia
	WHERE     CI.idCuestionario = @idCuestionario and CE.idEmpresa = @idEmpresa
	Union all
	Select distinct CI.idTema, CI.ordinal, I.descripcion descripcion, T.descripcion Tema,
						  EI.idTipoEvidencia, EI.descripcion COLLATE DATABASE_DEFAULT AS Evidencia, TE.descripcion AS Tipo_Evidencia, TR.descripcion Tipo_Respuesta
	FROM         Cuestionario_Indicador AS CI Inner join
				 Indicador AS I ON CI.idTema = I.idTema AND CI.idIndicador = I.idIndicador Inner join
				 Tema as T On CI.idTema = T.idTema inner join
				 Respuesta_Indicador RI On RI.idCuestionario = CI.idCuestionario and RI.idEmpresa = @idEmpresa and RI.idTema = CI.idTema and RI.idIndicador = CI.idIndicador Inner join
				 Tipo_Respuesta TR On RI.idTipoRespuesta = TR.idTipoRespuesta left join
				 ESR_2021.dbo.Evidencia_indicador EI on CI.idIndicador = EI.idIndicador and CI.idTema = EI.idTema and EI.idCuestionario=@idCuestionarioAnterior and RI.idEmpresa = EI.idEmpresa and EI.idTipoRespuesta = RI.idTipoRespuesta and EI.descripcion not in (select descripcion  COLLATE DATABASE_DEFAULT AS descripcion from Tipo_Evidencia) and EI.copia = 1 inner join
				 Tipo_Evidencia TE on EI.idTipoEvidencia = TE.idTipoEvidencia Inner join
				 Cuestionario_Indicador AS CIN on CI.idTema = CIN.idTema and CI.idIndicador = CIN.idIndicador and CIN.idCuestionario = @idCuestionario
	WHERE     CI.idCuestionario = @idCuestionario  and RI.idEmpresa = @idEmpresa
	Union all
	Select distinct CI.idTema, CI.ordinal, I.descripcion descripcion, T.descripcion Tema,
						  EI.idTipoEvidencia, EI.descripcion COLLATE DATABASE_DEFAULT AS Evidencia, TE.descripcion AS Tipo_Evidencia, TR.descripcion Tipo_Respuesta
	FROM         Cuestionario_Indicador AS CI Inner join
				 Indicador AS I ON CI.idTema = I.idTema AND CI.idIndicador = I.idIndicador Inner join
				 Tema as T On CI.idTema = T.idTema inner join
				 Respuesta_Indicador RI On RI.idCuestionario = CI.idCuestionario and RI.idEmpresa = @idEmpresa and RI.idTema = CI.idTema and RI.idIndicador = CI.idIndicador Inner join
				 Tipo_Respuesta TR On RI.idTipoRespuesta = TR.idTipoRespuesta left join
				 ESR_2020.dbo.Evidencia_indicador EI on CI.idIndicador = EI.idIndicador and CI.idTema = EI.idTema and EI.idCuestionario=@idCuestionarioPreAnterior and RI.idEmpresa = EI.idEmpresa and EI.idTipoRespuesta = RI.idTipoRespuesta and EI.descripcion not in (select descripcion  COLLATE DATABASE_DEFAULT AS descripcion from Tipo_Evidencia) and EI.copia = 1 inner join
				 Tipo_Evidencia TE on EI.idTipoEvidencia = TE.idTipoEvidencia Inner join
				 Cuestionario_Indicador AS CIN on CI.idTema = CIN.idTema and CI.idIndicador = CIN.idIndicador and CIN.idCuestionario = @idCuestionario
	WHERE     CI.idCuestionario = @idCuestionario  and RI.idEmpresa = @idEmpresa) as T
	Order by idTema, ordinal
