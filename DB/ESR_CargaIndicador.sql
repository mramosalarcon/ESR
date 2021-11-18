USE [ESR_2022]
GO
/****** Object:  StoredProcedure [dbo].[CESR_CargaEvidenciaIndicador]    Script Date: 9/1/2021 1:16:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[CESR_CargaEvidenciaIndicador] 
	(
	@idIndicador int,
	@idTema int,
	@idTipoEvidencia int,
	@idEmpresa int,
	@idCuestionario int,
	@fileName varchar(100)
	)
	
AS
declare @idCuestionarioAnterior int
Declare @idCuestionarioPreAnterior int
Declare @idCuestionarioPrePreAnterior int

	if (@idCuestionario = 89 and (@idEmpresa in (select idEmpresa from Cuestionario_Empresa where idCuestionario = 76))) -- Excepción por prorroga y cierre de proceso, pero hay que quitar este tramo
		set @idCuestionarioAnterior = 76
	else
	begin
		select @idCuestionarioAnterior = anterior from Cuestionario where idCuestionario = @idCuestionario
		select @idCuestionarioPreAnterior = anterior from Cuestionario where idCuestionario = @idCuestionarioAnterior
		select @idCuestionarioPrePreAnterior = anterior from Cuestionario where idCuestionario = @idCuestionarioPreAnterior
	end

	if not exists (Select
					evidencia
				From
					Evidencia_Indicador
				Where
					idIndicador = @idIndicador And
					idTema = @idTema And
					idTipoEvidencia = @idTipoEvidencia And
					idEmpresa = @idEmpresa And
					idCuestionario = @idCuestionario And
					descripcion = @fileName)
			if not exists (Select
							evidencia
						From
							ESR_2021.dbo.Evidencia_Indicador
						Where
							idIndicador = @idIndicador And
							idTema = @idTema And
							idTipoEvidencia = @idTipoEvidencia And
							idEmpresa = @idEmpresa And
							idCuestionario = @idCuestionarioAnterior And
							descripcion = @fileName)
				if not exists (Select
							evidencia
						From
							ESR_2020.dbo.Evidencia_Indicador
						Where
							idIndicador = @idIndicador And
							idTema = @idTema And
							idTipoEvidencia = @idTipoEvidencia And
							idEmpresa = @idEmpresa And
							idCuestionario = @idCuestionarioPreAnterior And
							descripcion = @fileName)		
						Select
							evidencia
						From
							ESR_2019.dbo.Evidencia_Indicador
						Where
							idIndicador = @idIndicador And
							idTema = @idTema And
							idTipoEvidencia = @idTipoEvidencia And
							idEmpresa = @idEmpresa And
							idCuestionario = @idCuestionarioPrePreAnterior And
							descripcion = @fileName
				else
					Select
						evidencia
					From
						ESR_2020.dbo.Evidencia_Indicador
					Where
						idIndicador = @idIndicador And
						idTema = @idTema And
						idTipoEvidencia = @idTipoEvidencia And
						idEmpresa = @idEmpresa And
						idCuestionario = @idCuestionarioPreAnterior And
						descripcion = @fileName
			else
				Select
						evidencia
					From
						ESR_2021.dbo.Evidencia_Indicador
					Where
						idIndicador = @idIndicador And
						idTema = @idTema And
						idTipoEvidencia = @idTipoEvidencia And
						idEmpresa = @idEmpresa And
						idCuestionario = @idCuestionarioAnterior And
						descripcion = @fileName
	else
		Select
			evidencia
		From
			Evidencia_Indicador
		Where
			idIndicador = @idIndicador And
			idTema = @idTema And
			idTipoEvidencia = @idTipoEvidencia And
			idEmpresa = @idEmpresa And
			idCuestionario = @idCuestionario And
			descripcion = @fileName
	/* SET NOCOUNT ON */ 
	RETURN
