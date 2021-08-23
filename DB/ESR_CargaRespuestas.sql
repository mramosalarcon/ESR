USE [ESR_2022]
GO
/****** Object:  StoredProcedure [dbo].[CESR_CargaRespuestas]    Script Date: 8/23/2021 3:39:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[CESR_CargaRespuestas]
	(
	@idIndicador int,
	@idTema int,
	@idEmpresa int,
	@idCuestionario int
	)
AS

declare @idCuestionarioAnterior int
declare @idCuestionarioPreAnterior int

	/* Respuesta_indicador */
	Select
		idIndicador,
		idTema,
		idEmpresa,
		idCuestionario,
		idTipoRespuesta,
		valor,
		calificacionRevisor
	From
		Respuesta_Indicador
	Where
		idIndicador = @idIndicador And
		idTema = @idTema And
		idEmpresa = @idEmpresa And
		idCuestionario = @idCuestionario
	
	/* Respuesta_inciso */
	Select top 1
		idIndicador,
		idTema,
		idInciso,
		idEmpresa,
		idCuestionario,
		idTipoRespuesta,
		Valor
	From
		Respuesta_Inciso
	Where
		idIndicador = @idIndicador And
		idTema = @idTema And
		idEmpresa = @idEmpresa And
		idCuestionario = @idCuestionario
		
	--****************************************************************************************************************************
	if (@idCuestionario = 89 and (@idEmpresa in (select idEmpresa from Cuestionario_Empresa where idCuestionario = 76))) -- Excepción por prorroga y cierre de proceso, pero hay que quitar este tramo
		set @idCuestionarioAnterior = 76
	else
	Begin
		select @idCuestionarioAnterior = anterior from Cuestionario where idCuestionario = @idCuestionario
		select @idCuestionarioPreAnterior = anterior from Cuestionario where idCuestionario = @idCuestionarioAnterior
	end
	/*
		select * from Cuestionario order by idCuestionario desc			
		select  anterior from Cuestionario where idCuestionario = 90
	--****************************************************************************************************************************/
	
	/* 
	ESR_2016.dbo.Evidencia_Indicador (idCuestionarioAnterior) +
	ESR_2018.dbo.Evidencia_Indicador (idCuestionarioAnterior) +
	ESR_2019.dbo.Evidencia_Indicador (idCuestionarioAnterior)
	Evidencia_Indicador (idCuestionario)
	
	
	Evidencia_Indicador (idCuestionario)
	Evidencia_Indicador (idCuestionarioAnterior)
	ESR_2018.dbo.Evidencia_Indicador (idCuestionario) +
	ESR_2018.dbo.Evidencia_Indicador (idCuestionarioAnterior) +
	ESR_2016.dbo.Evidencia_Indicador (idCuestionario) +
	ESR_2016.dbo.Evidencia_Indicador (idCuestionarioAnterior) +
	
	25/04/2019:
	Evidencia_Indicador (idCuestionario) +_
	ESR_2019.dbo.Evidencia_Indicador (idCuestionario) +
	ESR_2019.dbo.Evidencia_Indicador (idCuestionarioAnterior) +
	ESR_2018.dbo.Evidencia_Indicador (idCuestionarioPreAnterior)

	*/
	select distinct 
		idIndicador,
		idTema,
		idTipoEvidencia,
		idEmpresa,
		@idCuestionario,
		descripcion,
		idTipoRespuesta,
		url from (	Select
						idIndicador,
						idTema,
						idTipoEvidencia,
						idEmpresa,
						idCuestionario,
						descripcion,
						idTipoRespuesta,
						url/*,	
						evidencia*/
					From
						Evidencia_Indicador
					Where
						idIndicador = @idIndicador And
						idTema = @idTema And
						idEmpresa = @idEmpresa And
						idCuestionario = @idCuestionario and
						copia = 1
				union all
				Select
						idIndicador,
						idTema,
						idTipoEvidencia,
						idEmpresa,
						idCuestionario,
						descripcion,
						idTipoRespuesta,
						url/*,	
						evidencia*/
					From
						ESR_2021.dbo.Evidencia_Indicador
					Where
						idIndicador = @idIndicador And
						idTema = @idTema And
						idEmpresa = @idEmpresa And
						idCuestionario = @idCuestionarioAnterior and
						copia = 1
				union all
					Select
						idIndicador,
						idTema,
						idTipoEvidencia,
						idEmpresa,
						idCuestionario,
						descripcion COLLATE DATABASE_DEFAULT as descripcion,
						idTipoRespuesta,
						url COLLATE DATABASE_DEFAULT as url/*,	
						evidencia*/
					From
						ESR_2020.dbo.Evidencia_Indicador
					Where
						idIndicador = @idIndicador And
						idTema = @idTema And
						idEmpresa = @idEmpresa And
						idCuestionario = @idCuestionarioPreAnterior and
						copia = 1
				) as T
	Order by
		descripcion
	
	Select
		idIndicador,
		idTema,
		idInciso,
		idTipoEvidencia,
		idEmpresa,
		idCuestionario,
		descripcion,
		idTipoRespuesta/*,
		evidencia*/
	From
		Evidencia_Inciso
	Where
		idIndicador = @idIndicador And
		idTema = @idTema And
		idEmpresa = @idEmpresa And
		idCuestionario = @idCuestionario
	Order by
		descripcion
		
	Select
		idIndicador,
		idTema,
		idPregunta,
		idEmpresa,
		idCuestionario,
		Respuesta
	From
		Respuesta_Pregunta_Adicional
	Where
		idIndicador = @idIndicador And
		idTema = @idTema And
		idEmpresa = @idEmpresa And
		idCuestionario = @idCuestionario
	Order by
		idTema,
		idIndicador,
		idPregunta
		
	Select
		idIndicador,
		idTema,
		idEmpresa,
		idCuestionario,
		idGrupo
	From
		Respuesta_Grupo
	Where
		idIndicador = @idIndicador And
		idTema = @idTema And
		idEmpresa = @idEmpresa And
		idCuestionario = @idCuestionario
	Order by
		idTema,
		idIndicador,
		idGrupo


	/* SET NOCOUNT ON */ 
	RETURN
